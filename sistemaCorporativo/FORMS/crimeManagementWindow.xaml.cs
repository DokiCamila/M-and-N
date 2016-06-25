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
using System.Windows.Media.Animation;

namespace sistemaCorporativo
{
	/// <summary>
	/// Interaction logic for crimeManagementWindow.xaml
	/// </summary>
	public partial class crimeManagementWindow : MetroWindow
	{
		public crimeManagementWindow()
		{
			this.InitializeComponent();
		}

		private void Menu_Loaded(object sender, System.Windows.RoutedEventArgs e)
		{
			expDpci.IsExpanded = true;
			expCitacoes.IsExpanded = false;
		}
		
		#region GRID INVESTIGAÇÕES
		
		private void btnInvestigacoes_Click(object sender, System.Windows.RoutedEventArgs e)
		{
            grdInvestigações.IsEnabled = true;
			Storyboard animation = this.FindResource("grdInvestigaçõesAnim") as Storyboard;
            animation.Begin();
            grdInvestigações.Visibility = Visibility.Visible;
		}

		private void btnAddCaso_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			CadCaso addCaso = new CadCaso();
			addCaso.ShowDialog();
		}
		
		private void btnClose_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			grdInvestigações.IsEnabled = false;
            grdInvestigações.Visibility = Visibility.Hidden;
		}
		#endregion
	}
}