using Snake_Game.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
			}
		}

		public DelegateCommand StartStopGEMA { get; }
		private MovieDirection _currentMD = MovieDirection.Right;
        public MWVM()
        {
            StartStopGEMA = new DelegateCommand(() => ContinueGAME = !ContinueGAME);
        }

		public List<List<CellVM>> AllCells = new List<List<CellVM>>();

		private int _row = 10;
		private int _col = 10;
    }
}
