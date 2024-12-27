using Business.Abstract;
using Business.Operations;
using Core.Extensions;
using Core.IoC;
using DataAccess.Abstract;
using LetsQuizCore.Entities;
using LetsQuizCore.Entities.DTOs;
using LetsQuizWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace LetsQuizWeb.Controllers;

public class QuizController : Controller
{
    ILectureService _lectureService;
    IQuestionService _questionService;
    IQuizService _quizService;
    IOptionDal _optionDal;
    IUserService _userService;
    IScoreDal _scoreDal;
    private IHttpContextAccessor _httpContextAccessor;
    
    
    public QuizController(ILectureService lectureService,IQuizService quizService,IQuestionService questionService,IOptionDal optionDal,IUserService userService,IScoreDal scoreDal)
    {
        _lectureService  = lectureService;
        _quizService = quizService;
        _optionDal  = optionDal;
        _questionService = questionService;
        _userService = userService;
        _scoreDal = scoreDal;
        _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
    }
    // GET
    public async Task<IActionResult> Index()
    {
        return View();
    }
    
    [SecuredOperation("student,admin")]
    public async Task<IActionResult> AddLecture(string name)
    {
        Lecture lecture = new Lecture
        {
            Name = name,
        };
         await _lectureService.AddAsync(lecture);
        return Ok();
    }
    
    
    [HttpPost]
    public async Task<IActionResult> AddQuiz( [FromBody]QuizAddDto quizAddDto)
    {
        if (string.IsNullOrEmpty(quizAddDto.QuizName) || string.IsNullOrEmpty(quizAddDto.LectureName))
        {
            return BadRequest("Quiz name or lecture is missing.");
        }
        var lecture = await _lectureService.GetByNameAsync(quizAddDto.LectureName);
        var quizzes = await _quizService.GetAllAsync();
        var result = await _quizService.AddToAdminAsync(quizAddDto.QuizName, lecture.Data.Id);
        if (result.Success)
        {
            return Ok(result.Message);
        }
        else
        {
            return BadRequest(result.Message);
        }
    }

