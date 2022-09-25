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
        private Action<string> _exeptionAlert;

        public string Path 
        { 
            get 
            { 
                return _path; 
            } 
            set 
            { 
                _path = value;
                _users = null;
                GetUsers();
            } 
        }

        public UserProvider(string pathToCatalog)
        {
            if (_path == null)
            {
                _path = string.Empty;
            }

            _path = pathToCatalog;
        }

        public UserProvider(string pathToCatalog, Action<string> exeptionAlert) : this(pathToCatalog)
        {
            _exeptionAlert = exeptionAlert;
        }

        public List<UserProfile> GetUsers()
        {
            if (_users != null)
            {
                return _users;
            }

            List<DayProfile> profiles = new();
            var users = UsersInit();

            foreach (var file in GetJsonFilesName())
            {
                using var fileStream = new FileStream(file, FileMode.OpenOrCreate);
                try
                {
                    profiles = JsonSerializer.Deserialize<List<DayProfile>>(fileStream);
                }
                catch
                {
                }

                AssignDayToUser(profiles, users);
            }

            _users = users;
            return users;
        }

        public async Task<List<UserProfile>> GetUsersAsync()
        {
            var task = Task.Run(() => GetUsers());
            return await task;
        }

        public List<UserProfile> UsersInit()
        {
            using var fs = new FileStream(Directory.GetFiles(_path).First(), FileMode.OpenOrCreate);

            List<UserProfile> userProfils;

            try
            {
                userProfils = JsonSerializer.Deserialize<List<DayProfile>>(fs)
                .Select(x => new UserProfile() { UserName = x.User }).ToList();
            }
            catch
            {
                if (_exeptionAlert != null)
                {
                    _exeptionAlert("В каталоге отсувствуют данные");
                }
                userProfils = new List<UserProfile>();
            }

            return userProfils;
        }

        private static void AssignDayToUser(List<DayProfile> profiles, List<UserProfile> users)
        {
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

        private IEnumerable<string> GetJsonFilesName() => Directory.GetFiles(_path).Where(x => x.EndsWith(".json"));
    }
}
