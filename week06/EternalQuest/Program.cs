using System;
using System.Collections.Generic;
using System.IO;

// Base class Goal
abstract class Goal
{
    protected string Name;
    protected string Description;
    protected int Points;
    protected bool IsCompleted;

    public Goal(string name, string description, int points)
    {
        Name = name;
        Description = description;
        Points = points;
        IsCompleted = false;
    }

    public abstract int RecordEvent();
    public abstract string GetStatus();
    public abstract string SaveFormat();

    public string GetName() => Name;
}

// Simple Goal
class SimpleGoal : Goal
{
    public SimpleGoal(string name, string description, int points)
        : base(name, description, points) { }

    public override int RecordEvent()
    {
        if (!IsCompleted)
        {
            IsCompleted = true;
            return Points;
        }
        return 0;
    }

    public override string GetStatus() => IsCompleted ? "[X]" : "[ ]";
    public override string SaveFormat() => $"Simple,{Name},{Description},{Points},{IsCompleted}";
}

// Eternal Goal
class EternalGoal : Goal
{
    public EternalGoal(string name, string description, int points)
        : base(name, description, points) { }

    public override int RecordEvent() => Points;
    public override string GetStatus() => "[∞]";
    public override string SaveFormat() => $"Eternal,{Name},{Description},{Points}";
}

// Checklist Goal
class ChecklistGoal : Goal
{
    private int TimesCompleted;
    private int TargetCount;
    private int BonusPoints;

    public ChecklistGoal(string name, string description, int points, int targetCount, int bonusPoints)
        : base(name, description, points)
    {
        TargetCount = targetCount;
        BonusPoints = bonusPoints;
        TimesCompleted = 0;
    }

    public override int RecordEvent()
    {
        TimesCompleted++;
        if (TimesCompleted >= TargetCount)
        {
            IsCompleted = true;
            return Points + BonusPoints;
        }
        return Points;
    }

    public override string GetStatus() => IsCompleted ? "[X]" : $"[{TimesCompleted}/{TargetCount}]";
    public override string SaveFormat() => $"Checklist,{Name},{Description},{Points},{TimesCompleted},{TargetCount},{BonusPoints}";
}

// User class
class User
{
    public int Score { get; private set; }
    private List<Goal> Goals = new List<Goal>();
    private string SaveFile = "goals.txt";

    public void AddGoal(Goal goal) => Goals.Add(goal);
    public void RecordGoal(string name)
    {
        Goal goal = Goals.Find(g => g.GetName() == name);
        if (goal != null) Score += goal.RecordEvent();
    }

    public void DisplayGoals()
    {
        foreach (var goal in Goals)
            Console.WriteLine($"{goal.GetStatus()} {goal.GetName()}");
        Console.WriteLine($"Total Score: {Score}");
    }

    public void SaveGoals()
    {
        List<string> lines = new List<string> { Score.ToString() };
        foreach (var goal in Goals) lines.Add(goal.SaveFormat());
        File.WriteAllLines(SaveFile, lines);
    }

    public void LoadGoals()
    {
        if (!File.Exists(SaveFile)) return;
        string[] lines = File.ReadAllLines(SaveFile);
        Score = int.Parse(lines[0]);
        foreach (string line in lines[1..])
        {
            string[] parts = line.Split(',');
            switch (parts[0])
            {
                case "Simple": Goals.Add(new SimpleGoal(parts[1], parts[2], int.Parse(parts[3]))); break;
                case "Eternal": Goals.Add(new EternalGoal(parts[1], parts[2], int.Parse(parts[3]))); break;
                case "Checklist": Goals.Add(new ChecklistGoal(parts[1], parts[2], int.Parse(parts[3]), int.Parse(parts[5]), int.Parse(parts[6]))); break;
            }
        }
    }
}

// Main Program
class Program
{
    static void Main()
    {
        User user = new User();
        user.LoadGoals();
        
        while (true)
        {
            Console.WriteLine("1. Create Goal\n2. Record Event\n3. Show Goals\n4. Save and Exit");
            string choice = Console.ReadLine();
            
            if (choice == "1")
            {
                Console.Write("Name: ");
                string name = Console.ReadLine();
                Console.Write("Description: ");
                string desc = Console.ReadLine();
                Console.Write("Points: ");
                int points = int.Parse(Console.ReadLine());
                Console.WriteLine("Type: 1. Simple 2. Eternal 3. Checklist");
                string type = Console.ReadLine();
                
                if (type == "1") user.AddGoal(new SimpleGoal(name, desc, points));
                else if (type == "2") user.AddGoal(new EternalGoal(name, desc, points));
                else if (type == "3")
                {
                    Console.Write("Required times: ");
                    int target = int.Parse(Console.ReadLine());
                    Console.Write("Bonus: ");
                    int bonus = int.Parse(Console.ReadLine());
                    user.AddGoal(new ChecklistGoal(name, desc, points, target, bonus));
                }
            }
            else if (choice == "2")
            {
                Console.Write("Goal Name: ");
                string name = Console.ReadLine();
                user.RecordGoal(name);
            }
            else if (choice == "3") user.DisplayGoals();
            else if (choice == "4") { user.SaveGoals(); break; }
        }
    }
}
