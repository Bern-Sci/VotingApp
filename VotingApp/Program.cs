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
            //Demo puposes
            //Mo kuha ang candidateList ug voterList sa tanan candidates ug voters gikan sa text file
            CandidateList candidateList = new CandidateList("Candidates.txt");
            VoterList voters = new VoterList("VoterList.txt");
            //Then after kuhaon ni Login ang objects, for checking if ni exist ba na sila
            Login newLog = new Login(candidateList, voters);
            //Login na ari na part, mo return si newLog ug Voter if VALID ang credentials
            Voter Name = newLog.LoginPrompt();
            //Start na ug vote
            Vote Vote1 = new Vote(Name, candidateList);
            Vote1.startVote();
            //If naay prompt na mogawas, type capital Y, kay mo bug pa siya
            if(Vote1.ShowVoteSummary())
                Vote1.VoteAgain();
            Vote1.ShowVoteSummary();
            Console.ReadLine();
        }
    }
}
