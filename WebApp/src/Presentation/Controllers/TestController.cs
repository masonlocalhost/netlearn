using Microsoft.AspNetCore.Mvc;
using WebApp.Application.DTOs;
using WebApp.Domain.Entities;

namespace WebApp.Presentation.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class TestController : ControllerBase
{
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<GetCourseResponse>> GetCourse(GetCourseRequest req, CancellationToken cancellationToken)
    {
        return Ok(new GetCourseResponse(
            new CourseDTO
            {
                Id = req.Id,
                Name = "etest",
                Topic = "pubhorn",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            }

        ));
    }

    [HttpGet]
    public async Task<ActionResult<ListCoursesResponse>> ListCourses(ListCoursesRequest req, CancellationToken cancellationToken)
    {
        return Ok(new ListCoursesResponse());
    }

    [HttpPost]
    public async Task<ActionResult<CreateCourseResponse>> CreateCourse([FromBody] CreateCourseRequest req, CancellationToken cancellationToken)
    {
        return Ok(new CreateCourseResponse
        {
            Course = new CourseDTO
            {
                Id = Guid.NewGuid(),
                Name = req.Name,
                Topic = req.Topic,
            }
        });
    }
}
