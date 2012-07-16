using System.Windows;
using Model;
using View.Shared;
using Color = Model.Color;

namespace View.Views
{
    /// <summary>
    /// Interaction logic for GUIDisplay.xaml
    /// </summary>
    public partial class GUIDisplay : Window, IView
    {
        public GUIDisplay()
        {
            InitializeComponent();
        }

        #region IView
        public void Display()
        {
            new Application().Run(this);
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

        private void UpdateBoardView(Color[,] board)
        {
            boardView.Board = board;
        }

        
    }
}
