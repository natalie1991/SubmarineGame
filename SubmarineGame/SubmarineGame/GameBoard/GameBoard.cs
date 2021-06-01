using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace SubmarineGame.GameBoard
{
    public class Gameboard
    {
        public Grid ShipLayer { get; set; }
        public Grid PlayerLayer { get; set; }
        private ConsoleColor Background { get; set; }
        private ConsoleColor Foreground { get; set; }

        public int Score { get; set; }
        public int RocketGus { get; set; }
        public int RocketUnsGus { get; set; }
        public int BackToMenu { get; set; }

        public Gameboard(Grid shipGrid, Grid playerGrid)
        {
            ShipLayer = shipGrid;
            PlayerLayer = playerGrid;
            Score = 0;
            RocketGus = 0;
            RocketUnsGus = 0;
            BackToMenu = 0;
            Foreground = ConsoleColor.White;
            Background = ConsoleColor.Black;
            Console.ForegroundColor = Foreground;
            Console.BackgroundColor = Background;
        }
        public Gameboard()
        {
            Grid playerGrid = new Grid();
            PlayerLayer = playerGrid;
            RocketGus = 0;
            Score = 0;
            RocketUnsGus = 0;
            BackToMenu = 0;
            Grid shipGrid = new Grid(10, 10, " ");
            List<Ships> shipList = Ships.GenerateShips();
            Ships.PlaceShips(shipList, shipGrid);
            ShipLayer = shipGrid;
            Foreground = ConsoleColor.Gray;
            Background = ConsoleColor.Black;
            Console.ForegroundColor = Foreground;
            Console.BackgroundColor = Background;
        }

        public Gameboard(Coordinate c)
        {
            Grid playerGrid = new Grid(c.XCoordinate,c.YCoordinate);
            PlayerLayer = playerGrid;
            RocketGus = 0;
            Score = 0;
            RocketUnsGus = 0;
            BackToMenu = 0;
            Grid shipGrid = new Grid(c.XCoordinate, c.YCoordinate," ");
            List<Ships> shipList = Ships.GenerateShipsBySize(c);
            Ships.PlaceShips(shipList, shipGrid);
            ShipLayer = shipGrid;
            Foreground = ConsoleColor.Gray;
            Background = ConsoleColor.Black;
            Console.ForegroundColor = Foreground;
            Console.BackgroundColor = Background;
        }
        public void Shoot()
        {
                string coordinates = "";
                Console.ForegroundColor = ConsoleColor.White;
                Console.ForegroundColor = Foreground;
                Console.WriteLine("Enter in the coordinates for your shot letter and then digit:(for exit the game press 0)");
                coordinates = Console.ReadLine();
            while(Char.IsDigit(coordinates.ToLower()[0])) 
            {
                if(coordinates.ToLower()[0] == '0')
                {
                    Menu.qesExit();
                    BackToMenu = 0;
                    Console.WriteLine("Enter in the coordinates for your shot:(for exit the game press 0)");
                    coordinates = Console.ReadLine();
                }
                else
                {
                    Console.WriteLine("is not a valid location...... Try again\n");
                    Console.WriteLine("Enter again in the coordinates for your shot letter and then digit:(for exit the game press 0)");
                    coordinates = Console.ReadLine();
                }
     
            }
                bool flagC = false;

            
            while ((isShotValid(coordinates.ToLower()) != true && BackToMenu == 0)
                    || (isShotUsedBefore(coordinates.ToLower()) == true && BackToMenu == 0))
                {
                if (coordinates.ToLower() == "0")
                {
                    flagC = true;
                }
                while (flagC == true)
                {
                    if (coordinates.ToLower() == "0")
                    {
                        Menu.qesExit();
                        flagC = true;
                        Console.WriteLine("Enter again the coordinates for your shot:(for exit the game press q)");
                        coordinates = Console.ReadLine();
                    }
                    else
                    {
                        flagC = false;
                        break;
                    }
                }
                    Console.Clear();
                    Console.WriteLine("\n\n");
                    Grid.WriteGrid(PlayerLayer);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.ForegroundColor = Foreground;
                    if (isShotUsedBefore(coordinates))
                    {
                        Console.WriteLine(String.Format("{0}: {1}",
                            coordinates,
                            "You already shot there... Try again\n"));
                    }
                   
                    else
                    {
                        Console.WriteLine(String.Format("'{0}' {1}",
                            coordinates,
                            "is not a valid location...... Try again\n"));
                    }
                    coordinates = Console.ReadLine();

            }
            if (BackToMenu == 0)
                {
                    Console.Clear();
                    RocketGus++;
                    WriteShot(coordinates);
                }

        }
        private void WriteShot(string shot)
        {
            int[] arr = new int[2];
            arr = culColAndRow(shot);
            int row = arr[0];
            int col = arr[1];
            bool hit = isShotHit(shot);
            if (hit)
            {
                this.PlayerLayer.Matrix[row, col] = "X";
                this.PlayerLayer.ColorM[row, col] = "red";
               
                Ships hitShip = HitShip(shot); 
                if(IsShipSunk(hitShip))
                {                  
                    for(int i = 0; i<hitShip.Length;i++)
                    {
                        if (hitShip.Length == 1)
                        {
                            Score = hitShip.score_ship + Score;
                            this.PlayerLayer.ColorM[row, col] = "green";
                        }

                        else if (hitShip.Coordinates[0, 0] == hitShip.Coordinates[1, 0])
                        {
                            Score = hitShip.score_ship + Score;
                            this.PlayerLayer.ColorM[hitShip.Coordinates[0, 0], hitShip.Coordinates[i, 1]] = "green";
                        }
                           
                        else
                        {
                            Score = hitShip.score_ship + Score;
                            this.PlayerLayer.ColorM[hitShip.Coordinates[i, 0], hitShip.Coordinates[0, 1]] = "green";
                        }
                            
                    }
     
                }
            }
            else
            {
                this.PlayerLayer.ColorM[row,col] = "white";
                this.PlayerLayer.Matrix[row, col] = "X";
            }
        }
        private Ships HitShip(string shot)
        {
            int[] arr = new int[2];
            arr = culColAndRow(shot);
            int row = arr[0];
            int col = arr[1];
           
            Ships hitShip;
            foreach (Ships ship in ShipLayer.Ships)
            {
                hitShip = ship;
                bool rowMatch = false;
                for (int i = 0; i < ship.Length; i++)
                {
                    for (int ii = 0; ii < 2; ii++)
                    {
                        if (ii == 0 && ship.Coordinates[i, ii] == row)
                        {
                            rowMatch = true;
                        }
                        if (ii == 1 &&
                            rowMatch &&
                            ship.Coordinates[i, ii] == col)
                        {
                            hitShip.Hits++;
                            IsShipSunk(hitShip);
                            return hitShip;
                        }
                    }
                }
            }
            throw new ArgumentException("No hit ship");
        }
        private bool IsShipSunk(Ships hitShip)
        {
            string msg = string.Empty;
            if (hitShip.Hits >= hitShip.Length)
            {
                hitShip.Sunk = true;
                Console.ForegroundColor = ConsoleColor.Green;
                if(Console.CursorTop == 0)
                    Console.SetCursorPosition(0, Console.CursorTop);
                else        
                    Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop - 1);
                msg = "You sunk the ship !";
                ControlOutput.ShowFlashScreen(msg);
                Console.ForegroundColor = Foreground;
                return true;
            }
            return false;
        }
        private bool isShotHit(string shot)
        {
            int[] arr = new int[2];
            arr = culColAndRow(shot);
            int row = arr[0];
            int col = arr[1];

            string msg = string.Empty;
            if (ShipLayer.Matrix[row,col]!= ShipLayer.Symbol)
            {
                msg = " Shot location:" + shot + " HIT !";
                ControlOutput.ShowFlashScreen(msg);
                return true;
            }
            msg = "Shot location: " + shot + " MISSED !";
            ControlOutput.ShowFlashScreen(msg);
            RocketUnsGus++;
            return false;
        }
        private bool isShotUsedBefore(string shot)
        {
            int[] arr = new int[2];
      
            if (isShotValid(shot) != true)
            {
                return false;
            }

            arr = culColAndRow(shot);
            int row = arr[0];
            int col = arr[1];
            
            if (PlayerLayer.Matrix[row, col] == "X" )  
            {
                return true;
            }
            return false;
        }

        private bool isShotValid(string shot)
        {
            int[] arr = new int[2];
            if (isInputValid(shot) != true)
            {
                return false;
            }
            arr = culColAndRow(shot);
            int row = arr[0];
            int col = arr[1];
           
            if (row > 0 &&
                col > 0 &&
                row < PlayerLayer.Matrix.GetLength(0) &&
                col < PlayerLayer.Matrix.GetLength(1))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private char[] splitStr(string shot)
        {
            shot = shot.Replace(" ", "");
            char[] ch_split = shot.ToCharArray();
            return ch_split;
        }
        private string intStrSplit(char[] cArr)
        {
            string str = string.Empty;
            for(int i=0; i< cArr.Length-1; i++)
            {
                if (Char.IsDigit(cArr[i]))
                    str = string.Concat(cArr[i],cArr[i+1]);
            }
            return str;
        }
        private int[] culColAndRow(string shot)
        {
            int col = 0;
            int row = 0;
            int[] arr = new int[2];

            if (shot.Length == 3)
            {
                if (Char.IsLetter(shot[1]))
                {
                    row = GetRowNumber(shot[0]) + 26;
                    col = GetColumnNumber(Convert.ToString(shot[2]));
                }
                else if (Char.IsDigit(shot[1]))
                {
                    row = GetRowNumber(shot[0]);
                    col = GetColumnNumber(Convert.ToString(string.Concat(shot[1], shot[2])));
                }

            }
            else if (shot.Length == 4)
            {
                row = GetRowNumber(shot[0]) + 26;
                col = GetColumnNumber(Convert.ToString(string.Concat(shot[2], shot[3])));
            }

            else
            {
                row = GetRowNumber(shot[0]);
                col = GetColumnNumber(Convert.ToString(shot[1]));
            }
            arr[0] = row;
            arr[1] = col;

            return arr;
        }
        private bool isInputValid(string shot)
        {
            char [] ch_split = splitStr(shot);
            int l2;
            string l1;
            char lc;
            if (shot == "" || shot == " " || shot == null)
            {
                return false;
            }
            if (shot.ToLower() == "exit")
            {
                Menu.exit();
            }

            if(shot.Length == 4)
            {
                l2 = GetColumnNumber(intStrSplit(ch_split));
                l1 = string.Concat(shot[0],shot[1]);

            }

            else if (shot.Length == 3 && Char.IsDigit(ch_split[1]))
            {
                l2 = GetColumnNumber(intStrSplit(ch_split));
                l1 = string.Concat(shot[0]);

            }

            else if (shot.Length == 3 && Char.IsLetter(ch_split[1]))
            {
                l2 = GetColumnNumber(Convert.ToString(shot[2]));
                l1 = string.Concat(shot[0], shot[1]);

            }
            else if(shot.Length == 2 && Char.IsLetter(shot[0]))
            {
                lc = shot[0];
                l2 = GetColumnNumber(Convert.ToString(shot[1]));
            }
            else
            {
                return false;
            }

            
            string pattern = @"^[A-za-z]+\d+";
            Regex reg = new Regex(pattern);
            Match match = reg.Match(shot);
            if (match.Value != shot)
            {
                return false;
            }
            if (l2 - 1 < PlayerLayer.coorGrid.YCoordinate)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private int GetColumnNumber(string column)
        {
            return int.Parse(column) + 1;
        }
        private static int GetRowNumber(char letter)
        {
            int index = char.ToUpper(letter) - 64;
            return index;
        }
    }
}

