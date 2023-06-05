using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using People.Core.DataContext;
using People.Core.Entities;

namespace People.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PeopleController : ControllerBase
{
    private readonly ILogger<PeopleController> _logger;
    private readonly PeopleDbContext _context;

    public PeopleController(ILogger<PeopleController> logger, PeopleDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpGet]
    public async Task<IResult> Get(CancellationToken ct = default)
    {
        var people = await _context.People.ToListAsync(ct);
        
        return Results.Ok(people);
    }
    
    [HttpGet("{id}")]
    public async Task<IResult> Get(int id, CancellationToken ct = default)
    {
        var person = await _context.People.FindAsync(new object[] { id }, ct);

        if (person == null)
        {
            return Results.NotFound();
        }

        return Results.Ok(person);
    }
    
    [HttpPost]
    public async Task<IResult> Post([FromBody] Person person, CancellationToken ct = default)
    {
        await _context.People.AddAsync(person, ct);
        await _context.SaveChangesAsync(ct);
        
        return Results.Created($"/api/people/{person.Id}", person);
    }
    
    [HttpDelete("{id}")]
    public async Task<IResult> Delete(int id, CancellationToken ct = default)
    {
        var person = await _context.People.FindAsync(new object[] { id }, ct);

        if (person == null)
        {
            return Results.NotFound();
        }

        _context.People.Remove(person);
        await _context.SaveChangesAsync(ct);

        return Results.Ok();
    }
}