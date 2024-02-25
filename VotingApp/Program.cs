using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FireSharp.Config;
using FireSharp.Response;
using FireSharp.Interfaces;

namespace VotingApp
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            string Auth = "6yFfNdlL6Sd6Xi81eOIyBLeOpINPgVHsV8x9e35C";
            string Path = "https://voter-app-26744-default-rtdb.firebaseio.com/";
            CandidateList candidateList = await CandidateList.CreateAsync(Auth, Path);
            VoterList voterList = await VoterList.CreateAsync(Auth, Path);
            while (true)
            {
                Console.Clear();
                Login newLog = new Login(candidateList, voterList);
                Voter Name = newLog.LoginPrompt();
                Vote Vote1 = new Vote(Name, candidateList);
                Vote1.startVote();
                while (Vote1.ShowVoteSummary())
                {
                    Vote1.VoteAgain();
                }
                Console.Clear();
                await voterList.MarkVoterAsVoted(Name);
                Console.ReadKey();
            }
        }
    }
}
