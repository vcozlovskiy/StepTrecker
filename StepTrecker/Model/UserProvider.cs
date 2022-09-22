using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace StepTrecker.Model
{
    internal class UserProvider : IUserProvider
    {
        private string _path;
        private List<UserProfile> _users;

        public UserProvider(string pathToCatalog)
        {
            _path = pathToCatalog;
        }

        public List<UserProfile> GetUsers()
        {
            if (_users != null)
            {
                return _users;
            }

            List<DayProfile> profiles = new();
            var users = UsersInit();

            foreach (var file in Directory.GetFiles(_path))
            {
                using var fileStream = new FileStream(file, FileMode.OpenOrCreate);
                profiles = JsonSerializer.Deserialize<List<DayProfile>>(fileStream);

                foreach (var userDayResult in profiles.GroupBy(x => x.User)
                    .Select(x => new { User = x.Key, DayResult = x.Single(u => u.User == x.Key) }))
                {
                    if (users.Exists(x => x.UserName == userDayResult.User))
                    {
                        users.Where(u => u.UserName == userDayResult.User)
                            .Single().DayProfiles.Add(userDayResult.DayResult);
                    }
                    else
                    {
                        var newUser = new UserProfile()
                        {
                            UserName = userDayResult.User
                        };
                        newUser.DayProfiles.Add(userDayResult.DayResult);
                        users.Add(newUser);
                    }
                }
            }

            _users = users;
            return users;
        }

        public async Task<List<UserProfile>> GetUsersAsync()
        {
            var task = Task.Run(() => GetUsers());
            return await task;
        }

        public  List<UserProfile> UsersInit()
        {
            using var fs = new FileStream(Directory.GetFiles(_path).First(), FileMode.OpenOrCreate);

            var userProfils = JsonSerializer.Deserialize<List<DayProfile>>(fs)
                .Select(x => new UserProfile() { UserName = x.User }).ToList();

            return userProfils;
        }
    }
}
