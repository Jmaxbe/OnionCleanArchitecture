using Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Employees.EventHandlers;

public class EmployeeCreatedEventHandler : INotificationHandler<EmployeeCreatedEvent>
{
    private readonly ILogger<EmployeeCreatedEventHandler> _logger;

    public EmployeeCreatedEventHandler(ILogger<EmployeeCreatedEventHandler> logger)
    {
        _logger = logger;
    }
    
    public Task Handle(EmployeeCreatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("OnionCleanArchitecture Domain Event: {DomainEvent}", notification.GetType().Name);
        
        return Task.CompletedTask;
    }
}