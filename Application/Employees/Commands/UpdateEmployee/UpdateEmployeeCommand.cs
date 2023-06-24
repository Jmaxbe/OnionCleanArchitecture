using System.ComponentModel.DataAnnotations;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Employees.Commands.UpdateEmployee;

public record UpdateEmployeeCommand : IRequest<UpdateEmployeeDto>
{
    [Required] public int Id { get; set; }
    [Required] public string FirstName { get; init; }
    [Required] public string LastName { get; init; }
    public string? MiddleName { get; init; }
    [Required] public bool IsMale { get; init; }
    [Required] public DateTime HireDate { get; init; }
    [Required] public DateTime BirthDate { get; init; }
}

public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand, UpdateEmployeeDto>
{
    private readonly IUnitOfWork _context;
    private readonly IMapper _mapper;

    public UpdateEmployeeCommandHandler(IUnitOfWork context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<UpdateEmployeeDto> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
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

        await _context.SaveChangesAsync(cancellationToken);
        
        return _mapper.Map<UpdateEmployeeDto>(employee);
    }
}