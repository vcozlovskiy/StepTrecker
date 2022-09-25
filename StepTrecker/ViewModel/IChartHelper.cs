using LiveChartsCore;
using System.Windows.Controls;

namespace StepTrecker.ViewModel
{
    public interface IChartHelper
    {
        ISeries[] Data { get; set; }
        ListViewItem SelectedProfile { get; set; }
    }
}
