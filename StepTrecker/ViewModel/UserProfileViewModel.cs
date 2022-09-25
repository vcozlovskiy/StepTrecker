using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using StepTrecker.Model;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;
using System;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;
using Microsoft.Win32;

namespace StepTrecker.ViewModel
{
    public class UserProfileViewModel
    {
        public ObservableCollection<ListViewItem> Users { get; set; } = new ObservableCollection<ListViewItem>();

        private ListViewItem _selectedUser = new ListViewItem();
        
        public ListViewItem SelectedUser
        {
            get { return _selectedUser; }
            set
            {
                if(value.Content is UserProfile profile)
                {
                    _selectedUser = value;
                    UpdateChart();
                }
            } 
        }

        public ISeries[] Series { get; set; } = 
            new ISeries[]
                {
                    new LineSeries<int>
                    {
                        Values = new int[] { 2, 1, 3, 5, 3, 4, 6 },
                        Fill = new SolidColorPaint(SKColors.Blue),
                    }
                };

        private RelayCommand _serializeCommand;

        public RelayCommand SerializeCommand
        {
            get
            {
                return _serializeCommand ?? (_serializeCommand = new RelayCommand(obj =>
                {
                    var openFileDialog = new OpenFileDialog();
                    openFileDialog.InitialDirectory = "c:\\";
                    openFileDialog.RestoreDirectory = true;

                    if (openFileDialog.ShowDialog() == true)
                    {
                        
                    }
                }));
            }
        }

        public UserProfileViewModel() 
        {

        }

        public UserProfileViewModel(IUserProvider provider)
        {
            var users = provider.GetUsers().Select(x =>
            {
                ListViewItem listView = new ListViewItem()
                {
                    Content = x,
                };

                if (x.BestResult - x.AverageSteps > x.AverageSteps * 0.2)
                {
                    listView.Background = new SolidColorBrush(Colors.GreenYellow);
                }

                if (x.AverageSteps - x.WorseResult > x.AverageSteps * 0.2)
                {
                    listView.Background = new SolidColorBrush(Colors.OrangeRed);
                }

                return listView;
            });
            Users = new ObservableCollection<ListViewItem>(users);

            SelectedUser = Users[0];
        }

        private void UpdateChart()
        {
            if (_selectedUser.Content is UserProfile user)
            {
                Series[0].Values = user.DayProfiles.Select(x => x.Steps);
            }
        }
    }
}
