using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace StepTrecker.Model
{
    public class UserProfile
    {
        public string UserName { get; set; }
        public int AverageSteps 
        { 
            get 
            {
                return (int)DayProfiles.Average(x => x.Steps);
            }
        }
        public int BestResult
        {
            get => DayProfiles.Max(x => x.Steps);
        }
        public int WorseResult
        {
            get => DayProfiles.Min(x => x.Steps);
        }

        public List<DayProfile> DayProfiles { get; set; } = new List<DayProfile>();
    }
}
