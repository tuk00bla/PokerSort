using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Task1.Combinations
{
    public class Pair : CombinationClass
    {
        public Rank PairRank { get; }
        public List<Rank> Kickers { get; }
        public Pair(Rank pr, List<Rank> k)
        {
            this.PairRank = pr;
            this.Kickers = k;
            this.Marker = Combination.Pair;
        }
        protected override int CompareWithSame(object obj)
        {
            Pair other = (Pair)obj;
            if (this.PairRank > other.PairRank) return 1;
            if (this.PairRank < other.PairRank) return -1;
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
            return "PAIR: PAIR RANK -> " + this.PairRank + " KICKER -> " + String.Join(" ", this.Kickers);
        }
        public override int GetHashCode()
        {
            int hash = 0;
            foreach (var kicker in this.Kickers)
            {
                hash += kicker.GetHashCode();
            }
            return this.PairRank.GetHashCode() * 22 + hash * 33;
        }

        public override bool Equals(object obj)
        {
            var other = obj as CombinationClass;
            if (this.Marker != other.Marker) return false;
            var otherEquals = obj as Pair;
            if (this.PairRank != otherEquals.PairRank) return false;
            if (!this.Kickers.SequenceEqual(otherEquals.Kickers)) return false;
            return true;
        }

       public static bool IsPair(IList<Card> combCards, Dictionary<Rank, int> ranks)
        {
            return ranks.ContainsValue(2);
        }

        public static Pair MakePair(IList<Card> combCards, Dictionary<Rank, int> ranks)
        {
            Rank highCard = Rank.Value2;
            List<Rank> potentialKickers = ranks.Keys.ToList();
            foreach (KeyValuePair<Rank, int> entry in ranks)
            {
                if (entry.Value == 2)
                { highCard = entry.Key; }
            }
            potentialKickers.Remove(highCard);
            potentialKickers.Sort();
            return new Pair(highCard, potentialKickers);

        }
    }
}
