#pragma warning disable CS8618
using BRTicTacToe.General;

namespace BRTicTacToe.Events
{
    public class OnMoveEventArgs
    {
        public Player Player;
        public int Position;
    }
}
