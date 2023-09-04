using MediatR;
using StaffTimetable.Application.Common.Exceptions;
using StaffTimetable.Application.Common.Interfaces;
using StaffTimetable.Domain.Entities;

namespace StaffTimetable.Application.Employees.Commands.DeleteEmployee;

public class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand>
{
    private readonly IUnitOfWork _context;

    public DeleteEmployeeCommandHandler(IUnitOfWork context)
    {
        _context = context;
    }
    
    public async Task Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
    {
        var employee = await _context.Employees.GetByIdAsync(request.Id, cancellationToken);

        if (employee is null)
        {
            throw new NotFoundException(nameof(Employee), request.Id);
        }

        _context.Employees.Remove(employee);

        await _context.CompleteAsync(cancellationToken);
    }
}