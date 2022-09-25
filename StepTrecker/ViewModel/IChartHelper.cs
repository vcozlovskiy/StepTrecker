using LiveChartsCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace StepTrecker.ViewModel
{
    public interface IChartHelper
    {
        ISeries[] Data { get; set; }
        ListViewItem SelectedProfile { get; set; }
    }
}
