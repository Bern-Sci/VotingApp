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
                if (parts.Length == 5)
                {
                    string name = parts[0] + " " + parts[1];
                    string sID = parts[2];
                    string pass = parts[3];
                    bool canVote = bool.Parse(parts[4]);
                    Voter voter = new Voter(name, sID, pass, canVote);
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

        public void MarkVoterAsVoted(string studentId)
        {
            Voter voter = voters.Find(v => v.StudentId == studentId);
            if (voter != null)
            {
                voter.canVote = false;
                SaveChangesToFile(); // Save the changes to the file
            }
            else
            {
                Console.WriteLine($"Voter with ID {studentId} not found.");
            }
        }

        private void SaveChangesToFile()
        {
            string filePath = "VoterList.txt";
            File.WriteAllLines(filePath, voters.Select(v => $"{v.Name} {v.StudentId} {v.Password} {v.canVote}"));
        }


    }
}
