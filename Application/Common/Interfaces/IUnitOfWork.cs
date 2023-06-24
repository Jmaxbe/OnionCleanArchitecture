using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IEmployeeRepository Employees { get; }
    
    /// <summary>
    /// DbContext Class SaveChangesAsync method
    /// </summary>
    /// <param name="cancellationToken">A CancellationToken</param>
    /// <returns></returns>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}