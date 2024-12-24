using Core.Entities;
using LetsQuizCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DataAccess.EntityFramework;

public class LetsQuizDbContext : DbContext
{
    private readonly IConfiguration _configuration;

   
    public LetsQuizDbContext(DbContextOptions<LetsQuizDbContext> options):base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            var idProperty = entityType.FindProperty("Id");
            if (idProperty != null && idProperty.ClrType == typeof(Guid))
            {
                idProperty.SetDefaultValueSql("newid()");
            }
        }
        base.OnModelCreating(modelBuilder);
    }

    public DbSet<User> Users { get; set; }

    public DbSet<Student> Students { get; set; }
    public DbSet<Admin> Admins { get; set; }
    public DbSet<Lecture> Lectures { get; set; }
    public DbSet<Quiz> Quizzes { get; set; }
    public DbSet<Question> Questions { get; set; }
    public DbSet<Option> Options { get; set; }
    public DbSet<OperationClaim> OperationClaims { get; set; }
    public DbSet<VerificationCode> VerificationCodes { get; set; }
    public DbSet<UserOperationClaim> UserOperationClaims { get; set; }
}