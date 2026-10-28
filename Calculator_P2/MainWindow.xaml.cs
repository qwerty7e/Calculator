using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;


namespace Calculator_P2
{
    public partial class MainWindow : Window
    {
        private CalculatorService calc = new CalculatorService();
        private string currentInput = "";
        private string mode = "standard";

        public MainWindow()
        {
            InitializeComponent();

            calc.History = calc.LoadHistory();

            foreach (var item in calc.History)
                HistoryList.Items.Add(item);
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string value = (sender as Button).Content.ToString();
            currentInput += value;
            Display.Text = currentInput;
        }

        private void Function_Click(object sender, RoutedEventArgs e)
        {
            string func = (sender as Button).Content.ToString();

            if (func == "sqrt" || func == "inv")
                currentInput = func + currentInput;
            else
                currentInput = func + " " + currentInput;

            Display.Text = currentInput;
        }
        private void Equals_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                double result = calc.Calculate(currentInput, mode);
                Display.Text = result.ToString();

                string record = currentInput + " = " + result;

                calc.History.Add(record);
                HistoryList.Items.Add(record);
                calc.SaveHistory(record);

                currentInput = result.ToString();
            }
            catch (Exception ex)
            {
                Display.Text = ex.Message;
            }
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            currentInput = "";
            Display.Text = "";
        }

        private void ClearHistory_Click(object sender, RoutedEventArgs e)
        {
            calc.ClearHistory();
            HistoryList.Items.Clear();
        }

        private void ModeChanged(object sender, SelectionChangedEventArgs e)
        {
            bool isEngineering = ModeBox.SelectedIndex == 1;
            mode = isEngineering ? "engineering" : "standard";

            Visibility v = isEngineering ? Visibility.Visible : Visibility.Collapsed;

            BtnSin.Visibility = v;
            BtnCos.Visibility = v;
            BtnTan.Visibility = v;
            BtnLog.Visibility = v;

            BtnPow.Visibility = v;
            BtnSqrt.Visibility = v;
            BtnInv.Visibility = v;
            BtnPercent.Visibility = v;
        }

        private void ThemeChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (ThemeBox.SelectedIndex)
            {
                case 0: ApplyLightTheme(); break;
                case 1: ApplyDarkTheme(); break;
                case 2: ApplyBlueTheme(); break;
                case 3: ApplyGreenTheme(); break;
            }
        }

        private void ApplyDarkTheme()
        {
            this.Background = Brushes.Black;
            Display.Background = Brushes.Black;
            Display.Foreground = Brushes.White;

            foreach (var btn in FindVisualChildren<Button>(this))
            {
                btn.Background = Brushes.Gray;
                btn.Foreground = Brushes.White;
            }
        }

        private void ApplyLightTheme()
        {
            this.Background = Brushes.White;
            Display.Background = Brushes.White;
            Display.Foreground = Brushes.Black;

            foreach (var btn in FindVisualChildren<Button>(this))
            {
                btn.Background = Brushes.LightGray;
                btn.Foreground = Brushes.Black;
            }
        }

        private void ApplyBlueTheme()
        {
            this.Background = Brushes.DarkBlue;
            Display.Background = Brushes.LightBlue;
            Display.Foreground = Brushes.Black;

            foreach (var btn in FindVisualChildren<Button>(this))
            {
                btn.Background = Brushes.SteelBlue;
                btn.Foreground = Brushes.White;
            }
        }

        private void ApplyGreenTheme()
        {
            this.Background = Brushes.DarkGreen;
            Display.Background = Brushes.LightGreen;
            Display.Foreground = Brushes.Black;

            foreach (var btn in FindVisualChildren<Button>(this))
            {
                btn.Background = Brushes.Green;
                btn.Foreground = Brushes.White;
            }
        }
        public static System.Collections.Generic.IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj)
            where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);

                    if (child != null && child is T)
                        yield return (T)child;

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                        yield return childOfChild;
                }
            }
        }

    }
}
