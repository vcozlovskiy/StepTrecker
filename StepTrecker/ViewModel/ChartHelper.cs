using LiveChartsCore;
using LiveChartsCore.Defaults;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;
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
    public class ChartHelper : IChartHelper
    {
        public ISeries[] Data { get; set; } = new ISeries[]
                {
                    new LineSeries<int>
                    {
                        GeometryStroke = null,
                        GeometryFill = null,
                        Fill = null,
                        TooltipLabelFormatter = (point) => $"{point.Model} шагов",
                        DataPadding = new LiveChartsCore.Drawing.LvcPoint(2, 2),
                        Values = Array.Empty<int>(),
                    },
                    new ScatterSeries<ObservablePoint>
                    {
                        Fill = new SolidColorPaint(SKColors.Red),
                        TooltipLabelFormatter = (point) => $"{point.Model} шагов",
                        DataPadding = new LiveChartsCore.Drawing.LvcPoint(2, 2),
                    },
                    new ScatterSeries<ObservablePoint>
                    {
                        Fill = new SolidColorPaint(SKColors.Green),
                        TooltipLabelFormatter = (point) => $"{point.Model} шагов",
                        DataPadding = new LiveChartsCore.Drawing.LvcPoint(2, 2),
                    }
                };

        private ListViewItem _selectedProfile;

        public ListViewItem SelectedProfile
        {
            get
            {
                return _selectedProfile;
            }
            set
            {
                _selectedProfile = value;
                UpdateChart();
            }
        }

        public ChartHelper()
        {
            _selectedProfile = new ListViewItem();
        }

        public void UpdateChart()
        {
            if (_selectedProfile.Content is UserProfile user)
            {
                Data[0].Values = user.DayProfiles.Select(x => x.Steps);
                var maxSteps = user.DayProfiles.Max(x => x.Steps);
                var indexMaxItem = user.DayProfiles.IndexOf(user.DayProfiles.Where(x => x.Steps == maxSteps).First());

                var s = new ObservableCollection<ObservablePoint>()
                {
                    new ObservablePoint(indexMaxItem, maxSteps)
                };

                Data[1].Values = s;


                var minSteps = user.DayProfiles.Min(x => x.Steps);
                var indexMinItem = user.DayProfiles.IndexOf(user.DayProfiles.Where(x => x.Steps == minSteps).First());

                Data[2].Values = new ObservableCollection<ObservablePoint>()
                {
                    new ObservablePoint(indexMinItem, minSteps)
                }; ;
            }
        }
    }
}
