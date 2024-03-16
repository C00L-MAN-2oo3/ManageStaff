using ManageStaff.Domain.Entities;
using ManageStaff.EF.Services;
using Microsoft.EntityFrameworkCore;


namespace ManageStaff.EF.Context
{
    public class ManageStaffContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }

        public DbSet<Position> Positions { get; set; }

        public DbSet<Department> Departments { get; set; }

        public ManageStaffContext() => Database.EnsureCreated();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=manageStaffDb;Trusted_Connection=True;");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {       
            modelBuilder.Entity<Employee>()
                        .HasData(
                new Employee
                {
                    Id = 1,
                    FirstName = "Антон",
                    MiddleName = "Сергеевич",
                    LastName = "Петров",
                    Logo = null,
                    PositionId = 1,
                    DepartmentId = 1,
                    Birthdate = new DateTime(1993, 5, 15),
                    PhoneNumber = "+7-999-911-56-65"
                },
                new Employee
                {
                    Id = 2,
                    FirstName = "Сергей",
                    MiddleName = "Платонович",
                    LastName = "Жуков",
                    Logo = null,
                    PositionId = 2,
                    DepartmentId = 2,
                    Birthdate = new DateTime(2000, 4, 11),
                    PhoneNumber = "+7-909-771-56-35"
                },
                new Employee
                {
                    Id = 3,
                    FirstName = "Мария",
                    MiddleName = "Дмитриевна",
                    LastName = "Онегина",
                    Logo = null,
                    PositionId = 3,
                    DepartmentId = 3,
                    Birthdate = new DateTime(1990, 11, 6),
                    PhoneNumber = "+7-949-911-46-55"
                });

            modelBuilder.Entity<Position>()
                        .HasData(
                new Position
                {
                    Id = 1,
                    Name = "Middle-разработчик",
                    Salary = 150000,
                },
                new Position
                {
                    Id = 2,
                    Name = "Senior-разработчик",
                    Salary = 210000,
                },
                new Position
                {
                    Id = 3,
                    Name = "Бухгалтер",
                    Salary = 70000,
                },
                new Position
                {
                    Id = 4,
                    Name = "HR-менеджер",
                    Salary = 90000,
                },
                new Position
                {
                    Id = 5,
                    Name = "Дата-инженер",
                    Salary = 250000,
                },
                new Position
                {
                    Id = 6,
                    Name = "Инженер по тестированию",
                    Salary = 100000
                });

            modelBuilder.Entity<Department>()
                        .HasData(
                new Department
                {
                    Id = 1,
                    Name = "Разработка ПО",
                    Address = "060954, Магаданская область, город Мытищи, проезд Ладыгина, 35",
                },
                new Department
                {
                    Id = 2,
                    Name = "Продажи",
                    Address = "563729, Новгородская область, город Солнечногорск, шоссе Славы, 11",
                },
                new Department
                {
                    Id = 3,
                    Name = "Бухгалтерия",
                    Address = "927304, Орловская область, город Зарайск, пл. Гагарина, 48",
                },
                new Department
                {
                    Id = 4,
                    Name = "Управление данными",
                    Address = "065954, Владимирская область, город Мытищи, проезд Ладыгина, 36",
                },
                new Department
                {
                    Id = 5,
                    Name = "Тестирование ПО",
                    Address = "065954, Ленинградская область, город Мытищи, проезд Левых, 326А"
                });
        }
    }
}
