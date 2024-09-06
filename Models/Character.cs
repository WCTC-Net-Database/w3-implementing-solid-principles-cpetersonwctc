public class Character
{
    public string Name { get; set; }
    public string Class { get; set; }
    public int Level { get; set; }
    public int Health { get; set; }
    public string[] Equipment { get; set; }

    public void LevelUp()
    {
        Level++;
    }
}