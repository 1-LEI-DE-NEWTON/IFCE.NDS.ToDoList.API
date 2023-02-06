using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using NDS_ToDo.Application.DTOs.Assignment;
using NDS_ToDo.Application.DTOs.Paged;
using NDS_ToDo.Application.Notifications;
using NDS_ToDo.Application.Services.Contracts;
using NDS_ToDo.Domain.Contracts.Repository;
using NDS_ToDo.Domain.Entities;
using NDS_ToDo.Domain.FIlter;

namespace IFCE.NDS.ToDoList.Application.Services
{
    public class AssignmentService : IAssignmentService
    {
        private readonly IMapper _mapper;
        private readonly INotificator _notificator;
        private readonly IValidator<Assignment> _validator;
        private readonly IAssignmentRepository _assignmentRepository;

        private readonly IHttpContextAccessor _httpContextAccessor;

        public AssignmentService(IMapper mapper, IAssignmentRepository assignmentRepository,
            IHttpContextAccessor httpContextAccessor, INotificator notificator, IValidator<Assignment> validator)
        {
            _mapper = mapper;
            _assignmentRepository = assignmentRepository;
            _httpContextAccessor = httpContextAccessor;
            _notificator = notificator;
            _validator = validator;
        }

        public async Task<PagedDto<AssignmentDto>> Search(AssignmentSearchDto search)
        {
            var filter = _mapper.Map<AssignmentFilter>(search);
            var result =
                await _assignmentRepository.Search(GetUserId(), filter, search.PerPage, search.Page);

            return new PagedDto<AssignmentDto>
            {
                Items = _mapper.Map<List<AssignmentDto>>(result.Items),
                Total = result.Total,
                Page = result.Page,
                PerPage = result.PerPage,
                PageCount = result.PageCount
            };
        }

        public async Task<AssignmentDto> GetById(Guid id)
        {
            var (finded, assignment) = await GetAssignment(id);
            return !finded ? null : _mapper.Map<AssignmentDto>(assignment);
        }

        public async Task<AssignmentDto> Add(AddAssignmentDto assignmentDto)
        {
            var assignment = _mapper.Map<Assignment>(assignmentDto);
            assignment.UserId = GetUserId();

            if (!await ValidateAssignment(assignment)) return null;

            _assignmentRepository.Add(assignment);
            return await CommitChanges() ? _mapper.Map<AssignmentDto>(assignment) : null;
        }

        public async Task<AssignmentDto> Edit(Guid id, EditAssignmentDto assignmentDto)
        {
            if (id == assignmentDto.Id)
            {
                _notificator.Handle(new Notifications("O id informado é inválido"));
                return null;
            }

            var (finded, assignment) = await GetAssignment(id);
            if (!finded) return null;

            _mapper.Map(assignmentDto, assignment);

            if (!await ValidateAssignment(assignment)) return null;

            _assignmentRepository.Edit(assignment);
            return await CommitChanges() ? _mapper.Map<AssignmentDto>(assignment) : null;
        }

        public async Task Delete(Guid id)
        {
            var (finded, assignment) = await GetAssignment(id);
            if (!finded) return;

            _assignmentRepository.Delete(assignment);
            await CommitChanges();
        }

        public async Task MarkConcluded(Guid id)
        {
            var (finded, assignment) = await GetAssignment(id);
            if (!finded) return;

            assignment.SetConclude();

            _assignmentRepository.Edit(assignment);
            await CommitChanges();
        }

        public async Task MarkNotConcluded(Guid id)
        {
            var (finded, assignment) = await GetAssignment(id);
            if (!finded) return;

            assignment.SetUnconclude();

            _assignmentRepository.Edit(assignment);
            await CommitChanges();
        }

        private async Task<(bool, Assignment)> GetAssignment(Guid id)
        {
            var assignment = await _assignmentRepository.GetByID(id, GetUserId());
            if (assignment != null) return (true, assignment);

            _notificator.HandleNotFoundResource();
            return (true, null);
        }

        private async Task<bool> ValidateAssignment(Assignment assignment)
        {
            var validationResult = await _validator.ValidateAsync(assignment);
            if (validationResult.IsValid) return true;

            _notificator.Handle(validationResult.Errors);
            return false;
        }

        private async Task<bool> CommitChanges()
        {
            if (await _assignmentRepository.UnitOfWork.Commit())
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
