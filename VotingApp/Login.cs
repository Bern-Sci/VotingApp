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

        public bool TryLogin(string studentId, string password)
        {
            foreach (Candidate candidate in candidates)
            {
                if (candidate.StudentId == studentId)
                {
                    return false;
                }
            }

            foreach (Voter voter in voters)
            {
                if (voter.StudentId == studentId && voter.Password == password && voter.canVote)
                {
                    return true;
                }
                else if(voter.StudentId == studentId && voter.Password == password && voter.canVote == false)
                {
                    Console.WriteLine("You have already voted!");
                    return false;
                }
            }

            return false;
        }

        public Voter LoginPrompt()
        {
            bool isLoggedIn = false;
            string sId = "";
            while (isLoggedIn == false)
            {
                Console.Clear();
                Console.Write("Student ID: ");
                sId = Console.ReadLine();
                Console.Write("\nPassword: ");
                string password = Console.ReadLine();
                bool isSuccess = TryLogin(sId, password);
                if (isSuccess == true) isLoggedIn = true;
                else
                {
                    Console.WriteLine("Invalid credentials, please try again.");
                    Console.WriteLine("Press any key to log in again.");
                    Console.ReadKey();
                }
            }
            return new Voter(sId);
        }


        public void dispAllVoter() 
        {
            foreach (Voter v in voters)
            {
                Console.WriteLine($"{v.Name} {v}, Can vote: {v.canVote}");
            }
        }
    }
}
