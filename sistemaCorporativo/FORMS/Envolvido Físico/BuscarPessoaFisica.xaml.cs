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
using sistemaCorporativo.FORMS;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using MahApps.Metro.Behaviours;

namespace sistemaCorporativo
{
	/// <summary>
	/// Interaction logic for BuscarPessoaFisica.xaml
	/// </summary>
	public partial class BuscarPessoaFisica : MetroWindow
	{
		private String categoria;
		public BuscarPessoaFisica(String info)
		{
			this.InitializeComponent();
			categoria = info;
			
			// Insert code required on object creation below this point.
		}

		private void btnAddNovo_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			CadPessoaFisica cadPessoaWnd = new CadPessoaFisica(categoria,"Cadastrar");
            cadPessoaWnd.ShowDialog();
            this.Close();
		}
	}
}