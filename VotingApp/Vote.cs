using System.Collections.Generic;
using System;
using VotingApp;
using System.Runtime.CompilerServices;

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
                choices.Add(-1);
                maxVotes--;
            }
        }
        return choices;
    }
    public void changeVoteInThisPos(Position position)
    {
        List<int> indexOfPos = new List<int>();
        //kuhaon ang index sa candidate nga ang position kay equal sa position parameter
        for (int i = 0; i < chosenCandidate.Count; i++)
            if (chosenCandidate[i].pos.Equals(position))
            {
                indexOfPos.Add(i);
                chosenCandidate[i] = null; //diko sure og diba ni mag memory leak. ichange nalang basta dapat ma null value
            }
        //Para ng babaw maedit ang value sa index diha sa chosenCandidate
        int index = 0; //index ctr sa indexOfPos

        List<Candidate> samePosCandidate = candidateList.getCandidatesInPos(position);
        int remainingVotes = indexOfPos.Count;
        while (remainingVotes != 0)
        {
            Console.Clear();
            Console.WriteLine($"Position: {position.ToString()} - Remaining Votes: {remainingVotes}");
            Console.WriteLine("Choose a candidate to vote:");

            int ctr = 1;
            foreach (Candidate c in samePosCandidate)
                Console.WriteLine($"{ctr++}. {c.Name}");
            Console.WriteLine($"{ctr}. ABSTAIN");

            string temp = Console.ReadLine();
            bool success = int.TryParse(temp, out int choice);
            if (success && choice > 0 && choice < samePosCandidate.Count + 1)
            {
                if (chosenCandidate[indexOfPos[index]] == null)
                {
                    chosenCandidate[indexOfPos[index]] = samePosCandidate[choice - 1];
                    index++;
                    remainingVotes--;
                }
                else
                {
                    Console.WriteLine("You cannot vote the same candidate twice..");
                    Console.ReadKey();
                }
            }
            else if (success && choice > 0 && choice == samePosCandidate.Count + 1)
                remainingVotes--;
        }
    }

    public void ShowVoteSummary()
    {
        Console.Clear();
        foreach (Candidate c in chosenCandidate)
        {
            Console.WriteLine($"{c.pos.ToString()}: {c.Name}");
        }
 
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


}
