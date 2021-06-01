using SubmarineGame.GameBoard;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class Grid
{
    public Coordinate coorGrid;
    public string Symbol { get; set; }
    public string[,] ColorM { get; set; }
    public string[,] Matrix { get; set; }
    public List<Ships> Ships { get; set; }

    public Grid()
    {
        string[,] grid = GenerateGrid(10, 10);
        this.Ships = new List<Ships>();
        this.Matrix = grid;
  
        this.Symbol = "*";
        this.ColorM = GenerateGrid(10, 10);
        this.coorGrid = new Coordinate(10, 10);

    }

    public Grid(string symbol)
    {
        string[,] grid = GenerateGrid(10, 10, symbol);
        this.Ships = new List<Ships>();
        this.Matrix = grid;
        this.Symbol = symbol;
        this.ColorM = GenerateGrid(10, 10);
        this.coorGrid = new Coordinate(10,10);

    }
    public Grid(int height, int width)
    {
        string[,] grid = GenerateGrid(height, width);
        this.Ships = new List<Ships>();
        this.Matrix = grid;
        this.Symbol = "*";
        this.ColorM = GenerateGrid(height, width);
        this.coorGrid = new Coordinate(height, width);
    }

    public Grid(int height, int width, string symbol)
    {
        string[,] grid = GenerateGrid(height, width, symbol);
        this.Ships = new List<Ships>();
        this.Matrix = grid;
        this.Symbol = symbol;
        this.ColorM = GenerateGrid(height, width, symbol);
        this.coorGrid = new Coordinate(height, width);

    }

    private static string[,] GenerateGrid(int x, int y)
    {
        if (x < 0 || y < 0)
        {
            throw new ArgumentException("Must be positive values");
        }
        x = x + 1;
        y = y + 1;
        string[,] grid = new System.String[x, y];
        for (int i = 0; i < grid.GetLength(0); i++)
        {
            for (int ii = 0; ii < grid.GetLength(1); ii++)
            {
                if (i == 0 && ii == 0)
                {
                    grid[i, ii] = string.Empty;
                }
                else if (i == 0 && ii > 0)
                {
                    string num = (ii - 1).ToString();
                    grid[i, ii] = num;

                }
                else if (ii == 0 && i > 0)
                {
                    if (i > 26)
                    {
                        grid[i, ii] = GetColumnName(i-26) + GetColumnName(i - 26);

                    }
                    else
                        grid[i, ii] = GetColumnName(i);
                
                }
                else
                {
                    grid[i, ii] = "*";
                
                }
            }
        } 
        return grid;
    }

    private static string[,] GenerateGrid(int x, int y, string symbol)
    {
        if (x < 0 || y < 0)
        {
            throw new ArgumentException("Must be positive values");
        }
        x = x + 1;
        y = y + 1;
        string[,] grid = new System.String[x, y];
        for (int i = 0; i < grid.GetLength(0); i++)
        {
            for (int ii = 0; ii < grid.GetLength(1); ii++)
            {
                if (i == 0 && ii == 0)
                {
                    grid[i, ii] = symbol;
                }
                else if (i == 0 && ii > 0)
                {
                    string num = (ii - 1).ToString();
                    grid[i, ii] = num;
                }
                else if (ii == 0 && i > 0)
                {
                    grid[i, ii] = GetColumnName(i);
                }
                else
                {
                    grid[i, ii] = symbol;
                }
            }
        }
        return grid;
    }
    public static void WriteGrid(string[,] matrix)
    {
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int ii = 0; ii < matrix.GetLength(1); ii++)
            {
                Console.Write("{0}\t", matrix[i, ii]);
            }
            Console.WriteLine("\n");
        }
    }
    public static void WriteGrid(Grid grid)
    {
        for (int i = 0; i < grid.Matrix.GetLength(0); i++)
        {
            for (int ii = 0; ii < grid.Matrix.GetLength(1); ii++)
            {
                if ((grid.Matrix[i, ii] == "X")&&(grid.ColorM[i,ii]=="red"))
                {
                    ConsoleColor curColor = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("{0}\t", grid.Matrix[i, ii]);
                    Console.ForegroundColor = curColor;

                }
                else if ((grid.Matrix[i, ii] != "X") &&
                    grid.Matrix[i, ii] != grid.Symbol &&
                    i != 0 && ii != 0)
                {
                    ConsoleColor curColor = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("{0}\t", grid.Matrix[i, ii]);
                    Console.ForegroundColor = curColor;

                }
                else if((grid.Matrix[i,ii] == "X")&&(grid.ColorM[i,ii]== "green"))
                {
                    ConsoleColor curColor = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("{0}\t", grid.Matrix[i, ii]);
                    Console.ForegroundColor = curColor;

                }

                else
                { 
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("{0}\t", grid.Matrix[i, ii]);
             
                }
             
            }
            Console.WriteLine("\n");
        }
    }
    private static string GetColumnName(int columnNumber)
    {
        int dividend = columnNumber;
        string columnName = String.Empty;
        int modulo;

        while (dividend > 0 && dividend < 27)
        {
            modulo = (dividend - 1) % 26;
            columnName = Convert.ToChar(65 + modulo) + columnName;
            dividend = (int)((dividend - modulo) / 26);
        }

        return columnName;
    }

}