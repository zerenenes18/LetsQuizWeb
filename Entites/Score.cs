using Core.Entities;

namespace LetsQuizCore.Entities;

public class Score : IEntity
{
    public Guid Id { get; set; }
    public Guid QuizId { get; set; }
    public Guid UserId { get; set; }
    public string? UserName { get; set; }
    
    public double ScoreValue { get; set; }
    public decimal SuccessRate { get; set; }
    public string? LectureName { get; set; }
    public string? QuizName { get; set; }
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; }
}
