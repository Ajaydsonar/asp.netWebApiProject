using System.ComponentModel.DataAnnotations;

namespace GameStroreApi.Dtos;

public record class CreateGameDto
(
    [Required][StringLength(50)] string Name,
    [Required] int GenreId,
    [Required][Range(1, 100)] decimal Price,
    [Required] DateOnly ReleaseDate
);
