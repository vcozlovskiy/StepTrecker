using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using StepTrecker.Model;

namespace StepTrecker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<UserProfile> users;
        public MainWindow()
        {
            users = new ObservableCollection<UserProfile>();
            InitializeComponent();

            var userHelper = new UserProvider(@"C:\Users\kozlo\source\repos\StepTrecker\StepTrecker\TestData\");

            foreach (var i in userHelper.GetUsers())
            {
                users.Add(i);
            }

            this.Resources["users"] = users;
        }
    }
}
