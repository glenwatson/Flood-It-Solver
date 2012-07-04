using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Model;
using Color = Model.Color;

namespace View
{
    /// <summary>
    /// Interaction logic for GUIDisplay.xaml
    /// </summary>
    public partial class GUIDisplay : Window, IView
    {
        #region IView
        public void Display()
        {
            InitializeComponent();
        }

        public void BoardUpdated(Color[,] board)
        {
            UpdateBoardView(board);
        }
        public void GameOver(WinEventArgs e)
        {
            MessageBox.Show("Game Over. It took you " + e.Turns + " turns.");
        }
        #endregion

        public void Init(Color[,] board)
        {
            UpdateBoardView(board);
            Show();
        }
        private void UpdateBoardView(Color[,] board)
        {
            boardView.Board = board;
        }

        
    }
}
