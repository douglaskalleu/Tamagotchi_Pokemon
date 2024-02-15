namespace Tamagotchi_Pokemon.Entities
{
  public class PokemonResult
  {
    public string name { get; set; }
    public string url { get; set; }
  }

  public class Results
  {
    public List<PokemonResult> results { get; set; }
  }
}
