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
using System.Data;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
using sistemaCorporativo.UTIL.databaseAdress;

namespace sistemaCorporativo
{
    /// <summary>
    /// Interaction logic for BuscarPessoaFisica.xaml
    /// </summary>
    public partial class BuscarPessoaFisica : MetroWindow
    {
        private String categoria;
        string idCaso;
        public BuscarPessoaFisica(String info, string idCasoInfo)
        {
            this.InitializeComponent();
            categoria = info;
            idCaso = idCasoInfo;
        }

        #region ESCOPO

        #region SQL SCRIPTS
        //Pesquisae por nome
        private string SQL_SEARCHBYNAME = "SELECT id_Envolvido, Primeiro_Nome, Ultimo_Nome, Sexo, Dt_Nascimento, CPF, Nacionalidade from ENVOLVIDO_F where status = 1 and primeiro_nome ||' '|| ultimo_nome like '%";
        private string SQL_SEARCHBYNICKNAME = "SELECT id_Envolvido, Primeiro_Nome, Ultimo_Nome, Sexo, Dt_Nascimento, CPF, Nacionalidade from ENVOLVIDO_F where status = 1 and Conhecido_Como like '%";
        //Pesquisar por data
        private string SQL_SEARCHBYDATEFROMBETWEEN = "and Dt_Nascimento between :DataDe and :DataA";
        private string SQL_SEARCHBYDATEFROMLIKE = "and Dt_Nascimento like '";
        //Checar se o envolvido ja esta na investigação
        private string SQL_ENVEXISTS = "SELECT id_Caso, id_EnvolvidoF from ENV_CASO where id_Caso = :idCaso and id_EnvolvidoF = :idEnvolvido";
        #endregion

        //Endereço banco de dados
        databaseAddress db = new databaseAddress();

        #endregion

        #region CRUD (R)

        private void txtNomeEnvolvido_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            int underline = txtDataDe.Text.IndexOf("_");

            /*Não tem Underline
            if (underline == -1)
            {
                dgvEnvolvido.ItemsSource = ConsultarDataExataNome(txtNomeEnvolvido.Text, txtDataDe.Text).Tables[0].DefaultView;
                if (dgvEnvolvido.Items.Count == 0)
                {
                    dgvEnvolvido.ItemsSource = ConsultarDataExataNome(txtNomeEnvolvido.Text, txtDataDe.Text).Tables[0].DefaultView;
                }
            }*/


            dgvEnvolvido.ItemsSource = ConsultarComNome(txtNomeEnvolvido.Text).Tables[0].DefaultView;
            if (dgvEnvolvido.Items.Count == 0)
            {
                dgvEnvolvido.ItemsSource = ConsultarComApelido(txtNomeEnvolvido.Text).Tables[0].DefaultView;
            }

            dgvEnvolvido.Columns[0].Header = "ID";
            dgvEnvolvido.Columns[1].Header = "Primeiro Nome";
            dgvEnvolvido.Columns[2].Header = "Ultimo Nome";
            dgvEnvolvido.Columns[3].Header = "Sexo";
            dgvEnvolvido.Columns[4].Header = "Data Nascimento";
            dgvEnvolvido.Columns[5].Header = "CPF";
            dgvEnvolvido.Columns[6].Header = "Nacionalidade";
        }

        private void txtDataDe_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {

        }

        private void txtDataA_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {

        }

        private DataSet ConsultarComNome(string filtro)
        {
            DataSet ds = new DataSet();

            OracleConnection Oracon = new OracleConnection(db.oradb);
            Oracon.Open();

            OracleCommand cmdSearch = new OracleCommand(SQL_SEARCHBYNAME + filtro + "%'", Oracon);
            cmdSearch.ExecuteNonQuery();

            OracleDataAdapter adapter = new OracleDataAdapter(cmdSearch);

            adapter.Fill(ds);
            Oracon.Close();
            return ds;
        }
        private DataSet ConsultarComApelido(string filtro)
        {
            DataSet ds = new DataSet();

            OracleConnection Oracon = new OracleConnection(db.oradb);
            Oracon.Open();

            OracleCommand cmdSearch = new OracleCommand(SQL_SEARCHBYNICKNAME + filtro + "%'", Oracon);
            cmdSearch.ExecuteNonQuery();

            OracleDataAdapter adapter = new OracleDataAdapter(cmdSearch);

            adapter.Fill(ds);
            Oracon.Close();
            return ds;
        }

        private DataSet ConsultarDataExataNome(string filtro, string data)
        {
            DataSet ds = new DataSet();

            OracleConnection Oracon = new OracleConnection(db.oradb);
            Oracon.Open();

            OracleCommand cmdSearch = new OracleCommand(SQL_SEARCHBYNAME + filtro + "%' " + SQL_SEARCHBYDATEFROMLIKE + data + "' ", Oracon);

            cmdSearch.ExecuteNonQuery();

            OracleDataAdapter adapter = new OracleDataAdapter(cmdSearch);

            adapter.Fill(ds);
            Oracon.Close();
            return ds;
        }

        private DataSet ConsultarDataExataApelido(string filtro, string data)
        {
            DataSet ds = new DataSet();

            OracleConnection Oracon = new OracleConnection(db.oradb);
            Oracon.Open();

            OracleCommand cmdSearch = new OracleCommand(SQL_SEARCHBYNICKNAME + filtro + "%' " + SQL_SEARCHBYDATEFROMLIKE + data + "' ", Oracon);
            cmdSearch.ExecuteNonQuery();

            OracleDataAdapter adapter = new OracleDataAdapter(cmdSearch);

            adapter.Fill(ds);
            Oracon.Close();
            return ds;
        }

        #endregion

        private void btnAddNovo_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            CadPessoaFisica cadPessoaWnd = new CadPessoaFisica(categoria, null, idCaso);
            cadPessoaWnd.Edicao = false;
            cadPessoaWnd.ShowDialog();
            this.Close();
        }

        private async void btnSelecionar_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                if (dgvEnvolvido.SelectedIndex != -1)
                {
                    OracleConnection Oracon = new OracleConnection(db.oradb);
                    Oracon.Open();
                    object item = dgvEnvolvido.SelectedItem;
                    string idEnvolvidoF = (dgvEnvolvido.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text;

                    OracleCommand cmdCheckEnv = new OracleCommand(SQL_ENVEXISTS, Oracon);
                    cmdCheckEnv.Parameters.Add("idCaso", idCaso);
                    cmdCheckEnv.Parameters.Add("idEnvolvido", idEnvolvidoF);
                    OracleDataReader reader = cmdCheckEnv.ExecuteReader();
                    
                    reader.Read();
                    if (reader.HasRows)
                    {
                        Oracon.Close();
                        await this.ShowMessageAsync("Aviso", "Essa pessoa ja foi adicionada a investigação!");
                        return;
                    }
                    Oracon.Close();
                    CadPessoaFisica CadEnvolvidoFWnd = new CadPessoaFisica(categoria, idEnvolvidoF, idCaso);
                    CadEnvolvidoFWnd.Edicao = true;
                    CadEnvolvidoFWnd.ShowDialog();
                    this.Close();

                }
                else
                {
                    await this.ShowMessageAsync("Aviso", "Selecione uma pessoa física para ligar a investigação!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dgvEnvolvido_AutoGeneratingColumn(object sender, System.Windows.Controls.DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyType == typeof(System.DateTime))
                (e.Column as DataGridTextColumn).Binding.StringFormat = "dd/MM/yyyy";
        }

        private void btnCancelar_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.Close();
        }

        private void wndBuscarPessoaFisica_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            txtNomeEnvolvido.Focus();
        }

    }
}