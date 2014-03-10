﻿/**********************************************************************
 * VLC for WinRT
 **********************************************************************
 * Copyright © 2013-2014 VideoLAN and Authors
 *
 * Licensed under GPLv2+ and MPLv2
 * Refer to COPYING file of the official project for license
 **********************************************************************/

using Windows.Storage;

namespace VLC_WINRT.ViewModels.MainPage
{
    public class RemovableLibraryViewModel : VideoLibraryViewModel
    {
        public RemovableLibraryViewModel(StorageFolder location, string id) : base(location)
        {
            Id = id;
        }

        public string Id { get; private set; }
    }
}
