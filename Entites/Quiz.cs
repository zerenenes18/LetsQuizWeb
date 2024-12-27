using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;

namespace LetsQuizCore.Entities
{
    public class Quiz : IEntity
    {
        public Guid Id { get; set; }
        public Guid LectureId { get; set; }
        public string Name { get; set; }
        public Guid AdminId { get; set; }
        
        
    }
}
