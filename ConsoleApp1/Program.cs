// See https://aka.ms/new-console-template for more information

using Tamagotchi_Pokemon.Requests;

Console.WriteLine("Insira o pokemon desejado: ");
var filter = Console.ReadLine();

var requests = new Requests();
var response = requests.GetPokemon(filter!);

Console.WriteLine(response);
Console.ReadKey();
