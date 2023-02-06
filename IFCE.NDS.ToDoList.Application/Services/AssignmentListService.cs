using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using NDS_ToDo.Application.DTOs.Assignment;
using NDS_ToDo.Application.DTOs.AssignmentList;
using NDS_ToDo.Application.DTOs.Paged;
using NDS_ToDo.Application.Notifications;
using NDS_ToDo.Application.Services.Contracts;
using NDS_ToDo.Domain.Contracts.Repository;
using NDS_ToDo.Domain.Entities;
using NDS_ToDo.Domain.FIlter;

namespace IFCE.NDS.ToDoList.Application.Services
{
    public class AssignmentListService : IAssignmentListService
    {
        private readonly IMapper _mapper;
        private readonly INotificator _notificator;
        private readonly IValidator<AssignmentList> _validator;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAssignmentRepository _assignmentRepository;
        private readonly IAssignmentListRepository _assignmentListRepository;

        public AssignmentListService(IMapper mapper, INotificator notificator, IValidator<AssignmentList> validator,
            IHttpContextAccessor contextAccessor, IAssignmentRepository assignmentRepository, IAssignmentListRepository assignmentListRepository)
        {
            _mapper = mapper;
            _notificator = notificator;
            _validator = validator;
            _httpContextAccessor = contextAccessor;
            _assignmentRepository = assignmentRepository;
            _assignmentListRepository = assignmentListRepository;
        }

        public async Task<PagedDto<AssignmentListDto>> Search(AssignmentListSearchDto search)
        {
            var result = await _assignmentListRepository
                                                            .Search(GetUserId(), search.name, search.PerPage, search.Page);

            return new PagedDto<AssignmentListDto>
            {
                Items = _mapper.Map<List<AssignmentListDto>>(result.Items),
                Total = result.Total,
                Page = result.Page,
                PerPage = result.PerPage,
                PageCount = result.PageCount
            };
        }

        public async Task<PagedDto<AssignmentDto>> SearchAssignments(Guid id, AssignmentSearchDto search)
        {
            var filter = _mapper.Map<AssignmentFilter>(search);
            var result = await _assignmentRepository
                .Search(GetUserId(), filter, search.PerPage, search.Page, id);

            return new PagedDto<AssignmentDto>
            {
                Items = _mapper.Map<List<AssignmentDto>>(result.Items),
                Total = result.Total,
                Page = result.Page,
                PerPage = result.PerPage,
                PageCount = result.PageCount
            };
        }

        public async Task<AssignmentListDto> GetById(Guid id)
        {
            var (finded, list) = await GetAssignmentList(id);
            return !finded ? null : _mapper.Map<AssignmentListDto>(list);
        }

        public async Task<AssignmentListDto> Add(AddAssignmentListDto assignmentListDto)
        {
            var list = _mapper.Map<AssignmentList>(assignmentListDto);

            list.UserId = GetUserId();

            if (!await ValidateAssignmentList(list))
                return null;

            _assignmentListRepository.Add(list);
            return await CommitChanges() ? _mapper.Map<AssignmentListDto>(list) : null;
        }

        public async Task<AssignmentListDto> Edit(Guid id, EditAssignmentListDto assignmentListDto)
        {
            if (id != assignmentListDto.Id)
            {
                _notificator.Handle(new Notifications("O id informado é inválido"));
                return null;
            }

            var (finded, list) = await GetAssignmentList(id, false);
            if (!finded) return null;

            _mapper.Map(assignmentListDto, list);

            if (!await ValidateAssignmentList(list))
                return null;

            _assignmentListRepository.Edit(list);
            return await CommitChanges() ? _mapper.Map<AssignmentListDto>(list) : null;
        }

        public async Task Delete(Guid id)
        {
            var (finded, list) = await GetAssignmentList(id);
            if (!finded) return;

            if (list.Assignments.Any(a => !a.Concluded))
            {
                _notificator.Handle(new Notifications("Não é possível excluir lista com tasks não concluídas!"));
                return;
            }

            _assignmentListRepository.Delete(list);
            await CommitChanges();
        }

        private async Task<(bool, AssignmentList)> GetAssignmentList(Guid id, bool withAssignments = true)
        {
            var assignmentList = withAssignments
                    ? await _assignmentListRepository.GetByIdWithAssignments(id, GetUserId())
                    : await _assignmentListRepository.GetById(id, GetUserId());

            if (assignmentList != null) return (true, assignmentList);

            _notificator.HandleNotFoundResource();
            return (true, null);
        }

        private async Task<bool> ValidateAssignmentList(AssignmentList assignmentList)
        {
            var validationResult = await _validator.ValidateAsync(assignmentList);
            if (validationResult.IsValid) return true;

            _notificator.Handle(validationResult.Errors);
            return false;
        }

        private async Task<bool> CommitChanges()
        {
            if (await _assignmentListRepository.UnitOfWork.Commit())
                return true;

            _notificator.Handle(new Notifications("Ocorreu um erro salvar alterações!"));
            return false;
        }

        private Guid GetUserId()
        {
            var claim = _httpContextAccessor?.HttpContext?.User.Claims.FirstOrDefault(c => c.Type == "Id");
            if (claim == null)
                return Guid.Empty;

            return string.IsNullOrWhiteSpace(claim.Value) ? Guid.Empty : Guid.Parse(claim.Value);
        }

    }
}
