using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using SubmarineGame.GameBoard;

public class Game
{
    public Gameboard Board { get; set; }
    public int Difficulty { get; set; }

    public Game()
    {
        Board = new Gameboard();
        Difficulty = 0;
        Console.Clear();
    }
    public Game(int level, Coordinate c)
    {
        int x = c.XCoordinate;
        int y = c.YCoordinate;
        if(x == 10 && y == 10)
        {
            Board = new Gameboard();
        }
        else
        {
            Board = new Gameboard(c);
        }

        Difficulty = level;
        Console.Clear();
    }
    public bool IsGameOver()
    {
        if (AllShipsSunk(Board))
        {
            return true;
        }
        return false;
    }
    public void GameOver()
    {
        if (AllShipsSunk(Board))
        {
            int guess_score = 0;
            int hit_gusses = Board.RocketGus - Board.RocketUnsGus;
            int mis_gusses = Board.RocketUnsGus;
            int total_gusses = Board.RocketGus;
            int board_size_x = Board.PlayerLayer.coorGrid.XCoordinate;
            int board_size_y = Board.PlayerLayer.coorGrid.YCoordinate;
            if (hit_gusses > mis_gusses)
            {
                guess_score = hit_gusses - mis_gusses;
            }
            int total_score = Board.Score + Difficulty * 3 + guess_score;

            List<Ships> shipList = Board.ShipLayer.Ships;
            ControlOutput.ShowShotResult(board_size_x, board_size_y, total_score,
                            total_gusses, mis_gusses, hit_gusses, shipList);
        }
    }

    private bool AllShipsSunk(Gameboard board)
    {
        foreach (Ships ship in board.ShipLayer.Ships)
        {
            if (ship.Sunk != true)
            {
                return false;
            }
        }
        return true;
    }

}