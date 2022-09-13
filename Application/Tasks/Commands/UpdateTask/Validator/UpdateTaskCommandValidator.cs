using Domain.Common.Interfaces;
using Domain.Entities;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Tasks.Commands.UpdateTask.Validator
{
    public class UpdateTaskCommandValidator : AbstractValidator<UpdateTaskCommand>
    {
        public readonly IRepository<ToDoTask, int> _taskRepository;

        public UpdateTaskCommandValidator(IRepository<ToDoTask, int> taskRepository)
        {
            _taskRepository = taskRepository;

            RuleFor(v => v.Name)
                .NotEmpty().WithMessage("Name is required!")
                .MaximumLength(30).WithMessage("Name must net exceed 30 characters.")
                .MustAsync(BeUniqueTitle).WithMessage("The specified name already exists");
        }

        public async Task<bool> BeUniqueTitle(UpdateTaskCommand model, string name, CancellationToken cancellationToken)
        {
            return await _taskRepository.GetAll()
                .Where(t => t.Id != model.ID)
                .AllAsync(t => t.Name != name, cancellationToken);
        }
    }
}
