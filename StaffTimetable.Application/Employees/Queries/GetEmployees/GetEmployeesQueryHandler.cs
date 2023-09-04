using AutoMapper;
using MediatR;
using StaffTimetable.Application.Common.Interfaces;
using StaffTimetable.Application.Common.Models.Dto.Employees.Response;

namespace StaffTimetable.Application.Employees.Queries.GetEmployees;

public class GetEmployeesQueryHandler : IRequestHandler<GetEmployeesQuery, List<GetEmployeesResponseDto>>
{
    private readonly IUnitOfWork _context;
    private readonly IMapper _mapper;

    public GetEmployeesQueryHandler(IUnitOfWork context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<List<GetEmployeesResponseDto>> Handle(GetEmployeesQuery request, CancellationToken cancellationToken)
    {
        return _mapper.Map<List<GetEmployeesResponseDto>>(await _context.Employees.GetAllAsync(cancellationToken));
    }
}