using System;
using System.Collections.Generic;
using System.Text;

namespace SubmarineGame.GameBoard
{
    public class Ships
    {
        public int Length { get; private set; }
        public string[,] Position { get; set; }
        public int[,] Coordinates { get; set; }
        public int Orientation { get; set; }
        public int[] Seed { get; set; }
        public string Symbol { get; set; }
        public int Hits { get; set; }
        public bool Sunk { get; set; }
        public int score_ship { get; set; }

        private static Random rand = new Random();

        public Ships(int length)
        {
            this.Length = length;
            this.Hits = 0;
            this.Sunk = false;
            this.Orientation = rand.Next(0, 2);
            this.Symbol = "X";
            this.score_ship = length * 3;
        }

        public static List<Ships> GenerateShips()
        {
            List<Ships> shipList = new List<Ships>();
            shipList.Add(new Ships(1));
            shipList.Add(new Ships(2));
            shipList.Add(new Ships(3));
            shipList.Add(new Ships(4));
            return shipList;
        }
        public static List<Ships> GenerateShipsBySize(Coordinate c)
        {
            List<Ships> shipList = GenerateShips();
            int total_size = c.XCoordinate * c.YCoordinate;
            float number_of__size_ships = total_size / 10;
            int n = Convert.ToInt32(number_of__size_ships);
            n = n - 10;
            Random rnd = new Random();
            int rand;
            while (n != 0) 
            { 
                if(n >= 4)
                {
                    rand = rnd.Next(1, 5);
                    shipList.Add(new Ships(rand));
                    n = n - rand;
                }
                else if (n >= 3)
                {
                    rand = rnd.Next(1, 4);
                    shipList.Add(new Ships(rand));
                    n = n - rand;
                }
                else if (n >= 2)
                {
                    rand = rnd.Next(1, 2);
                    shipList.Add(new Ships(rand));
                    n = n - rand;
                }
                else if (n == 1)
                {
                    shipList.Add(new Ships(n));
                    n = n - 1;
                }
            }
            return shipList;
        }

        public static void PlaceShips(List<Ships> ships, Grid shipGrid)
        {
            foreach (Ships ship in ships)
            {
                ship.PlaceShip(shipGrid);
            }
        }
        private static int[] GetShipSeed(string[,] Grid)
        {
            int height = Grid.GetLength(0);
            int width = Grid.GetLength(1);
            int w = rand.Next(1, width);
            int h = rand.Next(1, height);
            int[] seedCoord = { h, w };
            return seedCoord;
        }

        private static void VerticalShip(Ships ship, Grid grid)
        {
            ship.Seed = GetShipSeed(grid.Matrix);

            if (ship.Seed[0] + ship.Length > grid.Matrix.GetLength(0))
            {
                if (!(ship.Seed[0] - ship.Length < 1))
                {
                    VerticalShipUp(ship, grid);
                }
                else
                {
                    throw new ArgumentException(
                       "Bad Seed: " + ship.Seed[0] + "X" + ship.Seed[1]);
                }
            }
            else
            {
                VerticalShipDown(ship, grid);
            }
        }
        private static void VerticalShipUp(Ships ship, Grid grid)
        {
            int[,] coords = new int[ship.Length, 2];
            string[,] position =
                new string[grid.Matrix.GetLength(0), grid.Matrix.GetLength(1)];
            for (int i = ship.Seed[0]; i > ship.Seed[0] - ship.Length; i--)
            {
                position[i, ship.Seed[1]] = ship.Symbol;
            }
            for (int ii = 0; ii < coords.GetLength(0); ii++)
            {
                for (int iii = 0; iii < 2; iii++)
                {
                    if (iii == 0)
                    {
                        coords[ii, iii] = ship.Seed[0] - ii;
                    }
                    else
                    {
                        coords[ii, iii] = ship.Seed[1];
                    }
                }
            }
            ship.Position = position;
            ship.Coordinates = coords;
        }

        private static void VerticalShipDown(Ships ship, Grid grid)
        {
            int[,] coords = new int[ship.Length, 2];
            string[,] position =
                new string[grid.Matrix.GetLength(0), grid.Matrix.GetLength(1)];
            for (int i = ship.Seed[0]; i < ship.Seed[0] + ship.Length; i++)
            {
                position[i, ship.Seed[1]] = ship.Symbol;
            }
            for (int ii = 0; ii < coords.GetLength(0); ii++)
            {
                for (int iii = 0; iii < 2; iii++)
                {
                    if (iii == 0)
                    {
                        coords[ii, iii] = ship.Seed[0] + ii;
                    }
                    else
                    {
                        coords[ii, iii] = ship.Seed[1];
                    }
                }
            }
            ship.Position = position;
            ship.Coordinates = coords;
        }

