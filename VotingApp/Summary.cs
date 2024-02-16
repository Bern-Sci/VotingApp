using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VotingApp
{
    public class Vote : CandidateList
    {
        public List<int> Choices;
        public Vote() 
        {
            this.Choices = new List<int>();
        }
        public List<int> startVote(Voter voter)
        {
            List <int> choices = new List<int>();
            List <Candidate> Pres = getCandidatesInPos(Position.President);
            List <Candidate> VicePresident = getCandidatesInPos(Position.VicePresident);
            List <Candidate> Secretary = getCandidatesInPos(Position.Secretary);
            List <Candidate> Treasurer = getCandidatesInPos(Position.Treasurer);
            List <Candidate> Auditor = getCandidatesInPos(Position.Auditor);
            List <Candidate> PIO = getCandidatesInPos(Position.PIO);
            List <Candidate> SgtAtArms = getCandidatesInPos(Position.SgtAtArms);
            List <Candidate> FirstYrRep = getCandidatesInPos(Position.FirstYrRep);
            List <Candidate> SecondYrRep = getCandidatesInPos(Position.SecondYrRep);
            List <Candidate> ThirdYrRep = getCandidatesInPos(Position.ThirdYrRep);
            List <Candidate> FourthYrRep = getCandidatesInPos(Position.FourthYrRep);
            List <Candidate> IrregRep = getCandidatesInPos(Position.IrregRep);



            return choices;
        }

        public int ChoicesPrompt(List <Candidate> candidate)
        {
            int choice;
            if (candidate.Count > 0)
            {
                Console.WriteLine("Choose a candidate to vote:");
                int counter = 1;
                foreach (Candidate c in candidate)
                {

                    counter++;
                }
            }
            return choice;

        }
    }
}
