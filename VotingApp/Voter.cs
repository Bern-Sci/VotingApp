﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VotingApp
{
    public class Voter : Student
    {
        //naa dri dapat ang iyang mga na vote
        public bool canVote = true;
        public Voter(string name, string sID, string pass) : base(name, sID, pass) { }
        public Voter(string name, string sID, string pass, bool canVote) : base(name, sID, pass) 
        {
            this.canVote = canVote;
        }
        public Voter(string sID, string pass) : base(sID, pass) { }
        public Voter(string sID) : base(sID) { }

        public override string ToString()
        {
            return "\nStudentID: " + this.StudentId;
        }
    }
    
}
