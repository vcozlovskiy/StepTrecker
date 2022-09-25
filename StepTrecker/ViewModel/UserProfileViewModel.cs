using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using StepTrecker.Model;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;
using System;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;
using Microsoft.Win32;
using System.Text.Json;
using System.IO;
using Ookii.Dialogs.Wpf;
using LiveChartsCore.Defaults;

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
        private RelayCommand _selectFolder;
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
        public RelayCommand SelectFolder
        {
            get
            {
                return _selectFolder ??= new RelayCommand(obj =>
                {
                    var dialog = new VistaFolderBrowserDialog();
                    if (dialog.ShowDialog().GetValueOrDefault())
                    {
                        var folder = dialog.SelectedPath;
                        userProvider.Path = folder;

                        TableHelper.InitTable(userProvider.GetUsers());
                        SelectedUser = TableHelper.Users.FirstOrDefault();
                    }
                });
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
