namespace LetsQuizWeb.Models;



public class QuizzesModel
{
    public List<AdminQuizzesModel> adminQuizzesModel { get; set; }
    public List<string> lectureList { get; set; }
}


public class AdminQuizzesModel
{
    public Guid QuizId { get; set; }
    public string LectureName { get; set; }
    public string QuizName { get; set; }
}

public class DeleteQuestionModel
{
    public Guid QuestionId { get; set; }
}
public class DeleteOptionModel
{
    public Guid OptionId { get; set; }
}


public class QuizDetailModel
{
    public Guid AdminId { get; set; }
    public Guid QuizId { get; set; }
    public string AdminName { get; set; }
    public string QuizName { get; set; }
    public string LectureName { get; set; }
    public int ExamDuration { get; set; }
    public int QuestionCount { get; set; }
}



