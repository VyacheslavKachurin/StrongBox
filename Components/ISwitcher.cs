using System;

namespace StrongBox.Components
{
    public interface ISwitcher
    {
        SwitcherState State { get; }

        event Action<int,int> SwitcherPressed;
        int ImageSide { get; }
        void Rotate();
        void SetPosition(int row, int column);
    }
}