using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VotingApp
{
    public class Login
    {
        public List<Candidate> candidates { get; private set; }
        public List<Voter> voters { get; private set; }

        public Login(CandidateList candidateList, VoterList voterList)
        {
            candidates = candidateList.candidates;
            voters = voterList.voters;
        }

        public bool TryLogin(string code)
        {
            if (candidates.Count == 0) return false;
            else
            {
                foreach (Voter v in voters)
                {
                    if (v.Code.ToString() == code && v.canVote) return true;
                    else if (v.canVote == false)
                    {
                        Console.WriteLine("You have voted already!");
                        Console.ReadKey();
                        return false;
                    }
                }
                    
            }
            return false;
        }

        public Voter LoginPrompt()
        {
            while (true)
            {
                Console.Clear();
                Console.Write("Enter your code: ");
                string code = Console.ReadLine();

                if (TryLogin(code))
                {
                    Console.WriteLine($"{code} login successful!");
                    return voters.First(v => v.Code.ToString() == code);
                }
                else
                {
                    Console.WriteLine("Invalid code. Please try again.");
                }
            }
        }
    }
}