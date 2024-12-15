using Core.Entities;

namespace LetsQuizCore.Entities;

public class Guest : IEntity
{
    public Guid Id { get; set; }
    public Guid QuizId { get; set; }
    public string UserName { get; set; }
}