using System;
using System.Collections.Generic;
using System.Linq;
using Task1.Combinations;

namespace Task
{
    class ActionsToSort
    {
        public static void SortCards(string checkedString)
        {
            var tokens = checkedString.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            switch (tokens[0])
            {
                case "texas-holdem":
                    List<Card> board = Card.ParseCards(tokens[1]);
                    var hands = new List<List<Card>>();
                    for (int i = 2; i < tokens.Length; i++)
                    {
                        List<Card> newCards = Card.ParseCards(tokens[i]);
                        hands.Add(newCards);
                    }
                    var pairTxsHands = new List<Hand>();
                    foreach (var hand in hands)
                    {
                        var combination = CombinationClass.FindTexasHandValue(hand, board);
                        var handObj = new Hand
                        {
                            Cards = hand,
                            Combination = combination
                        };
                        pairTxsHands.Add(handObj);
                    }
                    PrintSortedHands(pairTxsHands);
                    break;
                case "omaha-holdem":
                    List<Card> omhBoard = Card.ParseCards(tokens[1]);
                    var omhHands = new List<List<Card>>();
                    for (int i = 2; i < tokens.Length; i++)
                    {
                        List<Card> newCards = Card.ParseCards(tokens[i]);
                        omhHands.Add(newCards);
                    }
                    var pairOmahaHands = new List<Hand>();
                    foreach (var hand in omhHands)
                    {
                        var combination = CombinationClass.FindOmahaHandValue(hand, omhBoard);
                        var handObj = new Hand
                        {
                            Cards = hand,
                            Combination = combination
                        };
                        pairOmahaHands.Add(handObj);
                    }
                    PrintSortedHands(pairOmahaHands);
                    break;
                case "five-card-draw":
                    var fiveCardsList = new List<List<Card>>();
                    for (int i = 1; i < tokens.Length; i++)
                    {
                        List<Card> newCards = Card.ParseCards(tokens[i]);
                        fiveCardsList.Add(newCards);
                    }
                    var pairFcdHands = new List<Hand>();
                    foreach (var hand in fiveCardsList)
                    {
                        var combination = CombinationClass.FindFiveCardsHandValue(hand);
                        var handObj = new Hand
                        {
                            Cards = hand,
                            Combination = combination
                        };
                        pairFcdHands.Add(handObj);
                    }
                    PrintSortedHands(pairFcdHands);
                    break;
                default:
                    break;
            }
        }

        private static void PrintSortedHands(List<Hand> hand)
        {
            var query = hand.GroupBy(combination => combination.Combination).OrderBy(z => z.Key).ToList();

            string strHand = string.Empty;
            foreach (var handDict in query)
            {
                List<string> handsList = handDict.ToList().Select(x => x.ToString()).ToList();
                handsList.Sort();
                Console.Write(String.Join("=", handsList));
                Console.Write(" ");
            }

            Console.WriteLine();
        }  
    }
}
