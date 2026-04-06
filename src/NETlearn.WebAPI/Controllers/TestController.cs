using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using NETlearn.Application.DTOs;
using NETlearn.Domain.Entities;

namespace NETlearn.WebAPI.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class TestController(ILogger<TestController> logger) : ControllerBase
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
        var userId = this.HttpContext.Items["UserId"]?.ToString();
        logger.LogInformation(userId);

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
