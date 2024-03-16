using ClosedXML.Excel;
using ManageStaff.Domain.Entities;
using ManageStaff.EF.Context;
using ManageStaff.UI.Tools;
using ManageStaff.UI.Views;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using System.Windows;


namespace ManageStaff.UI.ViewModels
{
    public class MainWindowVM : BaseVM
    {
        private readonly ManageStaffContext _context;

        private string? _filterText;
        public string? FilterText
        {
            get => _filterText;
            set
            {
                _filterText = value;
                OnPropertyChanged();
            }
        }

        private int _selectedIndexSortComboBox;
        public int SelectedIndexSortComboBox
        {
            get => _selectedIndexSortComboBox;
            set
            {
                _selectedIndexSortComboBox = value;
                OnPropertyChanged();
            }
        }

        private int _selectedIndexFilterComboBox;
        public int SelectedIndexFilterComboBox
        {
            get => _selectedIndexFilterComboBox;
            set
            {
                _selectedIndexFilterComboBox = value;
                OnPropertyChanged();
            }
        }

        private Employee? _selectedEmployee;
        public Employee? SelectedEmployee
        {
            get => _selectedEmployee;
            set
            {
                _selectedEmployee = value;
                OnPropertyChanged();
            }
        }

        private List<Employee> _employees;
        public List<Employee> Employees
        {
            get => _employees;
            set
            {
                _employees = value;
                OnPropertyChanged();
            }
        }

        public List<string> SortComboBoxItems { get; set; } 

        public List<string> FilterComboBoxItems { get; set; }

        public RelayCommand DeleteItemCommand => new RelayCommand(execute => DeleteItem());
        
        public RelayCommand SearchItemsCommand => new RelayCommand(execute => SearchItems());

        public RelayCommand SortItemsCommand => new RelayCommand(execute => SortItems());

        public RelayCommand FilterItemsCommand => new RelayCommand(execute => FilterItems());

        public RelayCommand AllItemsCommand => new RelayCommand(execute => AllItems());

        public RelayCommand AddItemCommand => new RelayCommand(execute => AddItem());

        public RelayCommand UpdateItemCommand => new RelayCommand(execute => UpdateItem());

        public RelayCommand SaveAsFileCommand => new RelayCommand(execute => ExportToExcel());

        public MainWindowVM()
        {
            _context = new ManageStaffContext();

            Employees = new List<Employee>(_context.Employees.Include(e => e.Position).Include(e => e.Department));
            SortComboBoxItems = new List<string> { "По возрастанию", "По убыванию" };
            FilterComboBoxItems = new List<string> { "Senior-разработчик", "Middle-разработчик", "Бухгалтер", "HR-менеджер", "Дата-инженер", "Инженер по тестированию" };
        }

        private void AddItem()
        {
            AddWindow addWindow = new AddWindow();

            if (addWindow.ShowDialog() == false) 
            {
                Employees = new List<Employee>(_context.Employees.Include(e => e.Position).Include(e => e.Department));
                SelectedEmployee = null;
            }
        }

        private void UpdateItem()
        {
            if (SelectedEmployee != null)
            {
                SupplyVM.SelectedSupply = SelectedEmployee;
                AddWindow addWindow = new AddWindow();

                if (addWindow.ShowDialog() == false)
                {
                    Employees = new List<Employee>(_context.Employees.Include(e => e.Position).Include(e => e.Department));
                    SupplyVM.SelectedSupply = null;
                    SelectedEmployee = null;
                }
            }
            else
                return;
        }

