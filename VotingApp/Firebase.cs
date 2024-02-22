using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FireSharp.Config;
using FireSharp.Response;
using FireSharp.Interfaces;
using FireSharp;

namespace VotingApp
{
    public class Firebase
    {
        public string Auth_secret { get; private set; }
        public string Base_path { get; private set; }
        public IFirebaseClient client { get; private set; }

        public Firebase(string auth_secret, string base_path)
        {

            Auth_secret = auth_secret;
            Base_path = base_path;
            IFirebaseConfig config = new FirebaseConfig
            {
                AuthSecret = Auth_secret,
                BasePath = Base_path
            };

           this.client = new FirebaseClient(config);
        }
        public void pushObject(string path, List<Candidate> candidate)
        {
            foreach (Candidate c in candidate)
            {
                PushResponse response = client.Push(path +"/"+c.Name, c);
                if (response == null) { Console.WriteLine("Push Failed..."); }
            }
        }
        public void pushObject(string path, List<Voter> voters)
        {
            foreach (Voter v in voters)
            {
                PushResponse response = client.Push(path +"/"+v.Name, v);
                if (response == null) { Console.WriteLine("Push Failed..."); }
            }
        }
    }
}
