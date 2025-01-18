using System.Text;
using Business.Abstract;
using Core.Extensions;
using Core.IoC;
using DataAccess.Abstract;
using LetsQuizCore.Entities;
using LetsQuizWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace LetsQuizWeb.Controllers;

public class UserController : Controller
{
    
    IUserService _userService;
    ILectureService _lectureService;
    IQuizService _quizService;
    IUserStartDal _userStartDal;
    IQuestionService _questionService;
    IUserOptionDal _userOptionDal;
    IOptionDal _optionDal;
    IScoreDal _scoreDal;
    private IHttpContextAccessor _httpContextAccessor;
    public UserController(IUserService userService,ILectureService lectureService,IQuizService quizService,IScoreDal scoreDal,IQuestionService questionService,IOptionDal optionDal,IUserOptionDal userOptionDal,IUserStartDal userStartDal)
    {
            _userService = userService;
            _lectureService = lectureService;
            _userStartDal = userStartDal;
            _quizService = quizService;
            _optionDal = optionDal; 
            _userOptionDal = userOptionDal;
            _scoreDal = scoreDal;
            _questionService = questionService;
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
    }
    // GET
    public async Task<IActionResult> Index()
    {
        return View();
    }
    
    
    
    
    public async Task<IActionResult> MyQuizzes()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> QuizStart([FromBody] QuizStartRequest request)
    {
        if (request == null || request.QuizId == Guid.Empty)
        {
            return BadRequest("Invalid request data.");
        }
        var userId = Guid.Parse(_httpContextAccessor.HttpContext.User.ClaimIdentifier());
        UserStart userStart = new UserStart{UserId = userId, QuizId = request.QuizId};
        await _userStartDal.AddAsync(userStart);
        return Ok();
    }
 
    public async Task<IActionResult> GetQuizContent(Guid quizId)
    {
        var contentModel = new QuizContentModel(); 
        List<QuestionModel> questionList = new List<QuestionModel>();
        var questions = await _questionService.GetQuestionsByQuizIdAsync(quizId);
        var Quiz =  await _quizService.GetByIdAsync(quizId);
        var Lecture = await _lectureService.GetByIdAsync(Quiz.Data.LectureId);
        List<UserOption> userOptions = new List<UserOption>();
        var adminId = Guid.Parse(_httpContextAccessor.HttpContext.User.ClaimIdentifier());

        userOptions = await _userOptionDal.GetAllAsync(o=> o.UserId == adminId);
        
        
        foreach (var question in questions.Data)
        {
            bool isExistUserOption = false;   
          
            var optionModelList = new List<OptionModel>();
            var options = await _optionDal.GetAllAsync(o=> o.QuestionId == question.Id);
            bool isQuestionOptionInUserOptions = userOptions.Any(uo => uo.QuestionId == question.Id);
            
            foreach (var option in options)
            {
                bool IsRed = false;
                bool IsYellow  = false;
                bool IsGreen  = false;
                bool isOptionInUserOptions = userOptions.Any(uo => uo.OptionId == option.Id);
                if (option.IsTrue && !isQuestionOptionInUserOptions)
                {
                    IsYellow = true;
                }
                else if (option.IsTrue && isOptionInUserOptions)
                {
                    IsGreen = true;
                }
               else if (!option.IsTrue && isOptionInUserOptions)
               {
                   IsRed = true;
               }
                
                optionModelList.Add(new OptionModel
                {
                    OptionId = option.Id,
                    OptionImagePath = option.ImagePath,
                    OptionText = option.Description,
                    IsTrue = option.IsTrue,
                    IsYellow = IsYellow,
                    IsGreen = IsGreen,
                    IsRed = IsRed
                });
            }
            questionList.Add(
                new QuestionModel
                {
                    QuestionId = question.Id,
                    QuestionText = question.Description,
                    QuestionImagePath = question.ImagePath,
                    QuestionSecondTime = question.QuestionSecondTime,
                    QuestionScore = question.QuestionScore,
                    Options = optionModelList
                });
        }
        contentModel.QuizId = Quiz.Data.Id;
        contentModel.questions = questionList;
        contentModel.LectureName = Lecture.Data.Name;
        contentModel.QuizName = Quiz.Data.Name;
        if (questions.Data == null || !questions.Data.Any())
        {
            return Ok(new QuizContentModel
                {
                    QuizName = Quiz.Data.Name,
                    LectureName = Lecture.Data?.Name ?? "Unknown",
                    questions = new List<QuestionModel>()
                });
        }

        if (Lecture.Data == null)
        {
            throw new Exception("Lecture not found for the given quiz name.");
        }
        return Ok(contentModel);
    }
    
    
    
    
    
    public async Task<QuizzesModel> GetQuizzesPageData()
    {
        
        var lectures = await _lectureService.GetAdminLecturesAsync();
        List<string> lectureNames = new List<string>();
        foreach (var lectureName in lectures.Data)
        {
            lectureNames.Add(lectureName.Name);
        }
        var adminId = Guid.Parse(_httpContextAccessor.HttpContext.User.ClaimIdentifier());
        
        var UserStarts = await _userStartDal.GetAllAsync(s=> s.UserId == adminId);
       
        List<Guid> quizzesIds = new List<Guid>();
        foreach (var starts in UserStarts)
        {
                quizzesIds.Add(starts.QuizId);
        }
        List<Quiz> Quizzes = new List<Quiz>();
        foreach (var id in quizzesIds)
        {
            var quiz = await _quizService.GetByIdAsync(id);
            Quizzes.Add(quiz.Data);
        }

        //var Quizzes = await _quizService.GetAdminQuizzesAsync();
        List<AdminQuizzesModel> adminQuizzes = new List<AdminQuizzesModel>();
        foreach (var quiz in Quizzes)
        {
            var lectureName = await _lectureService.GetByIdAsync(quiz.LectureId);
            adminQuizzes.Add(new AdminQuizzesModel
            {
                QuizId = quiz.Id,
                LectureName = lectureName.Data.Name,
                QuizName = quiz.Name,
            });
        }
        
        return new QuizzesModel
        {
            adminQuizzesModel = adminQuizzes,
            lectureList = lectureNames
        };

    }

    [HttpGet]
    public async Task<IActionResult> GetALl()
    {
       var users = await _userService.GetAllAsync();
       
       return Ok(users);
    }
    

}

    // Örnek Model (Veritabanı için)

    
    
    
    
    
    
    
    
    
