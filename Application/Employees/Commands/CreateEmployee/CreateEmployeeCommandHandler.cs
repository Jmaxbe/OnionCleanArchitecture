﻿using Application.Common.Interfaces;
using Application.Common.Models.Dto.Employees;
using Application.Common.Models.Dto.Employees.Response;
using Application.Common.Models.Dto.Keycloak.Request;
using AutoMapper;
using Domain.Entities;
using Domain.Events;
using MediatR;

namespace Application.Employees.Commands.CreateEmployee;

public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, CreateEmployeeResponseDto>
{
    private readonly IUnitOfWork _context;
    private readonly IMapper _mapper;
    private readonly IKeyCloakApi _keyCloakApi;

    public CreateEmployeeCommandHandler(IUnitOfWork context, IMapper mapper, IKeyCloakApi keyCloakApi)
    {
        _context = context;
        _mapper = mapper;
        _keyCloakApi = keyCloakApi;
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
            UserEmail = request.Email
        };
        
        var keycloakUser = _mapper.Map<CreateUserDto>(request);
        keycloakUser.Credentials = new List<Credentials>()
        {
            new Credentials
            {
                Type = "password",
                Value = request.Password,
                Temporary = false
            }
        };

        var userId = await _keyCloakApi.CreateUser(keycloakUser);

        employee.ExternalUserId = userId;
        
        await _context.Employees.AddWithDomainEventAsync(employee, new EmployeeCreatedEvent(employee),
            cancellationToken);

        await _context.CompleteAsync(cancellationToken);
        
        var response = _mapper.Map<CreateEmployeeResponseDto>(employee);

        return response;
    }
}