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
            //VoterList voters = new VoterList("D:\\CandidateList\\VoterList.txt");
            //Login newLog = new Login(candidateList, voters);
            //if (newLog.LoginPrompt())
            //{
                Voter Bern = new Voter("Bern", "20-1-02139", "12345");
                Vote Kiosk = new Vote(Bern, candidateList);
                Kiosk.startVote();
                Kiosk.disp2dArr();
                Console.ReadLine();
            //}
           
        }
    }
}
