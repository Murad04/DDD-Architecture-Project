using System.Threading;
using System.Threading.Tasks;
using Domain.Common.Interfaces;
using Domain.Entities;
using FluentValidation;

namespace Application.Tasks.Commands.CreateTask.Validator
{
    public class CreateTaskCommandValidator:AbstractValidator<CreateTaskCommand>
    {
        private readonly IRepository<ToDoTask, int> _taskRepository;

        public CreateTaskCommandValidator(IRepository<ToDoTask, int> taskRepository)
        {
            _taskRepository = taskRepository;

            RuleFor(v => v.Name)
                .NotEmpty().WithMessage("Name is required!")
                .MaximumLength(30).WithMessage("Name must net exceed 30 characters.")
                .MustAsync(BeUniqueTitle).WithMessage("The specified name already exists");
        }

        public async Task<bool> BeUniqueTitle(string name,CancellationToken cancellationToken)
        {
            return await _taskRepository.All(t => t.Name != name);
        }
    }
}
