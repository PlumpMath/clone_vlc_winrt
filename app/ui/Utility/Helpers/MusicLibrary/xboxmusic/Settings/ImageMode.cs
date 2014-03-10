﻿/**********************************************************************
 * VLC for WinRT
 **********************************************************************
 * Copyright © 2013-2014 VideoLAN and Authors
 *
 * Licensed under GPLv2+ and MPLv2
 * Refer to COPYING file of the official project for license
 **********************************************************************/

namespace XboxMusicLibrary.Settings
{
    public enum ImageMode
    {
        Scale = 1,
        LetterBox = 2,
        Crop = 3
    }

    public class ImageSettings
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public ImageMode Mode { get; set; }
        public string Background { get; set; }

        public ImageSettings(int width, int height, ImageMode mode, string background)
        {
            this.Width = width;
            this.Height = height;
            this.Mode = mode;
            this.Background = background;
        }
    }
}
