﻿/**********************************************************************
 * VLC for WinRT
 **********************************************************************
 * Copyright © 2013-2014 VideoLAN and Authors
 *
 * Licensed under GPLv2+ and MPLv2
 * Refer to COPYING file of the official project for license
 **********************************************************************/

using VLC_WINRT.Common;

namespace VLC_WINRT.Utility.Commands.MainPage
{
    public class GoToPanelCommand : AlwaysExecutableCommand
    {
        public async override void Execute(object parameter)
        {
            var frame = App.ApplicationFrame;
            var page = frame.Content as Views.MainPage;
            if (page != null)
            {
                page.ChangedSectionsHeadersState(int.Parse(parameter.ToString()));
            }
        }
    }
}
