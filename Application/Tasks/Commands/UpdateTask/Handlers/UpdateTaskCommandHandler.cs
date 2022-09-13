using Application.Common.Exceptions;
using AutoMapper;
using Domain.Common.Interfaces;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Tasks.Commands.UpdateTask.Handlers
{
    public class UpdateTaskCommandHandler : IRequestHandler<UpdateTaskCommand>
    {
        private readonly IRepository<ToDoTask, int> _taskRepository;
        private readonly IMapper _mapper;

        public UpdateTaskCommandHandler(IRepository<ToDoTask, int> taskRepository, IMapper mapper)
        {
            _taskRepository = taskRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
        {
            var task = await _taskRepository.GetFirst(request.ID);

            if (task is null) throw new NotFoundException(nameof(ToDoTask), request.ID);

            _mapper.Map(request, task);

            await _taskRepository.Commit(cancellationToken);

            return Unit.Value;
        }
    }
}