    public async Task<IActionResult> GetQuizContent(string quizName)
    {
        var contentModel = new QuizContentModel(); 
        List<QuestionModel> questionList = new List<QuestionModel>();
        var questions = await _questionService.GetQuestionsByQuizNameAsync(quizName);
        var Quiz =  await _quizService.GetByNameAsync(quizName);
        var Lecture = await _lectureService.GetByIdAsync(Quiz.Data.LectureId);
        foreach (var question in questions.Data)
        {
            var optionModelList = new List<OptionModel>();
            var options = await _optionDal.GetAllAsync(o=> o.QuestionId == question.Id);
            foreach (var option in options)
            {
                optionModelList.Add(new OptionModel
                {
                    OptionId = option.Id,
                    OptionImagePath = option.ImagePath,
                    OptionText = option.Description,
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
        contentModel.questions = questionList;
        contentModel.LectureName = Lecture.Data.Name;
        contentModel.QuizName = quizName;
        if (questions.Data == null || !questions.Data.Any())
        {
            return Ok(new QuizContentModel
                {
                    QuizName = quizName,
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
    
    public async Task<IActionResult> AddQuestion([FromBody] AddQuestionRequest request)
        {
            if (request == null || string.IsNullOrEmpty(request.QuizName) || request.Question == null)
            {
                return BadRequest("Invalid request data.");
            }

            var quizResult = await _quizService.GetByNameAsync(request.QuizName);
            if (!quizResult.Success || quizResult.Data == null)
            {
                return NotFound($"Quiz with name '{request.QuizName}' not found.");
            }

            var newQuestion = new Question
            {
                QuizId = quizResult.Data.Id,
                Description = request.Question.QuestionText,
                ImagePath = request.Question.QuestionImagePath,
                QuestionScore = request.Question.QuestionScore,
                QuestionSecondTime = request.Question.QuestionSecondTime
            };

            var result = await _questionService.AddAsync(newQuestion);
            if (result.Success)
            {
                return Ok("Question added successfully.");
            }

            return StatusCode(500, "Failed to add the question.");
        }

    
    
    public async Task<IActionResult> AddOptions([FromBody] AddOptionsRequest request)
        {
            if (request == null || string.IsNullOrEmpty(request.QuestionId.ToString()) || request.Options == null || !request.Options.Any())
            {
                return BadRequest("Invalid request data.");
            }

            var questionResult = await _questionService.GetByIdAsync(request.QuestionId);
            if (!questionResult.Success || questionResult.Data == null)
            {
                return NotFound($"Question with ID '{request.QuestionId}' not found.");
            }

            foreach (var option in request.Options)
            {
                var newOption = new Option
                {
                    QuestionId = request.QuestionId,
                    Description = option.OptionText,
                    IsTrue = option.IsTrue,
                    ImagePath = option.OptionImagePath
                };
                await _optionDal.AddAsync(newOption);
              
            }

            return Ok("Options added successfully.");
        }
    
    
    [HttpDelete]
    public async Task<IActionResult> DeleteQuestion([FromBody] DeleteQuestionModel model)
    {
       var deletedQuestion = await _questionService.GetByIdAsync(model.QuestionId);
       _questionService.DeleteAsync(deletedQuestion.Data);
       
       return Ok("Question deleted successfully.");
    }
    
    [HttpDelete]
    public async Task<IActionResult> DeleteOption([FromBody] DeleteOptionModel model)
    {
        var deletedQuestion = await _optionDal.GetAsync(o=>o.Id ==   model.OptionId);
        _optionDal.DeleteAsync(deletedQuestion);
       
        return Ok("Question deleted successfully.");
    }
    
    
    
    public async Task<IActionResult> QuizDetail(string quizId)
    {
        Guid quizIdGuid = Guid.Parse(quizId);
        var quiz = await _quizService.GetByIdAsync(quizIdGuid);
        var lecture = await _lectureService.GetByIdAsync(quiz.Data.LectureId);
        var admin = await _userService.GetByIdAsync(quiz.Data.AdminId);
        
        var questionlist = await _questionService.GetQuestionsByQuizIdAsync(quizIdGuid);
        int duration = 0;
        foreach (var question in questionlist.Data)
        {
            duration += question.QuestionSecondTime;
        }
        QuizDetailModel detailModel = new QuizDetailModel();
        
       
        detailModel.QuizName = quiz.Data.Name;
        detailModel.LectureName = lecture.Data.Name;
        detailModel.AdminId = admin.Data.Id;
        detailModel.AdminName = admin.Data.FirstName + " " + admin.Data.LastName;
        detailModel.QuestionCount = questionlist.Data.Count;
        detailModel.ExamDuration = duration;
        detailModel.QuizId = quiz.Data.Id;
        
        return View(detailModel);
    }
    
    
    [HttpPost]
    public async Task<IActionResult> QuickStart(QuizDetailModel model)
    {
        var quiz = await _quizService.GetByIdAsync(model.QuizId);
        if (quiz?.Data == null)
        {
            return NotFound("Quiz not found");
        }

        QuizContentModel quizContentModel = new QuizContentModel
        {
            QuizId = quiz.Data.Id,
            QuizName = quiz.Data.Name,
            questions = new List<QuestionModel>() // Başlatılıyor
        };

        var questionList = await _questionService.GetQuestionsByQuizIdAsync(model.QuizId);
        if (questionList?.Data == null || !questionList.Data.Any())
        {
            return NotFound("No questions found for this quiz");
        }

        foreach (var question in questionList.Data)
        {
            QuestionModel questionModel = new QuestionModel
            {
                QuestionId = question.Id,
                QuestionText = question.Description,
                QuestionScore = question.QuestionScore,
                QuestionImagePath = question.ImagePath,
                QuestionSecondTime = question.QuestionSecondTime,
                Options = new List<OptionModel>() // Başlatılıyor
            };

            var options = await _optionDal.GetAllAsync(o => o.QuestionId == questionModel.QuestionId);
            if (options != null && options.Any())
            {
                foreach (var option in options)
                {
                    questionModel.Options.Add(
                        new OptionModel
                        {
                            OptionId = option.Id,
                            OptionImagePath = option.ImagePath ?? "",
                            OptionText = option.Description,
                            IsTrue = option.IsTrue
                        });
                }
            }

            quizContentModel.questions.Add(questionModel);
        }

        if (quizContentModel != null)
        {
            return View(quizContentModel);
        }

        return BadRequest("Model is invalid");
    }
        

    public async Task<QuizzesModel> GetQuizzesPageData()
    {
        
        var lectures = await _lectureService.GetAdminLecturesAsync();
        List<string> lectureNames = new List<string>();
        foreach (var lectureName in lectures.Data)
        {
            lectureNames.Add(lectureName.Name);
        }
       
        var Quizzes = await _quizService.GetAdminQuizzesAsync();
        List<AdminQuizzesModel> adminQuizzes = new List<AdminQuizzesModel>();
        foreach (var quiz in Quizzes.Data)
        {
            var lectureName = await _lectureService.GetByIdAsync(quiz.LectureId);
            adminQuizzes.Add(new AdminQuizzesModel
            {
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
  
    public async Task<QuizzesModel> GetGlobalQuizzesData()
    {
        
        var lectures = await _lectureService.GetAllAsync();
        List<string> lectureNames = new List<string>();
        foreach (var lectureName in lectures.Data)
        {
            lectureNames.Add(lectureName.Name);
        }
       
        var QuizzesResult = await _quizService.GetAllAsync();
        List<AdminQuizzesModel> QuizList = new List<AdminQuizzesModel>();
        foreach (var quiz in QuizzesResult.Data)
        {
            var lectureName = await _lectureService.GetByIdAsync(quiz.LectureId);
            QuizList.Add(new AdminQuizzesModel
            {
                QuizId = quiz.Id,
                LectureName = lectureName.Data.Name,
                QuizName = quiz.Name,
            });
        }
        
        return new QuizzesModel
        {
            adminQuizzesModel = QuizList,
            lectureList = lectureNames
        };

    }



    [HttpPost]
    public async Task<IActionResult> FinishQuiz([FromBody] List<QuizResultModel> results)
    {
        if (results == null || !results.Any())
        {
            return BadRequest("No results received.");
        }
        var adminId = Guid.Parse(_httpContextAccessor.HttpContext.User.ClaimIdentifier());
        // Quiz ID'yi tüm sonuçlardan al
        var quizId = results.First().QuizId;
        var quiz = await _quizService.GetByIdAsync(quizId);
        if (quiz == null || quiz.Data == null)
        {
            return NotFound("Quiz not found.");
        }
        Score NewScore = new Score();
        NewScore.UserId = adminId;
        var user  = await _userService.GetByIdAsync(adminId) ;
        NewScore.UserName = user.Data.FirstName + " " + user.Data.LastName;
        NewScore.QuizId = quiz.Data.Id;
        NewScore.QuizName = quiz.Data.Name;
        NewScore.CreatedAt = DateTime.Now;
        double scoreValueTmp = 0;
        decimal successValueTmp = 0;
        int isTrueCount = 0;
        foreach (var result in results)
        {
            
            if (result.IsTrue)
            {
                isTrueCount++;
            }
            scoreValueTmp += result.Score;
        }
        NewScore.ScoreValue = scoreValueTmp;
        successValueTmp = (isTrueCount / results.Count) * 100; 
        NewScore.SuccessRate = successValueTmp;
        
        await _scoreDal.AddAsync(NewScore);
        
        
        return Ok(new
        {
            Message = "Results submitted successfully.",
            TotalScore = scoreValueTmp,
            SuccessRate = successValueTmp
        });
    }

// QuizResultModel sınıfı
    public class QuizResultModel
    {
        public Guid QuestionId { get; set; }
        public Guid QuizId { get; set; }
        public int Time { get; set; }
        public double Score { get; set; }
        public bool IsTrue { get; set; }
    }


}