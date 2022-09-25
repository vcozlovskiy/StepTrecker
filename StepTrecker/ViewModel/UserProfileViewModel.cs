using System.Linq;
using System.Windows.Controls;
using Microsoft.Win32;
using System.Text.Json;
using System.IO;
using Ookii.Dialogs.Wpf;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using StepTrecker.Model;

namespace StepTrecker.ViewModel
{
    public class UserProfileViewModel
    {
        private ListViewItem _selectedUser = new();
        public ListViewItem SelectedUser
        {
            get { return _selectedUser; }
            set
            {
                if (value != null && value.Content is UserProfile)
                {
                    _selectedUser = value;
                    ChartHelper.SelectedProfile = value;
                }
            }
        }
        private RelayCommand _serializeCommand;
        public RelayCommand SerializeCommand
        {
            get
            {
                return _serializeCommand ?? (_serializeCommand = new RelayCommand(obj =>
                {
                    SaveFileDialog saveFileDialog = new SaveFileDialog() { DefaultExt = ".json" };

                    if (saveFileDialog.ShowDialog() == true)
                    {
                        var options = new JsonSerializerOptions() { WriteIndented = true };
                        var json = JsonSerializer.Serialize(SelectedUser.Content, options);

                        File.WriteAllText(saveFileDialog.FileName, json);
                    }
                }));
            }
        }
        public ITableHelper TableHelper { get; init; }
        public IChartHelper ChartHelper { get; init; }
        private IUserProvider userProvider { get; init; }
        public UserProfileViewModel()
        {

        }
        public UserProfileViewModel(IUserProvider provider, IChartHelper helper, ITableHelper tableHelper)
        {
            TableHelper = tableHelper;
            userProvider = provider;
            ChartHelper = helper;

            TableHelper.InitTable(provider.GetUsers());
            SelectedUser = TableHelper.Users.FirstOrDefault();
        }

    }
}
