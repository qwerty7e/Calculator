using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Calculator_P2
{
    internal class CalculatorService
    {

        public List<string> History { get; set; } = new List<string>();

        public List<string> LoadHistory()
        {
            if (File.Exists("history.txt"))
                return File.ReadAllLines("history.txt").ToList();

            return new List<string>();
        }

        public double Calculate(string input, string mode)
        {
            if (mode == "standard")
                return Standard(input);

            if (mode == "engineering")
                return Engineering(input);

            throw new Exception("Неизвестный режим");
        }

        private double Standard(string input)
        {
            if (input.Contains("+"))
            {
                var p = input.Split('+');

                double a = double.Parse(p[0].Trim());
                double b = double.Parse(p[1].Trim());

                return a + b;
            }
            else if (input.Contains("-"))
            {
                var p = input.Split('-');
                double a = double.Parse(p[0].Trim());
                double b = double.Parse(p[1].Trim());
                return a - b;
            }
            else if (input.Contains("*"))
            {
                var p = input.Split('*');
                double a = double.Parse(p[0].Trim());
                double b = double.Parse(p[1].Trim());
                return a * b;
            }
            else if (input.Contains("/"))
            {
                var p = input.Split('/');

                double a = double.Parse(p[0].Trim());
                double b = double.Parse(p[1].Trim());

                if (b == 0)
                    throw new Exception("Деление на 0");

                return a / b;
            }

            throw new Exception("Ошибка ввода");
        }

        private double Engineering(string input)
        {
            double a, b;

            if (input.Contains("^"))
            {
                var p = input.Split('^');
                return Math.Pow(double.Parse(p[0]), double.Parse(p[1]));
            }

            if (input.StartsWith("sqrt"))
            {
                a = double.Parse(input.Substring(4).Trim());
                return Math.Sqrt(a);
            }

            if (input.StartsWith("inv"))
            {
                a = double.Parse(input.Substring(4));
                return 1 / a;
            }

            if (input.Contains("%"))
            {
                var p = input.Split('%');
                a = double.Parse(p[0]);
                b = double.Parse(p[1]);
                return a * b / 100;
            }

            if (input.StartsWith("sin"))
                return Math.Sin(double.Parse(input.Substring(4)) * Math.PI / 180);

            if (input.StartsWith("cos"))
                return Math.Cos(double.Parse(input.Substring(4)) * Math.PI / 180);

            if (input.StartsWith("tan"))
                return Math.Tan(double.Parse(input.Substring(4)) * Math.PI / 180);

            if (input.StartsWith("ln"))
                return Math.Log(double.Parse(input.Substring(3)));

            if (input.StartsWith("log"))
                return Math.Log10(double.Parse(input.Substring(4)));

            if (input.Contains("+"))
            {
                var p = input.Split('+');

                 a = double.Parse(p[0].Trim());
                 b = double.Parse(p[1].Trim());

                return a + b;
            }

            if (input.Contains("-"))
            {
                var p = input.Split('-');
                 a = double.Parse(p[0].Trim());
                 b = double.Parse(p[1].Trim());
                return a - b;
            }

            if (input.Contains("*"))
            {
                var p = input.Split('*');
                 a = double.Parse(p[0].Trim());
                 b = double.Parse(p[1].Trim());
                return a * b;
            }

            else if (input.Contains("/"))
            {
                var p = input.Split('/');

                 a = double.Parse(p[0].Trim());
                 b = double.Parse(p[1].Trim());

                if (b == 0)
                    throw new Exception("Деление на 0");

                return a / b;
            }

            throw new Exception("Ошибка ввода");
        }
        public void SaveHistory(string record)
        {
            File.AppendAllText("history.txt", record + Environment.NewLine);
        }

        public void ClearHistory()
        {
        History.Clear();
        File.WriteAllText("history.txt", "");
        }
    }
}

