using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VotingApp
{
    public class Student
    {
        public string Name { get; private set; }
        public string StudentId { get; private set; }
        public string Password { get; private set; }

        public Student(string name, string sID, string pass)
        {
            this.Name = name;
            this.StudentId = sID;
            this.Password = pass;
        }
        public Student(string sID, string pass)
        {
            this.StudentId=sID;
            this.Password = pass;
        }
        public Student(string sID)
        { this.StudentId=sID; }

    }
}
