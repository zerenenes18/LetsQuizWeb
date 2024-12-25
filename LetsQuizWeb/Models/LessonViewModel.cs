namespace LetsQuizWeb.Models;

public class HomeViewModel
{
    public IEnumerable<LessonViewModel> Lessons { get; set; } // Dersler
    public IEnumerable<ScoreViewModel> Scores { get; set; } // Skorlar
}

public class LessonViewModel
{
    public string Title { get; set; } // Ders başlığı
    public List<QuizViewModel> Quizzes { get; set; } // Ders içindeki quizler
}

public class QuizViewModel
{
    public int Id { get; set; } // Quiz ID
    public string Title { get; set; } // Quiz başlığı
}

public class ScoreViewModel
{
    public string UserName { get; set; } // Kullanıcı adı
    public int Score { get; set; } // Skor
    public string QuizName { get; set; } // Quiz adı
    public string QuizLecture { get; set; } // Quizin dersi
    public decimal SuccessRate { get; set; } // Başarı oranı
    public int Points { get; set; } // Puan
}