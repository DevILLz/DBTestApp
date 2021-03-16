using System;
namespace DevApp
{
    /// <summary>
    /// Класс данных с информацией о конкретном департаменте
    /// </summary>
    public class Departments
    {
        public Departments()
        {

        }
        /// <summary>
        /// Департамент
        /// </summary>
        /// <param name="id">ID номер департамента</param>
        /// <param name="name">Имя департамента</param>
        /// <param name="creationDate">Дата создания департамента</param>
        /// <param name="employeesCount">Колличество сотрудинков, работающих в данном департамента</param>
        /// <param name="leader">Начальник департамента</param>
        public Departments(int id, string name, DateTime creationDate, int employeesCount, string leader)
        {
            this.Id = id;
            this.Name = name;
            this.CreationDate = creationDate;
            this.EmployeesCount = employeesCount;
            this.Leader = leader;
        }
        /// <summary>
        /// ID номер департамента
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Имя департамента
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Дата создания департамента
        /// </summary>
        public DateTime CreationDate { get; set; }
        /// <summary>
        /// Колличество сотрудинков, работающих в данном департамента
        /// </summary>
        public int EmployeesCount { get; set; }
        /// <summary>
        /// Начальник департамента
        /// </summary>
        public string Leader { get; set; }
    }
    
}
/// У каждого департамента есть поля: наименование, дата создания,
/// количество сотрудников числящихся в нём 
/// (можно добавить свои пожелания)