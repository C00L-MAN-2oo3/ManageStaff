using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ManageStaff.EF.Migrations
{
    /// <inheritdoc />
    public partial class ManageStaffMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Positions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Salary = table.Column<decimal>(type: "decimal(8,2)", precision: 8, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Positions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    MiddleName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Birthdate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    Logo = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    PositionId = table.Column<int>(type: "int", nullable: false),
                    DepartmentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employees_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Employees_Positions_PositionId",
                        column: x => x.PositionId,
                        principalTable: "Positions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "Id", "Address", "Name" },
                values: new object[,]
                {
                    { 1, "060954, Магаданская область, город Мытищи, проезд Ладыгина, 35", "Разработка ПО" },
                    { 2, "563729, Новгородская область, город Солнечногорск, шоссе Славы, 11", "Продажи" },
                    { 3, "927304, Орловская область, город Зарайск, пл. Гагарина, 48", "Бухгалтерия" },
                    { 4, "065954, Владимирская область, город Мытищи, проезд Ладыгина, 36", "Управление данными" },
                    { 5, "065954, Ленинградская область, город Мытищи, проезд Левых, 326А", "Тестирование ПО" }
                });

            migrationBuilder.InsertData(
                table: "Positions",
                columns: new[] { "Id", "Name", "Salary" },
                values: new object[,]
                {
                    { 1, "Middle-разработчик", 150000m },
                    { 2, "Senior-разработчик", 210000m },
                    { 3, "Бухгалтер", 70000m },
                    { 4, "HR-менеджер", 90000m },
                    { 5, "Дата-инженер", 250000m },
                    { 6, "Инженер по тестированию", 100000m }
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "Birthdate", "DepartmentId", "FirstName", "LastName", "Logo", "MiddleName", "PhoneNumber", "PositionId" },
                values: new object[,]
                {
                    { 1, new DateTime(1993, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Антон", "Петров", null, "Сергеевич", "+7-999-911-56-65", 1 },
                    { 2, new DateTime(2000, 4, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "Сергей", "Жуков", null, "Платонович", "+7-909-771-56-35", 2 },
                    { 3, new DateTime(1990, 11, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, "Мария", "Онегина", null, "Дмитриевна", "+7-949-911-46-55", 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_DepartmentId",
                table: "Employees",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_PositionId",
                table: "Employees",
                column: "PositionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "Positions");
        }
    }
}
