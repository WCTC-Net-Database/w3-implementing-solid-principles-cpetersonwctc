using System.Xml.Linq;

public class CharacterReader
{
    public List<Character> ReadAllCharacters()
    {
        string[] allLines = File.ReadAllLines("input.csv");
        List<Character> characters = new List<Character>();

        for (int c=1; c<allLines.Length;c++)
        {
            string thisLine = allLines[c];

            string name;
            if (thisLine.StartsWith("\""))
            {
                thisLine = thisLine.Substring(1, thisLine.Length - 1);
                name = thisLine.Substring(0, thisLine.IndexOf('"'));
                thisLine = thisLine.Substring(thisLine.IndexOf('"') + 2, thisLine.Length - thisLine.IndexOf('"') - 2);
            }
            else
            {
                name = thisLine.Substring(0, thisLine.IndexOf(','));
                thisLine = thisLine.Substring(thisLine.IndexOf(',') + 1, thisLine.Length - thisLine.IndexOf(',') - 1);
            }

            var charData = thisLine.Split(",");
            string characterClass = charData[0];
            int level = Convert.ToInt32(charData[1]);
            int hitPoints = Convert.ToInt32(charData[2]);
            string[] equipment = charData[3].Split('|');

            characters.Add(new Character { Name = name, Class = characterClass, Level = level, Health = hitPoints, Equipment = equipment });

            }
        return characters;
    }

    
}