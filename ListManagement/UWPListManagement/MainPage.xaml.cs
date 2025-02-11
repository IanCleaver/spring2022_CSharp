﻿using Newtonsoft.Json;
using System;
using System.IO;
using UWPListManagement.Dialogs;
using UWPListManagement.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace UWPListManagement
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private string persistencePath = $"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\\SaveData.json";
        public MainPage()
        {
            this.InitializeComponent();

            DataContext = new MainViewModel(persistencePath);

        }

        private async void AddToDoClick(object sender, RoutedEventArgs e)
        {
            var dialog = new ToDoDialog();
            await dialog.ShowAsync();
        }

        private async void EditToDoClick(object sender, RoutedEventArgs e)
        {
            var dialog = new ToDoDialog((DataContext as MainViewModel).SelectedItem);
            await dialog.ShowAsync();
        }
    }
}
