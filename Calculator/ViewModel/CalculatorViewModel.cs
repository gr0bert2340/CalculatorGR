using Calculator.Models;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace Calculator.ViewModel
{
    public class CalculatorViewModel : ViewModelBase
    {
        private readonly Models.Calculator _calculator;

        public string Display
        {
            get { return _calculator.Display; }
        }

        public string Calculation
        {
            get { return _calculator.Calculation; }
        }

        private RelayCommand<string> _inputNumberCommand;
        public RelayCommand<string> InputNumberCommand
        {
            get { return _inputNumberCommand ?? (_inputNumberCommand = new RelayCommand<string>(InputNumber)); }
        }

        private RelayCommand<Operators> _inputOperatorCommand;
        public RelayCommand<Operators> InputOperatorCommand
        {
            get { return _inputOperatorCommand ?? (_inputOperatorCommand = new RelayCommand<Operators>(InputOperator)); }
        }

        private RelayCommand _inputDecimalCommand;
        public RelayCommand InputDecimalCommand
        {
            get { return _inputDecimalCommand ?? (_inputDecimalCommand = new RelayCommand(InputDecimal)); }
        }

        private RelayCommand _invertCommand;
        public RelayCommand InvertCommand
        {
            get { return _invertCommand ?? (_invertCommand = new RelayCommand(Invert)); }
        }

        private RelayCommand _backspaceCommand;
        public RelayCommand BackspaceCommand
        {
            get { return _backspaceCommand ?? (_backspaceCommand = new RelayCommand(Backspace)); }
        }

        private RelayCommand _clearCommand;
        public RelayCommand ClearCommand
        {
            get { return _clearCommand ?? (_clearCommand = new RelayCommand(Clear)); }
        }

        private RelayCommand _calculateCommand;
        public RelayCommand CalculateCommand
        {
            get { return _calculateCommand ?? (_calculateCommand = new RelayCommand(Calculate)); }
        }

        public CalculatorViewModel()
        {
            _calculator = new Models.Calculator();
        }

        public void InputNumber(string input)
        {
            _calculator?.InputNumber(int.Parse(input));
            UpdateDisplay();
        }

        public void InputDecimal()
        {
            _calculator?.InputDecimal();
            UpdateDisplay();
        }

        public void Invert()
        {
            _calculator?.Invert();
            UpdateDisplay();
        }

        public void Backspace()
        {
            _calculator?.BackSpace();
            UpdateDisplay();
        }

        public void InputOperator(Operators op)
        {
            _calculator?.InputOperator(op);
            UpdateDisplay();
        }

        public void Clear()
        {
            _calculator?.Clear();
            UpdateDisplay();
        }

        public void Calculate()
        {
            _calculator?.Calculate();
            UpdateDisplay();
        }

        private void UpdateDisplay()
        {
            RaisePropertyChanged(() => Display);
            RaisePropertyChanged(() => Calculation);
        }


    }
}