using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace VotingApp
{
    public class CandidateList
    {
        public List<Candidate> candidates { get; private set; }

        public CandidateList()
        {
            candidates = new List<Candidate>();
        }
        public CandidateList(string filePath)
        {
            this.candidates = new List<Candidate>();
            LoadCandidates(filePath);
        }
        public void LoadCandidates(string filePath)
        {
            string[] lines = File.ReadAllLines(filePath);
            foreach (string line in lines)
            {
                string[] parts = line.Split(' ');
                if (parts.Length == 5)
                {
                    string name = parts[0] + " " + parts[1];
                    string sID = parts[2];
                    Position pos;
                    if (Enum.TryParse(parts[3], true, out pos))
                    {
                        string pass = parts[4];
                        Candidate candidate = new Candidate(name, sID, pass, pos);
                        candidates.Add(candidate);
                    }
                }
            }
        }

        public void showCandidates()
        {
            foreach (Candidate c in this.candidates)
                Console.WriteLine(c);
        }

        public List<Candidate> getCandidatesInPos(Position pos)
        {
            List<Candidate> candidatesInPos = new List<Candidate>();
            foreach (Candidate c in this.candidates)
            {
                if (c.pos == pos)
                    candidatesInPos.Add(c);
            }
            return candidatesInPos;
        }

    }
}
