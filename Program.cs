using System;

public class Point
{
    public int X { get; set; }
    public int Y { get; set; }
}

public class Line
{
    private Point _p0;
    private Point _p1;
    private double _slope;
    private Point? _midpoint;

    public Line(Point p0, Point p1)
    {
        _p0 = p0;
        _p1 = p1;
    }

    public Line()
    {
        _p0 = new Point();
        _p1 = new Point();
    }

    public Line(int x0, int y0, int x1, int y1)
    {
        _p0 = new Point { X = x0, Y = y0 };
        _p1 = new Point { X = x1, Y = y1 };
    }

    public Point P0
    {
        get { return _p0; }
        set { _p0 = value; }
    }

    public Point P1
    {
        get { return _p1; }
        set { _p1 = value; }
    }

    public double Slope
    {
        get
        {
            _slope = (_p1.Y - _p0.Y) / (double)(_p1.X - _p0.X);
            return _slope;
        }
    }

    public Point? Midpoint
    {
        get { return _midpoint; }
    }

    public void Draw()
    {
        int dx = _p1.X - _p0.X;
        int dy = _p1.Y - _p0.Y;
        int steps = Math.Max(Math.Abs(dx), Math.Abs(dy));

        float xIncrement = dx / (float)steps;
        float yIncrement = dy / (float)steps;

        float x = _p0.X;
        float y = _p0.Y;

        for (int i = 0; i <= steps; i++)
        {
            Console.WriteLine($"Pixel at ({Math.Round(x)}, {Math.Round(y)})");
            x += xIncrement;
            y += yIncrement;
        }
    }

    public void Perturb(int xmin, int xmax, int ymin, int ymax)
    {
        Random random = new Random();
        _p0.X += random.Next(xmin, xmax + 1);
        _p0.Y += random.Next(ymin, ymax + 1);
        _p1.X += random.Next(xmin, xmax + 1);
        _p1.Y += random.Next(ymin, ymax + 1);
    }

    public void Collapse()
    {
        int x = (_p0.X + _p1.X) / 2;
        int y = (_p0.Y + _p1.Y) / 2;
        _midpoint = new Point { X = x, Y = y };
        _p0 = _midpoint;
        _p1 = _midpoint;
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Create a line using user input
        Console.WriteLine("Enter the coordinates for Point P0:");
        Point p0 = GetUserInput();

        Console.WriteLine("Enter the coordinates for Point P1:");
        Point p1 = GetUserInput();

        Line line = new Line(p0, p1);

        // Access and modify properties
        Console.WriteLine($"P0: ({line.P0.X}, {line.P0.Y})");
        Console.WriteLine($"P1: ({line.P1.X}, {line.P1.Y})");
        Console.WriteLine($"Slope: {line.Slope}");
        Console.WriteLine($"Midpoint: {line.Midpoint}");

        // Draw the line
        line.Draw();

        // Perturb the line
        Console.WriteLine("Enter the minimum and maximum perturbation values for x and y:");
        int xmin = GetUserInput("xmin");
        int xmax = GetUserInput("xmax");
        int ymin = GetUserInput("ymin");
        int ymax = GetUserInput("ymax");

        line.Perturb(xmin, xmax, ymin, ymax);
        Console.WriteLine($"Perturbed P0: ({line.P0.X}, {line.P0.Y})");
        Console.WriteLine($"Perturbed P1: ({line.P1.X}, {line.P1.Y})");

        // Collapse the line
        line.Collapse();
        Console.WriteLine($"Collapsed P0: ({line.P0.X}, {line.P0.Y})");
        Console.WriteLine($"Collapsed P1: ({line.P1.X}, {line.P1.Y})");
    }

    static Point GetUserInput()
    {
        Console.Write("Enter X coordinate: ");
        int x = int.Parse(Console.ReadLine());

        Console.Write("Enter Y coordinate: ");
        int y = int.Parse(Console.ReadLine());

        return new Point { X = x, Y = y };
    }

    static int GetUserInput(string variableName)
    {
        Console.Write($"Enter {variableName}: ");
        return int.Parse(Console.ReadLine());
    }
}
