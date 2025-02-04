﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using WeeklyXamarin.Core.Helpers;
using WeeklyXamarin.Core.Services;
using Xamarin.Essentials.Interfaces;
using MvvmHelpers.Commands;
using System.Threading.Tasks;
using Xamarin.Essentials;
using WeeklyXamarin.Core.Models;

namespace WeeklyXamarin.Core.ViewModels
{
    public class AboutViewModel : ViewModelBase
    {
        private readonly IPreferences preferences;
        private readonly IBrowser browser;
        public List<LinkInfo> AboutLinks { get; set; } = new List<LinkInfo>
        {
            new LinkInfo {Text="Github Repository", Url="https://github.com/weeklyxamarin/WeeklyXamarin.mobile"},
            new LinkInfo {Text="Weekly Xamarin Website", Url="http://weeklyxamarin.com"},
        };

        public List<Contributor> Contributors { get; set; } = new List<Contributor>
        {
            new Contributor{Name = "Kym Phillpotts", Initials = "KP", ImageUrl="https://avatars.githubusercontent.com/u/1327346", ProfileUrl="https://github.com/kphillpotts"},
            new Contributor{Name="Lachlan Gordon", Initials="LG", ImageUrl="https://avatars.githubusercontent.com/u/29908924", ProfileUrl="https://github.com/lachlanwgordon"},
            new Contributor{Name="Luce Carter", Initials="LC", ImageUrl="https://avatars.githubusercontent.com/u/6980734", ProfileUrl="https://github.com/LuceCarter"},
            new Contributor{Name="Ryan Davis", Initials="RD", ImageUrl="https://avatars.githubusercontent.com/u/7392704", ProfileUrl="https://github.com/rdavisau"},
            new Contributor{Name="David Wahid", Initials="DW", ImageUrl="https://avatars.githubusercontent.com/u/30383473", ProfileUrl="https://github.com/wahid-d"},
        };

        public List<LinkInfo> Libraries { get; set; } = new List<LinkInfo>
        {
            new LinkInfo {Text="Xamarin Essentials", Url="https://github.com/xamarin/Essentials"},
            new LinkInfo {Text="Monkey Cache", Url="https://github.com/jamesmontemagno/monkey-cache"},
            new LinkInfo {Text="Newtonsoft Json", Url="https://github.com/JamesNK/Newtonsoft.Json"},
            new LinkInfo {Text="Refactored MvvmHelpers", Url="https://github.com/jamesmontemagno/mvvm-helpers"},
            new LinkInfo {Text="Xamarin Essentials Interfaces", Url="https://github.com/rdavisau/essential-interfaces"},
            new LinkInfo {Text="Lottie Xamarin", Url="https://github.com/Baseflow/LottieXamarin"},
            new LinkInfo {Text="Sharpnado MaterialFrame", Url="https://github.com/roubachof/Sharpnado.MaterialFrame"},
            new LinkInfo {Text="Shiny", Url="https://github.com/shinyorg/shiny"},
            new LinkInfo {Text="Xamarin Community Toolkit", Url="https://github.com/xamarin/XamarinCommunityToolkit"},
            new LinkInfo {Text="Xamarin Forms", Url="https://github.com/xamarin/Xamarin.Forms"},
            new LinkInfo {Text="Mobile Build Tools", Url="https://github.com/dansiegel/Mobile.BuildTools"},
            new LinkInfo {Text="Microsoft AppCenter", Url="https://www.nuget.org/packages/Microsoft.AppCenter/"},
        };

        public ICommand OpenUrlCommand { get; }
        public ICommand OpenAcknowlegementsCommand { get; }

        public AboutViewModel(INavigationService navigation, IAnalytics analytics, IPreferences preferences, IBrowser browser,
                              IMessagingService messagingService) : base(navigation, analytics, messagingService)
        {
            Title = "About";
            OpenUrlCommand = new AsyncCommand<string>(ExecuteOpenUrlCommand);
            OpenAcknowlegementsCommand = new AsyncCommand(ExecuteOpenAcknowledgmentsCommand);

            this.preferences = preferences;
            this.browser = browser;
        }

        public bool OpenLinksInApp
        {
            get => preferences.Get(Constants.Preferences.OpenLinksInApp, true);
            set => preferences.Set(Constants.Preferences.OpenLinksInApp, value);
        }

        public bool Analytics
        {
            get => preferences.Get(Constants.Preferences.Analytics, true);
            set
            {
                _ = analytics.SetEnabledAsync(value);
                preferences.Set(Constants.Preferences.Analytics, value);
            }
        }


        private async Task ExecuteOpenAcknowledgmentsCommand()
        {
            await navigation.GoToAsync(Constants.Navigation.Paths.Acknowlegements);
        }

        private async Task ExecuteOpenUrlCommand(string url)
        {
            await browser.OpenAsync(url, new BrowserLaunchOptions
            {
                LaunchMode = preferences.Get(Constants.Preferences.OpenLinksInApp, true) ? BrowserLaunchMode.SystemPreferred : BrowserLaunchMode.External,
                TitleMode = BrowserTitleMode.Show
            });
        }
    }
}