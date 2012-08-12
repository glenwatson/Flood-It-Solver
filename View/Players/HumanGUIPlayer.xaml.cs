using System;
using System.Windows;
using Model;
using View.Shared;
using Color = Model.Color;
namespace View.Players
{
	/// <summary>
	/// Interaction logic for HumanGUIPlayer.xaml
	/// </summary>
    //I don't know where to put this class because it's both an IView and an IInput.
    //I know it's not an actual Player, but it's close
    public partial class HumanGUIPlayer : Window, IView, IInput
	{
		public HumanGUIPlayer()
		{
			InitializeComponent();
		}

        private Controller getController()
        {
            return Controller.Instance(null, null);
        }

        //private void ResizeButtons()
        //{
        //    outerGrid.RowDefinitions[0].Height = new GridLength(50, GridUnitType.Star);
        //    outerGrid.RowDefinitions[1].Height = new GridLength(50, GridUnitType.Star);
        //}

		private void GameWinner(int turns)
		{
			MessageBox.Show("It took you "+turns+" turns");
            //Close(); //play again?
            getController().Reset();
		}


        #region button clicks
        private void btnRed_Click(object sender, RoutedEventArgs e)
        {
            SelectColor(Color.Red);
        }
        private void btnOrange_Click(object sender, RoutedEventArgs e)
        {
            SelectColor(Color.Orange);
        }

        private void btnYellow_Click(object sender, RoutedEventArgs e)
        {
            SelectColor(Color.Yellow);
        }

        private void btnGreen_Click(object sender, RoutedEventArgs e)
        {
            SelectColor(Color.Green);
        }

        private void btnBlue_Click(object sender, RoutedEventArgs e)
        {
            SelectColor(Color.Blue);
        }

        private void btnPurple_Click(object sender, RoutedEventArgs e)
        {
            SelectColor(Color.Purple);
        }
        private void SelectColor(Color color)
        {
            getController().PickColor(color);
        }
        #endregion

        #region IView
        [STAThread]
        public void Display()
        {
            new Application().Run(this);
        }

        public void BoardUpdated(Color[,] board)
		{
			boardView.Board = board;
		}

		public void GameOver(WinEventArgs e)
		{
			GameWinner(e.Turns);
		}
        #endregion
	}
}
