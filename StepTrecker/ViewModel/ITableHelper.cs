using StepTrecker.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace StepTrecker.ViewModel
{
    public interface ITableHelper
    {
        ObservableCollection<ListViewItem> Users { get; set; }

        void InitTable(IEnumerable<UserProfile> userProfiles);
    }
}
