using System.ComponentModel.DataAnnotations;

namespace NETlearn.Application.DTOs;

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
    [Required]
    public Guid Id { get; set; }
}

public class GetCourseResponse(CourseDTO course)
{
    public CourseDTO Course { get; set; } = course;
};

public class ListCoursesRequest
{
    public Guid[]? Ids { get; set; }
    public string[]? Names { get; set; }
    public string[]? Topics { get; set; }
    public long Page { get; set; } = 0;
    public long Size { get; set; } = 100;
}

public class ListCoursesResponse()
{
    public CourseDTO[] Courses { get; set; } = [];
}

public class CreateCourseRequest
{
    [Required]
    [StringLength(50)]
    public string Name { get; set; } = string.Empty;
    [Required]
    [StringLength(20)]
    public string Topic { get; set; } = string.Empty;
}

public class CreateCourseResponse
{
    required public CourseDTO Course { get; set; }
}
