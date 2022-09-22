using System;

namespace StrongBox.Components
{
    public interface IGameLogic
    {
        void CreateCells(int amount);
        void ClearGrid();
        event Action WinConditionsMet;
    }
}