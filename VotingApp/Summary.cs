using FireSharp.Response;
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
    public class Summary
    {
        public Vote voteResult;
        IFirebaseClient client;
        IFirebaseConfig config;

        public Summary(string Auth, string Path, Vote voteResult)
        {
            config = new FirebaseConfig
            {
                AuthSecret = Auth,
                BasePath = Path
            };
            client = new FireSharp.FirebaseClient(config);
            if (client != null) Console.WriteLine("You are connected!");
            else Console.WriteLine("Not connected!");
            this.voteResult = voteResult;
        }

        public async Task UploadSummaryAsync()
        {
            int currentYear = DateTime.Now.Year;
            string electionEvent = $"Election Event {currentYear}";

            foreach (var candidate in voteResult.chosenCandidate)
            {
                string path = $"{electionEvent}/Vote Result/{voteResult.voter.YearLevel}/{voteResult.voter.Code}/{candidate.pos}/{candidate.Name}";
                FirebaseResponse resp = await Task.Run(() => client.Get(path));
                if (resp.Body == "null")
                {
                    var voteResData = new { chosenCandidate = candidate.Name };
                    SetResponse res = await client.SetTaskAsync(path, voteResData);
                    Console.WriteLine($"{voteResult.voter.Code} summary has been uploaded.");
                }
                else
                {
                    Console.WriteLine("Voter's summary already exists.");
                    break;
                }
            }
        }


        public void displayChosen()
        {
            foreach(Candidate c in voteResult.chosenCandidate)
                Console.WriteLine(c.Name);
        }
    }
}
