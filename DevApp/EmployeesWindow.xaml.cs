using System;
using System.Windows;
using System.Collections.ObjectModel;
using System.IO;
using Newtonsoft.Json;
using System.Xml.Serialization;
using System.Linq;

namespace DevApp
{
    public partial class EmployeesWindow : Window
    {
        MainWindow parent;
        private string defaultFileName = "Employees.json";
        ObservableCollection<Employees> db_employees = new ObservableCollection<Employees>();
        public EmployeesWindow(MainWindow parent)
        {
            InitializeComponent();
            this.parent = parent;
            if (File.Exists(defaultFileName)) Import(defaultFileName);
            DG_employees.ItemsSource = db_employees;
        }

        private void Add_button(object sender, RoutedEventArgs e)
        {
            NewEmployeeWindow window = new NewEmployeeWindow(this);
            window.Owner = parent;
            window.Show();
        }
        private void Delete_button(object sender, RoutedEventArgs e)
        {
            if (DG_employees.SelectedIndex > -1 && DG_employees.SelectedIndex < DG_employees.Items.Count - 1)
            {
                if (DG_employees.SelectedItems != null && DG_employees.SelectedItems.Count > 0)
                {
                    var toRemove = DG_employees.SelectedItems.Cast<Employees>().ToList();
                    var items = DG_employees.ItemsSource as ObservableCollection<Employees>;
                    if (items != null)
                    {
                        foreach (var Employees in toRemove)
                        {
                            items.Remove(Employees);
                        }
                    }
                }
            }
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            string json = JsonConvert.SerializeObject(db_employees);
            File.WriteAllText("Employees.json", json);

            XmlSerializer xmlImport = new XmlSerializer(typeof(ObservableCollection<Employees>));
            Stream xmlStream = new FileStream("employees.xml", FileMode.Create, FileAccess.Write);
            xmlImport.Serialize(xmlStream, db_employees);
            xmlStream.Close();
        }
        private void Import_Button(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.Filter = "Json files (*.json)|*.json|All files (*.*)|*.*";
            dialog.FilterIndex = 0;
            dialog.DefaultExt = "json";
            Nullable<bool> result = dialog.ShowDialog();
            if (result == true)
            {
                Import(dialog.FileName);
            }
        }



        /// <summary>
        /// Добавление нового сотрудника
        /// </summary>
        /// <param name="firstName">Имя</param>
        /// <param name="secondName">Фамилия</param>
        /// <param name="age">Возраст</param>
        /// <param name="department">Департамент</param>
        /// <param name="salary">Зарплата</param>
        /// <param name="numbOfProjects">Колличество проектов в котором задействован сотрудник</param>
        public void AddEmployee(string firstName, string secondName, int age, string department, int salary, int numbOfProjects)
        {
            int id = 0;
            if (db_employees.Count != 0)
            {
                id = db_employees[db_employees.Count-1].Id+1;
            }

            db_employees.Add(new Employees(id, firstName, secondName, age, department, salary, numbOfProjects));
        }
        /// <summary>
        /// Импорт данных
        /// </summary>
        /// <param name="filename">Имя файла</param>
        private void Import(string filename)
        {
            string json = File.ReadAllText(filename);
            db_employees = JsonConvert.DeserializeObject<ObservableCollection<Employees>>(json);
            DG_employees.ItemsSource = db_employees;
        }




        /// <summary>
        /// тестовая функция автодобавление большого числа сотрудников
        /// </summary>
        /// <param name="count"></param>
        public void Add_BunchOfEmployees(int count)
        {
            string firstName = "test";
            string secondName = "test";
            int age = 18;
            string department = "test";
            int salary = 0;
            int numbOfProjects = 0;
            int id = 0;
            for (int i = 0; i < count; i++)
            {
                if (db_employees.Count != 0) id = db_employees[db_employees.Count - 1].Id + 1;
                db_employees.Add(new Employees(id, firstName, secondName, age, department, salary, numbOfProjects));
            }
            
        }

        private void Test(object sender, RoutedEventArgs e)
        {
            Add_BunchOfEmployees(1_000_000);
        }

       
    }
}
