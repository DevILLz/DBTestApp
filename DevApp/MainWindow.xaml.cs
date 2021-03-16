using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using Newtonsoft.Json;
using System.Xml.Serialization;
using System.Linq;

namespace DevApp
{
    public partial class MainWindow : Window
    {
        ObservableCollection<Departments> db_departments = new ObservableCollection<Departments>();
        private string defaultFileName = "Departments.json";

        public MainWindow()
        {
            InitializeComponent();
            if (File.Exists(defaultFileName))
            {
                Import(defaultFileName);
            }

            DG_departments.ItemsSource = db_departments;
        }
        private void Add_button(object sender, RoutedEventArgs e)
        {
            NewDepartmentWindow window = new NewDepartmentWindow(this);
            window.Owner = this;
            window.Show();
        }
        private void Delete_button(object sender, RoutedEventArgs e)
        {
            if (DG_departments.SelectedIndex > -1 && DG_departments.SelectedIndex < DG_departments.Items.Count-1)
            {
                if (DG_departments.SelectedItems != null && DG_departments.SelectedItems.Count > 0)
                {
                    var toRemove = DG_departments.SelectedItems.Cast<Departments>().ToList();
                    var items = DG_departments.ItemsSource as ObservableCollection<Departments>;
                    if (items != null)
                    {
                        foreach (var Department in toRemove)
                        {
                            items.Remove(Department);
                        }
                    }
                }
            }    
        }
        private void Export_button(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog dialog = new Microsoft.Win32.SaveFileDialog();
            dialog.Filter = "Json files (*.json)|*.json|All files (*.*)|*.*";
            dialog.FilterIndex = 0;
            dialog.DefaultExt = "json";
            Nullable<bool> result = dialog.ShowDialog();
            if (result == true)
            {
                Export(dialog.FileName);
            }
        }
        private void Import_button(object sender, RoutedEventArgs e)
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
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Export(defaultFileName);
        }
        /// <summary>
        /// Открыть окно с данными сотрудников
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Show_Employees(object sender, RoutedEventArgs e)
        {
            EmployeesWindow window = new EmployeesWindow(this);
            window.Owner = this;
            window.Show();
        }        





        /// <summary>
        /// импорт данных
        /// </summary>
        /// <param name="filename">Имя файла</param>
        private void Import(string fileName)
        {
            string json = File.ReadAllText(fileName);
            db_departments = JsonConvert.DeserializeObject<ObservableCollection<Departments>>(json);
            DG_departments.ItemsSource = db_departments;

        }

        /// <summary>
        /// Экспорт данных
        /// </summary>
        /// <param name="filename">Имя файла</param>
        private void Export(string fileName)
        {
            string json = JsonConvert.SerializeObject(db_departments);
            File.WriteAllText(fileName, json);

            XmlSerializer xmlImport = new XmlSerializer(typeof(ObservableCollection<Departments>));
            Stream xmlStream = new FileStream("Departments.xml", FileMode.Create, FileAccess.Write);
            xmlImport.Serialize(xmlStream, db_departments);
            xmlStream.Close();
        }

