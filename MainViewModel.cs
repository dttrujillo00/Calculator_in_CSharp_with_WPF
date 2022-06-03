using System;
using System.ComponentModel;
using System.Windows.Input;

namespace Calculator
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public MainViewModel()
        {
            InitializeCommands();
        }

        private void InitializeCommands()
        {
            SumCommand = new CommandHandler(() => SumAction(), () => CanExecute);
            SubtractionCommand = new CommandHandler(() => SubtractionAction(), () => CanExecute);
            MultiplyCommand = new CommandHandler(() => MultiplyAction(), () => CanExecute);
            DivideCommand = new CommandHandler(() => DivideAction(), () => CanExecuteDivide);
            ConvertToBinaryCommand = new CommandHandler(() => ConvertAction(), () => CanExecuteConversion);
        }

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler? PropertyChanged;

        #endregion

        #region Commands    

        public ICommand SumCommand { get; private set; }
        public ICommand SubtractionCommand { get; private set; }
        public ICommand MultiplyCommand { get; private set; }
        public ICommand DivideCommand { get; private set; }
        public ICommand ConvertToBinaryCommand { get; private set; }

        public void SumAction()
        {
            PropertyForResult = (Double.Parse(PropertyForInput1) + Double.Parse(PropertyForInput2)).ToString();
            
        }

        public void SubtractionAction()
        {
            PropertyForResult = (Double.Parse(PropertyForInput1) - Double.Parse(PropertyForInput2)).ToString();
        }

        public void MultiplyAction()
        {
            PropertyForResult = (Double.Parse(PropertyForInput1) * Double.Parse(PropertyForInput2)).ToString();
        }

        public void DivideAction()
        {
            PropertyForResult = (Double.Parse(PropertyForInput1) / Double.Parse(PropertyForInput2)).ToString();
            
        }

        public void ConvertAction()
        {
            int decimal_number = Int32.Parse(PropertyForDecimal);
            int reminder;
            string binary = string.Empty;

            while (decimal_number > 0)
            {
                reminder = decimal_number % 2;
                decimal_number /= 2;
                binary = reminder.ToString() + binary;
            }

            PropertyForBinary = binary;
        }

        public bool CanExecute
        {
            get
            {
                if (PropertyForInput1 == "" || PropertyForInput2 == "")
                    return false;
                return true;
            }
        }

        public bool CanExecuteDivide
        {
            get
            {
                if (PropertyForInput1 == "" || PropertyForInput2 == "" || Double.Parse(PropertyForInput2) == 0.0)
                    return false;
                return true;
            }
        }

        public bool CanExecuteConversion
        {
            get
            {
                if (PropertyForDecimal == "")
                    return false;
                return true;
            }
        }

        #endregion

        #region Property

        private string _propertyForResult;

        public string PropertyForResult
        {
            get { return _propertyForResult; }
            set { 
                _propertyForResult = value;
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(nameof(PropertyForResult)));
            }
        }

        private string _propertyForBinary;

        public string PropertyForBinary
        {
            get { return _propertyForBinary; }
            set { 
                _propertyForBinary = value; 
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(nameof(PropertyForBinary)));
            }
        }


        public string PropertyForInput1 { get; set; } = string.Empty;
        public string PropertyForInput2 { get; set; } = string.Empty;
        public string PropertyForDecimal { get; set; } = string.Empty;

        #endregion

    }

    /*********************************************
     *  IMPLEMENTACION DE LA INTERFAZ ICOMMAND   *
     *********************************************/
    public class CommandHandler : ICommand
    {
        private Action _action;
        private Func<bool> _canExecute;

        public CommandHandler(Action action, Func<bool> canExecute)
        {
            _action = action;
            _canExecute = canExecute;

        }

        event EventHandler? ICommand.CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }

            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }

        bool ICommand.CanExecute(object? parameter)
        {
            return _canExecute.Invoke();
        }

        void ICommand.Execute(object? parameter)
        {
            _action();
        }
    }
}
