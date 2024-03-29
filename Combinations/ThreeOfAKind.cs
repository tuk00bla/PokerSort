﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
namespace Task1.Combinations
{
    public class ThreeOfAKind : CombinationClass
    {
        public Rank CardRank { get; }
        public List<Rank> Kickers { get; }

        public ThreeOfAKind(Rank cr, List<Rank> k)
        {
            this.CardRank = cr;
            this.Kickers = k;
            this.Marker = Combination.ThreeOfAKind;
        }

        protected override int CompareWithSame(object obj)
        {
            ThreeOfAKind other = (ThreeOfAKind)obj;
            if (this.CardRank > other.CardRank) return 1;
            if (this.CardRank < other.CardRank) return -1;
            var sortedKickers = this.Kickers.OrderByDescending(i => i).ToList();
            var sortComparedKickers = other.Kickers.OrderByDescending(i => i).ToList();
            for (int i = 0; i < sortedKickers.Count(); i++)
            {
                if (sortedKickers[i] > sortComparedKickers[i])
                {
                    return 1;
                }
                if (sortedKickers[i] < sortComparedKickers[i])
                {
                    return -1;
                }
            }
            return 0;
        }

        public override string ToString()
        {
            return "THREE OF A KIND: RANK -> " + this.CardRank + " KICKER -> " + String.Join(" ", this.Kickers);
        }
        public override int GetHashCode()
        {
            int hash = 0;
            foreach (var kicker in this.Kickers)
            {
                hash += kicker.GetHashCode();
            }
            return this.CardRank.GetHashCode() * 94 + hash * 59;
        }

        public override bool Equals(object obj)
        {
            var other = obj as CombinationClass;
            if (this.Marker != other.Marker) return false;
            var otherEquals = obj as ThreeOfAKind;
            if (this.CardRank != otherEquals.CardRank) return false;
            if (!this.Kickers.SequenceEqual(otherEquals.Kickers)) return false;
            return true;
        }
        public static bool IsThreeOfAKind(IList<Card> combCards, Dictionary<Rank, int> ranks)
        {
            return ranks.ContainsValue(3);
        }

        public static ThreeOfAKind MakeThreeOfAKind(IList<Card> combCards, Dictionary<Rank, int> ranks)
        {
            Rank highCard = Rank.Value2;

            foreach (KeyValuePair<Rank, int> entry in ranks)
            {
                if (entry.Value == 3)
                {
                    highCard = entry.Key;
                }
            }

            List<Rank> sortedRanks = ranks.Keys.ToList();
            sortedRanks.Remove(highCard);
            List<Rank> potentialKickers = new List<Rank>();
            foreach (var item in ranks)
            {
                potentialKickers.Add(item.Key);
            }
            return new ThreeOfAKind(highCard, potentialKickers);

        }
    }
}
