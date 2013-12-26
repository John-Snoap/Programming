using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConnectFourGUI
{
    class ConnectFourGameState
    {
        // private constants
        private const int ROW = 0;
        private const int COLUMN = 1;
        
        // private enumerations
        private enum GameBoard { empty, black, red };

        // public enumerations
        public enum GameState { notWon, won, draw };

        // private data
        private GameBoard[,] gameBoard;
        private int numberOfMoves;

        // constructor
        public ConnectFourGameState()
        {
            gameBoard = new GameBoard[6, 7];
            resetGame();
        } // end constructor

        // public methods
        public void resetGame()
        {
            numberOfMoves = 0;

            // These nested loops set all the values in the grid to empty
            for (int row = 0; row < gameBoard.GetLength(ROW); row++)
            {
                for (int column = 0; column < gameBoard.GetLength(COLUMN); column++)
                {
                    gameBoard[row, column] = GameBoard.empty;
                } // end inner for loop
            } // end outer for loop
        } // end method resetGame

        public GameState playTurn(bool redsTurn, int column, out int row)
        {
            GameState gameState = GameState.notWon;
            bool pieceSet = false;
            row = 0;

            if (column >= 0 && column < 7)
            {
                for (row = 0; row < gameBoard.GetLength(ROW) && !pieceSet; row++)
                {
                    if (gameBoard[row, column] == GameBoard.empty)
                    {
                        if (redsTurn)
                        {
                            gameBoard[row, column] = GameBoard.red;
                        } // end if
                        else
                        {
                            gameBoard[row, column] = GameBoard.black;
                        } // end else
                        
                        pieceSet = true;
                        numberOfMoves++;
                        gameOverCheck(row, column, out gameState);
                    } // end if
                } // end for loop
            } // end if

            return gameState;
        } // end method playTurn

        // private methods
        private void gameOverCheck(int row, int column, out GameState gameState)
        {
            gameState = GameState.notWon;

            checkHorizontal(row, column, out gameState);
            checkVertical(row, column, out gameState);


            if (gameState == GameState.notWon && numberOfMoves >= 42)
            {
                gameState = GameState.draw;
            } // end if
        } // end method gameOverCheck

        private void checkHorizontal(int row, int column, out GameState gameState)
        {
            gameState = GameState.notWon;
            int numberInARow = 1;
            bool noBreaks = true;

            // compare to the next one on the right
            int nextRow = row; // row is y
            int nextColumn = column + 1; // column is x

            while ((nextColumn < gameBoard.GetLength(COLUMN)) && (numberInARow < 4) && (noBreaks)) // check to the right
            {
                compareColors(row, column, nextRow, nextColumn, ref numberInARow, ref noBreaks);
                nextColumn++; // look at the next space to the right
            } // end while

            // reset the column to compare to the next one on the left
            nextColumn = column - 1; // column is x
            noBreaks = true; // reset so we can look the other direction

            while ((nextColumn >= 0) && (numberInARow < 4) && (noBreaks)) // check to the left
            {
                compareColors(row, column, nextRow, nextColumn, ref numberInARow, ref noBreaks);
                nextColumn--; // look at the next space to the left
            } // end while

            if (numberInARow >= 4)
            {
                gameState = GameState.won;
            } // end if
        } // end method checkHorizonatal

        private void checkVertical(int row, int column, out GameState gameState)
        {
            gameState = GameState.notWon;
            int numberInARow = 1;
            bool noBreaks = true;

            // compare to the next one up
            int nextRow = row + 1; // row is y
            int nextColumn = column; // column is x

            while ((nextRow < gameBoard.GetLength(ROW)) && (numberInARow < 4) && (noBreaks)) // check up
            {
                compareColors(row, column, nextRow, nextColumn, ref numberInARow, ref noBreaks);
                nextRow++; // look at the next space to the right
            } // end while

            // reset the row to compare to the next one down
            nextRow = row - 1; // row is x
            noBreaks = true; // reset so we can look the other direction

            while ((nextRow >= 0) && (numberInARow < 4) && (noBreaks)) // check down
            {
                compareColors(row, column, nextRow, nextColumn, ref numberInARow, ref noBreaks);
                nextRow--; // look at the next space to the left
            } // end while

            if (numberInARow >= 4)
            {
                gameState = GameState.won;
            } // end if
        } // end moethod checkVertical

        private void compareColors(int row1, int column1, int row2, int column2, ref int numberInARow, ref bool noBreaks)
        {
            if (gameBoard[row1, column1] == gameBoard[row2, column2])
            {
                numberInARow++;
            } // end if
            else
            {
                noBreaks = false;
            } // end else
        } // end method compareColors
    } // end class ConnectFourGameState
} // end namespace ConnectFourGUI
