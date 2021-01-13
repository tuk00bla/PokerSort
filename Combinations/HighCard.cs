using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Task1.Combinations
{
    public class HighCard : CombinationClass
    {
        public List<Rank> Kickers { get; }
        public HighCard(List<Rank> r)
        {
            this.Kickers = r;
            this.Marker = Combination.HighCard;
        }

        protected override int CompareWithSame(object obj)
        {
            HighCard compObj = (HighCard)obj;
            var sortedKickers = this.Kickers.OrderByDescending(i => i).ToList();
            var sortComparedKickers = compObj.Kickers.OrderByDescending(i => i).ToList();
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
            return "HIGH CARD -> " + String.Join(" ", this.Kickers);
        }
        public override int GetHashCode()
        {
            int hash = 0;
            foreach (var kicker in this.Kickers)
            {
                hash += kicker.GetHashCode();
            }
            return hash * 91;
        }

        public override bool Equals(object obj)
        {
            var other = obj as CombinationClass;
            if (this.Marker != other.Marker) return false;
            var otherEquals = obj as HighCard;
            if (!this.Kickers.SequenceEqual(otherEquals.Kickers)) return false;
            return true;
        }

        public static HighCard MakeHighCard(IList<Card> combCards, Dictionary<Rank, int> ranks)
        {
            List<Rank> potentialKickers = new List<Rank>();
            foreach (var item in ranks)
            {
                potentialKickers.Add(item.Key);
            }
            
            return new HighCard(potentialKickers);
        }
    }
}
