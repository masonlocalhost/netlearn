using Microsoft.AspNetCore.Mvc;
using WebApp.Application.DTOs;
using WebApp.Domain.Entities;

namespace WebApp.Presentation.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class TestController: ControllerBase
{
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<GetCourseResponse>> GetCourse(Guid id, CancellationToken cancellationToken)
    {
        return Ok(new GetCourseResponse(
            new CourseDTO(
                Guid.NewGuid(),
                "etest",
                "pubhorn",
                DateTime.UtcNow,
                DateTime.UtcNow
            )
        ));
    }
    
    public async Task<ActionResult<GetCourseResponse>> ListCourses([FromRoute]Guid id, CancellationToken cancellationToken)
    {
        return Ok(new GetCourseResponse(
            new CourseDTO(
                Guid.NewGuid(),
                "etest",
                "pubhorn",
                DateTime.UtcNow,
                DateTime.UtcNow
            )
        ));
    }
}