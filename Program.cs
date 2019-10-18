using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BlackJack;
using TolchokInfo;

namespace Everything
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to our Luna park!\n\nHere you can find some Blackjack, Tolchki and Hookers\n(to be honest, sometimes it is hard to find the difference between last ones).");
            Console.WriteLine("\nPlease chose Where to go: (B)lackjack board, (T)olchok Info Panel or try to find some (H)ookers. \n\nAlso you may press anything else to go back to your boring home.");
            var userInput = Console.ReadKey();
            switch (userInput.Key)
            {
                case ConsoleKey.B:
                    Console.WriteLine("\n");
                    BlackJackClass.StartBlackJackGame();
                    break;
                case ConsoleKey.T:
                    Console.WriteLine("\n");
                    //place for tolchok info (use tolchok input method)
                    break;
                case ConsoleKey.H:
                    Console.WriteLine("\nThis is Luna park in Belarus. We honor the Criminal code.\nLook at our tolchki instead.\nBTW, as it's been said above, you may see no difference.");
                    //place for hookers (but use tolchok input method)
                    break;
                default:
                    Console.WriteLine("\nGoodbye! We'll be glad to see you again!");
                    break;
            }
            Console.ReadKey();
        }

    }
}