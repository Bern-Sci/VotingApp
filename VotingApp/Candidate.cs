using VotingApp;

public class Candidate : Student
{
    public Position pos { get; private set; }
    public int RemainingVotes { get; set; }

    public Candidate(string name, string sID, string pass, Position pos) : base(name, sID, pass)
    {
        this.pos = pos;
        this.RemainingVotes = GetInitialVotes(pos);
    }

    private int GetInitialVotes(Position pos)
    {
        return pos == Position.PIO || pos == Position.SgtAtArms ? 2 : 1;
    }

    public override string ToString()
    {
        return $"{this.Name}";
    }
}
