using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Application.DTOs;

public class CourseDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Topic { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
};

public class GetCourseRequest
{
    [FromRoute]
    [Required]
    public Guid Id { get; set; }
}

public class GetCourseResponse(CourseDTO course)
{
    public CourseDTO Course { get; set; } = course;
};

public class ListCoursesRequest
{
    [FromQuery]
    public Guid[]? Ids { get; set; }
    [FromQuery]
    public string[]? Names { get; set; }
    [FromQuery]
    public string[]? Topics { get; set; }
    [FromQuery]
    public long Page { get; set; } = 0;
    [FromQuery]
    public long Size { get; set; } = 100;
}

public class ListCoursesResponse()
{
    public CourseDTO[] Courses { get; set; } = [];
}

public class CreateCourseRequest
{
    [FromBody]
    [Required]
    [StringLength(50)]
    public string Name { get; set; } = string.Empty;
    [FromBody]
    [Required]
    [StringLength(20)]
    public string Topic { get; set; } = string.Empty;
}

public class CreateCourseResponse
{
    required public CourseDTO Course { get; set; }
}
