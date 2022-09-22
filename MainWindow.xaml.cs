using StrongBox.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace StrongBox
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IGameLogic _gameLogic;

        public MainWindow()
        {
            InitializeComponent();

            _gameLogic = new GameLogic(GameGrid);
            _gameLogic.WinConditionsMet += SetWinState;

            PlayButton.Click += (a, b) => ValidateInput();
            ClearButton.Click += (a, b) => _gameLogic.ClearGrid();
            ClearButton.Click += (a, b) => ToggleButtons(true,false);
            ExitButton.Click += (a, b) => ExitGame();

            ToggleButtons(true, false);
            this.MouseDown += DragWindow;
            SetDimensions();
        }

        private void ExitGame() => System.Windows.Application.Current.Shutdown();

        private void ValidateInput()
        {
            var parseResult = Int32.TryParse(GridSizeBox.Text, out int gridSizeNumber);

            if (!parseResult)
                MessageBox.Show("Numbers only");
            else if (gridSizeNumber < 4)
                MessageBox.Show("Number should be 4-99");
            else
            {
                _gameLogic.CreateCells(gridSizeNumber);
                ToggleButtons(false, true);
            }
        }

        private void ToggleButtons(bool playButton, bool clearButton)
        {
            PlayButton.IsEnabled = playButton;
            ClearButton.IsEnabled = clearButton;
        }

        private void SetWinState()
        {
            MessageBox.Show("You won");
            _gameLogic.ClearGrid();
            ToggleButtons(true, false);
        }

        private void DragWindow(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void SetDimensions()
        {
            this.MaxHeight = System.Windows.SystemParameters.PrimaryScreenHeight * 0.8;
            this.MaxWidth = System.Windows.SystemParameters.PrimaryScreenWidth * 0.8;
        }
    }
}
