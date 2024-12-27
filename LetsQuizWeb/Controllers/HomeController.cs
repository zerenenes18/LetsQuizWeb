using System.Diagnostics;
using Business.Operations;
using DataAccess.Abstract;
using LetsQuizCore.Entities;
using Microsoft.AspNetCore.Mvc;
using LetsQuizWeb.Models;

namespace LetsQuizWeb.Controllers;

public class HomeController : Controller
{

    IScoreDal _scoreDal;

    public HomeController(IScoreDal scoreDal )
    {
        _scoreDal = scoreDal;
    }
    
    
    
    [SecuredOperation("admin,manager,Student")]
    public async Task<IActionResult> Index()
    {

        List<Score> scoreList = await _scoreDal.GetAllAsync();

       

        var scores = new List<ScoreViewModel>();

        foreach (var score in scoreList)
        {
            scores.Add(new ScoreViewModel
            {
                UserName = score.UserName,
                Points = score.ScoreValue,
                QuizName = score.QuizName,
                QuizLecture = score.QuizName,
                SuccessRate = score.SuccessRate
                
            });
        }


        var viewModel = new HomeViewModel
        {
            Lessons = new List<LessonViewModel>(),
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