// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NDS_ToDo.Infra.Context;

#nullable disable

namespace IFCE.NDS.ToDoList.Infra.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230206213037_CreateInitial")]
    partial class CreateInitial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("NDS_ToDo.Domain.Entities.Assignment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid?>("AssignmentListId")
                        .HasColumnType("char(36)");

                    b.Property<Guid?>("AssingmentListId")
                        .HasColumnType("char(36)");

                    b.Property<sbyte>("Concluded")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TINYINT")
                        .HasDefaultValue((sbyte)0);

                    b.Property<DateTime?>("ConcludedAt")
                        .HasColumnType("DATETIME");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("DATETIME");

                    b.Property<DateTime?>("Deadline")
                        .HasColumnType("DATETIME");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("VARCHAR(255)");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("DATETIME");

                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("AssignmentListId");

                    b.HasIndex("UserId");

                    b.ToTable("Assignment");
                });

            modelBuilder.Entity("NDS_ToDo.Domain.Entities.AssignmentList", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AssignmentLists");
                });

            modelBuilder.Entity("NDS_ToDo.Domain.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(150)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("NDS_ToDo.Domain.Entities.Assignment", b =>
                {
                    b.HasOne("NDS_ToDo.Domain.Entities.AssignmentList", "AssignmentList")
                        .WithMany("Assignments")
                        .HasForeignKey("AssignmentListId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("NDS_ToDo.Domain.Entities.User", "User")
                        .WithMany("Assignments")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("AssignmentList");

                    b.Navigation("User");
                });

            modelBuilder.Entity("NDS_ToDo.Domain.Entities.AssignmentList", b =>
                {
                    b.HasOne("NDS_ToDo.Domain.Entities.User", "User")
                        .WithMany("AssignmentsLists")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("NDS_ToDo.Domain.Entities.AssignmentList", b =>
                {
                    b.Navigation("Assignments");
                });

            modelBuilder.Entity("NDS_ToDo.Domain.Entities.User", b =>
                {
                    b.Navigation("Assignments");

                    b.Navigation("AssignmentsLists");
                });
#pragma warning restore 612, 618
        }
    }
}
