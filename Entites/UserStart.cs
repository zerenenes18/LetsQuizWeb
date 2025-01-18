using Core.Entities;

namespace LetsQuizCore.Entities;

public class UserStart : IEntity
{
    public Guid Id { get; set; }
    public Guid QuizId { get; set; }
    public Guid UserId { get; set; }
}