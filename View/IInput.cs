﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;

namespace View
{
    public interface IInput
    {

        void GameOver(WinEventArgs winArgs);
    }
}
