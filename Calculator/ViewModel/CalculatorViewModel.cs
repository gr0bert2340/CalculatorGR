using System;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace Calculator.ViewModel
{
    public class CalculatorViewModel : ViewModelBase
    {
        private bool _isNum1 = true;

        private string _number1 = "0";
        public string Number1
        {
            get { return _number1; }
        }

        private string _number2 = "0";
        public string Number2
        {
            get { return _number2; }
        }

        private Operators? _operator;
        public Operators? Operator
        {
            get { return _operator; }
        }

        public string Display
        {
            get { return _isNum1 ? Number1 : Number2; }
            private set
            {
                if (_isNum1)
                {
                    _number1 = value;
                    RaisePropertyChanged(() => Number1);
                }
                else
                {
                    _number2 = value;
                    RaisePropertyChanged(() => Number2);
                }
                RaisePropertyChanged(() => Display);
            }
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

        public void InputNumber(string input)
        {
            if (Operator != null)
            {
                _isNum1 = false;
            }

            if (Display == "0")
            {
                Display = input;
            }
            else if (Display == "-0")
            {
                Display = "-" + input;
            }
            else
            {
                Display = Display + input;
            }
        }

        public void InputDecimal()
        {
            if (Operator != null)
            {
                _isNum1 = false;
                _number2 = "0";
            }
            if (!Display.Contains("."))
            {
                Display = Display + ".";
            }
        }

        public void Invert()
        {
            if (Operator != null)
            {
                _isNum1 = false;
            }
            if (Display.StartsWith("-"))
            {
                Display = Display.Substring(1);
            }
            else
            {
                Display = "-" + Display;
            }
        }

        public void InputOperator(Operators op)
        {
            if (!_isNum1)
            {
                Calculate();
            }
            _number2 = "0";
            _operator = op;
            RaisePropertyChanged(() => Operator);
            RaisePropertyChanged(() => Number2);
            RaisePropertyChanged(() => Display);
        }

        public void Clear()
        {
            _isNum1 = true;
            _number1 = "0";
            _number2 = "0";
            _operator = null;
            RaisePropertyChanged(() => Number1);
            RaisePropertyChanged(() => Number2);
            RaisePropertyChanged(() => Operator);
            RaisePropertyChanged(() => Display);
        }

        public void Calculate()
        {
            if (Operator != null)
            {
                string result = string.Empty;
                try
                {
                    var n1 = double.Parse(_number1);
                    var n2 = double.Parse(_number2);
                    switch (Operator)
                    {
                        case Operators.Add:
                            result = (n1 + n2).ToString();
                            break;
                        case Operators.Subtract:
                            result = (n1 - n2).ToString();
                            break;
                        case Operators.Divide:
                            result = (n1 / n2).ToString();
                            break;
                        case Operators.Multiply:
                            result = (n1 * n2).ToString();
                            break;
                        case Operators.Power:
                            result = (Math.Pow(n1, n2)).ToString();
                            break;
                    }
                }
                catch (Exception e)
                {
                    result = "Err";
                    Console.WriteLine(e);
                }
                finally
                {
                    _isNum1 = true;
                    Display = result;
                }
            }
        }

    }
}