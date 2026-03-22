namespace WebApp.Application.DTOs;

public record CourseDTO(Guid Id, string Name, string Topic, DateTime CreatedAt, DateTime UpdatedAt);

public record GetCourseResponse(CourseDTO course);

public record ListCoursesRequest(Guid[] Ids, string[] Names, string[] Topics, long Page, long Size);

public record ListCoursesResponse(CourseDTO[] courses);