using FizzBuzzFlex.Api.Dtos;

namespace FizzBuzzFlex.Api.Services;

public interface IGameService
{
    Task<GameReadDto> Create(GameWriteDto dto);

    Task<IEnumerable<GameMinimalDto>> GetAll();

    Task<GameReadDto?> GetById(Guid id);
}