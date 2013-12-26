/* John Snoap
 * Joshua Mullen
 * Assignment 7
 * Connect Four
 * Game State
 * Object Oriented Programming
 * November 20, 2013
 */

using System;
using System.Collections.Generic;

namespace ConnectFour
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
            row = 7; // if row stays at 7 then the column is full and nothing should happen

            if (column >= 0 && column < 7)
            {
                for (int nextRow = 0; nextRow < gameBoard.GetLength(ROW) && !pieceSet; nextRow++)
                {
                    if (gameBoard[nextRow, column] == GameBoard.empty)
                    {
                        if (redsTurn)
                        {
                            gameBoard[nextRow, column] = GameBoard.red;
                        } // end if
                        else
                        {
                            gameBoard[nextRow, column] = GameBoard.black;
                        } // end else
                        
                        pieceSet = true;
                        numberOfMoves++;
                        gameOverCheck(nextRow, column, out gameState);
                        row = nextRow;
                    } // end if
                } // end for loop
            } // end if

            return gameState;
        } // end method playTurn

        // private methods
        private void gameOverCheck(int row, int column, out GameState gameState)
        {
            gameState = GameState.notWon;

            // check horizontal for a win
            checkHorizontal(row, column, out gameState);

            // check vertical for a win
            if (gameState == GameState.notWon)
            {
                checkVertical(row, column, out gameState);
            } // end if

            // check diagonal (up and right, down and left) for a win
            if (gameState == GameState.notWon)
            {
                checkDiagonal(row, column, out gameState);
            } // end if

            // check antidiagonal (up and left, down and right) for a win
            if (gameState == GameState.notWon)
            {
                checkAntiDiagonal(row, column, out gameState);
            } // end if

            // check to see if it is a draw
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
                nextRow++; // look at the next space up
            } // end while

            // reset the row to compare to the next one down
            nextRow = row - 1; // row is y
            noBreaks = true; // reset so we can look the other direction

            while ((nextRow >= 0) && (numberInARow < 4) && (noBreaks)) // check down
            {
                compareColors(row, column, nextRow, nextColumn, ref numberInARow, ref noBreaks);
                nextRow--; // look at the next space down
            } // end while

            if (numberInARow >= 4)
            {
                gameState = GameState.won;
            } // end if
        } // end moethod checkVertical

        private void checkDiagonal(int row, int column, out GameState gameState)
        {
            gameState = GameState.notWon;
            int numberInARow = 1;
            bool noBreaks = true;

            // row is y, column is x
            if (((row - column) < 3) && ((row + (6 - column)) > 2)) // make sure we are not comparing if it is in the top left or bottom right corner
            {
                // compare to the next one up and right
                int nextRow = row + 1; // row is y
                int nextColumn = column + 1; // column is x

                while (((nextRow < gameBoard.GetLength(ROW)) && (nextColumn < gameBoard.GetLength(COLUMN))) && (numberInARow < 4) && (noBreaks)) // check up and right
                {
                    compareColors(row, column, nextRow, nextColumn, ref numberInARow, ref noBreaks);
                    nextRow++; // look at the next space up
                    nextColumn++; // look at the next space to the right
                } // end while

                // reset the row to compare to the next one down and left
                nextRow = row - 1; // row is y
                nextColumn = column - 1;
                noBreaks = true; // reset so we can look the other direction

                while (((nextRow >= 0) && (nextColumn >= 0)) && (numberInARow < 4) && (noBreaks)) // check down and left
                {
                    compareColors(row, column, nextRow, nextColumn, ref numberInARow, ref noBreaks);
                    nextRow--; // look at the next space down
                    nextColumn--; // look at the next space to the left
                } // end while

                if (numberInARow >= 4)
                {
                    gameState = GameState.won;
                } // end if
            } // end if
        } // end method checkDiagonal

        private void checkAntiDiagonal(int row, int column, out GameState gameState)
        {
            gameState = GameState.notWon;
            int numberInARow = 1;
            bool noBreaks = true;

            // row is y, column is x
            if (((row + column) > 2) && ((row - (6 - column)) < 3)) // make sure we are not comparing if it is in the top right or bottom left corner
            {
                // compare to the next one up and left
                int nextRow = row + 1; // row is y
                int nextColumn = column - 1; // column is x

                while (((nextRow < gameBoard.GetLength(ROW)) && (nextColumn >= 0)) && (numberInARow < 4) && (noBreaks)) // check up and left
                {
                    compareColors(row, column, nextRow, nextColumn, ref numberInARow, ref noBreaks);
                    nextRow++; // look at the next space up
                    nextColumn--; // look at the next space to the right
                } // end while

                // reset the row to compare to the next one down and right
                nextRow = row - 1; // row is y
                nextColumn = column + 1;
                noBreaks = true; // reset so we can look the other direction

                while (((nextRow >= 0) && (nextColumn < gameBoard.GetLength(COLUMN))) && (numberInARow < 4) && (noBreaks)) // check down and right
                {
                    compareColors(row, column, nextRow, nextColumn, ref numberInARow, ref noBreaks);
                    nextRow--; // look at the next space down
                    nextColumn++; // look at the next space to the left
                } // end while

                if (numberInARow >= 4)
                {
                    gameState = GameState.won;
                } // end if
            } // end if
        } // end method checkAntiDiagnonal

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
} // end namespace ConnectFour
