using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        // Initialize scripture with reference and text
        Scripture scripture = new Scripture(
            new Reference("Proverbs", 3, 5, 6),
            "Trust in the Lord with all your heart and lean not on your own understanding; in all your ways submit to him, and he will make your paths straight."
        );

        // Main program loop
        while (!scripture.AllWordsHidden())
        {
            Console.Clear();
            Console.WriteLine(scripture.Display());
            Console.WriteLine("\nPress Enter to hide words or type 'quit' to exit.");
            string input = Console.ReadLine()?.Trim().ToLower();

            if (input == "quit")
                break;

            scripture.HideRandomWords();
        }

        Console.Clear();
        Console.WriteLine("All words are now hidden. Scripture memorization complete!");
        Console.WriteLine(scripture.Display());
    }
}

// Class to represent a scripture reference
class Reference
{
    public string Book { get; }
    public int StartChapter { get; }
    public int StartVerse { get; }
    public int? EndVerse { get; }

    public Reference(string book, int chapter, int verse)
    {
        Book = book;
        StartChapter = chapter;
        StartVerse = verse;
        EndVerse = null;
    }

    public Reference(string book, int chapter, int startVerse, int endVerse)
    {
        Book = book;
        StartChapter = chapter;
        StartVerse = startVerse;
        EndVerse = endVerse;
    }

    public override string ToString()
    {
        return EndVerse.HasValue
            ? $"{Book} {StartChapter}:{StartVerse}-{EndVerse}"
            : $"{Book} {StartChapter}:{StartVerse}";
    }
}

// Class to represent individual words in the scripture
class Word
{
    private string Text { get; } // Encapsulated text
    public bool IsHidden { get; private set; }

    public Word(string text)
    {
        Text = text;
        IsHidden = false;
    }

    // Method to hide the word
    public void Hide()
    {
        IsHidden = true;
    }

    public override string ToString()
    {
        return IsHidden ? new string('_', Text.Length) : Text;
    }
}

// Class to represent the scripture and its functionality
class Scripture
{
    private Reference Reference { get; }
    private List<Word> Words { get; }
    private Random Random { get; }

    public Scripture(Reference reference, string text)
    {
        Reference = reference;
        Words = text.Split(' ').Select(word => new Word(word)).ToList();
        Random = new Random();
    }

    // Displays the scripture with the current state of the words
    public string Display()
    {
        return $"{Reference}\n{string.Join(" ", Words)}";
    }

    // Hides random visible words
    public void HideRandomWords()
    {
        List<Word> visibleWords = Words.Where(word => !word.IsHidden).ToList();
        if (visibleWords.Any())
        {
            int wordsToHide = Math.Min(3, visibleWords.Count); // Hide up to 3 words
            for (int i = 0; i < wordsToHide; i++)
            {
                Word wordToHide = visibleWords[Random.Next(visibleWords.Count)];
                wordToHide.Hide();
                visibleWords.Remove(wordToHide);
            }
        }
    }

    // Checks if all words are hidden
    public bool AllWordsHidden()
    {
        return Words.All(word => word.IsHidden);
    }
}