﻿using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using NeverSkipLegDay.ViewModels;

namespace NeverSkipLegDay.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DashboardDetailPage : ContentPage
    {
        public DashboardDetailPageViewModel ViewModel
        {
            get { return BindingContext as DashboardDetailPageViewModel; }
            set { BindingContext = value; }
        }
        public DashboardDetailPage()
        {
            PageService pageService = new PageService();
            ViewModel = new DashboardDetailPageViewModel(pageService);
            InitializeComponent();
        }
    }
}