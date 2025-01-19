using System;
using System.Collections.Generic;
using System.IO;

public class Entry
{
    public string Date { get; set; }
    public string Prompt { get; set; }
    public string Response { get; set; }

    public Entry(string date, string prompt, string response)
    {
        Date = date;
        Prompt = prompt;
        Response = response;
    }

    public override string ToString()
    {
        return $"{Date} | {Prompt} | {Response}";
    }
}

public class Journal
{
    private List<Entry> entries = new List<Entry>();
    private static Random random = new Random();

    // Lista de prompts
    private readonly List<string> prompts = new List<string>
    {
        "Who was the most interesting person I interacted with today?",
        "What was the best part of my day?",
        "How did I see the hand of the Lord in my life today?",
        "What was the strongest emotion I felt today?",
        "If I had one thing I could do over today, what would it be?"
    };

    // Agregar una nueva entrada
    public void AddEntry()
    {
        string prompt = prompts[random.Next(prompts.Count)];
        Console.WriteLine($"Prompt: {prompt}");
        Console.Write("Your response: ");
        string response = Console.ReadLine();
        string date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

        entries.Add(new Entry(date, prompt, response));
        Console.WriteLine("Entry added successfully!");
    }

    // Mostrar todas las entradas
    public void DisplayEntries()
    {
        if (entries.Count == 0)
        {
            Console.WriteLine("No entries in the journal.");
            return;
        }

        foreach (Entry entry in entries)
        {
            Console.WriteLine(entry);
        }
    }

    // Guardar las entradas en un archivo
    public void SaveToFile()
    {
        Console.Write("Enter the filename to save to: ");
        string filename = Console.ReadLine();

        try
        {
            using (StreamWriter writer = new StreamWriter(filename))
            {
                foreach (Entry entry in entries)
                {
                    writer.WriteLine($"{entry.Date}~|~{entry.Prompt}~|~{entry.Response}");
                }
            }
            Console.WriteLine("Journal saved successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving the journal: {ex.Message}");
        }
    }

    // Cargar las entradas desde un archivo
    public void LoadFromFile()
    {
        Console.Write("Enter the filename to load from: ");
        string filename = Console.ReadLine();

        try
        {
            if (!File.Exists(filename))
            {
                Console.WriteLine("File not found.");
                return;
            }

            entries.Clear();
            string[] lines = File.ReadAllLines(filename);
            foreach (string line in lines)
            {
                string[] parts = line.Split("~|~");
                if (parts.Length == 3)
                {
                    entries.Add(new Entry(parts[0], parts[1], parts[2]));
                }
            }
            Console.WriteLine("Journal loaded successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading the journal: {ex.Message}");
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        Journal journal = new Journal();
        int choice = 0;

        while (choice != 5)
        {
            Console.WriteLine("\nPlease select one of the following options:");
            Console.WriteLine("1. Write a new entry");
            Console.WriteLine("2. Display the journal");
            Console.WriteLine("3. Save the journal to a file");
            Console.WriteLine("4. Load the journal from a file");
            Console.WriteLine("5. Quit");
            Console.Write("What would you like to do? ");

            if (!int.TryParse(Console.ReadLine(), out choice))
            {
                Console.WriteLine("Invalid input. Please enter a number between 1 and 5.");
                continue;
            }

            switch (choice)
            {
                case 1:
                    journal.AddEntry();
                    break;
                case 2:
                    journal.DisplayEntries();
                    break;
                case 3:
                    journal.SaveToFile();
                    break;
                case 4:
                    journal.LoadFromFile();
                    break;
                case 5:
                    Console.WriteLine("Goodbye!");
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please select a valid option.");
                    break;
            }
        }
    }
}
