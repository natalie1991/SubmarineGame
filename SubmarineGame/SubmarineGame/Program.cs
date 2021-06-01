using SubmarineGame.GameBoard;
using System;

namespace SubmarineGame
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);

            while (true)
            {
                Game game = Menu.WriteMenu();
                bool flagFirst = true;
                Console.WriteLine("\n\n");

                    while (game.IsGameOver() != true)
                    {
                    if (5 - game.Difficulty > 0)
                    {
                        if (flagFirst == true)
                        {
                            Grid.WriteGrid(game.Board.ShipLayer);
                            System.Threading.Thread.Sleep((5 - game.Difficulty) * 1000);
                            flagFirst = false;
                            Console.Clear();
                            Grid.WriteGrid(game.Board.PlayerLayer);
                            game.Board.Shoot();
                        }
                        else if (flagFirst == false)
                        {
                            Console.Clear();
                            Grid.WriteGrid(game.Board.PlayerLayer);
                            game.Board.Shoot();
                        }

                    }

                    else if (game.Difficulty == 5)
                    {
                        Grid.WriteGrid(game.Board.PlayerLayer);
                        game.Board.Shoot();
                    }

                    }
              
                    if (game.Board.BackToMenu == 0)
                    {
                        game.GameOver();
                        Console.WriteLine("GOODBYE");
                        Menu.exit();
       

                    }
                    Console.Clear();
                if (Console.ReadKey().KeyChar == '0')
                {
                    Menu.qesExit();
                }
            }
            }
        }
    }
