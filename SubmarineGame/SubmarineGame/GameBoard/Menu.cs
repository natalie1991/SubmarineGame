using System;
using System.Collections.Generic;
using System.Text;

namespace SubmarineGame.GameBoard
{
    public class Menu
    {
        public static Game WriteMenu()
        {
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.White;
            Console.Clear();
            WriteTitle();
            LoopOptions();
            Coordinate boardSize = BoardSize();
            int level = Level();
            Game game = new Game(level,boardSize);
            return game;
        }

        private static Coordinate BoardSize()
        {
            Console.Clear();
            string x;
            string y;
            Coordinate c1 = new Coordinate(10, 10);

                Console.WriteLine("enter your board size x and y:defualt size is 10*10");
                do
                {
                    Console.WriteLine("enter your len of height board: value between 10-50");
                    x = Console.ReadLine();
                } while (Convert.ToInt32(x) < 10 || Convert.ToInt32(x) > 50);
                do
                {
                    Console.WriteLine("enter your len of width board: value between 10-50");
                    y = Console.ReadLine(); 
                }while (Convert.ToInt32(y) < 10 || Convert.ToInt32(y) > 50);
            int x_x = Convert.ToInt32(x);
            int y_y = Convert.ToInt32(y);
            c1.XCoordinate = x_x;
            c1.YCoordinate = y_y;
            return c1;
        }
               
        private static int Level()
        {
            Console.Clear();
            bool proceed = false;
            while (proceed == false)
            {
                Console.WriteLine("Select your level:");
                Console.WriteLine("1 = level 1");
                Console.WriteLine("2 = level 2");
                Console.WriteLine("3 = level 3");
                Console.WriteLine("4 = level 4");
                Console.WriteLine("5 = level 5");
                Console.WriteLine("you can exit any time by press q");
                string input = Console.ReadLine();
                switch (input.ToLower())
                {
                    case "1":
               
                        return 1;
                    case "2":
               
                        return 2;
                    case "3":
                     
                        return 3;
                    case "4":
                      
                        return 4;
                    case "5":
                    
                        return 5;
               
                    case "0":

                        qesExit();
                        Level();
                        break;

                    case "menu":
                        Console.Clear();
                        WriteTitle();
                        LoopOptions();
                        Console.Clear();
                        break;
                    default:
                        Console.WriteLine("Let's try that again shall we..\n");
   
                        break;
                }
            }
            return 0;
        }

        private static void LoopOptions()
        {
            bool notContinue = true;
            while (notContinue)
            {
                Console.WriteLine("1 - New Game (or press Enter)");
                Console.WriteLine("2 - Exit");
                string input = Console.ReadLine();

                switch (input.ToLower())
                {
                    case "":
                        notContinue = false;
                        break;
                    case "1":
                        Console.Clear();
                        notContinue = false;
                        break;
                    case "2":
                        qesExit();
                        break;

                    default:
                        Console.WriteLine("Please enter in 1 for a new game, or 2 to exit.");
                       // Console.WriteLine("You can also type exit\n");
                        break;
                }
            }
        }

        public static void qesExit()
        {
            Console.WriteLine("you sure you want to exit from the game ? :Y/N");
            string strAns = Console.ReadLine();
            strAns = strAns.Trim().ToLower();
            if (strAns == "y")
            {
                exit();
            }
        }
        public static void exit()
        {
            Environment.Exit(0);
        }

        private static void WriteTitle()
        {
           
            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("             ************************************");
  
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("                     THE SUBMARINE GAME!!!     "); 
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("             ************************************");
            Console.WriteLine("");


            string ship = @"                                        ___
                                       |___|
                                  ______|_|
                           _   __|_________|  _
            _        =====| | |            | | |==== _
      =====| |        .---------------------------. | |====
<-----------------'   .  .  .  .  .  .  .  .   '------------/
  \                                                        /
   \                                                      /
    \____________________________________________________/
";

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(ship);
            Console.ForegroundColor = ConsoleColor.Black;
        }
    }

}
