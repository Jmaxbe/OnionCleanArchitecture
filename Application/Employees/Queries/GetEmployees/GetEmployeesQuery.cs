using Application.Common.Interfaces;
using Application.Common.Mappings;
using AutoMapper;
using MediatR;

namespace Application.Employees.Queries.GetEmployees;

public record GetEmployeesQuery : IRequest<List<GetEmployeesDto>>
{
    
}

public class GetEmployeesQueryHandler : IRequestHandler<GetEmployeesQuery, List<GetEmployeesDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetEmployeesQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public Task<List<GetEmployeesDto>> Handle(GetEmployeesQuery request, CancellationToken cancellationToken)
    {
        return _context.Employee.ProjectToListAsync<GetEmployeesDto>(_mapper.ConfigurationProvider);
    }
}