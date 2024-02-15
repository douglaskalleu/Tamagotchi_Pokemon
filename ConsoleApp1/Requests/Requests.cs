using Newtonsoft.Json;
using RestSharp;
using System.Net;
using Tamagotchi_Pokemon.Contracts;

namespace Tamagotchi_Pokemon.Requests
{
  public class Requests : IRequests
  {
    const string _URI = "https://pokeapi.co/api/v2/pokemon";

    public string GetPokemon(string filter = null)
    {
      RestClient client;
      var message = string.Empty;

      if (string.IsNullOrEmpty(filter))
        client = new RestClient($"{_URI}/");
      else
        client = new RestClient($"{_URI}/{filter.ToLower()}/");

      var request = new RestRequest("", Method.Get);
      var response = client.Execute(request);

      if (response.StatusCode == HttpStatusCode.OK)
        message = response.Content;
      else
        message = response.ErrorException!.Message;

      return message!;
    }

    public T Deserializable<T>(string response) =>
      JsonConvert.DeserializeObject<T>(response);
  }
}
