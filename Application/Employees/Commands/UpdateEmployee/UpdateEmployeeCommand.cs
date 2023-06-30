using System.ComponentModel.DataAnnotations;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Employees.Commands.UpdateEmployee;

public record UpdateEmployeeCommand : IRequest<UpdateEmployeeDto>
{
    public int Id { get; set; }
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string? MiddleName { get; init; }
    public string UserName { get; init; }
    public string? UserEmail { get; init; }
    public string? UserPhone { get; init; }
    public bool IsMale { get; init; }
    public DateTime HireDate { get; init; }
    public DateTime BirthDate { get; init; }
}

public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand, UpdateEmployeeDto>
{
    private readonly IUnitOfWork _context;
    private readonly IMapper _mapper;
    private readonly IIdentityService _identityService;

    public UpdateEmployeeCommandHandler(IUnitOfWork context, IMapper mapper, IIdentityService identityService)
    {
        _context = context;
        _mapper = mapper;
        _identityService = identityService;
    }
    
    public async Task<UpdateEmployeeDto> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
    {
        var employee = await _context.Employees.GetByIdAsync(request.Id, cancellationToken);

        if (employee is null)
        {
            throw new NotFoundException(nameof(Employee), request.Id);
        }

        var updateAccount = await _identityService.UpdateUserCredentials(new UpdateAccountRequestDto
        {
            InitialUserName = employee.UserName,
            UserName = request.UserName,
            Email = request.UserEmail,
            Phone = request.UserPhone
        });

        employee.FirstName = request.FirstName.Trim();
        employee.LastName = request.LastName.Trim();
        employee.MiddleName = request.MiddleName?.Trim();
        employee.UserName = request.UserName;
        employee.UserEmail = request.UserEmail;
        employee.BirthDate = request.BirthDate;
        employee.HireDate = request.HireDate;
        employee.IsMale = request.IsMale;

        _context.Employees.Update(employee);

        await _context.CompleteAsync(cancellationToken);

        var response = _mapper.Map<UpdateEmployeeDto>(employee);
        response.UserPhone = updateAccount.Phone;
        
        return response;
    }
}