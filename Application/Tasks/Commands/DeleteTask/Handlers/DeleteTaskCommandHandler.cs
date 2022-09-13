using Application.Common.Exceptions;
using Domain.Common.Interfaces;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Tasks.Commands.DeleteTask.Handlers
{
    public class DeleteTaskCommandHandler : IRequestHandler<DeleteTaskCommand>
    {
        public readonly IRepository<ToDoTask, int> _taskRepository;

        public DeleteTaskCommandHandler(IRepository<ToDoTask, int> taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<Unit> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
        {
            var entity = await _taskRepository.GetFirst(request.ID);

            if (entity is null) throw new NotFoundException(nameof(ToDoTask), request.ID);

            await _taskRepository.Delete(entity);

            await _taskRepository.Commit(cancellationToken);

            return Unit.Value;
        }
    }
}
