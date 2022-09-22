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

namespace StrongBox.Components
{
    /// <summary>
    /// Логика взаимодействия для Switcher.xaml
    /// </summary>
    public partial class Switcher : UserControl, ISwitcher
    {
        public SwitcherState State { get { return _state; } }
        public event Action<int,int> SwitcherPressed;
        public int ImageSide { get; } = 64;

        private SwitcherState _state;
        private string _horizontalImage = "/Images/HorizontalLine.png";
        private string _verticalImage = "/Images/VerticalLine.png";
        private int _row;
        private int _column;

        public Switcher()
        {
            _state = SwitcherState.Horizontal;
            InitializeComponent();
            ChangeImage();
            SwitcherButton.Click += SwitcherButton_Click;
        }

        private void ChangeImage()
        {
            string imageSource = String.Empty;

            if (_state == SwitcherState.Horizontal)
                imageSource = _horizontalImage;
            else
                imageSource = _verticalImage;

            Image image = new Image();
            BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.UriSource = new Uri(imageSource, UriKind.Relative);
            bitmapImage.EndInit();
            image.Stretch = Stretch.Uniform;
            image.Width = ImageSide;
            image.Source = bitmapImage;
            SwitcherButton.Content = image;
        }

        private void ChangeState()
        {
            if (_state == SwitcherState.Horizontal)
                _state = SwitcherState.Vertical;
            else
                _state = SwitcherState.Horizontal;
        }

        private void SwitcherButton_Click(object sender, RoutedEventArgs e) => SwitcherPressed?.Invoke(_row,_column);


        public void Rotate()
        {
            ChangeState();
            ChangeImage();
        }

        public void SetPosition(int i, int j)
        {
            _row = i;
            _column = j;
        }
    }

    public enum SwitcherState { Horizontal, Vertical };
}
