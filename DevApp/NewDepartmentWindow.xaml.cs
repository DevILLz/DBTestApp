using System;
using System.Linq;
using System.Windows;



namespace DevApp
{
    public partial class NewDepartmentWindow : Window
    {
        private readonly MainWindow parent;
        public NewDepartmentWindow(MainWindow parent)
        {
            InitializeComponent();
            this.parent = parent;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int employeesCountTest = 0;
            if (employeesCount.Text != "")
            {
                employeesCountTest = Int32.Parse(employeesCount.Text);
            }

            parent.AddDepartment(name.Text,
                                 date.SelectedDate.Value,
                                 leader.Text,
                                 employeesCountTest);
            this.Close();
        }

        private void EmployeesCount_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
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
