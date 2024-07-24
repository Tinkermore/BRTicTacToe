using BRTicTacToe.AI;
using BRTicTacToe.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BRTicTacToe.General
{
    public class TicTacToeBoard
    {
        public const int BOARD_POSITIONS = 9;

        public Player PlayerX = new Player(X);
        public Player PlayerO = new Player(O);
        public EventManager EventManager = new EventManager();

        protected const char X = 'X';
        protected const char O = 'O';
        protected const char EMPTY = '-';

        protected char[] VALID_CHARACTERS_IN_BOARD_STATE = { X, O, EMPTY };

        public TicTacToeBoard() { EventManager.OnGameStarted(); }
        public TicTacToeBoard(string boardState) { SetBoardState(boardState); EventManager.OnGameStarted(); }

        public override string ToString()
        {
            string boardState = new string(EMPTY, 9);
            char[] boardArray = boardState.ToCharArray();

            for (int i = 0; i < BOARD_POSITIONS; i++)
            {
                if (PlayerX.State[i]) { boardArray[i] = X; }
                if (PlayerO.State[i]) { boardArray[i] = O; }
            }

            return new string(boardArray);
        }

        internal void SetBoardState(string boardState)
        {
            ValidateBoardString(boardState);

            PlayerX.Reset();
            PlayerO.Reset();

            char[] boardArray = boardState.ToCharArray();
            for (int i = 0; i < BOARD_POSITIONS; i++)
            {
                if (boardArray[i] == X) { PlayerX.State[i] = true; }
                if (boardArray[i] == O) { PlayerO.State[i] = true; }
            }
        }

        public void Move(int positionIndex)
        {
            Player player = GetPlayerTurn();
            if (!IsValidMove(positionIndex)) { EventManager.OnInvalidMoveAttempted(player, positionIndex); return; }

            player.Play(positionIndex);

            EventManager.OnPlayerMoved(player, positionIndex);
            EventManager.OnBoardUpdated();
            if (player.IsWinningState()) { EventManager.OnGameEnded(player); return; }
            if (IsBoardFull()) { EventManager.OnGameEnded(null); return; }

            Player oppositePlayer = player == PlayerX ? PlayerO : PlayerX;
            EventManager.OnPlayerTurnStart(oppositePlayer);
        }

        private void ValidateBoardString(string boardState)
        {
            HashSet<char> uniqueChars = new HashSet<char>();
            foreach(var character in boardState) { uniqueChars.Add(character); }

            foreach (var character in uniqueChars)
            {
                if (!VALID_CHARACTERS_IN_BOARD_STATE.Contains(character)) { throw new ArgumentException($"A valid board state must not contain the '{character}' character. Valid options: '${X}', '${O}', '{EMPTY}'"); }
            }

            if (boardState.Length != 9) { throw new ArgumentException("A valid board state must be of length 9."); }
        }

        public bool IsValidMove(int positionIndex)
        {
            if (IsGameOver() || IsBoardFull()) { return false; }
            if (positionIndex < 0 || positionIndex > BOARD_POSITIONS) { return false; }
            if (PlayerX.State[positionIndex] || PlayerO.State[positionIndex]) { return false; }
            return true;
        }

        public void UndoMove(Player player, int positionIndex)
        {
            player.Undo(positionIndex);
            EventManager.OnBoardUpdated();
        }

        public Player GetPlayerTurn()
        {
            string state = ToString();
            var xs = state.Count(o => o == X);
            var os = state.Count(o => o == O);

            if (xs <= os) { return PlayerX; }
            return PlayerO;
        }

        public void MoveForAgent(Difficulty difficulty)
        {
            if (IsGameOver()) { return; }

            OnMoveEventArgs recommended = GetRecommendedMove(difficulty);
            Move(recommended.Position);
        }

        public OnMoveEventArgs GetRecommendedMove(Difficulty difficulty)
        {
            MinimaxTree mmt = new MinimaxTree(ToString());
            int position = mmt.Solve(difficulty, 4);

            return new OnMoveEventArgs() { Player = GetPlayerTurn(), Position = position };
        }

        public bool IsBoardEmpty() { return ToString().All(o => o == EMPTY); }
        public bool IsBoardFull() { return !ToString().Contains(EMPTY); }
        public bool IsGameOver() { return PlayerX.IsWinningState() || PlayerO.IsWinningState() || IsBoardFull(); }
    }
}
