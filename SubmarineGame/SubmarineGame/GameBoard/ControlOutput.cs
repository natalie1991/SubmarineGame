using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace SubmarineGame.GameBoard
{
    class ControlOutput
    {
        static int counttime = 0;

        public static void ShowFlashScreen(string msg)
        {
            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("***************************************");
            Console.Write("****"); Console.ForegroundColor = ConsoleColor.White; Console.Write(msg); Console.ForegroundColor = ConsoleColor.Green; Console.WriteLine("****");
            Console.WriteLine("***************************************");
            Console.ForegroundColor = ConsoleColor.White;

            Timer t = new Timer(ClearFlashScreen, null, 0, 1000);
            Thread.Sleep(2100); 
            t.Dispose(); 
                    
        }

        private static void ClearFlashScreen(Object state)
        {
            if (counttime < 2)
                counttime += 1;
            else
            {
                Console.Clear();
                counttime = 0;
            }
        }
        public static void ShowShotResult(int board_size_x,int board_size_y, int total_score,
            int totalGus,int misGus, int hit_gusses, List<Ships> shipList)
        {
            int i = 1;
            String str = "";
            Console.ForegroundColor = ConsoleColor.Green;
            str =" Board size : " + board_size_x + "x" + board_size_y +"\n";
            str += "       ******\n";
            str += "Your Score = " + total_score + "\n";
            str += "       ******\n";
            str += "Your total number of guesses : " + totalGus + "\n";
            str += "       ******\n";
            str += "Your hit guesses : " + hit_gusses + "\n";
            str += "       ******\n";
            str += "Your missed guesses : " + misGus + "\n";
            str += "       ******\n";
            str += "SHIP LIST: \n";
            foreach (Ships ship in shipList)
            {
                str += "lenght of ship "+ i +") " + ship.Length +"\n";
                i++;
            }
            str += "************\n";
            str += " Game Over \n";
            str += "************\n";
            Console.WriteLine(str);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("");
        }

    }
        }


