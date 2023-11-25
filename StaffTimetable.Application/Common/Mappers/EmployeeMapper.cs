using System.Runtime.CompilerServices;
using Riok.Mapperly.Abstractions;
using StaffTimetable.Application.Common.Models.Dto.Employees.Request;
using StaffTimetable.Application.Common.Models.Dto.Keycloak.Request;
using StaffTimetable.Application.Employees.Commands.CreateEmployee;
using StaffTimetable.Application.Employees.Commands.UpdateEmployee;
using StaffTimetable.Domain.Entities;

namespace StaffTimetable.Application.Common.Mappers;

[Mapper]
public static partial class EmployeeMapper
{
    [MapProperty(nameof(CreateEmployeeCommand.FirstName), nameof(CreateUserDto.FirstName))]
    [MapProperty(nameof(CreateEmployeeCommand.LastName), nameof(CreateUserDto.LastName))]
    [MapProperty(nameof(CreateEmployeeCommand.UserName), nameof(CreateUserDto.Username))]
    [MapProperty(nameof(CreateEmployeeCommand.Email), nameof(CreateUserDto.Email))]
    public static partial CreateUserDto MapToCreateUserDto(this CreateEmployeeCommand createEmployeeCommand);

    public static partial CreateEmployeeCommand MapToCreateEmployeeCommand(this CreateEmployeeRequestDto createEmployeeRequest);
    
    [MapProperty(nameof(UpdateEmployeeCommand.Id), "id")]
    public static partial UpdateEmployeeCommand MapToUpdateEmployeeCommand(this UpdateEmployeeRequestDto updateEmployeeRequest);//TODO:Засунуть ID
    
    public static partial Employee MapToEmployee(this CreateEmployeeCommand createEmployeeCommand);
    
    
}