using Microsoft.AspNetCore.Mvc;

namespace portfolio_backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProjectController : ControllerBase
{
    public ProjectController(Database db)
    {
        Db = db;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        await Db.Connection.OpenAsync();
        var query = new Project(Db);
        var result = await query.GetAllAsync();
        return new OkObjectResult(result);
    }
    //select one
    //create new
    //edit existing
    //delete one

    public Database Db { get; }
}
