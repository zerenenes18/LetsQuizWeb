using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using LetsQuizWeb.Models;

namespace LetsQuizWeb.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public IActionResult Index()
    {
        var lessons = new List<LessonViewModel>
        {
            new LessonViewModel
            {
                Title = "Mathematics",
                Quizzes = new List<QuizViewModel>
                {
                    new QuizViewModel { Id = 1, Title = "Algebra Quiz" },
                    new QuizViewModel { Id = 2, Title = "Geometry Quiz" }
                }
            },
            new LessonViewModel
            {
                Title = "Science",
                Quizzes = new List<QuizViewModel>
                {
                    new QuizViewModel { Id = 3, Title = "Physics Quiz" },
                    new QuizViewModel { Id = 4, Title = "Chemistry Quiz" }
                }
            },
            new LessonViewModel
            {
                Title = "C# & .net 8",
                Quizzes = new List<QuizViewModel>
                {
                    new QuizViewModel { Id = 1, Title = "Algebra Quiz" },
                    new QuizViewModel { Id = 2, Title = "Geometry Quiz" }
                }
            }
        };

        var scores = new List<ScoreViewModel>
        {
            new ScoreViewModel { UserName = "JohnDoe", Score = 90, QuizName = "Algebra Quiz", QuizLecture = "Mathematics", SuccessRate = 95.5m, Points = 10 },
            new ScoreViewModel { UserName = "JaneSmith", Score = 85, QuizName = "Physics Quiz", QuizLecture = "Science", SuccessRate = 88.0m, Points = 9 }
        };

        var viewModel = new HomeViewModel
        {
            Lessons = lessons,
            Scores = scores
        };

        return View(viewModel);
    }
   

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}