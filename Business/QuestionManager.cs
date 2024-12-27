using Business.Abstract;
using Core.Extensions;
using Core.Utilities.Results;
using DataAccess.Abstract;
using LetsQuizCore.Entities;
using Microsoft.AspNetCore.Http;
using IResult = Core.Utilities.Results.IResult;

namespace Business;

public class QuestionManager : IQuestionService
{
    
    IQuestionDal _questionDal;
    IQuizDal _quizDal;
    private readonly IOptionDal _optionDal;
    private IHttpContextAccessor _httpContextAccessor;
    
    
    public QuestionManager(IQuestionDal questionDal,IHttpContextAccessor httpContextAccessor,IQuizDal quizDal,IOptionDal optionDal)
    {
        _questionDal = questionDal;
        _httpContextAccessor = httpContextAccessor;
        _quizDal = quizDal;
        _optionDal = optionDal;
    }
    
    
    public async Task<IDataResult<List<Question>>> GetAllAsync()
    {
        return new SuccessDataResult<List<Question>>( await _questionDal.GetAllAsync());
    }

    public async Task<IResult> DeleteAsync(Question question)
    {
        var options = await _optionDal.GetAllAsync(o=>o.QuestionId == question.Id);
        foreach (var option in options)
        {
           await _optionDal.DeleteAsync(option);
        }
        
       await _questionDal.DeleteAsync(question);
       return new SuccessResult();
    }

    public async Task<IDataResult<Question>> GetByIdAsync(Guid id)
    {
        return new SuccessDataResult<Question>(await _questionDal.GetAsync(l => l.Id == id));
    }
    

    public async Task<IResult> AddAsync(Question question)
    {
       
        _questionDal.AddAsync(question);
        return new SuccessResult();
    }

    public async Task<IDataResult<List<Question>>> GetQuestionsByQuizIdAsync(Guid quizId)
    {
        //var adminId = Guid.Parse(_httpContextAccessor.HttpContext.User.ClaimIdentifier());
        var questions = await _questionDal.GetAllAsync(q => q.QuizId == quizId);
        return new SuccessDataResult<List<Question>>(questions);     
       
    }

    public async Task<IDataResult<List<Question>>> GetQuestionsByQuizNameAsync(string quizName)
    {
        var adminId = Guid.Parse(_httpContextAccessor.HttpContext.User.ClaimIdentifier());
        List<Quiz> quizzes = await _quizDal.GetAllAsync(q=> q.AdminId == adminId);
        var quiz = quizzes.SingleOrDefault(q=> q.Name == quizName);
        var questions = await _questionDal.GetAllAsync(q => q.QuizId == quiz.Id);
        
        return new SuccessDataResult<List<Question>>(questions);
    }
}