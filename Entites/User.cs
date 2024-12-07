using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;

namespace LetsQuizCore.Entities
{
    public class User : IEntity
    {
        public Guid Id { get; set; }
        public Guid QuizId { get; set; }
        public string UserName { get; set; }
    }
}
