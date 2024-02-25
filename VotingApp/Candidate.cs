using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VotingApp;

namespace VotingApp
{
    public class Candidate
    {
        public string Name;
        public Position pos;

        public Candidate(string name, Position pos)
        {
            Name = name;
            this.pos = pos;
        }
    }
}