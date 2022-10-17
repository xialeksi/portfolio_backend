using Microsoft.AspNetCore.Mvc;

namespace portfolio_backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ImageController : ControllerBase
{
    public ImageController(Database db)
    {
        Db = db;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        await Db.Connection.OpenAsync();
        var query = new Image(Db);
        var result = await query.GetAllAsync();
        return new OkObjectResult(result);
    }
    //select one
    // GET api/Student/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetOne(int id)
    {
        await Db.Connection.OpenAsync();
        var query = new Image(Db);
        var result = await query.FindOneAsync(id);
        if (result is null)
            return new NotFoundResult();
        return new OkObjectResult(result);   
    }


    
    //create new
    // POST api/Student
    [HttpPost]
    public async Task<IActionResult> Post([FromBody]Image body)
    {
        await Db.Connection.OpenAsync();
        body.Db = Db;
        int result=await body.InsertAsync();
        if(result == 0){
            return new ConflictObjectResult(0);
        }
        return new OkObjectResult(result);
    }
    //edit existing
    // PUT api/Student/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutOne(int id, [FromBody]Image body)
    {
        await Db.Connection.OpenAsync();
        var query = new Image(Db);
        var result = await query.FindOneAsync(id);
        if (result is null)
            return new NotFoundResult();
        result.idimage = body.idimage;
        result.idproject = body.idproject;
        result.filename = body.filename;
        result.description = body.description;
        await result.UpdateAsync();
        return new OkObjectResult(result);
    }
    //delete one
    // DELETE api/Student/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOne(int id)
    {
        await Db.Connection.OpenAsync();
        var query = new Image(Db);
        var result = await query.FindOneAsync(id);
        if (result is null)
            return new NotFoundResult();
        await result.DeleteAsync();
        return new OkObjectResult(result);
    }
    public Database Db { get; }
}
