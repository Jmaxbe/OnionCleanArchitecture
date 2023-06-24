using Application.Common.Interfaces;
using Application.Common.Mappings;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Employees.Queries.GetEmployees;

public record GetEmployeesQuery : IRequest<List<GetEmployeesDto>>
{
    
}

public class GetEmployeesQueryHandler : IRequestHandler<GetEmployeesQuery, List<GetEmployeesDto>>
{
    private readonly IUnitOfWork _context;
    private readonly IMapper _mapper;

    public GetEmployeesQueryHandler(IUnitOfWork context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<List<GetEmployeesDto>> Handle(GetEmployeesQuery request, CancellationToken cancellationToken)
    {
        return _mapper.Map<List<GetEmployeesDto>>(await _context.Employees.GetAllAsync(cancellationToken));
    }
}