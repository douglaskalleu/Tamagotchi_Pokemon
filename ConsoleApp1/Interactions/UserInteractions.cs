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

    private string? _player = string.Empty;
    private IRequests _request;
    private Results _pokemonResults = new Results();

    public UserInteractions(IRequests request)
    {
      _request = request;
      Console.WriteLine(POKENGOTCHI);
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
      var choiceMainMenu = Console.ReadLine();
      return ValidSelected(choiceMainMenu);
    }

    public int Adopt()
    {
      Console.WriteLine(string.Format($"{Environment.NewLine}-------------------ADOTAR UM POKEMON---------------------"));
      Console.WriteLine($"{_player} Selecione um pokemon: ");
      Console.WriteLine("1 - Ver Pokemons Disponíveis");
      Console.WriteLine("2 - Ver Detalhes de um Pokemon");
      Console.WriteLine("3 - Adotar um Pokemon");
      Console.WriteLine("4 - Voltar ao Menu Principal");
      var choiceAdopt = Console.ReadLine();
      return ValidSelected(choiceAdopt);
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
      Console.WriteLine(string.Format($"{Environment.NewLine}-------------------DESTALHES POKEMON---------------------"));
      Console.WriteLine("Nome: " + detalhes.name);
      Console.WriteLine("Altura: " + detalhes.height);
      Console.WriteLine("Peso: " + detalhes.weight);
      Console.WriteLine("Habilidades:");

      foreach (var habilidade in detalhes.abilities)
      {
        Console.WriteLine("- " + habilidade.ability.name);
      }
    }

    public List<Pokemon> ConfirmAdoption(List<Pokemon> pokedex, Pokemon pokemon)
    {
      Console.WriteLine(string.Format($"{Environment.NewLine}--------------------DESTALHES ADOÇÃO---------------------"));
      Console.Write("Confirma adoção? (s/n): ");
      string resposta = Console.ReadLine();

      if (resposta.ToUpper() == "S")
      {
        pokedex.Add(pokemon);
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

    public void ShowPokedex(List<Pokemon> pokemonsOfPokeBall)
    {
      Console.WriteLine(string.Format($"{Environment.NewLine}------------------------POKEDEX--------------------------"));
      Console.WriteLine("Pokemons na pokedex:");
      if (pokemonsOfPokeBall.Count == 0)
        Console.WriteLine("Você ainda não possui pokemons.");
      else
      {
        for (int i = 0; i < pokemonsOfPokeBall.Count; i++)
        {
          Console.WriteLine((i + 1) + ". " + pokemonsOfPokeBall[i].name);
        }
      }
    }

    private int ValidSelected(string selected)
    {
      if (!int.TryParse(selected, out var value) && value < 1 || value > 4)
        Console.Write(string.Format($"{Environment.NewLine}Escolha inválida. Por favor, escolha uma opção entre 1 e 4:{Environment.NewLine}"));

      return value;
    }
  }
}