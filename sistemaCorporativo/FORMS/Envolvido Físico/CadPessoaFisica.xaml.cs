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
    /// Interaction logic for CadPessoaFisica.xaml
    /// </summary>
    public partial class CadPessoaFisica : MetroWindow
    {
        private String categoria;
		private String tipo;
        public CadPessoaFisica(String infocat, String infotipo)
        {
            InitializeComponent();
            categoria = infocat;
			tipo = infotipo;
        }
    }
}
