using Snake_Game.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake_Game.Models
{
    internal class Snake
    {
        public Queue<CellVM> SnakeCells { get; } = new Queue<CellVM> ();

        private CellVM _start;
        private Action _generateFood;
        private List<List<CellVM>> _cells;

        public Snake(List<List<CellVM>> cells, CellVM start, Action generateFood)
        {
            SnakeCells.Enqueue(start);
            _cells = cells;
            _start = start;
            _start.CellType = CellType.Snake;
            _generateFood = generateFood;
        }
        public void RESTART()
        {
            foreach (var cell in SnakeCells)
            {
                cell.CellType = CellType.None;
            }
            SnakeCells.Clear();
            _start.CellType = CellType.Snake;
            
            SnakeCells.Enqueue(_start);
        }

        public void Move(MovieDirection direction)
        {
            var ledercell = SnakeCells.Last();

            int nextrow = ledercell.Row;
            int nextcol = ledercell.Column;
            switch (direction)
            {
                case MovieDirection.Left:
                    nextcol--;
                    break;
                case MovieDirection.Right:
                    nextcol++;
                    break;
                case MovieDirection.Up:
                    nextrow--;
                    break;
                case MovieDirection.Down:
                    nextrow++;
                    break;
                default:
                    break;
            }

            try
            {
                var nextcell = _cells[nextrow][nextcol];

                switch (nextcell?.CellType)
                {
                    case CellType.None:
                        nextcell.CellType = CellType.Snake;
                        SnakeCells.Dequeue().CellType = CellType.None;
                        SnakeCells.Enqueue(nextcell);
                        break;
                    case CellType.Food:
                        nextcell.CellType = CellType.Snake;
                        SnakeCells.Enqueue(nextcell);
                        _generateFood?.Invoke();
                        break;
                    default:
                        throw new Exception("Конец игры!");
                }
            }
            catch (ArgumentOutOfRangeException) { throw new Exception("Конец игры!"); }
        }

       

    }
}
