using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FireSharp.Config;
using FireSharp.Response;
using FireSharp.Interfaces;
using System.Threading.Tasks;

namespace VotingApp
{
    public class CandidateList
    {
        public List<Candidate> candidates { get; private set; }
        IFirebaseClient client;
        IFirebaseConfig config;

        public CandidateList(string Auth, string Path)
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
            candidates = new List<Candidate>();
            //CandidateList candidateList = await CandidateList.CreateAsync("Auth", "Path");
        }

        public static async Task<CandidateList> CreateAsync(string Auth, string Path)
        {
            var candidateList = new CandidateList(Auth, Path);
            await candidateList.GetCandidatesFromDatabase();
            return candidateList;
        }

        private async Task GetCandidatesFromDatabase()
        {
            int currentYear = DateTime.Now.Year;
            string electionEvent = $"Election Event {currentYear}";

            FirebaseResponse response = await client.GetTaskAsync($"{electionEvent}/Candidate/Position");
            Dictionary<string, Dictionary<string, Dictionary<string, string>>> data = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, Dictionary<string, string>>>>(response.Body);

            foreach (var position in data)
            {
                Position pos = (Position)Enum.Parse(typeof(Position), position.Key);
                foreach (var candidate in position.Value)
                {
                    string candidateName = candidate.Value["Name"];
                    Candidate newCandidate = new Candidate(candidateName, pos);
                    candidates.Add(newCandidate);
                }
            }
        }


        public void displayCandidates()
        {
            foreach (var candidate in candidates)
            {
                Console.WriteLine($"Position: {candidate.pos}, {candidate.Name}");
            }
        }
        public List<Candidate> getCandidatesInPos(Position pos)
        {
            List<Candidate> candidatesInPos = new List<Candidate>();
            foreach (Candidate c in this.candidates)
            {
                if (c.pos == pos)
                    candidatesInPos.Add(c);
            }
            return candidatesInPos;
        }
    }
}
