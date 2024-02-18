using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VotingApp
{
    public class Summary : CandidateList
    {
        public List<int> Choices;
        public Summary() 
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
    }
}
