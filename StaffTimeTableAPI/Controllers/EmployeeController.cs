using Application.Common.Models.Dto.Employees.Response;
using Application.Employees.Commands.CreateEmployee;
using Application.Employees.Commands.DeleteEmployee;
using Application.Employees.Commands.UpdateEmployee;
using Application.Employees.Queries.GetEmployees;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace StaffTimeTableAPI.Controllers;

public class EmployeeController : ApiControllerBase
{
    /// <summary>
    /// Gets all employees
    /// </summary>
    /// <returns></returns>
    [Authorize]
    [HttpGet("[action]")]
    [ProducesResponseType(typeof(List<GetEmployeesResponseDto>), 200)]
    public async Task<ActionResult<List<GetEmployeesResponseDto>>> Get(CancellationToken cancellationToken)
    {
        return await Mediator.Send(new GetEmployeesQuery(), cancellationToken);
    }

    /// <summary>
    /// Creates employee
    /// </summary>
    /// <param name="request">data to create</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [Authorize(Roles = "Admin")]
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
            UserPhone = request.UserPhone,
            IsMale = request.IsMale,
            HireDate = request.HireDate,
            BirthDate = request.BirthDate
        }, cancellationToken);
    }
    
    /// <summary>
    /// Edit employee
    /// </summary>
    /// <param name="id">employee Id</param>
    /// <param name="data">data to update</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [Authorize(Roles = "admin")]
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
    [Authorize(Roles = "manage-account")]
    [HttpDelete("[action]/{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        await Mediator.Send(new DeleteEmployeeCommand(id), cancellationToken);
    
        return StatusCode(201);
    }
}