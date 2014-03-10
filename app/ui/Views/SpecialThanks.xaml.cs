﻿/**********************************************************************
 * VLC for WinRT
 **********************************************************************
 * Copyright © 2013-2014 VideoLAN and Authors
 *
 * Licensed under GPLv2+ and MPLv2
 * Refer to COPYING file of the official project for license
 **********************************************************************/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238
using VLC_WINRT.Utility.Services.RunTime;

namespace VLC_WINRT.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SpecialThanks : Page
    {
        public SpecialThanks()
        {
            this.InitializeComponent();
        }

        private async void GoBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.NavigateTo(typeof(MainPage));
        }
    }
}
