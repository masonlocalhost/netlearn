using Microsoft.AspNetCore.Mvc;
using WebApp.Application.DTOs;
using WebApp.Application.Interfaces;

namespace WebApp.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")] // Routes to /api/users
public class UsersController(IUserService userService) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<CreateUserResponse>> CreateUser([FromBody] CreateUserRequest request, CancellationToken cancellationToken)
    {
        // Note: Validation (FluentValidation) would ideally happen automatically before this line is hit.
        
        try
        {
            var response = await userService.CreateUserAsync(request, cancellationToken);
            
            var responseBody = new CreateUserResponse(response);
            // Returns a 201 Created status code, and sets the "Location" header to the GET endpoint
            return CreatedAtAction(nameof(GetUser), new { id = responseBody.User.Id }, responseBody);
        }
        catch (InvalidOperationException ex)
        {
            // Temporary manual error handling until we add the Global Exception Middleware
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpGet("{id:guid}")] // Enforces that 'id' must be a valid GUID
    public async Task<ActionResult<GetUserResponse>> GetUser(Guid id, CancellationToken cancellationToken)
    {
        var user = await userService.GetUserByIdAsync(id, cancellationToken);

        if (user is null)
        {
            return NotFound(); // Returns a 404
        }

        return Ok(new GetUserResponse(user)); // Returns a 200
    }
}