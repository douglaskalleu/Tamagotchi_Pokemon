using System.Text.Json;
using Tamagotchi_Pokemon.Entities;
using Tamagotchi_Pokemon.Requests;

Console.WriteLine("Insira o pokemon desejado: ");
var filter = Console.ReadLine();

var requests = new Requests();
var response = requests.GetPokemon(filter!);

var pokemon = Deserializable(response);

var message = $"Nome Pokemon: {pokemon.name} \nAltura: {pokemon.height} \nPeso: {pokemon.weight}";
Console.WriteLine(message);

Console.WriteLine("Habilidades: ");

foreach (var item in pokemon.abilities)
{
  Console.WriteLine(item.ability.name.ToUpper());
}

Console.ReadKey();

Pokemon Deserializable(string response) =>
  JsonSerializer.Deserialize<Pokemon>(response);
