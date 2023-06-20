﻿using Application.Common.Mappings;
using Domain.Entities;

namespace Application.Employees.Commands.CreateEmployee;

public class CreateEmployeeDto : IMapFrom<Employee>
{
    public int Id { get; set; }
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string? MiddleName { get; init; }
    public string FullName { get; init; }
    public bool IsMale { get; set; }
    public DateTime HireDate { get; set; }
    public DateTime BirthDate { get; set; }
    public string TestMapper { get; set; }
}