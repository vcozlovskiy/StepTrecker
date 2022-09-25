using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StepTrecker.Model
{
    public interface IUserProvider
    {
        Task<List<UserProfile>> GetUsersAsync();
        List<UserProfile> GetUsers();
    }
}
