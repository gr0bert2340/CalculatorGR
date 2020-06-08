namespace Calculator.Models
{
    public static class CalculatorHelper
    {
        public static string ToDisplay(this Operators? op)
        {
            switch (op)
            {
                case Operators.Add:
                    return "+";
                case Operators.Subtract:
                    return "-";
                case Operators.Divide:
                    return "÷";
                case Operators.Multiply:
                    return "x";
                case Operators.Power:
                    return "pow";
                default:
                    return string.Empty;
            }
        }
    }
}
