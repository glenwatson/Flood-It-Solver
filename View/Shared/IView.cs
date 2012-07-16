using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;

namespace View.Shared
{
    public interface IView
    {
        /// <summary>
        /// This will block on GUIs.
        /// Ensure everything is set-up and users (if any) are running
        /// </summary>
        void Display();
        void BoardUpdated(Color[,] board);
        void GameOver(WinEventArgs winEvent);
    }
}
