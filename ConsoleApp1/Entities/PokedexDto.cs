using AutoMapper;

namespace Tamagotchi_Pokemon.Entities
{
  public class PokedexDto
  {
    public int Food { get; private set; }
    public int Mud { get; private set; }
    public int Energy { get; private set; }
    public int Health { get; private set; }
    public int Height { get; set; }
    public int Weight { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<PokedexAbility> Abilities { get; set; }

    public void Eat()
    {
      Food = Math.Min(Food + 2, 10);
      Energy = Math.Max(Energy - 1, 0);

      Console.WriteLine("Pokémon Alimentado!");

    }

    public void Play()
    {
      Mud = Math.Min(Mud + 3, 10);
      Energy = Math.Max(Energy - 2, 0);
      Food = Math.Max(Food - 1, 0);

      Console.WriteLine("Pokémon Feliz!");

    }

    public void Sleep()
    {
      Energy = Math.Min(Energy + 4, 10);
      Mud = Math.Max(Mud - 1, 0);

      Console.WriteLine("Pokémon a Mimir!");

    }

    public void GivingCare()
    {
      Mud = Math.Min(Mud + 2, 10);
      Health = Math.Min(Health + 1, 10);

      Console.WriteLine("Pokémon Amado!");

    }

    public void ShowStatus()
    {
      Console.WriteLine(Environment.NewLine);
      Console.WriteLine("Status do pokémon:");
      Console.WriteLine($"Alimentação: {Food}");
      Console.WriteLine($"Humor: {Mud}");
      Console.WriteLine($"Energia: {Energy}");
      Console.WriteLine($"Saúde: {Health}");
    }
  }

  public class PokedexAbility
  {
    public string Name { get; set; }
  }
}