        private void DeleteItem()
        {
            if (SelectedEmployee != null)
            {
                Employee employee = SelectedEmployee;

                if (MessageBoxResult.Yes == MessageBox.Show($"Вы уверены, что хотите удалить cотрудника {employee.LastName}. {employee.FirstName[0]}. {employee.MiddleName[0]}?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Warning))
                {
                    try
                    {
                        _context.Employees.Remove(employee);
                        _context.SaveChanges();

                        Employees = new List<Employee>(_context.Employees.Include(e => e.Position).Include(e => e.Department));

                        MessageBox.Show($"Сотрудник {employee!.LastName}. {employee.FirstName[0]}. {employee.MiddleName[0]} удален!", "Успех!", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Произошла ошибка :(\nПопробуйте позже", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }

                SelectedEmployee = null;
            }
            else
                return;
        }

        private void FilterItems()
        {
            string selectedPosition = SelectedIndexFilterComboBox switch
            {
                0 => "Senior-разработчик",
                1 => "Middle-разработчик",
                2 => "Бухгалтер",
                3 => "HR-менеджер",
                4 => "Дата-инженер",
                5 => "Инженер по тестированию",
                _ => ""
            };

            if (selectedPosition != "")
                Employees = Employees.FindAll(e => e.Position!.Name == selectedPosition);
            else
                return;
        }

        private void SortItems() => Employees = SelectedIndexSortComboBox == 0 ? Employees.OrderBy(e => e.LastName).ToList() : Employees.OrderByDescending(e => e.LastName).ToList();    

        private void SearchItems()
        {
            if (!string.IsNullOrEmpty(FilterText))
            {
                string filter = FilterText.Trim().ToLower();

                Employees = Employees.Where(
                    e => e.FirstName.ToLower().Contains(filter)
                    || e.LastName.ToLower().Contains(filter)
                    || e.MiddleName.ToLower().Contains(filter)
                    || e.PhoneNumber.ToLower().Contains(filter)
                    || e.Birthdate.ToString().ToLower().Contains(filter)
                    || e.Position!.Salary.ToString().ToLower().Contains(filter)
                    || e.Position!.Name.ToLower().Contains(filter)
                    || e.Department!.Name.ToLower().Contains(filter)
                ).ToList();
            }
            else
                return;
        }

        private void AllItems()
        {
            Employees = _context.Employees.ToList();
            SelectedIndexFilterComboBox = 0;
            SelectedIndexSortComboBox = 0;
        }

        private void ExportToExcel()
        {
            SaveFileDialog file = new SaveFileDialog();

            file.FileName = "Employees";
            file.Filter = "Excel (.xlsx)|*.xlsx";

            if (file.ShowDialog() == true && Employees.Count > 0)
            {
                string filePath = file.FileName;

                try
                {
                    using (var workbook = new XLWorkbook())
                    {
                        var worksheet = workbook.Worksheets.Add("Employees");
                        var range = worksheet.Range($"A1:H{Employees.Count + 1}");
                        var fontHeader = worksheet.Style.Font;
                        var fontColumn = worksheet.Style.Font;

                        range.Style.Border.OutsideBorder = XLBorderStyleValues.Medium - 2;
                        range.Style.Border.InsideBorder = XLBorderStyleValues.Medium - 2;

                        worksheet.Column("A").Width = 25;
                        worksheet.Column("B").Width = 25;
                        worksheet.Column("C").Width = 25;
                        worksheet.Column("D").Width = 25;
                        worksheet.Column("E").Width = 25;
                        worksheet.Column("F").Width = 25;
                        worksheet.Column("G").Width = 25;
                        worksheet.Column("H").Width = 25;

                        fontHeader.FontSize = 16;
                        fontHeader.FontName = "Times New Roman";

                        fontColumn.FontName = "Times New Roman";
                        fontColumn.FontSize = 14;

                        worksheet.Cell("A1").Value = "Имя";
                        worksheet.Cell("A1").Style.Font = fontHeader;

                        worksheet.Cell("B1").Value = "Отчество";
                        worksheet.Cell("B1").Style.Font = fontHeader;

                        worksheet.Cell("C1").Value = "Фамилия";
                        worksheet.Cell("C1").Style.Font = fontHeader;

                        worksheet.Cell("D1").Value = "Дата рождения";
                        worksheet.Cell("D1").Style.Font = fontHeader;

                        worksheet.Cell("E1").Value = "Контактный телефон";
                        worksheet.Cell("E1").Style.Font = fontHeader;

                        worksheet.Cell("F1").Value = "Должность";
                        worksheet.Cell("F1").Style.Font = fontHeader;

                        worksheet.Cell("G1").Value = "Зарплата";
                        worksheet.Cell("G1").Style.Font = fontHeader;

                        worksheet.Cell("H1").Value = "Отдел";
                        worksheet.Cell("H1").Style.Font = fontHeader;

                        for (int i = 0; i < Employees.Count; i++)
                        {
                            worksheet.Cell(i + 2, 1).Value = Employees[i].FirstName;
                            worksheet.Cell(i + 2, 1).Style.Font = fontColumn;

                            worksheet.Cell(i + 2, 2).Value = Employees[i].MiddleName;
                            worksheet.Cell(i + 2, 2).Style.Font = fontColumn;

                            worksheet.Cell(i + 2, 3).Value = Employees[i].LastName;
                            worksheet.Cell(i + 2, 3).Style.Font = fontColumn;

                            worksheet.Cell(i + 2, 4).Value = Employees[i].Birthdate.ToString();
                            worksheet.Cell(i + 2, 4).Style.Font = fontColumn;

                            worksheet.Cell(i + 2, 5).Value = Employees[i].PhoneNumber;
                            worksheet.Cell(i + 2, 5).Style.Font = fontColumn;

                            worksheet.Cell(i + 2, 6).Value = Employees[i].Position!.Name;
                            worksheet.Cell(i + 2, 6).Style.Font = fontColumn;

                            worksheet.Cell(i + 2, 7).Value = Employees[i].Position!.Salary;
                            worksheet.Cell(i + 2, 7).Style.Font = fontColumn;

                            worksheet.Cell(i + 2, 8).Value = Employees[i].Department!.Name;
                            worksheet.Cell(i + 2, 8).Style.Font = fontColumn;
                        }

                        workbook.SaveAs(filePath);
                    }

                    MessageBox.Show("Данные сохранены!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception)
                {
                    MessageBox.Show("Произошла ошибка :(\nПопробуйте позже", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
                return;
        }
    }
}
