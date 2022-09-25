using StepTrecker.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace StepTrecker.ViewModel
{
    public class TableHelper : ITableHelper
    {
        public ObservableCollection<ListViewItem> Users { get; set; } = new ObservableCollection<ListViewItem>();

        public void InitTable(IEnumerable<UserProfile> userProfiles)
        {
            var users = userProfiles.Select(x =>
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
        }
    }
}
