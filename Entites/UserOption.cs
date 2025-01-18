using Core.Entities;

namespace LetsQuizCore.Entities;

public class UserOption : IEntity
{
    public Guid Id { get; set; }
    public Guid QuestionId { get; set; }
    public Guid UserId { get; set; }
    public Guid OptionId { get; set; }
    public bool  IsTrue { get; set; }
}