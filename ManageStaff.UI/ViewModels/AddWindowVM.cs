using ManageStaff.Domain.Entities;
using ManageStaff.EF.Context;
using ManageStaff.UI.Tools;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using System.IO;
using System.Windows;


namespace ManageStaff.UI.ViewModels
{
    public class AddWindowVM : BaseVM
    {
        private byte[] _imageBytes;
        private readonly ManageStaffContext _context;
        private Employee? _currentEmployee;

        private string _firstName;
        public string FirstName
        {
            get => _firstName;
            set
            {
                _firstName = value;
                OnPropertyChanged();
            }
        }

        private string _middleName;
        public string MiddleName
        {
            get => _middleName;
            set
            {
                _middleName = value;
                OnPropertyChanged();
            }
        }

        private string _lastName;
        public string LastName
        {
            get => _lastName;
            set
            {
                _lastName = value;
                OnPropertyChanged();
            }
        }

        private string _selectedPosition;
        public string SelectedPosition
        {
            get => _selectedPosition;
            set
            {
                _selectedPosition = value;
                OnPropertyChanged();
            }
        }

        private string _selectedDepartment;
        public string SelectedDepartment
        {
            get => _selectedDepartment;
            set
            {
                _selectedDepartment = value;
                OnPropertyChanged();
            }
        }

        private DateTime _birthdate;
        public DateTime Birthdate
        {
            get => _birthdate;
            set
            {
                _birthdate = value;
                OnPropertyChanged();
            }
        }

        private object? _logo;
        public object? Logo
        {
            get => _logo;
            set
            {
                _logo = value;
                OnPropertyChanged();
            }
        }

        private string _phoneNumber;
        public string PhoneNumber
        {
            get => _phoneNumber;
            set
            {
                _phoneNumber = value;
                OnPropertyChanged();
            }
        }

        public List<string> Positions { get; set; }

        public List<string> Departments { get; set; }

        public RelayCommand SelectImageCommand => new RelayCommand(async execute => await SelectImage());

        public RelayCommand SaveItemCommand => new RelayCommand(execute => AddOrUpdateItem());

        public AddWindowVM()
        {
            _context = new ManageStaffContext();
            Positions = _context.Positions.Select(p => p.Name).ToList();
            Departments = _context.Departments.Select(p => p.Name).ToList();
            _currentEmployee = SupplyVM.SelectedSupply as Employee;

            if (_currentEmployee != null)
                InitializeData();
            else
                return;
        }

        private void InitializeData()
        {
            FirstName = _currentEmployee!.FirstName;
            LastName = _currentEmployee.LastName;
            Birthdate = _currentEmployee.Birthdate;
            MiddleName = _currentEmployee.MiddleName;
            PhoneNumber = _currentEmployee.PhoneNumber;
            SelectedPosition = _currentEmployee.Position!.Name;
            SelectedDepartment = _currentEmployee.Department!.Name;
            Logo = _currentEmployee.Logo;
        }

        private async Task SelectImage()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            if (openFileDialog.ShowDialog() == true)
            {
                Logo = openFileDialog.FileName;
                _imageBytes = await File.ReadAllBytesAsync(openFileDialog.FileName);
            }
        }

        private void AddOrUpdateItem()
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(FirstName) 
                    && !string.IsNullOrWhiteSpace(LastName)
                    && !string.IsNullOrWhiteSpace(MiddleName) 
                    && !string.IsNullOrWhiteSpace(SelectedDepartment)
                    && !string.IsNullOrWhiteSpace(SelectedDepartment) 
                    && !string.IsNullOrWhiteSpace(Birthdate.ToString()))
                {

                    Employee employee = _currentEmployee ?? new Employee();

                    employee.FirstName = FirstName;
                    employee.LastName = LastName;
                    employee.MiddleName = MiddleName;
                    employee.PositionId = _context.Positions.FirstOrDefault(p => p.Name == SelectedPosition)!.Id;
                    employee.DepartmentId = _context.Departments.FirstOrDefault(d => d.Name == SelectedDepartment)!.Id;
                    employee.Birthdate = Birthdate;
                    employee.PhoneNumber = PhoneNumber;
                    employee.Logo = _imageBytes;

                    if (_currentEmployee == null)
                    {
                        _context.Employees.Add(employee);
                        _context.SaveChanges();

                        MessageBox.Show("Сотрудник добавлен!", "Успех!", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        _context.Entry(employee).State = EntityState.Modified;
                        _context.SaveChanges();

                        MessageBox.Show("Cотрудник обновлен!", "Успех!", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                else
                    MessageBox.Show("Заполните все поля!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (Exception)
            {
                MessageBox.Show("Произошла ошибка :(\nПопробуйте позже", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
