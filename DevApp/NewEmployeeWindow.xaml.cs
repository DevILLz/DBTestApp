using System;
using System.Linq;
using System.Windows;


namespace DevApp
{
    public partial class NewEmployeeWindow : Window
    {

        EmployeesWindow parent;
        
        public NewEmployeeWindow(EmployeesWindow parent)
        {
            InitializeComponent();
            this.parent = parent;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var r = new Random();
            int salaryTest = 0;
            int ageTest = 0;
            if (salary.Text != "") salaryTest = Int32.Parse(salary.Text);
            if (age.Text != "") ageTest = Int32.Parse(age.Text);
            parent.AddEmployee(firstName.Text,
                               secondName.Text,
                               ageTest,
                               department.Text,
                               salaryTest,
                               r.Next(0, 10));
            this.Close();
        }

        private void Salary_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            e.Handled = !e.Text.All(IsGood);
        }

        private void Age_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            e.Handled = !e.Text.All(IsGood);
        }

        private void OnPasting(object sender, DataObjectPastingEventArgs e)
        {
            var stringData = (string)e.DataObject.GetData(typeof(string));
            if (stringData == null || !stringData.All(IsGood))
            {
                e.CancelCommand();
            }
        }
        bool IsGood(char c)
        {
            if (c >= '0' && c <= '9')
                return true;
            return false;
        }
    }
}
