using LiveChartsCore.SkiaSharpView;
using Ookii.Dialogs.Wpf;
using StepTrecker.Model;
using StepTrecker.ViewModel;
using System.Collections.Generic;
using System.Windows;

namespace StepTrecker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            var userHelper = new UserProvider(GetDirectory(), (messege, title) => MessageBox.Show(messege, title, MessageBoxButton.OK));

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
