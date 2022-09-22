using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;

namespace StrongBox.Components
{
    public class GameLogic : IGameLogic
    {
        public event Action WinConditionsMet;
        private ISwitcher[,] _switchers;
        private UniformGrid _gameGrid;
        private bool _isGridBuilt;

        public GameLogic(UniformGrid gameGrid)
        {
            _gameGrid = gameGrid;
            _isGridBuilt = false;
        }

        public void CreateCells(int amount)
        {
            _switchers = (new ISwitcher[amount, amount]);

            SetGrid(amount);

            for (int i = 0; i < amount; i++)
            {
                for (int j = 0; j < amount; j++)
                {
                    ISwitcher switcher = new Switcher();
                    _gameGrid.Children.Add((UIElement)switcher);
                    _switchers[i, j] = switcher;

                    switcher.SetPosition(i, j);
                    switcher.SwitcherPressed += RotateSwitchers;
                }
            }
            RandomizeCells();
            _isGridBuilt = true;
        }

        public void ClearGrid()
        {
            _gameGrid.Children.Clear();
            Array.Clear(_switchers, 0, _switchers.Length);

            _gameGrid.Height = 0;
            _gameGrid.Width = 0;
            _isGridBuilt = false;
        }

        private void SetGrid(int size)
        {
            ISwitcher switcher = new Switcher();
            _gameGrid.Columns = size;
            _gameGrid.Width = size * (switcher.ImageSide + 5);
            _gameGrid.Height = size * (switcher.ImageSide + 5);
            _gameGrid.UpdateLayout();
            App.Current.MainWindow.UpdateLayout();
        }

        private void RotateSwitchers(int row, int column)
        {
            var length = _switchers.GetLength(0);
            for (int i = 0; i < length; i++)

                _switchers[row, i].Rotate();

            for (int i = 0; i < length; i++)
            {
                if (i == row)
                    continue;
                _switchers[i, column].Rotate();
            }

            if (CheckWinCondition())
                WinConditionsMet?.Invoke();

        }

        private bool CheckWinCondition()
        {
            if (!_isGridBuilt)
                return false;

            var state = _switchers[0, 0].State;

            foreach (var switcher in _switchers)
            {
                if (switcher.State != state)
                    return false;
            }
            return true;
        }

        private void RandomizeCells()
        {
            Random random = new();

            foreach (var switcher in _switchers)
            {
                var result = random.NextDouble() > 0.5;
                if (result)
                    switcher.Rotate();
            }
        }
    }
}
