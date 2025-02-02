using System;
using System.Collections.Generic;

// Class to represent a Comment
class Comment
{
    public string Name { get; set; }
    public string Text { get; set; }

    public Comment(string name, string text)
    {
        Name = name;
        Text = text;
    }
}

// Class to represent a Video
class Video
{
    public string Title { get; set; }
    public string Author { get; set; }
    public int Length { get; set; } // Length in seconds
    private List<Comment> comments = new List<Comment>();

    public Video(string title, string author, int length)
    {
        Title = title;
        Author = author;
        Length = length;
    }

    public void AddComment(Comment comment)
    {
        comments.Add(comment);
    }

    public int GetCommentCount()
    {
        return comments.Count;
    }

    public void DisplayVideoInfo()
    {
        Console.WriteLine($"Title: {Title}");
        Console.WriteLine($"Author: {Author}");
        Console.WriteLine($"Length: {Length} seconds");
        Console.WriteLine($"Number of comments: {GetCommentCount()}");
        Console.WriteLine("Comments:");
        foreach (var comment in comments)
        {
            Console.WriteLine($" - {comment.Name}: {comment.Text}");
        }
        Console.WriteLine();
    }
}

class Program
{
    static void Main()
    {
        // Create video objects
        Video video1 = new Video("Learning C#", "John Doe", 300);
        Video video2 = new Video("OOP Principles", "Jane Smith", 450);
        Video video3 = new Video("C# Collections", "Mike Johnson", 600);

        // Add comments to videos
        video1.AddComment(new Comment("Alice", "Great tutorial!"));
        video1.AddComment(new Comment("Bob", "Very helpful, thanks!"));
        video1.AddComment(new Comment("Charlie", "Can you explain more about classes?"));

        video2.AddComment(new Comment("David", "I love the examples!"));
        video2.AddComment(new Comment("Eve", "Very well explained."));
        video2.AddComment(new Comment("Frank", "I finally understand OOP!"));

        video3.AddComment(new Comment("Grace", "Awesome video!"));
        video3.AddComment(new Comment("Hannah", "Collections are tricky, but this helped."));
        video3.AddComment(new Comment("Ian", "Nice breakdown of the concepts."));

        // Store videos in a list
        List<Video> videos = new List<Video> { video1, video2, video3 };

        // Display video information
        foreach (var video in videos)
        {
            video.DisplayVideoInfo();
        }
    }
}
