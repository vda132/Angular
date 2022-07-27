using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TasksController : ControllerBase
{
    private readonly Logic.Logic.ITaskLogic logic;

    public TasksController(Logic.Logic.ITaskLogic logic) =>
        this.logic = logic;

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] DTOs.TaskDTO dto)
    {
        var model = Converter.TaskConverter.ConvertFromDTOToModel(dto);

        var result = await this.logic.AddAsync(model);

        if (result != default)
            return Ok();

        return BadRequest();
    }

    [HttpGet]
    public async Task<JsonResult> GetAll() =>
        new JsonResult(await this.logic.GetAllAsync());

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id) => 
        await this.logic.DeleteAsync(id) ? Ok() : BadRequest();

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(Guid id, DTOs.TaskDTO dto)
    {
        var model = Converter.TaskConverter.ConvertFromDTOToModel(dto);

        if (await this.logic.UpdateAsync(id, model))
            return Ok();

        return BadRequest();
    }
}
