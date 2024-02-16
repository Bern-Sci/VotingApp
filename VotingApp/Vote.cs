using System.Collections.Generic;
using System;
using VotingApp;

public class Vote
{
    public List<List<int>> Choices;
    public Voter voter;
    public CandidateList candidateList;

    public Vote(Voter voter, CandidateList candidateList)
    {
        this.Choices = new List<List<int>>();
        this.voter = voter;
        this.candidateList = candidateList;
    }

    public void startVote()
    {
        Choices.Add(VoteForCandidateInPos(candidateList.getCandidatesInPos(Position.President), 1));
        Choices.Add(VoteForCandidateInPos(candidateList.getCandidatesInPos(Position.VicePresident), 1));
        Choices.Add(VoteForCandidateInPos(candidateList.getCandidatesInPos(Position.Secretary), 1));
        Choices.Add(VoteForCandidateInPos(candidateList.getCandidatesInPos(Position.Treasurer), 1));
        Choices.Add(VoteForCandidateInPos(candidateList.getCandidatesInPos(Position.Auditor), 1));
        Choices.Add(VoteForCandidateInPos(candidateList.getCandidatesInPos(Position.PIO), 2));
        Choices.Add(VoteForCandidateInPos(candidateList.getCandidatesInPos(Position.SgtAtArms), 2));
        Choices.Add(VoteForCandidateInPos(candidateList.getCandidatesInPos(Position.FirstYrRep), 1));
        Choices.Add(VoteForCandidateInPos(candidateList.getCandidatesInPos(Position.SecondYrRep), 1));
        Choices.Add(VoteForCandidateInPos(candidateList.getCandidatesInPos(Position.ThirdYrRep), 1));
        Choices.Add(VoteForCandidateInPos(candidateList.getCandidatesInPos(Position.FourthYrRep), 1));
        Choices.Add(VoteForCandidateInPos(candidateList.getCandidatesInPos(Position.IrregRep), 1));
    }

    private List<int> VoteForCandidateInPos(List<Candidate> candidates, int maxVotes)
    {
        if (candidates.Count == 0)
        {
            // If there are no candidates, return a list of -1 indicating no votes
            return new List<int>() { 0,0 };
        }
        Position pos = candidates[0].pos;
        List<int> choices = new List<int>();

        while (maxVotes > 0)
        {
            Console.Clear();
            Console.WriteLine($"Position: {pos} - Remaining Votes: {maxVotes}");
            Console.WriteLine("Choose a candidate to vote:");
            int counter = 1;
            foreach (Candidate c in candidates)
            {
                Console.WriteLine($"{counter}. {c}");
                counter++;
            }
            Console.WriteLine($"{counter}. ABSTAIN");

            string temp = Console.ReadLine();
            bool success = int.TryParse(temp, out int choice);

            if (success && choice > 0 && choice <= candidates.Count)
            {
                choices.Add(choice - 1);
                maxVotes--;
            }
            else if (success && choice > 0 && choice == candidates.Count + 1)
            {
                choices.Add(-1);
                maxVotes--;
            }
        }
        return choices;
    }


    public void ShowVoteSummary()
    {
        List<Candidate> Presidents = candidateList.getCandidatesInPos(Position.President);
        List<Candidate> VicePres = candidateList.getCandidatesInPos(Position.VicePresident);
        List<Candidate> Secretary = candidateList.getCandidatesInPos(Position.Secretary);
        List<Candidate> Treasurer = candidateList.getCandidatesInPos(Position.Treasurer);
        List<Candidate> Auditor = candidateList.getCandidatesInPos(Position.Auditor);
        List<Candidate> PIO = candidateList.getCandidatesInPos(Position.PIO);
        List<Candidate> SgtAtArms = candidateList.getCandidatesInPos(Position.SgtAtArms); // Corrected here
        List<Candidate> FirstYrRep = candidateList.getCandidatesInPos(Position.FirstYrRep);
        List<Candidate> SecondYrRep = candidateList.getCandidatesInPos(Position.SecondYrRep);
        List<Candidate> ThirdYrRep = candidateList.getCandidatesInPos(Position.ThirdYrRep);
        List<Candidate> FourthYrRep = candidateList.getCandidatesInPos(Position.FourthYrRep);
        List<Candidate> IrregRep = candidateList.getCandidatesInPos(Position.IrregRep);
        Console.Clear();
        Console.WriteLine("Here are the summary of your votes:");
        PosSummary(Presidents, 0, 1);
        PosSummary(VicePres, 1, 1);
        PosSummary(Secretary, 2, 1);
        PosSummary(Treasurer, 3, 1);
        PosSummary(Auditor, 4, 1);
        PosSummary(PIO, 5, 2);
        PosSummary(SgtAtArms, 6, 2); 
        PosSummary(FirstYrRep, 7, 1);
        PosSummary(SecondYrRep, 8, 1);
        PosSummary(ThirdYrRep, 9, 1);
        PosSummary(FourthYrRep, 10, 1);
        PosSummary(IrregRep, 11, 1);
    }


    public void disp2dArr()
    {
        Console.Clear();
        for(int i = 0; i < Choices.Count; i++) 
        {
            for (int j = 0; j < Choices[i].Count; j++)
                Console.Write(Choices[i][j] + 1 + ", ");
            Console.WriteLine();
        }
    }
    public void PosSummary(List<Candidate> candidate, int index, int maxVote)
    {
        if((maxVote == 1) && candidate.Count > 0)
        {
            if (Choices[index][0] != -1)
            {
                Console.WriteLine($"Position: {candidate[0].pos} Name: {candidate[index]}");
            }
            else Console.WriteLine($"Position: {candidate[0].pos} ABSTAINED!");
        }
    }


}
