using RestSharp;
using System.Net;
using Tamagotchi_Pokemon.Contracts;

namespace Tamagotchi_Pokemon.Requests
{
    public class Requests : IRequests
    {
        const string _URI = "https://pokeapi.co/api/v2/pokemon";

        public string GetPokemon(string filter)
        {
            var message = string.Empty;

            if (string.IsNullOrEmpty(filter))
                return "Filtro não informado";

            var client = new RestClient($"{_URI}/{filter}/");
            var request = new RestRequest("", Method.Get);
            var response = client.Execute(request);

            if (response.StatusCode == HttpStatusCode.OK)
                message = response.Content;
            else
                message = response.ErrorException!.Message;

            return message!;
        }
    }
}
