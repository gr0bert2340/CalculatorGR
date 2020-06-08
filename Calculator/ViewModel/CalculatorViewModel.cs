using System;
using System.Diagnostics;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace Calculator.ViewModel
{
    public class CalculatorViewModel : ViewModelBase
    {
        private bool _isNum1 = true;

        private string _number1 = "0";
        private string _number2 = "0";
        private Operators? _operator;

        private string _display = "0";
        public string Display
        {
            get { return _display; }
            private set { Set(() => Display, ref _display, value); }
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
            if (_isNum1)
            {
                if (_operator != null)
                {
                    Clear();
                }

                _number1 = UpdateNumber(_number1, input);
            }
            else
            {
                _number2 = UpdateNumber(_number2, input);
            }

            UpdateDisplay();
        }

        private string UpdateNumber(string number, string input)
        {
            if (number == "0")
            {
                return input;
            }
            else if (number == "-0")
            {
                return "-" + input;
            }
            else
            {
                return number + input;
            }
        }

        public void InputDecimal()
        {
            if (_isNum1)
            {
                if (_operator != null)
                {
                    Clear();
                }

                _number1 = InputDecimal(_number1);
            }
            else
            {
                _number2 = InputDecimal(_number2);
            }

            UpdateDisplay();
        }

        private string InputDecimal(string number)
        {
            if (!number.Contains("."))
            {
                number = number + ".";
            }

            return number;
        }

        public void Invert()
        {
            if (_isNum1)
            {
                _number1 = Invert(_number1);
            }
            else
            {
                _number2 = Invert(_number2);
            }

            UpdateDisplay();
        }

        private string Invert(string number)
        {
            if (number.StartsWith("-"))
            {
                return number.Substring(1);
            }
            else
            {
                return "-" + number;
            }
        }

        public void InputOperator(Operators op)
        {
            if (!_isNum1)
            {
                Calculate();
            }
            _isNum1 = false;
            _number2 = "0";
            _operator = op;
            Print();
        }

        public void Clear()
        {
            _isNum1 = true;
            _number1 = "0";
            _number2 = "0";
            _operator = null;
            UpdateDisplay();
        }

        public void Calculate()
        {
            if (_operator != null)
            {
                string result = string.Empty;
                try
                {
                    var n1 = double.Parse(_number1);
                    var n2 = double.Parse(_number2);
                    switch (_operator)
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
                    _number1 = result;
                    UpdateDisplay();
                }
            }
        }

        private void UpdateDisplay()
        {
            Display = _isNum1 ? _number1 : _number2;
            Print();
        }

        private void Print()
        {
            Debug.Print($"{_number1} {_operator} {_number2} {_isNum1}");
        }

    }
}