        /// <summary>
        /// Добавление нового департамента
        /// </summary>
        /// <param name="name">Название</param>
        /// <param name="creationDate">Дата создания департамента</param>
        /// <param name="leader">Начальник</param>
        public void AddDepartment(string name, DateTime creationDate, string leader, int employeesCount)
        {
            int id = 0;
            if (db_departments.Count != 0)
            {
                id = db_departments[db_departments.Count - 1].Id + 1;
            }
            db_departments.Add(new Departments(id, name, creationDate, employeesCount, leader));
        }

        

    }
}
/// Создать прототип информационной системы, в которой есть возможност работать со структурой организации
/// В структуре присутствуют департаменты и сотрудники
/// Каждый департамент может содержать не более 1_000_000 сотрудников.
/// У каждого департамента есть поля: наименование, дата создания,
/// количество сотрудников числящихся в нём 
/// (можно добавить свои пожелания)
/// 
/// У каждого сотрудника есть поля: Фамилия, Имя, Возраст, департамент в котором он числится, 
/// уникальный номер, размер оплаты труда, количество проектов закрепленных за ним.
///
/// В данной информаиционной системе должна быть возможность 
/// - импорта и экспорта всей информации в xml и json
/// Добавление, удаление, редактирование сотрудников и департаментов
/// 
/// * Реализовать возможность упорядочивания сотрудников в рамках одно департамента 
/// по нескольким полям, например возрасту и оплате труда
/// 
///  №     Имя       Фамилия     Возраст     Департамент     Оплата труда    Количество проектов
///  1   Имя_1     Фамилия_1          23         Отдел_1            10000                      3 
///  2   Имя_2     Фамилия_2          21         Отдел_2            20000                      3 
///  3   Имя_3     Фамилия_3          22         Отдел_1            20000                      3 
///  4   Имя_4     Фамилия_4          24         Отдел_1            10000                      3 
///  5   Имя_5     Фамилия_5          22         Отдел_2            20000                      3 
///  6   Имя_6     Фамилия_6          22         Отдел_1            10000                      3 
///  7   Имя_7     Фамилия_7          23         Отдел_1            20000                      3 
///  8   Имя_8     Фамилия_8          23         Отдел_1            30000                      3 
///  9   Имя_9     Фамилия_9          21         Отдел_1            30000                      3 
/// 10  Имя_10    Фамилия_10          21         Отдел_2            10000                      3 
/// 
/// 
/// Упорядочивание по одному полю возраст
/// 
///  №     Имя       Фамилия     Возраст     Департамент     Оплата труда    Количество проектов
///  2   Имя_2     Фамилия_2          21         Отдел_2            20000                      3 
/// 10  Имя_10    Фамилия_10          21         Отдел_2            10000                      3 
///  9   Имя_9     Фамилия_9          21         Отдел_1            30000                      3 
///  3   Имя_3     Фамилия_3          22         Отдел_1            20000                      3 
///  5   Имя_5     Фамилия_5          22         Отдел_2            20000                      3 
///  6   Имя_6     Фамилия_6          22         Отдел_1            10000                      3 
///  1   Имя_1     Фамилия_1          23         Отдел_1            10000                      3 
///  8   Имя_8     Фамилия_8          23         Отдел_1            30000                      3 
///  7   Имя_7     Фамилия_7          23         Отдел_1            20000                      3 
///  4   Имя_4     Фамилия_4          24         Отдел_1            10000                      3 
/// 
///
/// Упорядочивание по полям возраст и оплате труда
/// 
///  №     Имя       Фамилия     Возраст     Департамент     Оплата труда    Количество проектов
/// 10  Имя_10    Фамилия_10          21         Отдел_2            10000                      3 
///  2   Имя_2     Фамилия_2          21         Отдел_2            20000                      3 
///  9   Имя_9     Фамилия_9          21         Отдел_1            30000                      3 
///  6   Имя_6     Фамилия_6          22         Отдел_1            10000                      3 
///  3   Имя_3     Фамилия_3          22         Отдел_1            20000                      3 
///  5   Имя_5     Фамилия_5          22         Отдел_2            20000                      3 
///  1   Имя_1     Фамилия_1          23         Отдел_1            10000                      3 
///  7   Имя_7     Фамилия_7          23         Отдел_1            20000                      3 
///  8   Имя_8     Фамилия_8          23         Отдел_1            30000                      3 
///  4   Имя_4     Фамилия_4          24         Отдел_1            10000                      3 
/// 
/// 
/// Упорядочивание по полям возраст и оплате труда в рамках одного департамента
/// 
///  №     Имя       Фамилия     Возраст     Департамент     Оплата труда    Количество проектов
///  9   Имя_9     Фамилия_9          21         Отдел_1            30000                      3 
///  6   Имя_6     Фамилия_6          22         Отдел_1            10000                      3 
///  3   Имя_3     Фамилия_3          22         Отдел_1            20000                      3 
///  1   Имя_1     Фамилия_1          23         Отдел_1            10000                      3 
///  7   Имя_7     Фамилия_7          23         Отдел_1            20000                      3 
///  8   Имя_8     Фамилия_8          23         Отдел_1            30000                      3 
///  4   Имя_4     Фамилия_4          24         Отдел_1            10000                      3 
/// 10  Имя_10    Фамилия_10          21         Отдел_2            10000                      3 
///  2   Имя_2     Фамилия_2          21         Отдел_2            20000                      3 
///  5   Имя_5     Фамилия_5          22         Отдел_2            20000                      3 
 