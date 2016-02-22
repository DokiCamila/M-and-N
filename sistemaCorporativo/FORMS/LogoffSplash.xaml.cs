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
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using MahApps.Metro.Behaviours;
using System.Timers;
using MahApps.Metro;

namespace sistemaCorporativo
{
	/// <summary>
	/// Interaction logic for LogoffSplash.xaml
	/// </summary>
	public partial class LogoffSplash : Window
	{
		private Timer timer;
		public LogoffSplash()
		{
			this.InitializeComponent();
			
			timer = new Timer(50);
            timer.Elapsed += new ElapsedEventHandler(timer_Elapsed);
            timer.Start();
			
			
			
			// Insert code required on object creation below this point.
		}

        void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            this.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, (Action)(() =>
           {
               if (pgbLogoff.Value < 50)
               {
                   pgbLogoff.Value += 1;
               }
               else
               {
                   ThemeManager.ChangeAppTheme(Application.Current, "BaseDark");
                   timer.Stop();
                   LoginScreen loginScreen = new LoginScreen();
                   loginScreen.Show();
                   this.Close();


               }
           }));
        }
		private void LogoffSplashDrag(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			DragMove();
		}
	}
}