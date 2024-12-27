using Business.Abstract;
using Core.Extensions;
using Core.IoC;
using Core.Utilities.Results;
using DataAccess.Abstract;
using LetsQuizCore.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using IResult = Core.Utilities.Results.IResult;

namespace Business;

public class QuizManager : IQuizService
{
    private readonly IQuizDal _quizDal;
    private readonly IQuestionService _questionService;

    private IHttpContextAccessor _httpContextAccessor;
    
    public QuizManager(IQuizDal quizDal,IQuestionService questionService )
    {
        _quizDal = quizDal;
        _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
        _questionService = questionService;
    }    
    
    public async Task<IDataResult<List<Quiz>>> GetAllAsync()
    {
        return new SuccessDataResult<List<Quiz>>( await _quizDal.GetAllAsync());
    }

    public async Task<IResult> DeleteAsync(Quiz quiz)
    {
        var questions = await _questionService.GetAllAsync();
        var myQuestions = questions.Data.Where(q=> q.QuizId == quiz.Id).ToList();
        foreach (var question in myQuestions)
        {
            await _questionService.DeleteAsync(question);
        }
        
      await _quizDal.DeleteAsync(quiz);
     return new SuccessResult();
    }

    public async Task<IDataResult<Quiz>> GetByIdAsync(Guid id)
    {
        return new SuccessDataResult<Quiz>(await _quizDal.GetAsync(l => l.Id == id));
    }

    public async Task<IDataResult<Quiz>> GetByNameAsync(string name)
    {
        var adminId = Guid.Parse(_httpContextAccessor.HttpContext.User.ClaimIdentifier());
        var allQuizzes = await _quizDal.GetAllAsync(q=> q.AdminId == adminId);
        
        return new SuccessDataResult<Quiz>(allQuizzes.SingleOrDefault(q=> q.Name == name));
    }
    
    
    public async Task<IDataResult<List<Quiz>>> GetAdminQuizzesAsync()
    {
        var adminId = Guid.Parse(_httpContextAccessor.HttpContext.User.ClaimIdentifier());
        var allQuizzes = await _quizDal.GetAllAsync(q=> q.AdminId == adminId);
        //var adminQuizzes = allQuizzes.Where(q=> q.AdminId == adminId).ToList();
        
        return new SuccessDataResult<List<Quiz>>(allQuizzes);
    }

    public async Task<IResult> AddToAdminAsync(string quizName, Guid lectureId)
    {
        var adminId = Guid.Parse(_httpContextAccessor.HttpContext.User.ClaimIdentifier());
        var addedQuiz = new Quiz { Name = quizName, LectureId = lectureId, AdminId = adminId };
        await _quizDal.AddAsync(addedQuiz);
        return new SuccessResult();
    }

    public async Task<IResult> AddAsync(Quiz quiz)
    {
        //var test = _httpContextAccessor.HttpContext.User.Claims;
        var adminId = Guid.Parse(_httpContextAccessor.HttpContext.User.ClaimIdentifier());
        quiz.AdminId = adminId;
        _quizDal.AddAsync(quiz);
        return new SuccessResult();
    }
}