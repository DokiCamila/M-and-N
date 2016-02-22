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

namespace sistemaCorporativo.FORMS
{
    /// <summary>
    /// Interaction logic for cadDenuncia.xaml
    /// </summary>
    public partial class CadDenuncia : MetroWindow
    {
        public CadDenuncia()
        {
            InitializeComponent();
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Close();   
            }
            catch (Exception ex)
            {
                
                MessageBox.Show(ex.Message);
            }
        }

        private void btnCancelar1_Click(object sender, System.Windows.RoutedEventArgs e)
        {
        	this.Close();
        }
    }
}
