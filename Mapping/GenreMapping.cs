using GameStroreApi.Dtos;
using GameStroreApi.Models;

namespace GameStroreApi.Mapping;

public static class GenreMapping
{
    public static GenreDto ToDto(this Genre genre)
    {
        return new GenreDto(genre.Id, genre.Name);
    }
}
