using System;
using System.Collections.Generic;
using Combinatorics.Collections;
using System.Linq;

namespace Task1.Combinations
{
    public enum Combination
    {
        HighCard,
        Pair,
        TwoPair,
        ThreeOfAKind,
        Straight,
        Flush,
        FullHouse,
        FourOfAKind,
        StraightFLush
    }

    public abstract class CombinationClass : IComparable
    {
        public Combination Marker { get; set; }
        protected abstract int CompareWithSame(object obj);

        public virtual int CompareTo(object obj)
        {
            CombinationClass other = (CombinationClass)obj;
            if (this.Marker > other.Marker) return 1;
            if (this.Marker < other.Marker) return -1;
            return this.CompareWithSame(other);
        }

        public static CombinationClass FindTexasHandValue(List<Card> hand, List<Card> board)
        {
            List<Card> availableCards = new List<Card>();
            availableCards.AddRange(hand);
            availableCards.AddRange(board);

            Combinations<Card> variants = new Combinations<Card>(availableCards, 5);

            List<CombinationClass> combinations = new List<CombinationClass>();
            foreach (IList<Card> variant in variants)
            {

                Dictionary<Rank, int> rankGroups = Card.GroupRanks(variant);
                Dictionary<Suit, int> suitGroups = Card.GroupSuits(variant);
                CombinationClass comb = FindCombination(variant, rankGroups, suitGroups);
                combinations.Add(FindCombination(variant, rankGroups, suitGroups));
            }
            combinations.Sort();
            var result = combinations.Last();
            return result;
        }

        public static CombinationClass FindOmahaHandValue(List<Card> hand, List<Card> board)
        {
            List<Card> availableCards = new List<Card>();
            var variantsOfHand = new Combinations<Card>(hand, 2);
            var variantsOfBoard = new Combinations<Card>(board, 3);

            List<CombinationClass> combinations = new List<CombinationClass>();

            foreach (IList<Card> variantOfTwo in variantsOfHand)
            {
                foreach (IList<Card> variantOfThree in variantsOfBoard)
                {
                    var variantThreeList = variantOfThree.ToList();
                    var variantTwoList = variantOfTwo.ToList();
                    variantThreeList.AddRange(variantTwoList);
                    Dictionary<Rank, int> rankGroups = Card.GroupRanks(variantThreeList);
                    Dictionary<Suit, int> suitGroups = Card.GroupSuits(variantThreeList);
                    combinations.Add(FindCombination(variantThreeList, rankGroups, suitGroups));
                }
            }
            combinations.Sort();
            return combinations.Last();
        }

        public static CombinationClass FindFiveCardsHandValue(List<Card> hand)
        {
            List<Card> availableCards = new List<Card>();
            availableCards.AddRange(hand);
            Combinations<Card> variants = new Combinations<Card>(availableCards, 5);

            List<CombinationClass> combinations = new List<CombinationClass>();
            foreach (IList<Card> variant in variants)
            {

                Dictionary<Rank, int> rankGroups = Card.GroupRanks(variant);
                Dictionary<Suit, int> suitGroups = Card.GroupSuits(variant);
                CombinationClass comb = FindCombination(variant, rankGroups, suitGroups);
                combinations.Add(FindCombination(variant, rankGroups, suitGroups));
            }
            combinations.Sort();
            return combinations.Last();
        }

        public static CombinationClass FindCombination(IList<Card> cards, Dictionary<Rank, int> ranks, Dictionary<Suit, int> suits)
        {
            if (StraightFlush.IsStraightFlush(cards, ranks, suits)) { return StraightFlush.MakeStraightFlush(cards, ranks); }
            else if (FourOfAKind.IsFourOfAKind(cards, ranks)) { return FourOfAKind.MakeFourOfAKind(cards, ranks); }
            else if (FullHouse.IsFullHouse(cards, ranks, suits)) { return FullHouse.MakeFullHouse(cards, ranks); }
            else if (Flush.IsFlush(cards, suits)) { return Flush.MakeFlush(cards, ranks); }
            else if (Straight.IsStraight(cards, ranks)) { return Straight.MakeStraight(cards, ranks); }
            else if (ThreeOfAKind.IsThreeOfAKind(cards, ranks)) { return ThreeOfAKind.MakeThreeOfAKind(cards, ranks); }
            else if (TwoPairs.IsTwoPairs(cards, ranks)) { return TwoPairs.MakeTwoPairs(cards, ranks); }
            else if (Pair.IsPair(cards, ranks)) { return Pair.MakePair(cards, ranks); }
            else return HighCard.MakeHighCard(cards, ranks);
        }
    }
}