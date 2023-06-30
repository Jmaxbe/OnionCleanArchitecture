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
    [HttpGet("[action]")]
    [ProducesResponseType(typeof(List<GetEmployeesDto>), 200)]
    public async Task<ActionResult<List<GetEmployeesDto>>> Get(CancellationToken cancellationToken)
    {
        return await Mediator.Send(new GetEmployeesQuery(), cancellationToken);
    }

    /// <summary>
    /// Creates employee
    /// </summary>
    /// <param name="command">data to create</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("[action]")]
    [ProducesResponseType(typeof(CreateEmployeeDto), 200)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<CreateEmployeeDto>> Create(CreateEmployeeCommand command, CancellationToken cancellationToken)
    {
        return await Mediator.Send(command, cancellationToken);
    }

    /// <summary>
    /// Edit employee
    /// </summary>
    /// <param name="id">employee Id</param>
    /// <param name="command">data to update</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPut("[action]/{id}")]
    [ProducesResponseType(typeof(UpdateEmployeeDto), 200)]
    public async Task<ActionResult<UpdateEmployeeDto>> Update(int id, UpdateEmployeeCommand command, CancellationToken cancellationToken)
    {
        if (id != command.Id)
        {
            return BadRequest();
        }
        
        return await Mediator.Send(command, cancellationToken);
    }

    /// <summary>
    /// Deletes employee
    /// </summary>
    /// <param name="id">employee Id</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpDelete("[action]/{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        await Mediator.Send(new DeleteEmployeeCommand(id), cancellationToken);

        return NoContent();
    }
}