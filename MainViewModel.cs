using System;
using System.ComponentModel;
using System.Windows.Input;

namespace Calculator
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private ICommand _clickCommandSum;
        private ICommand _clickCommandSub;
        private ICommand _clickCommandMultiply;
        private ICommand _clickCommandDivide;
        public string PropertyForResult { get; set; } = string.Empty;
        public string PropertyForInput1 { get; set; } = string.Empty;
        public string PropertyForInput2 { get; set; } = string.Empty;

        public ICommand SumCommand
        {
            get
            {
                return _clickCommandSum ?? (_clickCommandSum = new CommandHandler(() => SumAction(), () => CanExecute));
            }
        }
        public ICommand SubCommand
        {
            get
            {
                return _clickCommandSub ?? (_clickCommandSub = new CommandHandler(() => SubAction(), () => CanExecute));
            }
        }

        public ICommand MultiplyCommand
        {
            get
            {
                return _clickCommandMultiply ?? (_clickCommandMultiply = new CommandHandler(() => MultiplyAction(), () => CanExecute));
            }
        }

        public ICommand DivideCommand
        {
            get
            {
                return _clickCommandDivide ?? (_clickCommandDivide = new CommandHandler(() => DivideAction(), () => CanExecuteDivide));
            }
        }


        public void SumAction()
        {
            PropertyForResult = (Double.Parse(PropertyForInput1) + Double.Parse(PropertyForInput2)).ToString();
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs("PropertyForResult"));
        }

        public void SubAction()
        {
            PropertyForResult = (Double.Parse(PropertyForInput1) - Double.Parse(PropertyForInput2)).ToString();
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs("PropertyForResult"));
        }

        public void MultiplyAction()
        {
            PropertyForResult = (Double.Parse(PropertyForInput1) * Double.Parse(PropertyForInput2)).ToString();
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs("PropertyForResult"));
        }

        public void DivideAction()
        {
            PropertyForResult = (Double.Parse(PropertyForInput1) / Double.Parse(PropertyForInput2)).ToString();
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs("PropertyForResult"));
        }

        public bool CanExecute
        {
            get
            {
                if(PropertyForInput1 != "" && PropertyForInput2 != "")
                    return true;
                return false;
            }
        }

        public bool CanExecuteDivide
        {
            get
            {
                if(PropertyForInput1 != "" && PropertyForInput2 != "" && Double.Parse(PropertyForInput2) != 0.0)
                    return true;

                return false;
            }
        }
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
