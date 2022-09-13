using Application.Tasks.Queries.ViewModel;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Tasks.Queries.GetTasks
{
    public class GetTasksQuery:IRequest<TaskView>
    {
        public string Name { get; set; } = null!;
    }
}
