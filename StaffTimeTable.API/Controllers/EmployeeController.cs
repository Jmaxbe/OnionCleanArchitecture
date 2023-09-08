using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OpenTracing;
using StaffTimeTable.API.Models.Roles;
using StaffTimetable.Application.Common.Models.Dto.Employees.Request;
using StaffTimetable.Application.Common.Models.Dto.Employees.Response;
using StaffTimetable.Application.Employees.Commands.CreateEmployee;
using StaffTimetable.Application.Employees.Commands.DeleteEmployee;
using StaffTimetable.Application.Employees.Commands.UpdateEmployee;
using StaffTimetable.Application.Employees.Queries.GetEmployees;

namespace StaffTimeTable.API.Controllers;

public class EmployeeController : ApiControllerBase
{
    private readonly ITracer _tracer;
        
    public EmployeeController(ITracer tracer)
    {
        _tracer = tracer;
    }
    
    /// <summary>
    /// Gets all employees
    /// </summary>
    /// <returns></returns>
    [Authorize]
    [HttpGet("[action]")]
    [ProducesResponseType(typeof(List<GetEmployeesResponseDto>), 200)]
    public async Task<ActionResult<List<GetEmployeesResponseDto>>> Get(CancellationToken cancellationToken)
    {
        // var span = _tracer.BuildSpan("Тип спан новый").Start();
        // span.SetTag("Error", true);
        // span.Finish();
        return await Mediator.Send(new GetEmployeesQuery(), cancellationToken);
    }

    /// <summary>
    /// Creates employee
    /// </summary>
    /// <param name="request">data to create</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [Authorize(Roles = RoleConstants.Admin)]
    [HttpPost("[action]")]
    [ProducesResponseType(typeof(CreateEmployeeResponseDto), 200)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<CreateEmployeeResponseDto>> Create(CreateEmployeeRequestDto request, CancellationToken cancellationToken)
    {
        return await Mediator.Send(new CreateEmployeeCommand
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            MiddleName = request.MiddleName,
            UserPhone = request.Phone,
            Email = request.Email,
            UserName = request.UserName,
            Password = request.Password,
            IsMale = request.IsMale,
            HireDate = request.HireDate,
            BirthDate = request.BirthDate,
            UserRoles = request.UserRoles
        }, cancellationToken);
    }
    
    /// <summary>
    /// Edit employee
    /// </summary>
    /// <param name="id">employee Id</param>
    /// <param name="data">data to update</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [Authorize(Roles = RoleConstants.Admin)]
    [HttpPut("[action]/{id}")]
    [ProducesResponseType(typeof(UpdateEmployeeResponseDto), 200)]
    public async Task<ActionResult<UpdateEmployeeResponseDto>> Update(Guid id, UpdateEmployeeRequestDto data, CancellationToken cancellationToken)
    {
        if (!id.Equals(data.Id))
        {
            return BadRequest("Ids are not equal");
        }
        
        return await Mediator.Send(new UpdateEmployeeCommand
        {
            Id = data.Id,
            FirstName = data.FirstName,
            LastName = data.LastName,
            MiddleName = data.MiddleName,
            UserPhone = data.UserPhone,
            IsMale = data.IsMale,
            HireDate = data.HireDate,
            BirthDate = data.BirthDate
        }, cancellationToken);
    }
    
    /// <summary>
    /// Deletes employee
    /// </summary>
    /// <param name="id">employee Id</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [Authorize(Roles = RoleConstants.Admin)]
    [HttpDelete("[action]/{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        await Mediator.Send(new DeleteEmployeeCommand(id), cancellationToken);
    
        return StatusCode(201);
    }
}