        private static void HorizontalShip(Ships ship, Grid grid)
        {
            ship.Seed = GetShipSeed(grid.Matrix);
            if (ship.Seed[1] + ship.Length > grid.Matrix.GetLength(1))
            {
                if (!(ship.Seed[1] - ship.Length < 1))
                {
                    HorizontalShipLeft(ship, grid);
                }
                else
                {
                    throw new ArgumentException(
                        "Bad Seed: " + ship.Seed[0] + "x" + ship.Seed[1]);
                }
            }
            else
            {
                HorizontalShipRight(ship, grid);
            }
        }

        private static void HorizontalShipRight(Ships ship, Grid grid)
        {
            string[,] position =
                new string[grid.Matrix.GetLength(0), grid.Matrix.GetLength(1)];
            int[,] coords = new int[ship.Length, 2];
            for (int i = ship.Seed[1]; i < ship.Seed[1] + ship.Length; i++)
            {
                for (int ii = 0; ii < coords.GetLength(0); ii++)
                {
                    position[ship.Seed[0], i] = ship.Symbol;
                }
                for (int ii = 0; ii < coords.GetLength(0); ii++)
                {
                    for (int iii = 0; iii < 2; iii++)
                    {
                        if (iii == 0)
                        {
                            coords[ii, iii] = ship.Seed[0];
                        }
                        else
                        {
                            coords[ii, iii] = ship.Seed[1] + ii;
                        }
                    }
                }
            }
            ship.Position = position;
            ship.Coordinates = coords;
        }

        private static void HorizontalShipLeft(Ships ship, Grid grid)
        {
            string[,] position =
                new string[grid.Matrix.GetLength(0), grid.Matrix.GetLength(1)];
            int[,] coords = new int[ship.Length, 2];
            for (int i = ship.Seed[1]; i > ship.Seed[1] - ship.Length; i--)
            {
                position[ship.Seed[0], i] = ship.Symbol;
            }
            for (int ii = 0; ii < coords.GetLength(0); ii++)
            {
                for (int iii = 0; iii < 2; iii++)
                {
                    if (iii == 0)
                    {
                        coords[ii, iii] = ship.Seed[0];
                    }
                    else
                    {
                        coords[ii, iii] = ship.Seed[1] - ii;
                    }
                }
            }
            ship.Position = position;
            ship.Coordinates = coords;
        }


        public Grid PlaceShip(Grid grid)
        {
            if (this.Length > grid.Matrix.GetLength(0) ||
                this.Length > grid.Matrix.GetLength(1))
            {
                throw new ArgumentException(
                    "The 'Ship' length must be smaller than the 'Grid' dimensions");
            }
            if (this.Orientation == 1)
            {
                Ships.HorizontalShip(this, grid);
            }
            else
            {
                Ships.VerticalShip(this, grid);
            }
            while (!IsShipPositionValid(this, grid))
            {
                Seed = GetShipSeed(grid.Matrix);
                if (Orientation == 1)
                {
                    Ships.HorizontalShip(this, grid);
                }
                else
                {
                    Ships.VerticalShip(this, grid);
                }
                IsShipPositionValid(this, grid);
            }
            for (int i = 0; i < this.Position.GetLength(0); i++)
            {
                for (int ii = 0; ii < this.Position.GetLength(1); ii++)
                {
                    if (this.Position[i, ii] == Symbol)
                    {
                        grid.Matrix[i, ii] = this.Symbol;
                        grid.ColorM[i, ii] = "red";
                    }
                }
            }
            grid.Ships.Add(this);
            return grid;
        }

        private static bool IsShipPositionValid(Ships ship, Grid grid)
        {
            for (int i = 0; i < grid.Matrix.GetLength(0); i++)
            {
                for (int ii = 0; ii < grid.Matrix.GetLength(1); ii++)
                {
                    if (grid.Matrix[i, ii] != grid.Symbol &&
                        ship.Position[i, ii] == ship.Symbol)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
