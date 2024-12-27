using Core.Entities;

namespace LetsQuizCore.Entities.DTOs;

public class UserForLoginDto : IDto
{
    public string Email { get; set; }
    public string Password { get; set; }
}

public class QuizAddDto : IDto
{
    public string QuizName { get; set; }
    public string LectureName { get; set; }
}