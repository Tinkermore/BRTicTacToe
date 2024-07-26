# BlakRoq TicTacToe - CORE

Coding exercise to find the best way to work on a C# Unity game across 3 "teams"
- The **Core** team focuses on building game logic in C# into .dll libraries
- The **Interface** team focuses on building visualizations and user inputs, connecting players to the game's core
- The **Ops** team focuses on stack, process, and automation

## Tic Tac Toe
Simple game to start with. Creating a game based on Tic Tac Toe requires two codebases:
- *This* repository is maintained by the CORE team and builds the game logic into a .dll for Unity
- `BRTicTacToe-unity` repository is maintained by the INTERFACE team and is a playable web and mobile game

## Library Handoff
This repository contains game logic and tests for Tic Tac Toe.  DLL artifacts are generated and stored on Github.  Unity code will sync with the latest artifact when built.
