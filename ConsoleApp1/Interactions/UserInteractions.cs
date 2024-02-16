using AutoMapper;
using Tamagotchi_Pokemon.Contracts;
using Tamagotchi_Pokemon.Entities;

namespace Tamagotchi_Pokemon.Interactions
{
  public class UserInteractions
  {
    const string POKENGOTCHI = @"
                     _____   ____  _  ________ _   _  _____  ____ _______ _____ _    _ _____ 
                    |  __ \ / __ \| |/ /  ____| \ | |/ ____|/ __ \__   __/ ____| |  | |_   _|
                    | |__) | |  | | ' /| |__  |  \| | |  __| |  | | | | | |    | |__| | | |  
                    |  ___/| |  | |  < |  __| | . ` | | |_ | |  | | | | | |    |  __  | | |  
                    | |    | |__| | . \| |____| |\  | |__| | |__| | | | | |____| |  | |_| |_ 
                    |_|     \____/|_|\_\______|_| \_|\_____|\____/  |_|  \_____|_|  |_|_____|";

    private IRequests _request;
    private IMapper _mapper;


    private string? _player = string.Empty;
    private Results _pokemonResults = new Results();

    public UserInteractions(IRequests request, IMapper mapper)
    {
      Console.WriteLine(POKENGOTCHI);
      _request = request;
      _mapper = mapper;
    }

    public void ShowWelcome()
    {
      Console.WriteLine(string.Format($"{Environment.NewLine}---------------------------------------------------------"));
      Console.WriteLine("Bem-vindo(a) ao Pokengotchi");

      Console.WriteLine(string.Format($"{Environment.NewLine}"));
      Console.WriteLine("Informe seu nome ou nick: ");
      _player = Console.ReadLine();

      Console.WriteLine(string.Format($"{Environment.NewLine}{_player}, Siga as instruções abaixo para adotar um pokengotchi!{Environment.NewLine}"));
    }

    public int ShowMainMenu()
    {
      Console.WriteLine(string.Format($"{Environment.NewLine}-------------------------MENU----------------------------"));
      Console.WriteLine($"{_player} Você deseja: ");
      Console.WriteLine($"1 - Adotar um mascote virtual");
      Console.WriteLine($"2 - Abrir pokedex");
      Console.WriteLine($"3 - Sair");
      return GetSelectedPlayer(3);
    }

    public int Adopt()
    {
      Console.WriteLine(string.Format($"{Environment.NewLine}-------------------ADOTAR UM POKEMON---------------------"));
      Console.WriteLine($"{_player} Selecione um pokemon: ");
      Console.WriteLine("1 - Ver Pokemons Disponíveis");
      Console.WriteLine("2 - Ver Detalhes de um Pokemon");
      Console.WriteLine("3 - Adotar um Pokemon");
      Console.WriteLine("4 - Voltar ao Menu Principal");
      return GetSelectedPlayer(4);
    }

    public void ShowListPokemon()
    {
      var pokemons = _request.GetPokemon();
      var results = _request.Deserializable<Results>(pokemons);
      _pokemonResults = results;

      Console.WriteLine(string.Format($"{Environment.NewLine}------------------POKEMONS DISPONIVEIS-------------------"));
      for (int i = 0; i < _pokemonResults.results.Count; i++)
      {
        Console.WriteLine($"{(i + 1)} - {_pokemonResults.results[i].name.ToUpper()}");
      }
    }

    public string ShowPokemonSelected()
    {
      Console.WriteLine(string.Format($"{Environment.NewLine}------------------POKEMON SELECIONADO--------------------"));
      string selected;
      while (true)
      {
        Console.WriteLine($"{_player} Escolha um pokemon: ");
        selected = Console.ReadLine();
        if (!string.IsNullOrEmpty(selected))
          break;

        Console.WriteLine(string.Format($"{Environment.NewLine}Escolha inválida.{Environment.NewLine}"));
      }
      return selected;
    }

    public void ShowDetails(Pokemon detalhes)
    {
      Console.WriteLine(string.Format($"{Environment.NewLine}-------------------DESTALHES POKÉMON---------------------"));
      Console.WriteLine("Nome: " + detalhes.name);
      Console.WriteLine("Altura: " + detalhes.height);
      Console.WriteLine("Peso: " + detalhes.weight);
      Console.WriteLine("Habilidades:");

      foreach (var habilidade in detalhes.abilities)
      {
        Console.WriteLine("- " + habilidade.ability.name);
      }
    }

    public List<PokedexDto> ConfirmAdoption(List<PokedexDto> pokedex, Pokemon pokemon)
    {
      Console.WriteLine(string.Format($"{Environment.NewLine}--------------------DESTALHES ADOÇÃO---------------------"));
      Console.Write("Confirma adoção? (s/n): ");
      string resposta = Console.ReadLine();

      if (resposta.ToUpper() == "S")
      {
        var pokemonAdopt = _mapper.Map<PokedexDto>(pokemon);
        pokedex.Add(pokemonAdopt);
        Console.WriteLine($"Parabéns! Você adotou um {pokemon.name} !");
        Console.WriteLine("──────────────");
        Console.WriteLine("────▄████▄────");
        Console.WriteLine("──▄████████▄──");
        Console.WriteLine("──██████████──");
        Console.WriteLine("──▀████████▀──");
        Console.WriteLine("─────▀██▀─────");
        Console.WriteLine("──────────────");
      }

      return pokedex;
    }

    public void ShowPokedex(List<PokedexDto> pokemonsOfPokedex)
    {
      Console.WriteLine(string.Format($"{Environment.NewLine}------------------------POKÉDEX--------------------------"));
      Console.WriteLine("Pokemons na pokédex:");

      if (pokemonsOfPokedex.Count == 0)
        Console.WriteLine("Você ainda não possui pokémons.");
      else
      {
        Console.WriteLine("Escolha um pokémon para brincar: ");
        for (int i = 0; i < pokemonsOfPokedex.Count; i++)
        {
          Console.WriteLine((i + 1) + "- " + pokemonsOfPokedex[i].Name);
        }

        int indexSelected = GetSelectedPlayer(pokemonsOfPokedex.Count) - 1;
        PokedexDto pokemon = pokemonsOfPokedex[indexSelected];

        int optionInterection = 0;
        while (optionInterection != 6)
        {
          ShowMenuInterection();
          optionInterection = GetSelectedPlayer(7);

          switch (optionInterection)
          {
            case 1:
              pokemon.ShowStatus();
              break;
            case 2:
              pokemon.Eat();
              break;
            case 3:
              pokemon.Play();
              break;
            case 4:
              pokemon.Sleep();
              break;
            case 5:
              pokemon.GivingCare();
              break;
            case 6:
              break;
          }
        }

      }
    }

    private void ShowMenuInterection()
    {
      Console.WriteLine(string.Format($"{Environment.NewLine}----------------INTERAJA COM SEU POKÉMON-----------------"));
      Console.WriteLine("Menu de Interação:");
      Console.WriteLine("1- Saber como o pokémon está");
      Console.WriteLine("2- Alimentar o pokémon");
      Console.WriteLine("3- Brincar com o pokémon");
      Console.WriteLine("4- Colocar para dormir");
      Console.WriteLine("5- Fazer carinho");
      Console.WriteLine("6- Voltar menu principal");
      Console.Write("Escolha uma opção: ");
    }

    private int GetSelectedPlayer(int maxOpcao)
    {
      int escolha;
      while (!int.TryParse(Console.ReadLine(), out escolha) || escolha < 1 || escolha > maxOpcao)
      {
        Console.Write($"Escolha inválida. Por favor, escolha uma opção entre 1 e {maxOpcao}: ");
      }
      return escolha;
    }
  }
}