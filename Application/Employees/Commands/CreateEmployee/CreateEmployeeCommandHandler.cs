using Application.Common.Interfaces;
using Application.Common.Models.Dto.Employees;
using Application.Common.Models.Dto.Employees.Response;
using AutoMapper;
using Domain.Entities;
using Domain.Events;
using MediatR;

namespace Application.Employees.Commands.CreateEmployee;

public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, CreateEmployeeResponseDto>
{
    private readonly IUnitOfWork _context;
    private readonly IMapper _mapper;

    public CreateEmployeeCommandHandler(IUnitOfWork context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<CreateEmployeeResponseDto> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
    {
        var employee = new Employee
        {
            FirstName = request.FirstName.Trim(),
            LastName = request.LastName.Trim(),
            MiddleName = request.MiddleName?.Trim(),
            IsMale = request.IsMale,
            HireDate = request.HireDate,
            BirthDate = request.BirthDate,
        };

        await _context.Employees.AddWithDomainEventAsync(employee, new EmployeeCreatedEvent(employee),
            cancellationToken);

        await _context.CompleteAsync(cancellationToken);
        
        var response = _mapper.Map<CreateEmployeeResponseDto>(employee);

        return response;
    }
}