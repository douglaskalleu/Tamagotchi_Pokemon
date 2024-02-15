using AutoMapper;
using Tamagotchi_Pokemon.Entities;

namespace Tamagotchi_Pokemon.Services
{
  public class MapperProfile : Profile
  {
    public MapperProfile()
    {
      CreateMap<Pokemon, PokedexDto>()
      .ForMember(entity => entity.Name, map => map.MapFrom(dto => dto.name))
      .ForMember(entity => entity.Height, map => map.MapFrom(dto => dto.height))
      .ForMember(entity => entity.Weight, map => map.MapFrom(dto => dto.weight))
      .ForMember(entity => entity.Abilities, map => map.MapFrom(src => src.abilities.Select(a => new PokedexAbility { Name = a.ability.name })));
    }

    public class PokemonService
    {
      private readonly IMapper _mapper;

      public PokemonService(IMapper mapper)
      {
        _mapper = mapper;
      }

      public PokedexDto Create(Pokemon pokemon)
      {
        return _mapper.Map<PokedexDto>(pokemon);
      }
    }
  }
}
