using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;

namespace View.Shared
{
    public interface IView
    {
        void Display();
        void BoardUpdated(Color[,] board);
        void GameOver(WinEventArgs winEvent);
    }
}
