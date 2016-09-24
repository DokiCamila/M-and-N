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
using System.Data;

namespace sistemaCorporativo.FORMS
{
    /// <summary>
    /// Interaction logic for TipoPessoaFisicaWindow.xaml
    /// </summary>
    public partial class TipoPessoaFisicaWindow : MetroWindow
    {
        public TipoPessoaFisicaWindow()
        {
            InitializeComponent();
        }

        private void wndTipoPessoaFisica_Loaded(object sender, RoutedEventArgs e)
        {

            //Criando DataSource com 3 colunas
            DataTable dt = new DataTable();
            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("Categoria");
            dt.Columns.Add("Descrição");

            for (int iCount = 1; iCount <= 7; iCount++)
            {
                switch (iCount)
                {
                    case 1:
                        var row = dt.NewRow();
                        row["ID"] = iCount;
                        row["Categoria"] = "Criminoso";
                        row["Descrição"] = "Indivíduo sob investigação; Réu; Sujeito ativo.";
                        dt.Rows.Add(row);
                        break;
                    case 2:
                        row = dt.NewRow();
                        row["ID"] = iCount;
                        row["Categoria"] = "Cúmplice";
                        row["Descrição"] = "Indivíduo que está vinculado/ligado ao criminoso.";
                        dt.Rows.Add(row);
                        break;
                    case 3:
                        row = dt.NewRow();
                        row["ID"] = iCount;
                        row["Categoria"] = "Delator";
                        row["Descrição"] = "Indivíduo que fez a alegação; Denunciante.";
                        dt.Rows.Add(row);
                        break;
                    case 4:
                        row = dt.NewRow();
                        row["ID"] = iCount;
                        row["Categoria"] = "Interrogado";
                        row["Descrição"] = "O Indivíduo interrogado/entrevistado.";
                        dt.Rows.Add(row);
                        break;
                    case 5:
                        row = dt.NewRow();
                        row["ID"] = iCount;
                        row["Categoria"] = "Suspeito";
                        row["Descrição"] = "Indivíduo que é suspeito do delito/crime.";
                        dt.Rows.Add(row);
                        break;
                    case 6:
                        row = dt.NewRow();
                        row["ID"] = iCount;
                        row["Categoria"] = "Testemunha";
                        row["Descrição"] = "Indivíduo que testemunha na investigação; Assistiu o fato.";
                        dt.Rows.Add(row);
                        break;
                    case 7:
                        row = dt.NewRow();
                        row["ID"] = iCount;
                        row["Categoria"] = "Vítima";
                        row["Descrição"] = "Sujeito passivo de ilícito penal; Parte lesada.";
                        dt.Rows.Add(row);
                        break;
                }


                dgvCategogiaEnvolvido.ItemsSource = dt.DefaultView;
                dgvCategogiaEnvolvido.Columns[0].Header = "ID";
                dgvCategogiaEnvolvido.Columns[1].Header = "Categoria";
                dgvCategogiaEnvolvido.Columns[2].Header = "Descrição";
               
            }

        }

        private async void btnOk_Click(object sender, RoutedEventArgs e)
        {
            if (dgvCategogiaEnvolvido.SelectedIndex != -1)
            {
                object item = dgvCategogiaEnvolvido.SelectedItem;
                string cat = (dgvCategogiaEnvolvido.SelectedCells[1].Column.GetCellContent(item) as TextBlock).Text;

                BuscarPessoaFisica buscarPessoaFWnd = new BuscarPessoaFisica(cat);
                buscarPessoaFWnd.ShowDialog();
                this.Close();
            }
            else
            {
                await this.ShowMessageAsync("Aviso", "Selecione a categoria do envolvido para iniciar o cadastro!");
            }

        }

        private void btnCancelar_Click(object sender, System.Windows.RoutedEventArgs e)
        {
        	this.Close();
        }
    }
}
