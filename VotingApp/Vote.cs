using System.Collections.Generic;
using System;
using VotingApp;
using System.Runtime.CompilerServices;
using System.Diagnostics;
using System.IO;

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
    }

    private List<int> VoteForCandidateInPos(List<Candidate> candidates, int maxVotes)
    {
        if (candidates.Count == 0)
        {
            return new List<int>() { 0, 0 };
        }
        Position pos = candidates[0].pos;
        List<int> choices = new List<int>();

        while (maxVotes > 0)
        {
            Console.Clear();
            Console.WriteLine($"Currently logged in: {voter.StudentId}");
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
                if (choices.Count == 0)
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

    public bool ShowVoteSummary()
    {
        Console.Clear();
        int ctr = 0;
        foreach (Candidate c in chosenCandidate)
        {
            Console.WriteLine($"{++ctr}. {c.pos.ToString()}: {c.Name}");
        }
        Console.WriteLine("Vote again? Y/N (Any Key)");
        string res = Console.ReadLine();
        if (res.ToUpper() == "Y") return true;
        else return false;
    }



    public void VoteAgain()
    {
        Console.Clear();
        Array allpos = Enum.GetValues(typeof(Position));
        int ctr = 1;
        Console.WriteLine("Choose a position you want to change vote");
        foreach (Position cpos in allpos)
            Console.WriteLine($"{ctr++}. {cpos}");
        int index = int.Parse(Console.ReadLine());
        if (index < 1 || index > allpos.Length)
        {
            Console.WriteLine("Your input is not recognized.Please try again!");
            Console.ReadKey();
            VoteAgain();
        }

        switch (index)
        {
            case 1:
                changeVoteInThisPos(Position.President);
                break;
            case 2:
                changeVoteInThisPos(Position.VicePresident);
                break;
            case 3:
                changeVoteInThisPos(Position.Secretary);
                break;
            case 4:
                changeVoteInThisPos(Position.Treasurer);
                break;
            case 5:
                changeVoteInThisPos(Position.Auditor);
                break;
            case 6:
                changeVoteInThisPos(Position.PIO);
                break;
            case 7:
                changeVoteInThisPos(Position.SgtAtArms);
                break;
            case 8:
                changeVoteInThisPos(Position.FirstYrRep);
                break;
            case 9:
                changeVoteInThisPos(Position.SecondYrRep);
                break;
            case 10:
                changeVoteInThisPos(Position.ThirdYrRep);
                break;
            case 11:
                changeVoteInThisPos(Position.FourthYrRep);
                break;
        }
    }

    public void RecordVote()
    {
        // Provide the path to your desired text file
        string filePath = "D:\\CandidateList\\VoteRecord.txt";
        if (voter.canVote == true)
        {
            // Open the file in append mode, so it doesn't overwrite the existing content
            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                writer.WriteLine($"{voter.Name} {voter.StudentId}:");
                foreach (Candidate c in chosenCandidate)
                {
                    if (c != null)
                        writer.WriteLine($"{c.pos.ToString()}: {c.Name}");
                }
                writer.WriteLine();
            }

            Console.WriteLine("Vote recorded successfully!");
            Console.ReadKey();
        }
        else return;
    }
}