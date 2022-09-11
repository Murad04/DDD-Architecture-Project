using Domain.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Person:BaseEntity<int>
    {
        public string? Name { get; set; } 

        public string? SurName { get; set; } 

        public short Age { get; set; }

        public ICollection<ToDoTask>? Tasks { get; set; }
    }
}
