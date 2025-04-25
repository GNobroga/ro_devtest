namespace RO.DevTest.Application.DTOs;

public record ProductDTO(Guid Id, string Name, decimal Price, string? ImageUrl, string? Description);
