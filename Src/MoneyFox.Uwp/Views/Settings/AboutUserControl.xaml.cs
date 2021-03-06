﻿using System;
using Windows.UI.Xaml;
using Microsoft.Services.Store.Engagement;

namespace MoneyFox.Uwp.Views.Settings
{
    public sealed partial class AboutUserControl
    {
        public AboutUserControl()
        {
            InitializeComponent();

            if (StoreServicesFeedbackLauncher.IsSupported()) FeedbackButton.Visibility = Visibility.Visible;
        }

        private async void FeedbackButton_Click(object sender, RoutedEventArgs e)
        {
            StoreServicesFeedbackLauncher launcher = StoreServicesFeedbackLauncher.GetDefault();
            await launcher.LaunchAsync();
        }
    }
}
