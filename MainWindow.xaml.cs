using System.Windows;


namespace Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            DataContext = new MainViewModel();
            InitializeComponent();
           
        }

        /*private void Convertir(object sender, RoutedEventArgs e)
        {
            int decimal_number = (decimal_input.Text == "") ? 0 : Convert.ToInt32(decimal_input.Text);
            int resto;
            string binary = string.Empty;

            while (decimal_number > 0)
            {
                resto = decimal_number % 2;
                decimal_number /= 2;
                binary = resto.ToString() + binary;
            }

            binary_input.Text = binary;
        }

        private void toBinary(object sender, KeyEventArgs e)
        {
           
            int decimal_number = Convert.ToInt32(decimal_input.Text);
            int reminder;
            string binary = string.Empty;

            while(decimal_number > 0)
            {
                reminder = decimal_number % 2;
                decimal_number /= 2;
                binary = reminder.ToString() + binary;
            }

            binary_input.Text = binary;
        }

        private void toDecimal(object sender, KeyEventArgs e)
        {
            string binary = binary_input.Text;
            int decimal_number = 0;
            int exponente = 0;

            for(int i = binary.Length - 1; i >= 0; i--)
            {
                decimal_number += (Convert.ToInt32(binary[i]) * (int)Math.Pow(2, exponente));
                exponente++;
            }

            decimal_input.Text = Convert.ToString(decimal_number);
        }*/

       
    }
}
