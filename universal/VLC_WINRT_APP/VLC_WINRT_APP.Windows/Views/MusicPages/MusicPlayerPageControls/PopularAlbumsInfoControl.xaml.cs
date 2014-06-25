﻿using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace VLC_WINRT_APP.Views.MusicPages.MusicPlayerPageControls
{
    public sealed partial class PopularAlbumsInfoControl : UserControl
    {
        public PopularAlbumsInfoControl()
        {
            this.InitializeComponent();
            Loaded += OnLoaded;
            Window.Current.SizeChanged += CurrentOnSizeChanged;
        }

        private void CurrentOnSizeChanged(object sender, WindowSizeChangedEventArgs windowSizeChangedEventArgs)
        {
            Responsive();
        }

        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            Responsive();
        }

        void Responsive()
        {
            (PopularItemGridView.ItemsPanelRoot as WrapGrid).Orientation = (Window.Current.Bounds.Width < 1080)
                ? Orientation.Horizontal
                : Orientation.Vertical;
            if (Window.Current.Bounds.Width < 400)
            {
                (PopularItemGridView.ItemsPanelRoot as WrapGrid).ItemHeight = 120;
                (PopularItemGridView.ItemsPanelRoot as WrapGrid).ItemWidth = 120;
            }
            else if (Window.Current.Bounds.Width < 800)
            {
                (PopularItemGridView.ItemsPanelRoot as WrapGrid).ItemHeight = 160;
                (PopularItemGridView.ItemsPanelRoot as WrapGrid).ItemWidth = 160;
            }
            else 
            {
                (PopularItemGridView.ItemsPanelRoot as WrapGrid).ItemHeight = 200;
                (PopularItemGridView.ItemsPanelRoot as WrapGrid).ItemWidth = 200;
            }
        }
    }
}
