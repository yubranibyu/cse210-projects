using System;
using System.Collections.Generic;
using System.IO;

// Base class Goal
abstract class Goal
{
    protected string _shortName;
    protected string _description;
    protected int _points;

    public Goal(string name, string description, int points)
    {
        _shortName = name;
        _description = description;
        _points = points;
    }

    public abstract void RecordEvent();
    public abstract bool IsComplete();
    public abstract string GetDetailsString();
    public abstract string GetStringRepresentation();
    
    public string GetName() => _shortName;
}

// Simple Goal
class SimpleGoal : Goal
{
    private bool _isComplete;

    public SimpleGoal(string name, string description, int points)
        : base(name, description, points)
    {
        _isComplete = false;
    }

    public override void RecordEvent()
    {
        _isComplete = true;
    }

    public override bool IsComplete() => _isComplete;
    public override string GetDetailsString() => $"{_shortName}: {_description} ({_points} points)";
    public override string GetStringRepresentation() => $"Simple,{_shortName},{_description},{_points},{_isComplete}";
}

// Eternal Goal
class EternalGoal : Goal
{
    public EternalGoal(string name, string description, int points)
        : base(name, description, points) { }

    public override void RecordEvent() { }
    public override bool IsComplete() => false;
    public override string GetDetailsString() => $"{_shortName}: {_description} ({_points} points)";
    public override string GetStringRepresentation() => $"Eternal,{_shortName},{_description},{_points}";
}

// Checklist Goal
class ChecklistGoal : Goal
{
    private int _amountCompleted;
    private int _target;
    private int _bonus;

    public ChecklistGoal(string name, string description, int points, int target, int bonus)
        : base(name, description, points)
    {
        _target = target;
        _bonus = bonus;
        _amountCompleted = 0;
    }

    public override void RecordEvent()
    {
        _amountCompleted++;
    }

    public override bool IsComplete() => _amountCompleted >= _target;
    public override string GetDetailsString() => $"{_shortName}: {_description} ({_points} points) Progress: {_amountCompleted}/{_target}";
    public override string GetStringRepresentation() => $"Checklist,{_shortName},{_description},{_points},{_amountCompleted},{_target},{_bonus}";
}

// Goal Manager
class GoalManager
{
    private List<Goal> _goals = new List<Goal>();
    private int _score;
    private const string SaveFile = "goals.txt";

    public void Start()
    {
        LoadGoals();
        while (true)
        {
            Console.WriteLine("1. Create Goal\n2. Record Event\n3. Show Goals\n4. Save and Exit");
            string choice = Console.ReadLine();
            if (choice == "1") CreateGoal();
            else if (choice == "2") RecordEvent();
            else if (choice == "3") DisplayPlayerInfo();
            else if (choice == "4") { SaveGoals(); break; }
        }
    }

    private void CreateGoal()
    {
        Console.Write("Name: ");
        string name = Console.ReadLine();
        Console.Write("Description: ");
        string desc = Console.ReadLine();
        Console.Write("Points: ");
        int points = int.Parse(Console.ReadLine());
        Console.WriteLine("Type: 1. Simple 2. Eternal 3. Checklist");
        string type = Console.ReadLine();

        if (type == "1") _goals.Add(new SimpleGoal(name, desc, points));
        else if (type == "2") _goals.Add(new EternalGoal(name, desc, points));
        else if (type == "3")
        {
            Console.Write("Target Count: ");
            int target = int.Parse(Console.ReadLine());
            Console.Write("Bonus Points: ");
            int bonus = int.Parse(Console.ReadLine());
            _goals.Add(new ChecklistGoal(name, desc, points, target, bonus));
        }
    }

    private void RecordEvent()
    {
        Console.Write("Goal Name: ");
        string name = Console.ReadLine();
        Goal goal = _goals.Find(g => g.GetName() == name);
        if (goal != null) goal.RecordEvent();
    }

    private void DisplayPlayerInfo()
    {
        foreach (var goal in _goals)
            Console.WriteLine(goal.GetDetailsString());
        Console.WriteLine($"Total Score: {_score}");
    }

    private void SaveGoals()
    {
        List<string> lines = new List<string> { _score.ToString() };
        foreach (var goal in _goals) lines.Add(goal.GetStringRepresentation());
        File.WriteAllLines(SaveFile, lines);
    }

    private void LoadGoals()
    {
        if (!File.Exists(SaveFile)) return;
        string[] lines = File.ReadAllLines(SaveFile);
        _score = int.Parse(lines[0]);
        foreach (string line in lines[1..])
        {
            string[] parts = line.Split(',');
            switch (parts[0])
            {
                case "Simple": _goals.Add(new SimpleGoal(parts[1], parts[2], int.Parse(parts[3]))); break;
                case "Eternal": _goals.Add(new EternalGoal(parts[1], parts[2], int.Parse(parts[3]))); break;
                case "Checklist": _goals.Add(new ChecklistGoal(parts[1], parts[2], int.Parse(parts[3]), int.Parse(parts[4]), int.Parse(parts[5]))); break;
            }
        }
    }
}

// Main Program
class Program
{
    static void Main()
    {
        GoalManager manager = new GoalManager();
        manager.Start();
    }
}
