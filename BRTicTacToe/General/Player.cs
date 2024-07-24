namespace BRTicTacToe.General
{
    public class Player
    {
        public char DisplayCharacter { get; set; }
        public bool[] State = new bool[TicTacToeBoard.BOARD_POSITIONS];

        internal Player(char displayCharacter) { DisplayCharacter = displayCharacter; }

        internal void Play(params int[] indexes)
        {
            foreach (var index in indexes) { State[index] = true; }
        }

        internal void Undo(int index)
        {
            State[index] = false;
        }

        private int GetStateValue()
        {
            int number = 0;
            for (int i = 0; i < State.Length; i++) { if (State[i]) { number |= 1 << i; } }
            return number;
        }

        internal void Reset()
        {
            State = new bool[TicTacToeBoard.BOARD_POSITIONS];
        }

        internal bool IsWinningState()
        {
            if (IsAllTrue(0, 1, 2)) { return true; }
            if (IsAllTrue(3, 4, 5)) { return true; }
            if (IsAllTrue(6, 7, 8)) { return true; }
            if (IsAllTrue(0, 3, 6)) { return true; }
            if (IsAllTrue(1, 4, 7)) { return true; }
            if (IsAllTrue(2, 5, 8)) { return true; }
            if (IsAllTrue(0, 4, 8)) { return true; }
            if (IsAllTrue(2, 4, 6)) { return true; }

            return false;
        }

        private bool IsAllTrue(int a, int b, int c) { return State[a] && State[b] && State[c]; }
    }
}
