using System;
using System.Globalization;

namespace Calculator.Models
{
    public class CalculatorModel
    {
        private const string Zero = "0";
        private const string Error = "Err";
        private bool _isNum1 = true;

        private string _number1;
        private string _number2;
        private Operators? _operator;

        private string _display;
        public string Display
        {
            get { return _display; }
        }

        private string _calculation;
        public string Calculation
        {
            get { return _calculation; }
        }

        public bool IsInError
        {
            get { return _number1 == Error; }
        }

        public CalculatorModel()
        {
            _number1 = Zero;
            _number2 = string.Empty;
            _display = string.Empty;
            _calculation = string.Empty;
            UpdateDisplay();
        }

        public void InputNumber(int number)
        {
            if (IsInError)
            {
                Clear();
            }

            if (_isNum1)
            {
                if (_operator != null)
                {
                    Clear();
                }

                _number1 = UpdateNumber(_number1, number.ToString());
            }
            else
            {
                _number2 = UpdateNumber(_number2, number.ToString());
            }

            UpdateDisplay();
        }

        private string UpdateNumber(string number, string input)
        {
            if (number == Zero)
            {
                return input;
            }
            else if (number == "-" + Zero)
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
            if (!IsInError)
            {
                if (_isNum1)
                {
                    if (_operator != null)
                    {
                        Clear();
                    }

                    _number1 = UpdateNumberDecimal(_number1);
                }
                else
                {
                    _number2 = UpdateNumberDecimal(_number2);
                }

                UpdateDisplay();
            }
        }

        private string UpdateNumberDecimal(string number)
        {
            if (!number.Contains("."))
            {
                number = !string.IsNullOrWhiteSpace(number) ? 
                    number + "." : 
                    "0.";
            }

            return number;
        }

        public void Invert()
        {
            if (!IsInError)
            {
                if (_isNum1)
                {
                    _number1 = InvertNumber(_number1);
                }
                else
                {
                    _number2 = InvertNumber(_number2);
                }

                UpdateDisplay();
            }
        }

        private string InvertNumber(string number)
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
            if (!IsInError)
            {
                if (!_isNum1 && !string.IsNullOrWhiteSpace(_number2))
                {
                    Calculate();
                }
                _isNum1 = false;
                _number2 = string.Empty;
                _operator = op;
                UpdateCalculationPartial();
            }
        }

        public void Calculate()
        {
            if (_operator != null && !IsInError)
            {
                if (string.IsNullOrWhiteSpace(_number2))  {  _number2 = Zero; }
                UpdateCalculationFull();
                string result = string.Empty;
                try
                {
                    var n1 = double.Parse(_number1);
                    var n2 =  double.Parse(_number2);
                    switch (_operator)
                    {
                        case Operators.Add:
                            result = (n1 + n2).ToString(CultureInfo.InvariantCulture);
                            break;
                        case Operators.Subtract:
                            result = (n1 - n2).ToString(CultureInfo.InvariantCulture);
                            break;
                        case Operators.Divide:
                            result = (n1 / n2).ToString(CultureInfo.InvariantCulture);
                            break;
                        case Operators.Multiply:
                            result = (n1 * n2).ToString(CultureInfo.InvariantCulture);
                            break;
                        case Operators.Power:
                            result = (Math.Pow(n1, n2)).ToString(CultureInfo.InvariantCulture);
                            break;
                    }
                }
                catch (Exception e)
                {
                    Clear();
                    result = Error;
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

        public void BackSpace()
        {
            if (!IsInError)
            {
                if (_isNum1)
                {
                    if (_operator != null)
                    {
                        Clear();
                    }
                    else
                    {
                        _number1 = UpdateNumberBackspace(_number1);
                    }
                }
                else
                {
                    if (_number2.Length <= 1)
                    {
                        _operator = null;
                        _isNum1 = true;
                        _calculation = string.Empty;
                    }
                    else
                    {
                        _number2 = UpdateNumberBackspace(_number2);
                    }
                }
                UpdateDisplay();
            }
            else
            {
                Clear();
            }
        }

        private string UpdateNumberBackspace(string number)
        {
            if (!string.IsNullOrWhiteSpace(number) && number != Zero)
            {
                return number.Length > 1 ? 
                    number.Substring(0, number.Length - 1) : 
                    Zero;
            }

            return number;
        }

        public void Clear()
        {
            _isNum1 = true;
            _number1 = Zero;
            _number2 = string.Empty;
            _operator = null;
            _calculation = string.Empty;
            UpdateDisplay();
        }

        private void UpdateDisplay()
        {
            _display = _isNum1 ? 
                _number1 : 
                _number2 ;
        }

        private void UpdateCalculationPartial()
        {
            _calculation = $"{_number1} {_operator.ToDisplay()}";
        }

        private void UpdateCalculationFull()
        {
            _calculation = $"{_number1} {_operator.ToDisplay()} {_number2} =";
        }


    }
}
