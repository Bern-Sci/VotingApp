using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FireSharp.Config;
using FireSharp.Response;
using FireSharp.Interfaces;
using System.Threading.Tasks;
using FireSharp.Exceptions;

namespace VotingApp
{
    public class VoterList
    {
        IFirebaseClient client;
        IFirebaseConfig config;
        public List<Voter> voters { get; private set; }
        public VoterList(string Auth, string Path)
        {
            config = new FirebaseConfig
            {
                AuthSecret = Auth,
                BasePath = Path
            };
            client = new FireSharp.FirebaseClient(config);
            if (client != null) Console.WriteLine("You are connected!");
            else Console.WriteLine("Not connected!");
            Console.Clear();
            voters = new List<Voter>();
        }

        public static async Task<VoterList> CreateAsync(string Auth, string Path)
        {
            var voterList = new VoterList(Auth, Path);
            await voterList.GetVotersFromDatabase();
            return voterList;
            //VoterList voterList = await VoterList.CreateAsync("Auth", "Path");
        }

        private async Task GetVotersFromDatabase()
        {
            int currentYear = DateTime.Now.Year;
            string electionEvent = $"Election Event {currentYear}";

            FirebaseResponse response = await client.GetTaskAsync($"{electionEvent}/Vote");
            Dictionary<string, Dictionary<string, Dictionary<string, Dictionary<string, object>>>> data = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, Dictionary<string, Dictionary<string, object>>>>>(response.Body);

            foreach (var yearLevel in data)
            {
                YearLevel yr = (YearLevel)Enum.Parse(typeof(YearLevel), yearLevel.Key);
                foreach (var voter in yearLevel.Value)
                {
                    if (long.TryParse(voter.Key, out long code))
                    {
                        bool canVote = (bool)voter.Value["canVote"]["canVote"];
                        Voter newVoter = new Voter(yr, canVote, code);
                        voters.Add(newVoter);
                    }
                    else
                    {
                        Console.WriteLine($"Failed to parse voter code: {voter.Key}");
                    }
                }
            }
        }






        public void displayVoters()
        {
            foreach (Voter v in voters)
            {
                Console.WriteLine(v.Code + $" {v.YearLevel}, Can Vote?: {v.canVote}");
            }
        }

        public async Task MarkVoterAsVoted(Voter voter)
        {
            int currentYear = DateTime.Now.Year;
            string electionEvent = $"Election Event {currentYear}";

            try
            {
                FirebaseResponse response = await client.GetTaskAsync($"{electionEvent}/Vote/{voter.YearLevel}/{voter.Code}/canVote");
                Dictionary<string, object> voterFromDb = response.ResultAs<Dictionary<string, object>>();
                voterFromDb["canVote"] = false;
                SetResponse setResponse = await client.SetTaskAsync($"{electionEvent}/Vote/{voter.YearLevel}/{voter.Code}/canVote", voterFromDb);
                Console.WriteLine($"Voter with code {voter.Code} has been marked as voted");
                voter.canVote = false;
            }
            catch (FirebaseException ex)
            {
                Console.WriteLine($"Failed to mark voter with code {voter.Code} as voted. Error: {ex.Message}");
            }
        }


    }
}
