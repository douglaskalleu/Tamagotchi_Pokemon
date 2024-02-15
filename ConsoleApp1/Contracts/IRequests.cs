namespace Tamagotchi_Pokemon.Contracts
{
  public interface IRequests
  {
    string GetPokemon(string filter = null);

    T Deserializable<T>(string response);
  }
}