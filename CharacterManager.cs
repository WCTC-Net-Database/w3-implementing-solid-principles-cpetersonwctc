namespace CharacterConsole;

public class CharacterManager
{
    private readonly IInput _input;
    private readonly IOutput _output;
    private readonly string _filePath = "input.csv";

    private string[] lines;

    public CharacterManager(IInput input, IOutput output)
    {
        _input = input;
        _output = output;
    }

    public void Run()
    {
        _output.WriteLine("Welcome to Character Management");

        lines = File.ReadAllLines(_filePath);

        while (true)
        {
            _output.WriteLine("Menu:");
            _output.WriteLine("1. Display Characters");
            _output.WriteLine("2. Find Character");
            _output.WriteLine("3. Add Character");
            _output.WriteLine("4. Level Up Character");
            _output.WriteLine("5. Exit");
            _output.Write("Enter your choice: ");
            var choice = _input.ReadLine();

            switch (choice)
            {
                case "1":
                    DisplayCharacters();
                    break;
                case "2":
                    FindCharacter();
                    break;
                case "3":
                    AddCharacter();
                    break;
                case "4":
                    LevelUpCharacter();
                    break;
                case "5":
                    return;
                default:
                    _output.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }

    CharacterReader characterReader = new CharacterReader();
    CharacterWriter characterWriter = new CharacterWriter();
    public void DisplayCharacters()
    {
        List<Character> characters = characterReader.ReadAllCharacters();
        foreach (Character character in characters)
        {
            Console.WriteLine(character.Name);
        }
    }

    public void AddCharacter()
    {
        Console.Write("Give the player a name (No Quotes): ");
        var name = Console.ReadLine();
        if (name != null && name.Contains(","))
        {
            name = '"' + name + '"';
        }
        Console.Write("Give the player a class: ");
        var charClass = Console.ReadLine();
        Console.Write("Give the player a level: ");
        int level = Convert.ToInt32(Console.ReadLine());
        Console.Write("Give the player a max HP: ");
        int hp = Convert.ToInt32(Console.ReadLine());

        string[] items = { };
        while (true)
        {
            Console.Write("Add item? (y/n): ");
            var addItem = Console.ReadLine()?.ToLower();
            if (addItem == "n" || addItem == "no")
            {
                break;
            }
            else if (addItem == "y" || addItem == "yes")
            {
                Console.WriteLine("Give the item a name: ");
                string? newItem = Console.ReadLine();
                if (newItem != null)
                {
                    items = items.Append(newItem).ToArray();
                }
            }
        }
        Character newCharacter = new Character() { Name = name, Class = charClass, Level = level, Health = hp, Equipment = items};

        characterWriter.AddACharacter(newCharacter);

    }

    public void LevelUpCharacter()
    {
        Console.WriteLine("What character would you like to level up?: ");
        List<Character> characters = characterReader.ReadAllCharacters();
        for (int c=0; c<characters.Count; c++)
        {
            Console.WriteLine($"{c+1}. {characters[c].Name}");
        }
        Console.WriteLine("Character: ");
        int userChoice = Convert.ToInt32(Console.ReadLine());
        characters[userChoice - 1].LevelUp();

        characterWriter.SaveCharacter(characters[userChoice - 1]);
    }
    public void FindCharacter()
    {
        List<Character> characters = characterReader.ReadAllCharacters();
        Console.Write($"What is the characters name?: ");
        string? userSearch = Console.ReadLine();
        var foundChar = characters.Where(character => character.Name == userSearch).FirstOrDefault();
        if (foundChar != null)
        {
            Console.WriteLine($"The stats for the character are:\n------------------\nName- {foundChar.Name}\nClass- {foundChar.Class}\nLevel- {foundChar.Level}\nHealth- {foundChar.Health}\nEquipment- {string.Join(", ", foundChar.Equipment)}\n------------------");
        }
        else
        {
            Console.WriteLine($"No character by the name of \"{userSearch}\" Found");
        }
    }
}