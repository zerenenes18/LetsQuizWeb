using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;

namespace LetsQuizCore.Entities
{
    public class Question : IEntity
    {
        public Guid Id { get; set; }
        public Guid QuizId { get; set; }
        public string? Description { get; set; }
        public string? ImagePath { get; set; }
    }
}
