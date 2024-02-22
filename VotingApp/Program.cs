using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FireSharp;
using FireSharp.Config;
using FireSharp.Response;
using FireSharp.Interfaces;

namespace VotingApp
{
    public class Program
    {
        static void Main(string[] args)
        {

            IFirebaseConfig config = new FirebaseConfig
            {
                AuthSecret = "mk52qYtyN4zDxwDj3VUyQ4b7wCFphnMZ9878ExwM",
                BasePath = "https://fir-test-371b2-default-rtdb.firebaseio.com/"
            };

            IFirebaseClient client = new FireSharp.FirebaseClient(config);

            CandidateList candidateList = new CandidateList("C:\\Users\\Joerick Amadora\\source\\repository\\Bern-Sci\\VotingApp\\VotingApp\\Candidates.txt");
            VoterList voterList = new VoterList("C:\\Users\\Joerick Amadora\\source\\repository\\Bern-Sci\\VotingApp\\VotingApp\\VoterList.txt");

            UploadObjects(client, "Data/Candidates/", candidateList.candidates); //This can be deleted after you uploaded the data in database...
            UploadObjects(client, "Data/Voters/", voterList.voters);             //This can be deleted after you uploaded the data in database...
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
        static void UploadObjects(IFirebaseClient client, string collectionName, List<Candidate> candidate)
        {
            // Loop through the list of objects and upload each one
            int ctr = 0;
            foreach (Candidate c in candidate)
            {
                string path= collectionName + ctr.ToString();
                FirebaseResponse response = client.Set(path, c);
                ctr++;
            }
        }
        static void UploadObjects(IFirebaseClient client, string collectionName, List<Voter> voter)
        {
            // Loop through the list of objects and upload each one
            int ctr = 0;
            foreach (Voter c in voter)
            {
                string path = collectionName + ctr.ToString();
                FirebaseResponse response = client.Set(path, c);
                ctr++;
            }
        }
    }
}
