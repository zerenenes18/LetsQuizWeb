namespace LetsQuizCore.Entities;

public class Score
{
    public Guid Id { get; set; }
    public string UserName { get; set; }
    public int ScoreValue { get; set; }
    public decimal SuccessRate { get; set; }
    public string Description { get; set; }
    public DateTime CreatedAt { get; set; }
}
