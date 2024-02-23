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
            Firebase firebase = new Firebase("mk52qYtyN4zDxwDj3VUyQ4b7wCFphnMZ9878ExwM", "https://fir-test-371b2-default-rtdb.firebaseio.com/");
            CandidateList candidateList = new CandidateList("C:\\Users\\Joerick Amadora\\source\\repository\\Bern-Sci\\VotingApp\\VotingApp\\Candidates.txt");
            VoterList voterList = new VoterList("C:\\Users\\Joerick Amadora\\source\\repository\\Bern-Sci\\VotingApp\\VotingApp\\VoterList.txt");

            firebase.pushListOfObject("Data/Candidates", candidateList.candidates);
            firebase.pushListOfObject("Data/Voters", voterList.voters);
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
                voterList.MarkVoterAsVoted(Name.StudentId);
                Vote1.RecordVote();
                Console.ReadKey();
            }

        }
    }
}
