﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;

namespace View
{
    public interface IView
    {
        void BoardUpdated(Color[,] board);
        void GameOver(WinEventArgs winEvent);
    }
}
