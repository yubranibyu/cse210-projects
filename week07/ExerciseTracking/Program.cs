using System;
using System.Collections.Generic;

// Clase Base: Activity
public abstract class Activity
{
    public DateTime Date { get; set; }
    public int Duration { get; set; } // Duration in minutes

    public Activity(DateTime date, int duration)
    {
        Date = date;
        Duration = duration;
    }

    public abstract double GetDistance();
    public abstract double GetSpeed();
    public abstract double GetPace();
    
    public string GetSummary()
    {
        return $"{Date:dd MMM yyyy} {this.GetType().Name} ({Duration} min) - Distance {GetDistance():0.0} km, Speed: {GetSpeed():0.0} kph, Pace: {GetPace():0.0} min per km";
    }
}

// Clase Derivada: Running
public class Running : Activity
{
    private double _distance; // Distance in kilometers

    public Running(DateTime date, int duration, double distance) : base(date, duration)
    {
        _distance = distance;
    }

    public override double GetDistance() => _distance;

    public override double GetSpeed() => (_distance / Duration) * 60;

    public override double GetPace() => Duration / _distance;
}

// Clase Derivada: Cycling
public class Cycling : Activity
{
    private double _speed; // Speed in kilometers per hour

    public Cycling(DateTime date, int duration, double speed) : base(date, duration)
    {
        _speed = speed;
    }

    public override double GetDistance() => (_speed * Duration) / 60;

    public override double GetSpeed() => _speed;

    public override double GetPace() => 60 / _speed;
}

// Clase Derivada: Swimming
public class Swimming : Activity
{
    private int _laps; // Number of laps swum

    public Swimming(DateTime date, int duration, int laps) : base(date, duration)
    {
        _laps = laps;
    }

    public override double GetDistance() => _laps * 50 / 1000.0; // 50 meters per lap

    public override double GetSpeed() => (GetDistance() / Duration) * 60;

    public override double GetPace() => Duration / GetDistance();
}

// Clase Principal: Program
class Program
{
    static void Main()
    {
        List<Activity> activities = new List<Activity>
        {
            new Running(new DateTime(2022, 11, 3), 30, 5.0),
            new Cycling(new DateTime(2022, 11, 3), 45, 20.0),
            new Swimming(new DateTime(2022, 11, 3), 30, 20)
        };

        foreach (var activity in activities)
        {
            Console.WriteLine(activity.GetSummary());
        }
    }
}
