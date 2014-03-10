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
using VLC_WINRT.Utility.Helpers;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace VLC_WINRT.Views.Controls.MusicPage
{
    public sealed partial class AlbumFlyout : UserControl
    {
        public AlbumFlyout()
        {
            this.InitializeComponent();
            UIAnimationHelper.FadeOut(this);
        }
        public void Show()
        {
            UIAnimationHelper.FadeIn(this);
        }
        public void Hide()
        {
            UIAnimationHelper.FadeOut(this);
        }
    }
}
