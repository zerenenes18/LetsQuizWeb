using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;

namespace LetsQuizCore.Entities
{
    public class Lecture : IEntity
    {
        public Guid Id { get; set; }
        public Guid AdminId { get; set; }
        public string? Name { get; set; }
    }
}
