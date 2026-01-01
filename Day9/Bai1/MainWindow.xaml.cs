using System;
using System.Windows;

namespace Bai1
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void SolveButton_Click(object sender, RoutedEventArgs e)
        {
            resultTextBlock.Text = string.Empty;
            exceptionA.Text = string.Empty;
            exceptionB.Text = string.Empty;
            exceptionC.Text = string.Empty;

            double a, b, c;
            bool isValid = true;

            if (!double.TryParse(textBoxA.Text, out a))
            {
                exceptionA.Text = "Please enter a valid number for a.";
                isValid = false;
            }

            if (!double.TryParse(textBoxB.Text, out b))
            {
                exceptionB.Text = "Please enter a valid number for b.";
                isValid = false;
            }

            if (!double.TryParse(textBoxC.Text, out c))
            {
                exceptionC.Text = "Please enter a valid number for c.";
                isValid = false;
            }

            if (isValid)
            {
                string result = SolveQuadraticEquation(a, b, c);
                resultTextBlock.Text = result;
            }
        }

        private string SolveQuadraticEquation(double a, double b, double c)
        {
            double discriminant = b * b - 4 * a * c;

            if (discriminant > 0)
            {
                double root1 = (-b + Math.Sqrt(discriminant)) / (2 * a);
                double root2 = (-b - Math.Sqrt(discriminant)) / (2 * a);
                return $"The roots are real and different.\nRoot 1: {root1}\nRoot 2: {root2}";
            }
            else if (discriminant == 0)
            {
                double root = -b / (2 * a);
                return $"The root is real and the same.\nRoot: {root}";
            }
            else
            {
                double realPart = -b / (2 * a);
                double imaginaryPart = Math.Sqrt(-discriminant) / (2 * a);
                return $"The roots are complex and different.\nRoot 1: {realPart} + {imaginaryPart}i\nRoot 2: {realPart} - {imaginaryPart}i";
            }
        }
    }
}
