using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;
using Color = Model.Color;

namespace View.Views
{
    /// <summary>
    /// Interaction logic for BoardView.xaml
    /// </summary>
    public partial class BoardView : UserControl
    {
        private Color[,] _board;
        private bool _initialized = false;
        public Color[,] Board { 
            private get
            {
                return _board;
            }
            set
            {
                _board = value;
                if (_initialized)
                {
                    UpdateView();
                    //grdBoard.Dispatcher.Invoke(UpdateView, DispatcherPriority.Render);
                }
                else
                    SetupBoard();
            } 
        }
        public BoardView()
        {
            InitializeComponent();
        }

        private void SetupBoard()
        {
            //clear out the board
            grdBoard.RowDefinitions.Clear();
            for (int i = 0; i < Board.GetLength(0); i++)
                grdBoard.ColumnDefinitions.Add(new ColumnDefinition());
            grdBoard.ColumnDefinitions.Clear();
            for (int i = 0; i < Board.GetLength(1); i++)
                grdBoard.RowDefinitions.Add(new RowDefinition());

            for (int col = 0; col < grdBoard.ColumnDefinitions.Count; col++)
            {
                for (int row = 0; row < grdBoard.RowDefinitions.Count; row++)
                {
                    var color = ColorToBrush(Board[row, col]);
                    var rect = new Rectangle { Stroke = Brushes.Azure, Fill = color, Margin = new Thickness(-1), };

                    grdBoard.Children.Add(rect);
                    Grid.SetColumn(rect, col);
                    Grid.SetRow(rect, row);
                }
            }

            _initialized = true;
        }

        private void UpdateView()
        {
            if (!_initialized)
                throw new Exception("BoardView isn't initialized. Set the Board first.");
            //try
            //{
                grdBoard.Dispatcher.Invoke(delegate()
                {
                    foreach (var objChild in grdBoard.Children)
                    {
                        var child = objChild as Rectangle;
                        int col = Grid.GetColumn(child);
                        int row = Grid.GetRow(child);
                        var color = Brushes.Black;
                        switch (Board[row, col])
                        {
                            case Color.Red:
                                color = Brushes.Red;
                                break;
                            case Color.Blue:
                                color = Brushes.Blue;
                                break;
                            case Color.Green:
                                color = Brushes.Green;
                                break;
                            case Color.Orange:
                                color = Brushes.Orange;
                                break;
                            case Color.Purple:
                                color = Brushes.Purple;
                                break;
                            case Color.Yellow:
                                color = Brushes.Yellow;
                                break;
                        }
                        child.Fill = color;
                    }
                });
            //}
            //catch { }
        }
        private Color BrushToColor(Brush brush)
        {
            if (brush.Equals(Brushes.Red))
                return Color.Red;
            if (brush.Equals(Brushes.Orange))
                return Color.Orange;
            if (brush.Equals(Brushes.Yellow))
                return Color.Yellow;
            if (brush.Equals(Brushes.Green))
                return Color.Green;
            if (brush.Equals(Brushes.Blue))
                return Color.Blue;
            return Color.Purple;
        }
        private Brush ColorToBrush(Color color)
        {
            Brush brush = Brushes.Black;
            switch (color)
            {
                case Color.Red:
                    brush = Brushes.Red;
                    break;
                case Color.Blue:
                    brush = Brushes.Blue;
                    break;
                case Color.Green:
                    brush = Brushes.Green;
                    break;
                case Color.Orange:
                    brush = Brushes.Orange;
                    break;
                case Color.Purple:
                    brush = Brushes.Purple;
                    break;
                case Color.Yellow:
                    brush = Brushes.Yellow;
                    break;
            }
            return brush;
        }
    }
}
