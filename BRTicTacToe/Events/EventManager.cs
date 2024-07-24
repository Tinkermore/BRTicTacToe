#pragma warning disable CS8618

using BRTicTacToe.General;
using System;

namespace BRTicTacToe.Events
{
    public class EventManager
    {
        public event EventHandler GameStarted;
        public event EventHandler<OnMoveEventArgs> PlayerMoved;
        public event EventHandler<OnPlayerTurnStartArgs> PlayerTurnStart;
        public event EventHandler<OnMoveEventArgs> InvalidMoveAttempted;
        public event EventHandler BoardUpdated;
        public event EventHandler<OnGameCompleteArgs> GameEnded;

        internal void OnGameStarted() { GameStarted?.Invoke(this, EventArgs.Empty); }
        internal void OnBoardUpdated() { BoardUpdated?.Invoke(this, EventArgs.Empty); }
        internal void OnGameEnded(Player? winner) { GameEnded?.Invoke(this, new OnGameCompleteArgs { Winner = winner }); }
        internal void OnPlayerTurnStart(Player player) { PlayerTurnStart?.Invoke(this, new OnPlayerTurnStartArgs() { Player = player }); }
        internal void OnPlayerMoved(Player player, int position) { PlayerMoved?.Invoke(this, new OnMoveEventArgs { Player = player, Position = position }); }
        internal void OnInvalidMoveAttempted(Player player, int position) { InvalidMoveAttempted?.Invoke(this, new OnMoveEventArgs { Player = player, Position = position }); }
    }
}
