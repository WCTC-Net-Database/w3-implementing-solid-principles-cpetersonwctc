using System.Reflection.Emit;
using System.Xml.Linq;
using Xunit.Abstractions;

public class CharacterWriter
{
    public void SaveCharacter(Character character)
    {
        CharacterReader characterReader = new CharacterReader();
        List<Character> characters = characterReader.ReadAllCharacters();

        StreamWriter writer = new StreamWriter("input.csv", false);
        writer.WriteLine("Name,Class,Level,HP,Equipment");

        
        foreach (Character characterToSave in characters)
        {
            if (characterToSave.Name == character.Name)
            {
                writer.WriteLine($"\"{character.Name}\",{character.Class},{character.Level},{character.Health},{string.Join("|", character.Equipment)}");
            }
            else {
                writer.WriteLine($"\"{characterToSave.Name}\",{characterToSave.Class},{characterToSave.Level},{characterToSave.Health},{string.Join("|",characterToSave.Equipment)}");
            }
        }
        writer.Flush();
        writer.Close();
    }

    public void AddACharacter(Character character)
    {
        StreamWriter writer = new StreamWriter("input.csv", true);
        writer.WriteLine($"{character.Name},{character.Class},{character.Level},{character.Health},{string.Join("|", character.Equipment)}");
        writer.Close();
    }
}