using System;
using System.Collections.Generic;
using static BlackJack.CardsEnums;

namespace BlackJack
{
    public class BlackJackClass
    {
        static int chips;
        static Deck deck;
        static List<Card> userHand;
        static List<Card> dealerHand;

        public static void StartBlackJackGame()
        {
            Console.WriteLine("Welcome to our BlackJack board. You have 100 chips at start \n");

            chips = 100;
            deck = new Deck();
            deck.Shuffle();

            while (chips > 0)
            {
                DealHand();
                Console.WriteLine("\nPress any key for the next hand...\n");
                ConsoleKeyInfo userInput = Console.ReadKey(true);
            }

            Console.WriteLine("You Lost! see you next time...");
            Console.ReadLine();
        }

        static void DealHand()
        {
            if (deck.GetAmountOfRemainingCrads() < 20)
            {
                deck.Initialize();
                deck.Shuffle();
            }

            Console.WriteLine($"Remaining Cards: {deck.GetAmountOfRemainingCrads()}");
            Console.WriteLine($"Current Chips: {chips}");
            Console.WriteLine($"How much would you like to bet? (1 - {chips})");
            string input = Console.ReadLine().Trim().Replace(" ", "");
            int betAmount;
            while (!Int32.TryParse(input, out betAmount) || betAmount < 1 || betAmount > chips)
            {
                Console.WriteLine($"Amount is illegal. How much would you like to bet? (1 - {chips})");
                input = Console.ReadLine().Trim().Replace(" ", "");
            }
            Console.WriteLine();

            userHand = new List<Card>();
            userHand.Add(deck.DrawACard());
            userHand.Add(deck.DrawACard());

            foreach (Card card in userHand)
            {
                if (card.Face == Face.Ace)
                {
                    card.Value += 10;
                    break;
                }
            }

            Console.WriteLine("[Player]");
            Console.WriteLine($"Card 1: {userHand[0].Face} of {userHand[0].Suit}");
            Console.WriteLine($"Card 2: {userHand[1].Face} of {userHand[1].Suit}");
            Console.WriteLine($"Total: {userHand[0].Value + userHand[1].Value}\n");

            dealerHand = new List<Card>();
            dealerHand.Add(deck.DrawACard());
            dealerHand.Add(deck.DrawACard());

            foreach (Card card in dealerHand)
            {
                if (card.Face == Face.Ace)
                {
                    card.Value += 10;
                    break;
                }
            }

            Console.WriteLine("[Dealer]");
            Console.WriteLine($"Card 1: {dealerHand[0].Face} of {dealerHand[1].Suit}");
            Console.WriteLine("Card 2: [Hole Card]");
            Console.WriteLine($"Total: {dealerHand[0].Value}\n");

            bool insurance = false; ;

            if (dealerHand[0].Face == Face.Ace)
            {
                Console.WriteLine("Insurance? (y / n)");
                string userInput = Console.ReadLine();

                while (userInput != "y" && userInput != "n")
                {
                    Console.WriteLine("Could not understand. Insurance? (y / n)");
                    userInput = Console.ReadLine();
                }

                if (userInput == "y")
                {
                    insurance = true;
                    chips -= betAmount / 2;
                    Console.WriteLine("\n[Insurance Accepted!]\n");
                }
                else
                {
                    insurance = false;
                    Console.WriteLine("\n[Insurance Rejected]\n");
                }
            }

            if (dealerHand[0].Face == Face.Ace || dealerHand[0].Value == 10)
            {
                Console.WriteLine("Dealer checks if he has blackjack...\n");
                if (dealerHand[0].Value + dealerHand[1].Value == 21)
                {
                    Console.WriteLine("[Dealer]");
                    Console.WriteLine($"Card 1: {dealerHand[0].Face} of {dealerHand[1].Suit}");
                    Console.WriteLine($"Card 2: {dealerHand[1].Face} of {dealerHand[1].Suit}");
                    Console.WriteLine($"Total: {dealerHand[0].Value + dealerHand[1].Value}\n");

                    int amountLost = 0;

                    if (userHand[0].Value + userHand[1].Value == 21 && insurance)
                    {
                        amountLost = betAmount / 2;
                        chips -= betAmount / 2;
                    }
                    else if (userHand[0].Value + userHand[1].Value != 21 && !insurance)
                    {
                        amountLost = betAmount + betAmount / 2;
                        chips -= betAmount + betAmount / 2;
                    }

                    Console.WriteLine($"You lost {amountLost} chips");
                    return;
                }
                else
                {
                    Console.WriteLine("Dealer does not have a blackjack, moving on...\n");
                }
            }

            if (userHand[0].Value + userHand[1].Value == 21)
            {
                Console.WriteLine($"Blackjack, You Won! ({betAmount + betAmount / 2} chips)\n");
                chips += betAmount + betAmount / 2;
                return;
            }

            do
            {
                Console.WriteLine("Please choose a valid option: [(S)tand (H)it]");
                ConsoleKeyInfo userOption = Console.ReadKey(true);
                while (userOption.Key != ConsoleKey.H && userOption.Key != ConsoleKey.S)
                {
                    Console.WriteLine("illegal key. Please choose a valid option: [(S)tand (H)it]");
                    userOption = Console.ReadKey(true);
                }
                Console.WriteLine();

                switch (userOption.Key)
                {
                    case ConsoleKey.H:
                        userHand.Add(deck.DrawACard());
                        Console.WriteLine($"Hitted {userHand[userHand.Count - 1].Face} of {userHand[userHand.Count - 1].Suit}");
                        int totalCardsValue = 0;
                        foreach (Card card in userHand)
                        {
                            totalCardsValue += card.Value;
                        }
                        Console.WriteLine($"Total cards value now: {totalCardsValue}\n");
                        if (totalCardsValue > 21)
                        {
                            Console.Write("Busted!\n");
                            chips -= betAmount;
                            return;
                        }
                        else if (totalCardsValue == 21)
                        {
                            Console.WriteLine("Good job! I assume you want to stand from now on...\n");
                            continue;
                        }
                        else
                        {
                            continue;
                        }

                    case ConsoleKey.S:

                        Console.WriteLine("[Dealer]");
                        Console.WriteLine($"Card 1: {dealerHand[0].Face} of {dealerHand[1].Suit}");
                        Console.WriteLine($"Card 2: {dealerHand[1].Face} of {dealerHand[1].Suit}");

                        int dealerCardsValue = 0;
                        foreach (Card card in dealerHand)
                        {
                            dealerCardsValue += card.Value;
                        }

                        while (dealerCardsValue < 17)
                        {
                            dealerHand.Add(deck.DrawACard());
                            dealerCardsValue = 0;
                            foreach (Card card in dealerHand)
                            {
                                dealerCardsValue += card.Value;
                            }
                            Console.WriteLine($"Card {dealerHand.Count}: {dealerHand[dealerHand.Count - 1].Face} of {dealerHand[dealerHand.Count - 1].Suit}");
                        }
                        dealerCardsValue = 0;
                        foreach (Card card in dealerHand)
                        {
                            dealerCardsValue += card.Value;
                        }
                        Console.WriteLine($"Total: {dealerCardsValue}\n");

                        if (dealerCardsValue > 21)
                        {
                            Console.WriteLine($"Dealer bust! You win! ({betAmount} chips)");
                            chips += betAmount;
                            return;
                        }
                        else
                        {
                            int playerCardValue = 0;
                            foreach (Card card in userHand)
                            {
                                playerCardValue += card.Value;
                            }

                            if (dealerCardsValue > playerCardValue)
                            {
                                Console.WriteLine($"Dealer has {dealerCardsValue} and player has {playerCardValue}, dealer wins!");
                                chips -= betAmount;
                                return;
                            }
                            else
                            {
                                Console.WriteLine($"Player has {playerCardValue} and dealer has {dealerCardsValue}, player wins!");
                                chips += betAmount;
                                return;
                            }
                        }
                    default:
                        break;
                }
                Console.ReadLine();
            }
            while (true);
        }
    }

}

