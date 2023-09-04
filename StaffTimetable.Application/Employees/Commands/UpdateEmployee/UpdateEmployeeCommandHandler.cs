using AutoMapper;
using MediatR;
using StaffTimetable.Application.Common.Exceptions;
using StaffTimetable.Application.Common.Interfaces;
using StaffTimetable.Application.Common.Models.Dto.Employees.Response;
using StaffTimetable.Domain.Entities;

namespace StaffTimetable.Application.Employees.Commands.UpdateEmployee;

public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand, UpdateEmployeeResponseDto>
{
    private readonly IUnitOfWork _context;
    private readonly IMapper _mapper;
    
    public UpdateEmployeeCommandHandler(IUnitOfWork context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<UpdateEmployeeResponseDto> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
    {
        var employee = await _context.Employees.GetByIdAsync(request.Id, cancellationToken);

        if (employee is null)
        {
            throw new NotFoundException(nameof(Employee), request.Id);
        }

        employee.FirstName = request.FirstName.Trim();
        employee.LastName = request.LastName.Trim();
        employee.MiddleName = request.MiddleName?.Trim();
        employee.BirthDate = request.BirthDate;
        employee.HireDate = request.HireDate;
        employee.IsMale = request.IsMale;

        _context.Employees.Update(employee);

        await _context.CompleteAsync(cancellationToken);

        var response = _mapper.Map<UpdateEmployeeResponseDto>(employee);

        return response;
    }
}