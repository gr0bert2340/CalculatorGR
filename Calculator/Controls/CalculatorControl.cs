using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Calculator.Models;

namespace Calculator.Controls
{
    public class CalculatorControl : Control
    {
        public static RoutedCommand InputNumberCommand =
            new RoutedUICommand("Input Number", "InputNumber", typeof(CalculatorControl));
        public static RoutedCommand InputOperatorCommand =
            new RoutedUICommand("Input Operator", "InputOperator", typeof(CalculatorControl));
        public static RoutedCommand InputDecimalCommand =
            new RoutedUICommand("Input Decimal", "InputDecimal", typeof(CalculatorControl));
        public static RoutedCommand InvertCommand =
            new RoutedUICommand("Invert", "Invert", typeof(CalculatorControl));
        public static RoutedCommand BackspaceCommand =
            new RoutedUICommand("Backspace", "Backspace", typeof(CalculatorControl));
        public static RoutedCommand ClearCommand =
            new RoutedUICommand("Clear", "Clear", typeof(CalculatorControl));
        public static RoutedCommand CalculateCommand =
            new RoutedUICommand("Calculate", "Calculate", typeof(CalculatorControl));

        private readonly CalculatorModel _calculator;

        public static readonly DependencyPropertyKey DisplayKey =
            DependencyProperty.RegisterReadOnly("Display", typeof(object), typeof(CalculatorControl), new PropertyMetadata(null));
        public static readonly DependencyProperty DisplayProperty = DisplayKey.DependencyProperty;
        public double Display
        {
            get { return (double)GetValue(DisplayProperty); }
        }

        public static readonly DependencyPropertyKey CalculationDisplayKey =
            DependencyProperty.RegisterReadOnly("CalculationDisplay", typeof(object), typeof(CalculatorControl), new PropertyMetadata(null));
        public static readonly DependencyProperty CalculationDisplayProperty = CalculationDisplayKey.DependencyProperty;
        public double CalculationDisplay
        {
            get { return (double)GetValue(CalculationDisplayProperty); }
        }

        static CalculatorControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CalculatorControl), new FrameworkPropertyMetadata(typeof(CalculatorControl)));
        }

        public CalculatorControl()
        {
            _calculator = new CalculatorModel();

            var inputNumberBinding = new CommandBinding(InputNumberCommand, OnInputNumber, CanInputNumber);
            var inputOperatorBinding = new CommandBinding(InputOperatorCommand, OnInputOperator, CanInputOperator);
            var inputDecimalBinding = new CommandBinding(InputDecimalCommand, OnInputDecimal, CanInputDecimal);
            var invertBinding = new CommandBinding(InvertCommand, OnInvert, CanInvert);
            var backspaceBinding = new CommandBinding(BackspaceCommand, OnBackspace, CanBackspace);
            var clearBinding = new CommandBinding(ClearCommand, OnClear, CanClear);
            var calculateBinding = new CommandBinding(CalculateCommand, OnCalculate, CanCalculate);

            CommandBindings.Add(inputNumberBinding);
            CommandBindings.Add(inputOperatorBinding);
            CommandBindings.Add(inputDecimalBinding);
            CommandBindings.Add(invertBinding);
            CommandBindings.Add(backspaceBinding);
            CommandBindings.Add(clearBinding);
            CommandBindings.Add(calculateBinding);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            UpdateDisplay();
        }

        private void CanInputNumber(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void OnInputNumber(object sender, ExecutedRoutedEventArgs e)
        {
            if (e.Parameter is string parameter)
            {
                _calculator?.InputNumber(int.Parse(parameter));
                UpdateDisplay();
            }
        }

        private void CanInputOperator(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void OnInputOperator(object sender, ExecutedRoutedEventArgs e)
        {
            if (e.Parameter is Operators parameter)
            {
                _calculator?.InputOperator(parameter);
                UpdateDisplay();
            }
        }

        private void CanInputDecimal(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void OnInputDecimal(object sender, ExecutedRoutedEventArgs e)
        {
            _calculator?.InputDecimal();
            UpdateDisplay();
        }

        private void CanInvert(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void OnInvert(object sender, ExecutedRoutedEventArgs e)
        {
            _calculator?.Invert();
            UpdateDisplay();
        }

        private void CanBackspace(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void OnBackspace(object sender, ExecutedRoutedEventArgs e)
        {
            _calculator?.BackSpace();
            UpdateDisplay();
        }

        private void CanClear(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void OnClear(object sender, ExecutedRoutedEventArgs e)
        {
            _calculator?.Clear();
            UpdateDisplay();
        }

        private void CanCalculate(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void OnCalculate(object sender, ExecutedRoutedEventArgs e)
        {
            _calculator?.Calculate();
            UpdateDisplay();
        }

        private void UpdateDisplay()
        {
            SetValue(DisplayKey, _calculator.Display);
            SetValue(CalculationDisplayKey, _calculator.Calculation);
        }
    }
}
