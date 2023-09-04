namespace StaffTimetable.Application.Common.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IEmployeeRepository Employees { get; }
    
    /// <summary>
    /// Saves changes async
    /// </summary>
    /// <param name="cancellationToken">A CancellationToken</param>
    /// <returns></returns>
    Task<int> CompleteAsync(CancellationToken cancellationToken = default);
}