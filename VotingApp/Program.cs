using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VotingApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            CandidateList candidateList = new CandidateList("D:\\CandidateList\\Candidates.txt");
            VoterList voters = new VoterList("D:\\CandidateList\\VoterList.txt");

            while (true)
            {
                Console.Clear();
                Login newLog = new Login(candidateList, voters);
                Voter Name = newLog.LoginPrompt();
                Vote Vote1 = new Vote(Name, candidateList);
                Vote1.startVote();
                while (Vote1.ShowVoteSummary())
                {
                    Vote1.VoteAgain();
                }
                Console.Clear();
                voters.MarkVoterAsVoted(Name.StudentId);
                Vote1.RecordVote();
                Console.ReadKey();
            }
         
        }
    }
}
