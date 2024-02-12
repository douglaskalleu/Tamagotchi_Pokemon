namespace Tamagotchi_Pokemon.Entities
{
  public class Pokemon
  {
    public string name { get; set; } = string.Empty;
    public int height { get; set; }
    public int weight { get; set; }
    public int base_experience { get; set; }
    public int order { get; set; }
    public List<Abilities> abilities { get; set; }
  }
}
