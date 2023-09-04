using Microsoft.Extensions.Logging;
using StaffTimetable.Domain.Events;

namespace StaffTimetable.Application.Employees.EventHandlers;

public class EmployeeCreatedEventHandler
{
    private readonly ILogger<EmployeeCreatedEventHandler> _logger;

    public EmployeeCreatedEventHandler(ILogger<EmployeeCreatedEventHandler> logger)
    {
        _logger = logger;
    }
    
    public Task Handle(EmployeeCreatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("OnionCleanArchitecture StaffTimetable.Domain Event: {DomainEvent}", notification.GetType().Name);
        
        return Task.CompletedTask;
    }
}