using System.Collections.Generic;
using System;
using VotingApp;
using System.Runtime.CompilerServices;
using System.Diagnostics;

public class Vote
{
    public List<List<int>> Choices;
    public Voter voter;
    public CandidateList candidateList;
    public List<Candidate> chosenCandidate;

    public Vote(Voter voter, CandidateList candidateList)
    {
        this.Choices = new List<List<int>>();
        this.voter = voter;
        this.candidateList = candidateList;
        this.chosenCandidate = new List<Candidate>();
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
            bool success = int.TryParse(temp, out int choice); //Mo test if ang user input is lesser than sa number of candidates
                                                
            if (success && choice > 0 && choice <= candidates.Count)
            {
                if(choices.Count == 0)
                {
                    choices.Add(choice - 1);
                    chosenCandidate.Add(candidates[choice - 1]);
                    maxVotes--;
                }
                else
                {
                    if (choices[0] != choice - 1)
                    {
                        choices.Add(choice - 1);
                        chosenCandidate.Add(candidates[choice - 1]);
                        maxVotes--;
                    }
                    else
                    {
                        Console.WriteLine("You cannot vote the same candidate twice..");
                        Console.ReadKey();
                    }
                }

            }
            else if (success && choice > 0 && choice == candidates.Count + 1)
            {
                Candidate AbstainCandidate = new Candidate("ABSTAINED", "ABSTAINED", "ABSTAINED", candidates[0].pos);
                chosenCandidate.Add(AbstainCandidate);
                choices.Add(-1);
                maxVotes--;
            }
        }
        return choices;
    }


    public bool ShowVoteSummary()
    {
        Console.Clear();
        int ctr = 0;
        foreach (Candidate c in chosenCandidate)
        {
            Console.WriteLine($"{ctr++}. {c.pos.ToString()}: {c.Name}");
        }
        Console.WriteLine("Vote again? Y/N (Any Key)");
        string res = Console.ReadLine();
        res.ToUpper();
        if (res[0] == 'Y') return true;
        else return false;
        
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

    public void VoteAgain()
    {
        Console.WriteLine("Enter a position to vote again (1-12)");
        int index = int.Parse(Console.ReadLine());
   
        while (index < 1 || index > 12)
        {
            Console.Write("Please enter a valid index (1-12): ");
            index = int.Parse(Console.ReadLine());
        }
        List<Candidate> candidates = new List<Candidate>();
        int maxVote;
        if (index == 1) candidates = candidateList.getCandidatesInPos(Position.President);
        if (index == 2) candidates = candidateList.getCandidatesInPos(Position.VicePresident);
        if (index == 3) candidates = candidateList.getCandidatesInPos(Position.Secretary);
        if (index == 4) candidates = candidateList.getCandidatesInPos(Position.Treasurer);
        if (index == 5) candidates = candidateList.getCandidatesInPos(Position.Auditor);
        if (index == 6) candidates = candidateList.getCandidatesInPos(Position.PIO);
        if (index == 7) candidates = candidateList.getCandidatesInPos(Position.SgtAtArms);
        if (index == 8) candidates = candidateList.getCandidatesInPos(Position.FirstYrRep);
        if (index == 9) candidates = candidateList.getCandidatesInPos(Position.SecondYrRep);
        if (index == 10) candidates = candidateList.getCandidatesInPos(Position.ThirdYrRep);
        if (index == 11) candidates = candidateList.getCandidatesInPos(Position.FourthYrRep);
        if (index == 12) candidates = candidateList.getCandidatesInPos(Position.IrregRep);
        if (candidates[0].pos == Position.PIO || candidates[0].pos == Position.SgtAtArms)
            maxVote = 2;
        else maxVote = 1;
        VoteForCandidateInPos(candidates, maxVote);
    }

}
