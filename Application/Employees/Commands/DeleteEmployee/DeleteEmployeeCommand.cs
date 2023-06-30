using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Employees.Commands.DeleteEmployee;

public record DeleteEmployeeCommand(int Id) : IRequest;

public class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand>
{
    private readonly IUnitOfWork _context;
    private readonly IIdentityService _identityService;

    public DeleteEmployeeCommandHandler(IUnitOfWork context, IIdentityService identityService)
    {
        _context = context;
        _identityService = identityService;
    }
    
    public async Task Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
    {
        var employee = await _context.Employees.GetByIdAsync(request.Id, cancellationToken);

        if (employee is null)
        {
            throw new NotFoundException(nameof(Employee), request.Id);
        }

        await _identityService.DeleteUserByName(employee.UserName);

        _context.Employees.Remove(employee);

        await _context.CompleteAsync(cancellationToken);
    }
}