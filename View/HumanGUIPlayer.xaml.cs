using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using Control;
using Model;
using Color = Model.Color;

namespace View
{
	/// <summary>
	/// Interaction logic for HumanGUIPlayer.xaml
	/// </summary>
	public partial class HumanGUIPlayer : Window, IView, IInput
	{
		private IInputHandler _destination;
		public HumanGUIPlayer()
		{
			InitializeComponent();
		}

		private void ResizeButtons()
		{
			outerGrid.RowDefinitions[0].Height = new GridLength(50, GridUnitType.Star);
			outerGrid.RowDefinitions[1].Height = new GridLength(50, GridUnitType.Star);
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
			_destination.PickColor(color);
		}
		#endregion

		private void GameWinner(int turns)
		{
			MessageBox.Show("It took you "+turns+" turns");
            //Close(); //play again?
            Controller.Instance(this, this).Reset();
		}

		public void Init(Color[,] board)
		{
			_destination = Controller.Instance(this, this);
			BoardUpdated(board);
		}

		public void BoardUpdated(Color[,] board)
		{
			boardView.Board = board;
		}

		public void GameOver(WinEventArgs e)
		{
			GameWinner(e.Turns);
		}

		public event Player.ColorSelectedDel ColorSelected;
		private Player player;
		public void SetPlayer(Player p)
		{
			player = p;
		}
	}
}
