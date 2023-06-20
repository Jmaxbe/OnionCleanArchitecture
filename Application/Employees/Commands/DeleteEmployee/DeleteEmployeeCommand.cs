using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Employees.Commands.DeleteEmployee;

public record DeleteEmployeeCommand(int Id) : IRequest;

public class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteEmployeeCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
    {
        var employee = await _context.Employee.FindAsync(new object[] { request.Id }, cancellationToken);

        if (employee is null)
        {
            throw new NotFoundException(nameof(Employee), request.Id);
        }

        _context.Employee.Remove(employee);

        await _context.SaveChangesAsync(cancellationToken);
    }
}