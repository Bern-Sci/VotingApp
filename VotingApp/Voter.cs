using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VotingApp
{
    public class Voter
    {
        public long Code;
        public YearLevel YearLevel;
        public bool canVote = true;

        public Voter() { }
        public Voter(YearLevel yr) { this.YearLevel = yr; }
        public Voter(YearLevel yr, bool canVote, long Code) { this.YearLevel = yr; this.canVote = canVote; this.Code = Code; }
    }
}