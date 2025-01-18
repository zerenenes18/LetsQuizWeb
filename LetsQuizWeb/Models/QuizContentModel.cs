namespace LetsQuizWeb.Models;

public class QuizContentModel
{
    public Guid QuizId { get; set; }
    public string LectureName { get; set; }
    public string QuizName { get; set; }
    public List<QuestionModel> questions { get; set; }
}

public class QuestionModel
{
    public Guid QuestionId { get; set; }
    public string? QuestionText { get; set; }
    public string? QuestionImagePath { get; set; }
    public int QuestionScore { get; set; }
    public int QuestionSecondTime { get; set; }
    public List<OptionModel> Options { get; set; }
}

public class OptionModel
{
    public Guid OptionId { get; set; }
    public string? OptionText { get; set; }
    public string OptionImagePath { get; set; }
    public bool IsTrue { get; set; }
    
    public bool IsRed { get; set; } = false;
    public bool IsYellow { get; set; } = false;
    public bool IsGreen { get; set; } = false;
}

// Request models for AddQuestion and AddOptions
public class AddQuestionRequest
{
    public Guid QuizId { get; set; }
    public string QuizName { get; set; }
    public QuestionRequest Question { get; set; }
}

public class QuestionRequest
{
    public Guid QuestionId { get; set; }
    public string QuestionText { get; set; }
    public string QuestionImagePath { get; set; }
    public int QuestionScore { get; set; }
    public int QuestionSecondTime { get; set; }
}

public class AddOptionsRequest
{
    public Guid QuestionId { get; set; }
    public List<OptionRequest> Options { get; set; }
}

public class OptionRequest
{
    public bool IsTrue { get; set; }
    public string OptionText { get; set; }
    public string OptionImagePath { get; set; }
}


public class QuickStartModel
{
    public Guid AdminId { get; set; }
    public Guid QuizId { get; set; }
    public string AdminName { get; set; }
    public string QuizName { get; set; }
    public string LectureName { get; set; }
    public int ExamDuration { get; set; }
    public int QuestionCount { get; set; }
    
}

public class UpdateOptionsRequest
{
    public Guid QuestionId { get; set; }
    public List<UpdateOptionDto> Options { get; set; }
}

public class UpdateOptionDto
{
    public Guid OptionId { get; set; } // Mevcut seçenek için ID, yeni seçenek için boş bırakılabilir
    public string OptionText { get; set; }
    public string? OptionImagePath { get; set; }
    public bool IsTrue { get; set; }
}