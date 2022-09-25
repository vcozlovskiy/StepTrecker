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
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using Ookii.Dialogs.Wpf;
using StepTrecker.Model;
using StepTrecker.ViewModel;

namespace StepTrecker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            var userHelper = new UserProvider(GetDirectory(), (s) => MessageBox.Show(s, s, MessageBoxButton.OK));

            var c = new UserProfileViewModel(userHelper, new ChartHelper(), new TableHelper());

            InitializeComponent();
            DataContext = c;

            SetAxisName();
        }

        private void SetAxisName()
        {
            var xAxis = new Axis
            {
                Name = "Дни"
            };
            var yAxis = new Axis
            {
                Name = "Шаги"
            };


            chart.XAxes = new List<Axis>() { xAxis };
            chart.YAxes = new List<Axis>() { yAxis };
        }

        private static string GetDirectory()
        {
            var dialog = new VistaFolderBrowserDialog();

            if (dialog.ShowDialog().GetValueOrDefault())
            {
                return dialog.SelectedPath;
            }

            return null;
        }
    }
}
