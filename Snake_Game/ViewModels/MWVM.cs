using Snake_Game.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Snake_Game.ViewModels
{
    internal class MWVM : BindableBase
    {

		private bool _ContinueGAME;


		public bool ContinueGAME
		{
			get => _ContinueGAME;
			private set 
			{
				_ContinueGAME = value;
				RaisePropertyChanged(nameof(ContinueGAME));

				if (_ContinueGAME) SnakeGo();
			}
		}

        public List<List<CellVM>> AllCells { get; } = new List<List<CellVM>>();

		


        private int _row = 10;
        private int _col = 10;
		private int _speed = SPEED;
		private const int SPEED = 333;
		private CellVM _lastFood;

		private Snake _snake;
		private MainWindow _mainWindow;


        public DelegateCommand StartStopGEMA { get; }
		private MovieDirection _currentMD = MovieDirection.Right;


        public MWVM(MainWindow mainWindow)
        {
            _mainWindow = mainWindow;
            StartStopGEMA = new DelegateCommand(() => ContinueGAME = !ContinueGAME);

			for (int row = 0; row < _row; row++)
			{
				var rowList = new List<CellVM>();
				for (int col = 0; col < _col; col++)
				{
					var cell = new CellVM(row, col);
					rowList.Add(cell);
				}
				AllCells.Add(rowList);
			}
            _snake = new Snake(AllCells, AllCells[_row / 2][_col / 2], CreateFood);
			CreateFood();

            _mainWindow.KeyDown += UserKeyDown;
        }
	
		private async Task SnakeGo()
		{
			while (ContinueGAME)
			{
				await Task.Delay(_speed);

				try
				{
					_snake.Move(_currentMD);
				}
				catch (Exception ex)
				{
					ContinueGAME = false;
					MessageBox.Show(ex.Message);
					_speed = SPEED;
					_snake.RESTART();
					CreateFood();
					_lastFood.CellType = CellType.None;
				}


			}
		}

		private void UserKeyDown(object sender, KeyEventArgs e)
		{
			switch (e.Key)
			{
				case Key.A:
					if(_currentMD != MovieDirection.Right)
                        _currentMD = MovieDirection.Left;
                    break;
                case Key.D:
                    if (_currentMD != MovieDirection.Left)
                        _currentMD = MovieDirection.Right;
                    break;
                case Key.S:
                    if (_currentMD != MovieDirection.Up)
                        _currentMD = MovieDirection.Down;
                    break;
                case Key.W:
                    if (_currentMD != MovieDirection.Down)
                        _currentMD = MovieDirection.Up;
                    break;
                default:
					break;
			}
		}

        private void CreateFood()
		{
			var random = new Random();
			int row = random.Next(0, _row);
            int col = random.Next(0, _col);

			_lastFood = AllCells[row][col];
			if (_snake.SnakeCells.Contains(_lastFood))
			{
				CreateFood();
            }
			_lastFood.CellType = CellType.Food;
			_speed = (int)(_speed * 0.95);
        }
    }
}
