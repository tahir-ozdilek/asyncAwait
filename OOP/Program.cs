namespace OOP
{
    using System;

    // Base class with generic parameters
    public abstract class Shape<T>
    {
        public T width;
        public T height;

        // Constructor
        public Shape(T width, T height)
        {
            this.width = width;
            this.height = height;
        }

        // Abstract method
        public abstract double Area();
    }

    // Interface with generic parameter
    public interface IDrawable
    {
        void Draw();
    }

    // Interface with generic parameter
    public interface IScalable<T>
    {
        void Scale(T factor);
    }

    // Subclass 1 with generic parameters
    public class Rectangle<T> : Shape<T>, IDrawable, IScalable<T>
    {
        // Constructor
        public Rectangle(T width, T height) : base(width, height)
        {
        }

        // Method implementation
        public override double Area()
        {
            double w = Convert.ToDouble(width);
            double h = Convert.ToDouble(height);
            return w * h;
        }

        // Explicit interface implementation
        void IDrawable.Draw()
        {
            Console.WriteLine("Drawing Rectangle");
        }

        // Explicit interface implementation
        void IScalable<T>.Scale(T factor)
        {
            double w = Convert.ToDouble(width);
            double h = Convert.ToDouble(height);
            w *= Convert.ToDouble(factor);
            h *= Convert.ToDouble(factor);
            width = (T)Convert.ChangeType(w, typeof(T));
            height = (T)Convert.ChangeType(h, typeof(T));
        }
    }

    // Subclass 2 with generic parameters
    public class Circle<T> : Shape<T>, IDrawable
    {
        // Constructor
        public Circle(T radius) : base(radius, radius)
        {
        }

        // Method implementation
        public override double Area()
        {
            double r = Convert.ToDouble(width);
            return Math.PI * r * r;
        }

        // Explicit interface implementation
        void IDrawable.Draw()
        {
            Console.WriteLine("Drawing Circle");
        }
    }

    // Static class
    public static class ShapeUtility
    {
        // Static method with generic parameter
        public static bool IsSquare<T>(Shape<T> shape)
        {
            if (shape is Rectangle<T>)
            {
                Rectangle<T> rect = (Rectangle<T>)shape;
                return rect.width.Equals(rect.height);
            }
            return false;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Rectangle<string> rect = new Rectangle<string>("a", "b");
            Circle<double> circle = new Circle<double>(3.0);

            // Using methods
            Console.WriteLine($"Area of Rectangle: {rect.Area()}");
            Console.WriteLine($"Area of Circle: {circle.Area()}");

            // Using explicit interfaces
            ((IDrawable)rect).Draw();
            ((IScalable<int>)rect).Scale(2);
            Console.WriteLine($"Scaled Rectangle Area: {rect.Area()}");

            ((IDrawable)circle).Draw();

            // Using static class
            Console.WriteLine($"Is the Rectangle a Square? {ShapeUtility.IsSquare(rect)}");
        }
    }
}
