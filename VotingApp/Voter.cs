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

        public Voter(string name, string sID, string pass) : base(name, sID, pass) { }
        public override string ToString()
        {
            return "Name(Voter): " + this.Name + "\nStudentID: " + this.StudentId;
        }
    }
    
}
