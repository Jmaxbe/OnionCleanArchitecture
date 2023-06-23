using Application.Employees.Commands.CreateEmployee;
using Application.Employees.Commands.DeleteEmployee;
using Application.Employees.Commands.UpdateEmployee;
using Application.Employees.Queries.GetEmployees;
using Microsoft.AspNetCore.Mvc;

namespace StaffTimeTableAPI.Controllers;

public class EmployeeController : ApiControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(List<GetEmployeesDto>), 200)]
    public async Task<ActionResult<List<GetEmployeesDto>>> Get()
    {
        return await Mediator.Send(new GetEmployeesQuery());
    }

    [HttpPost]
    [ProducesResponseType(typeof(CreateEmployeeDto), 200)]
    public async Task<ActionResult<CreateEmployeeDto>> Create(CreateEmployeeCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(UpdateEmployeeDto), 200)]
    public async Task<ActionResult<UpdateEmployeeDto>> Update(int id, UpdateEmployeeCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest();
        }
        
        return await Mediator.Send(command);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> Delete(int id)
    {
        await Mediator.Send(new DeleteEmployeeCommand(id));

        return NoContent();
    }
}