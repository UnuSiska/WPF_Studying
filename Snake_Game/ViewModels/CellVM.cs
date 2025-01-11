using Snake_Game.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake_Game.ViewModels
{
    internal class CellVM : BindableBase
    {

        public int Row { get;}
        public int Column { get;}

        private CellType _CellType = CellType.None;

        public CellType CellType
        {
            get { return _CellType; }
            set
            {
                _CellType = value;
                RaisePropertyChanged(nameof(CellType));
            }
        }
        public CellVM(int row, int column)
        {
            Row = row;
            Column = column;
        }

    }
}
