using System.ComponentModel.DataAnnotations;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using Domain.Entities;
using Domain.Events;
using MediatR;

namespace Application.Employees.Commands.CreateEmployee;

public record CreateEmployeeCommand : IRequest<CreateEmployeeDto>
{
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string? MiddleName { get; init; }
    public string UserName { get; init; }
    public string? UserEmail { get; init; }
    public string? UserPhone { get; init; }
    public string UserPassword { get; init; }
    public bool IsMale { get; init; }
    public DateTime HireDate { get; init; }
    public DateTime BirthDate { get; init; }
}

public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, CreateEmployeeDto>
{
    private readonly IUnitOfWork _context;
    private readonly IIdentityService _identityService;
    private readonly IMapper _mapper;

    public CreateEmployeeCommandHandler(IUnitOfWork context, IIdentityService identityService, IMapper mapper)
    {
        _context = context;
        _identityService = identityService;
        _mapper = mapper;
    }
    
    public async Task<CreateEmployeeDto> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
    {
        var employee = new Employee
        {
            FirstName = request.FirstName.Trim(),
            LastName = request.LastName.Trim(),
            MiddleName = request.MiddleName?.Trim(),
            UserName = request.UserName,
            UserEmail = request.UserEmail,
            IsMale = request.IsMale,
            HireDate = request.HireDate,
            BirthDate = request.BirthDate,
        };
        
        var account = await _identityService.CreateUserAsync(new CreateAccountRequestDto
        {
            UserName = request.UserName,
            Email = request.UserEmail,
            Phone = request.UserPhone,
            Password = request.UserPassword
        });

        await _context.Employees.AddWithDomainEventAsync(employee, new EmployeeCreatedEvent(employee),
            cancellationToken);

        await _context.CompleteAsync(cancellationToken);
        
        var response = _mapper.Map<CreateEmployeeDto>(employee);
        response.UserPhone = account.Phone;

        return response;
    }
}