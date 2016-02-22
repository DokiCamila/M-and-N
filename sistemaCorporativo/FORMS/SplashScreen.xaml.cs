using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using MahApps.Metro.Behaviours;
using MahApps.Metro.Actions;
using MahApps.Metro.Converters;
using System.Timers;




namespace sistemaCorporativo.FORMS
{
    /// <summary>
    /// Interaction logic for SplashScreen.xaml
    /// </summary>
    public partial class SplashScreen : Window
    {
        private Timer timer;
        public  SplashScreen()
        {
            InitializeComponent();

            timer = new Timer(100);
            timer.Elapsed += new ElapsedEventHandler(timer_Elapsed);
            timer.Start();
           
        }

        private void Window_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
        	DragMove();
        }
        void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            this.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, (Action)(() =>
            {
                if (ProgressBar.Value < 100)
                {
                    ProgressBar.Value += 1;
                }
                else 
                {
                    timer.Stop();
                    LoginScreen loginScreen = new LoginScreen();
                    loginScreen.Show();
                    this.Close();
                }
					if (ProgressBar.Value == 2) 
            {
                lblStatus.Content = "Estabelecendo conexão";
            }
            if (ProgressBar.Value == 25)
            {
                               
            }
            if (ProgressBar.Value == 27)
            {
                lblStatus.Content = "Conectando com o banco de dados..";

            }
            if (ProgressBar.Value == 30)
            {
                lblStatus.Content = "Conectando com o banco de dados...";

            }
            if (ProgressBar.Value == 38)
            {
                lblStatus.Content = "Conectando com o banco de dados....";
            }
            if (ProgressBar.Value == 45)
            {
                lblStatus.Content = "Recebendo dados.";
            }
            if (ProgressBar.Value == 49)
            {
                lblStatus.Content = "Recebendo dados..";
            }
            if (ProgressBar.Value == 54)
            {
                lblStatus.Content = "Recebendo dados..";
            }
            if (ProgressBar.Value == 59)
            {
                lblStatus.Content = "Recebendo dados...";
            }
            if (ProgressBar.Value == 67)
            {
                lblStatus.Content = "Recebendo dados....";
            }
            }));
            
        }
    }
    
}
