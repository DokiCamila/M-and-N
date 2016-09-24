using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Timers;
using MahApps.Metro;

namespace sistemaCorporativo
{
	/// <summary>
	/// Interaction logic for crimeManagementSplash.xaml
	/// </summary>
	public partial class CrimeManagementSplash : Window
	{
		private Timer timer;
		public CrimeManagementSplash()
		{
			this.InitializeComponent();
			
			timer = new Timer(43);
            timer.Elapsed += new ElapsedEventHandler(timer_Elapsed);
            timer.Start();
		}
		
		void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            this.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, (Action)(() =>
           {
               if (pgbCrimeM.Value < 43)
               {
                   pgbCrimeM.Value += 1;
               }
               else
               {
                   timer.Stop();
                   CrimeManagementWindow cmw = new CrimeManagementWindow();
				   this.Hide();
				   this.Close();
				   cmw.ShowDialog();
                   
				
               }
           }));
        }
		
		private void crimeManagementSplashDrag(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			DragMove();
		}

		private void Window_Loaded(object sender, System.Windows.RoutedEventArgs e)
		{
            label1.Foreground = Brushes.White;
            label2.Foreground = Brushes.White;
            pgRCrimeM.Foreground = Brushes.White;
		}
	}
}