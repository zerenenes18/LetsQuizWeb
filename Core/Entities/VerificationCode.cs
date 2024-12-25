namespace Core.Entities;

public class VerificationCode: IEntity
{
    public Guid Id { get; set; }
    public string UserEmail { get; set; }
    public int Code { get; set; }
    public DateTime CreatedAt { get; set; }
}