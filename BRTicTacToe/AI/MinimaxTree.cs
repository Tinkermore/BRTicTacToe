using System;
using System.Collections.Generic;
using System.Linq;

namespace BRTicTacToe.AI
{
    public class MinimaxTree
    {
        public char[] Board;
        private string OriginalBoard;
        private const char X = 'X';
        private const char O = 'O';
        private const char EMPTY = '-';
        private Difficulty Difficulty;

        public MinimaxTree(string board)
        {
            OriginalBoard = board;
            Board = board.ToCharArray();
        }

        public int Solve(Difficulty difficulty, int depth)
        {
            Difficulty = difficulty;

            if (difficulty == Difficulty.EASY)
            {
                var options = GetOptions();
                Random random = new Random();
                return options[random.Next(0, options.Count)];
            }

            bool isMaximizing = GetPlayerTurn() == X;
            (int, int) bestOption = Minimax(depth, isMaximizing);
            return bestOption.Item2;
        }

        private (int, int) Minimax(int depth, bool isMaximizing)
        {
            int score = GetScore();
            if (score >= 100 || score <= -100 || depth == 0 || !GetOptions().Any()) { return (score, -1); }

            (int, int) bestOption = (-1, -1);

            if (isMaximizing)
            {
                int maxEval = int.MinValue;
                foreach (var option in GetOptions())
                {
                    Move(option);
                    (int, int) eval = Minimax(depth - 1, !isMaximizing);
                    if (eval.Item1 > maxEval) { maxEval = eval.Item1; bestOption = eval; bestOption.Item2 = option; }
                    UndoMove(option);
                }

                return bestOption;
            }
            else
            {
                int minEval = int.MaxValue;
                foreach (var option in GetOptions())
                {
                    Move(option);
                    (int, int) eval = Minimax(depth - 1, true);
                    if (eval.Item1 < minEval) { minEval = eval.Item1; bestOption = eval; bestOption.Item2 = option; }
                    UndoMove(option);
                }

                return bestOption;
            }
        }

        public List<int> GetOptions()
        {
            List<int> options = new List<int>();
            for (int i = 0; i < 9; i++) { if (Board[i] == EMPTY) { options.Add(i); } }
            return options;
        }

        public void Move(int position)
        {
            var playerMove = GetPlayerTurn();
            Board[position] = playerMove;
        }

        private char GetPlayerTurn()
        {
            var numberOfXs = Board.Count(o => o == X);
            var numberOfOs = Board.Count(o => o == O);
            return numberOfXs <= numberOfOs ? X : O;
        }

        public void UndoMove(int position) { Board[position] = EMPTY; }

        public int GetScore()
        {
            int numberOfTurnsTaken = 9 - Board.Count(o => o == EMPTY);
            int runningScore = 0;

            if (Difficulty >= Difficulty.HARD)
            {
                char playerTurn = GetPlayerTurn();
                runningScore = GetSquareValue(playerTurn);
                if (playerTurn == O) { runningScore = runningScore * -1; }
            }

            if (IsWinning(X)) return (int)(1000000.0 / numberOfTurnsTaken) + runningScore;
            if (IsWinning(O)) return (int)(-1000000.0 / numberOfTurnsTaken) + runningScore;
            if (!GetOptions().Any()) return 0;

            return runningScore;
        }

        private int GetSquareValue(char player)
        {
            int score = 0;

            if (Board[0] == player) { score += 25; }
            if (Board[2] == player) { score += 25; }
            if (Board[6] == player) { score += 25; }
            if (Board[8] == player) { score += 25; }

            return score;
        }

        private bool IsWinning(char player)
        {
            return
                (Board[0] == player && Board[1] == player && Board[2] == player) ||
                (Board[3] == player && Board[4] == player && Board[5] == player) ||
                (Board[6] == player && Board[7] == player && Board[8] == player) ||
                (Board[0] == player && Board[3] == player && Board[6] == player) ||
                (Board[1] == player && Board[4] == player && Board[7] == player) ||
                (Board[2] == player && Board[5] == player && Board[8] == player) ||
                (Board[0] == player && Board[4] == player && Board[8] == player) ||
                (Board[2] == player && Board[4] == player && Board[6] == player);
        }
    }
}
