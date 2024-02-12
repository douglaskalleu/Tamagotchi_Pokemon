using RestSharp;

namespace Tamagotchi_Pokemon.Contracts
{
    public interface IRequests
    {
        string GetPokemon(string filter);
    }
}
