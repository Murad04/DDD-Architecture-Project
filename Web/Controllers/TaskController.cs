using Application.Tasks.Commands.CreateTask;
using Application.Tasks.Commands.DeleteTask;
using Application.Tasks.Commands.UpdateTask;
using Application.Tasks.Queries.GetTasks;
using Application.Tasks.Queries.GetTasks.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class TaskController:ApiController
    {
        [HttpGet("{taskName}")]
        public async Task<ActionResult<TaskView>> GetByName(string taskName)
        {
            return await Mediator.Send(new GetTasksQuery() { Name = taskName });
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create(CreateTaskCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut("{taskID}")]
        public async Task<ActionResult> Update(int taskID,UpdateTaskCommand command)
        {
            if (taskID != command.ID) return BadRequest();
            await Mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{taskID}")]
        public async Task<ActionResult> Delete(int taskID,DeleteTaskCommand command)
        {
            await Mediator.Send(new DeleteTaskCommand { ID = taskID });
            return NoContent();
        }
    }
}
