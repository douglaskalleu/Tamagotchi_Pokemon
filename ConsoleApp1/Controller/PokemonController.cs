using AutoMapper;
using Tamagotchi_Pokemon.Contracts;
using Tamagotchi_Pokemon.Entities;
using Tamagotchi_Pokemon.Interactions;

namespace Tamagotchi_Pokemon.Controller
{
  public class PokemonController
  {
    private IMapper _mapper;
    private IRequests _requests;

    public PokemonController()
    {
      var config = new MapperConfiguration(cfg =>
      {
        cfg.AddProfile<Services.MapperProfile>();
      });

      _mapper = config.CreateMapper();
      _requests = new Requests.Requests();
    }

    public void Play()
    {
      var interactions = new UserInteractions(_requests, _mapper);
      bool openedGame = true;
      List<PokedexDto> pokedex = [];

      interactions.ShowWelcome();

      do
      {
        var choiceMenu = interactions.ShowMainMenu();

        switch (choiceMenu)
        {
          case 1:
            while (openedGame)
            {
              var choiceAdopt = interactions.Adopt();

              switch (choiceAdopt)
              {
                case 1:
                  interactions.ShowListPokemon();
                  break;
                case 2:
                  string pokemonSelected, response;
                  Pokemon pokemonDetails;
                  GetDatails(interactions, _requests, out pokemonSelected, out response, out pokemonDetails);
                  break;
                case 3:
                  GetDatails(interactions, _requests, out pokemonSelected, out response, out pokemonDetails);
                  pokedex = interactions.ConfirmAdoption(pokedex, pokemonDetails);
                  break;
                default:
                  break;
              }

              if (choiceAdopt == 4)
                break;
            }
            break;
          case 2:
            interactions.ShowPokedex(pokedex);
            break;
          case 3:
            Console.WriteLine(string.Format($"{Environment.NewLine}Vlw por jogar com a gente! Até mais!{Environment.NewLine}"));
            openedGame = false;
            return;
          default:
            break;
        }
      } while (openedGame == true);

      Console.ReadKey();
    }

    private void GetDatails(UserInteractions interactions, IRequests iRequest, out string pokemonSelected, out string response, out Pokemon pokemonDetails)
    {
      interactions.ShowListPokemon();
      pokemonSelected = interactions.ShowPokemonSelected();
      response = iRequest.GetPokemon(pokemonSelected);
      pokemonDetails = iRequest.Deserializable<Pokemon>(response);
      interactions.ShowDetails(pokemonDetails);
    }
  }
}
