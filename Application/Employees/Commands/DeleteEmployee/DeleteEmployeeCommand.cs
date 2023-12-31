﻿using MediatR;

namespace Application.Employees.Commands.DeleteEmployee;

public record DeleteEmployeeCommand : IRequest
{
    public DeleteEmployeeCommand(Guid id)
    {
        Id = id;
    }
    
    public Guid Id { get; init; }
}