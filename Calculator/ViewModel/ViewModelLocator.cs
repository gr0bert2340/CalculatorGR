
using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;

namespace Calculator.ViewModel
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            SimpleIoc.Default.Register<CalculatorViewModel>();
        }

        public CalculatorViewModel Calculator
        {
            get
            {
                return ServiceLocator.Current.GetInstance<CalculatorViewModel>();
            }
        }
        
        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}