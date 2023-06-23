using System.ComponentModel.DataAnnotations;
using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Employees.Commands.CreateEmployee;

public record CreateEmployeeCommand : IRequest<CreateEmployeeDto>
{
    [Required] public string FirstName { get; init; }
    [Required] public string LastName { get; init; }
    public string? MiddleName { get; init; }
    [Required] public bool IsMale { get; init; }
    [Required] public DateTime HireDate { get; init; }
    [Required] public DateTime BirthDate { get; init; }
}

public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, CreateEmployeeDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public CreateEmployeeCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<CreateEmployeeDto> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
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

        _context.Employee.Add(employee);

        await _context.SaveChangesAsync(cancellationToken);

        return _mapper.Map<CreateEmployeeDto>(employee);
    }
}