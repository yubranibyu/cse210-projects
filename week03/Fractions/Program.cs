using System;

public class Fraction
{
    // Atributos privados
    private int numerator;
    private int denominator;

    // Constructor sin parámetros: inicializa a 1/1
    public Fraction()
    {
        numerator = 1;
        denominator = 1;
    }

    // Constructor con un parámetro: inicializa a "top/1"
    public Fraction(int numerator)
    {
        this.numerator = numerator;
        this.denominator = 1;
    }

    // Constructor con dos parámetros: inicializa a "top/bottom"
    public Fraction(int numerator, int denominator)
    {
        if (denominator == 0)
        {
            throw new ArgumentException("El denominador no puede ser cero.");
        }

        this.numerator = numerator;
        this.denominator = denominator;
    }

    // Getters y setters para el numerador
    public int Numerator
    {
        get { return numerator; }
        set { numerator = value; }
    }

    // Getters y setters para el denominador
    public int Denominator
    {
        get { return denominator; }
        set
        {
            if (value == 0)
            {
                throw new ArgumentException("El denominador no puede ser cero.");
            }
            denominator = value;
        }
    }

    // Método para obtener la representación fraccional como cadena
    public string GetFractionString()
    {
        return $"{numerator}/{denominator}";
    }

    // Método para obtener el valor decimal
    public double GetDecimalValue()
    {
        return (double)numerator / denominator;
    }
}


class Program
{
    static void Main(string[] args)
    {
        // Crear fracciones usando los tres constructores
        Fraction fraction1 = new Fraction(); // 1/1
        Fraction fraction2 = new Fraction(5); // 5/1
        Fraction fraction3 = new Fraction(3, 4); // 3/4

        // Mostrar las representaciones de las fracciones
        Console.WriteLine(fraction1.GetFractionString());
        Console.WriteLine(fraction1.GetDecimalValue());

        Console.WriteLine(fraction2.GetFractionString());
        Console.WriteLine(fraction2.GetDecimalValue());

        Console.WriteLine(fraction3.GetFractionString());
        Console.WriteLine(fraction3.GetDecimalValue());

        // Modificar las fracciones usando setters
        fraction1.Numerator = 1;
        fraction1.Denominator = 3;

        Console.WriteLine(fraction1.GetFractionString());
        Console.WriteLine(fraction1.GetDecimalValue());
    }
}
