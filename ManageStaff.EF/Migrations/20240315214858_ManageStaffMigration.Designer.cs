﻿// <auto-generated />
using System;
using ManageStaff.EF.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ManageStaff.EF.Migrations
{
    [DbContext(typeof(ManageStaffContext))]
    [Migration("20240315214858_ManageStaffMigration")]
    partial class ManageStaffMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ManageStaff.Domain.Entities.Department", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.ToTable("Departments");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Address = "060954, Магаданская область, город Мытищи, проезд Ладыгина, 35",
                            Name = "Разработка ПО"
                        },
                        new
                        {
                            Id = 2,
                            Address = "563729, Новгородская область, город Солнечногорск, шоссе Славы, 11",
                            Name = "Продажи"
                        },
                        new
                        {
                            Id = 3,
                            Address = "927304, Орловская область, город Зарайск, пл. Гагарина, 48",
                            Name = "Бухгалтерия"
                        },
                        new
                        {
                            Id = 4,
                            Address = "065954, Владимирская область, город Мытищи, проезд Ладыгина, 36",
                            Name = "Управление данными"
                        },
                        new
                        {
                            Id = 5,
                            Address = "065954, Ленинградская область, город Мытищи, проезд Левых, 326А",
                            Name = "Тестирование ПО"
                        });
                });

            modelBuilder.Entity("ManageStaff.Domain.Entities.Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Birthdate")
                        .HasColumnType("datetime2");

                    b.Property<int>("DepartmentId")
                        .HasColumnType("int");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<byte[]>("Logo")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("MiddleName")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.Property<int>("PositionId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId");

                    b.HasIndex("PositionId");

                    b.ToTable("Employees");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Birthdate = new DateTime(1993, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DepartmentId = 1,
                            FirstName = "Антон",
                            LastName = "Петров",
                            MiddleName = "Сергеевич",
                            PhoneNumber = "+7-999-911-56-65",
                            PositionId = 1
                        },
                        new
                        {
                            Id = 2,
                            Birthdate = new DateTime(2000, 4, 11, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DepartmentId = 2,
                            FirstName = "Сергей",
                            LastName = "Жуков",
                            MiddleName = "Платонович",
                            PhoneNumber = "+7-909-771-56-35",
                            PositionId = 2
                        },
                        new
                        {
                            Id = 3,
                            Birthdate = new DateTime(1990, 11, 6, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DepartmentId = 3,
                            FirstName = "Мария",
                            LastName = "Онегина",
                            MiddleName = "Дмитриевна",
                            PhoneNumber = "+7-949-911-46-55",
                            PositionId = 3
                        });
                });

            modelBuilder.Entity("ManageStaff.Domain.Entities.Position", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<decimal>("Salary")
                        .HasPrecision(8, 2)
                        .HasColumnType("decimal(8,2)");

                    b.HasKey("Id");

                    b.ToTable("Positions");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Middle-разработчик",
                            Salary = 150000m
                        },
                        new
                        {
                            Id = 2,
                            Name = "Senior-разработчик",
                            Salary = 210000m
                        },
                        new
                        {
                            Id = 3,
                            Name = "Бухгалтер",
                            Salary = 70000m
                        },
                        new
                        {
                            Id = 4,
                            Name = "HR-менеджер",
                            Salary = 90000m
                        },
                        new
                        {
                            Id = 5,
                            Name = "Дата-инженер",
                            Salary = 250000m
                        },
                        new
                        {
                            Id = 6,
                            Name = "Инженер по тестированию",
                            Salary = 100000m
                        });
                });

            modelBuilder.Entity("ManageStaff.Domain.Entities.Employee", b =>
                {
                    b.HasOne("ManageStaff.Domain.Entities.Department", "Department")
                        .WithMany()
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ManageStaff.Domain.Entities.Position", "Position")
                        .WithMany()
                        .HasForeignKey("PositionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Department");

                    b.Navigation("Position");
                });
#pragma warning restore 612, 618
        }
    }
}
