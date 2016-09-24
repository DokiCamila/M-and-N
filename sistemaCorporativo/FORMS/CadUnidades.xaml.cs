using sistemaCorporativo.UTIL.databaseAdress;
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
using sistemaCorporativo.TO.Unidade;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
using System.Data;
using System.Windows.Forms;

namespace sistemaCorporativo
{
	/// <summary>
	/// Interaction logic for CadUnidades.xaml
	/// </summary>
	public partial class CadUnidades : MetroWindow
	{
		public CadUnidades()
		{
			this.InitializeComponent();
		}

        private string idUnidade;

        #region SQL STRINGS

        private string SQL_SELECT_ALL = "select * from UNIDADES where status = 1";
        private string SQL_INSERT = "insert into UNIDADES (id_Unidades, unidade_Local, status) values (seq_Unidade.NEXTVAL, :unidadelocal, 1)";
        private string SQL_UPDATE = "";
        private string SQL_DELETE = "";

        #endregion

        //Endereço banco de dados
        databaseAddress db = new databaseAddress();

        private void wndCadUnidades_Loaded(object sender, RoutedEventArgs e)
        {
            OracleConnection Oracon = new OracleConnection(db.oradb);

            try
            {
                Oracon.Open();

                OracleCommand cmdSelect = new OracleCommand(SQL_SELECT_ALL, Oracon);
                cmdSelect.ExecuteNonQuery();

                OracleDataAdapter adapter = new OracleDataAdapter(cmdSelect);

                DataTable dt = new DataTable("Unidades");
                adapter.Fill(dt);

                dgvUnidades.ItemsSource = dt.DefaultView;
                dgvUnidades.Columns[0].Header = "ID";
                dgvUnidades.Columns[1].Header = "Unidade / Local";
                dgvUnidades.Columns[2].Header = "Status";

                dgvUnidades.Columns[2].Visibility = Visibility.Hidden;

                adapter.Update(dt);

                Oracon.Close();

            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }

        }

        #region CRUD (CUD)

        private async void btnCadastrar_Click(object sender, System.Windows.RoutedEventArgs e)
		{
            //Checar Strings
            if (txtNomeUnidade.Text == "" || cmbMunicipio.Text == "")
            {
                await this.ShowMessageAsync("Aviso", "Todos os campos com '*' são obrigatórios");
            }
            else
            {

                #region TO 


                Unidade objUnidade = new Unidade();
                objUnidade.setUnidade(txtNomeUnidade.Text);
                objUnidade.setMunicipio(cmbMunicipio.Text);

                #endregion

                OracleConnection Oracon = new OracleConnection(db.oradb);

                if (idUnidade == null)
                {
                    #region CADASTRO

                    try
                    {
                        Oracon.Open();

                        //Juntar Unidade e Municipio
                        string unidademun = objUnidade.getUnidade() + " / " + objUnidade.getMunicipio();

                        OracleCommand cmdInsert = new OracleCommand(SQL_INSERT, Oracon);
                        cmdInsert.Parameters.Add("unidadelocal", unidademun);
                        cmdInsert.ExecuteNonQuery();

                        Oracon.Close();

                        await this.ShowMessageAsync("Aviso", "Unidade cadastrada com sucesso!");
                        btnLimpar_Click(null, null);
                        wndCadUnidades_Loaded(null, null);
                    }
                    catch (OracleException ex)
                    {

                        System.Windows.MessageBox.Show(ex.Message);
                    }
                   
                    #endregion
                }
                else
                {
                    #region

                    try
                    {
                        Oracon.Open();

                        //Juntar Unidade e Municipio
                        string unidademun = objUnidade.getUnidade() + " / " + objUnidade.getMunicipio();

                        SQL_UPDATE = "update UNIDADES set unidade_local = :unidadelocal where id_Unidades =" + idUnidade;
                        OracleCommand cmdUpdate = new OracleCommand(SQL_UPDATE, Oracon);
                        cmdUpdate.Parameters.Add("unidadelocal", unidademun);
                        cmdUpdate.ExecuteNonQuery();

                        Oracon.Close();

                        await this.ShowMessageAsync("Aviso", "Unidade atualizada com sucesso!");
                        btnLimpar_Click(null, null);
                        wndCadUnidades_Loaded(null, null);
                    }
                    catch (OracleException ex)
                    {
                        
                         System.Windows.MessageBox.Show(ex.Message);
                    }

                    #endregion
                }
            }
		}

		private async void btnExcluir_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			 if (dgvUnidades.SelectedIndex != -1)
            {
                var mySettings = new MetroDialogSettings()
              {
                  AffirmativeButtonText = "Sim",
                  NegativeButtonText = "Cancelar",
                  ColorScheme = MetroDialogOptions.ColorScheme
              };

                MessageDialogResult result = await this.ShowMessageAsync("Atenção", "Você tem certeza que deseja remover essa unidade?", MessageDialogStyle.AffirmativeAndNegative, mySettings);

                if (result == MessageDialogResult.Affirmative)
                {
                    OracleConnection Oracon = new OracleConnection(db.oradb);
                    try
                    {

                        //Abrir conexão com o banco de dados
                        Oracon.Open();


                        //obtendo valor da célula na coluna ID
                        object item = dgvUnidades.SelectedItem;
                        idUnidade = (dgvUnidades.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text;

                        //executando comando usando o ID como base para "apagar" um item
                        SQL_DELETE = "update UNIDADES set status=0 where id_Unidades =" + idUnidade;
                        OracleCommand deleteCommand = new OracleCommand(SQL_DELETE, Oracon);
                        deleteCommand.ExecuteNonQuery();

                        //Fechar conexão com o banco de dados
                        Oracon.Close();

                        System.Windows.Forms.MessageBox.Show("Unidade deletada com sucesso!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                        this.wndCadUnidades_Loaded(null, null);

                    }
                    catch (OracleException orclEx)
                    {

                        System.Windows.MessageBox.Show(orclEx.Message);
                    }
                }
            }
            else
            {
                await this.ShowMessageAsync("Aviso", "Selecione uma unidade para excluir!");
            }
		}

        #endregion

        private void btnLimpar_Click(object sender, System.Windows.RoutedEventArgs e)
		{
            idUnidade = null;
            txtNomeUnidade.Text = "";
            cmbMunicipio.Text = "";
		}

		private void btnCancelar_Click(object sender, System.Windows.RoutedEventArgs e)
		{
            this.Close();
		}

        private void dgvUnidades_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                //Coletando valores para atualização

                object itemid = dgvUnidades.SelectedItem;
                idUnidade = (dgvUnidades.SelectedCells[0].Column.GetCellContent(itemid) as TextBlock).Text;

                object item1 = dgvUnidades.SelectedItem;
                string unidademun = (dgvUnidades.SelectedCells[1].Column.GetCellContent(item1) as TextBlock).Text;

                int intunidade = unidademun.IndexOf("/");
                string unidade = unidademun.Substring(0, intunidade - 1);
                unidade = unidade.Replace(" ", "");
                string municipio = unidademun.Substring(intunidade + 2, (unidademun.Length - intunidade - 2));

                txtNomeUnidade.Text = unidade;
                cmbMunicipio.Text = municipio;

            }
            catch (Exception ex)
            {

                System.Windows.MessageBox.Show(ex.Message);
            }
        }
	}
}