using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevApp
{
    /// <summary>
    /// Класс данных с информацией о конкретном сотруднике
    /// </summary>
    public class Employees
    {
        public Employees()
        {

        }
        /// <summary>
        /// Сотрудник
        /// </summary>
        /// <param name="id">ID сотрудника</param>
        /// <param name="firstName">Имя</param>
        /// <param name="secondName">Фамилия</param>
        /// <param name="age">Возраст</param>
        /// <param name="department">Департамент, к которому принадлежит сотрудник</param>
        /// <param name="salary">Зарплата</param>
        /// <param name="numbOfProjects">Колличество проектов, в которых задействован сотрудник</param>
        public Employees(int id, string firstName, string secondName, int age, string department, int salary, int numbOfProjects)
        {
            this.Id = id;
            this.FirstName = firstName;
            this.SecondName = secondName;
            this.Age = age;
            this.Department = department;
            this.Salary = salary;
            this.NumbOfProjects = numbOfProjects;
        }
        /// <summary>
        /// ID сотрудника
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Имя
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// Фамилия
        /// </summary>
        public string SecondName { get; set; }
        /// <summary>
        /// Возраст
        /// </summary>
        public int Age { get; set; }
        /// <summary>
        /// Департамент, к которому принадлежит сотрудник
        /// </summary>
        public string Department { get; set; }
        /// <summary>
        /// Зарплата
        /// </summary>
        public int Salary { get; set; }
        /// <summary>
        /// Колличество проектов, в которых задействован сотрудник
        /// </summary>
        public int NumbOfProjects { get; set; }
    }
}
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