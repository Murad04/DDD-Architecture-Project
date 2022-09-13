using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Tasks.Commands.DeleteTask
{
    public class DeleteTaskCommand:IRequest
    {
        public int ID { get; set; }
    }
}
