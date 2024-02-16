using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace VotingApp
{
    public class VoterList
    {
        public List<Voter> voters { get; private set; }

        public VoterList()
        {
            voters = new List<Voter>();
        }

        public VoterList(string filePath)
        {
            this.voters = new List<Voter>();
            LoadVoters(filePath);
        }

        public void LoadVoters(string filePath)
        {
            string[] lines = File.ReadAllLines(filePath);
            foreach (string line in lines)
            {
                string[] parts = line.Split(' ');
                if (parts.Length == 4)
                {
                    string name = parts[0] + " " + parts[1];
                    string sID = parts[2];
                    string pass = parts[3];
                    Voter voter = new Voter(name, sID, pass);
                    voters.Add(voter);
                }
            }
        }
        public void showVoters()
        {
            foreach (Voter voter in voters)
            {
                Console.WriteLine(voter);
            }
            Console.WriteLine("Total: " + this.voters.Count());
        }

    }
}
