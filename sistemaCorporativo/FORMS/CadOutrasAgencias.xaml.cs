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
using sistemaCorporativo.UTIL.databaseAdress;
using sistemaCorporativo.TO.Agencia;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
using System.Data;
using System.Windows.Forms;

namespace sistemaCorporativo
{
    /// <summary>
    /// Interaction logic for CadAgenciaAux.xaml
    /// </summary>
    public partial class CadOutrasAgencias : MetroWindow
    {
        public CadOutrasAgencias()
        {
            this.InitializeComponent();
        }

        private string idAgencia;

        #region SQL STRINGS

        private string SQL_SELECT_ALL = "select * from AGENCIA where status = 1";
        private string SQL_INSERT = "insert into AGENCIA (id_Agencia, nome_Agencia, status) values (seq_Agencia.NEXTVAL, :nomesigla, 1)";
        private string SQL_UPDATE = "";
        private string SQL_DELETE = "";

        #endregion

        //Endereço banco de dados
        databaseAddress db = new databaseAddress();

        private void wndCadAgenciaAux_Loaded(object sender, RoutedEventArgs e)
        {
            OracleConnection Oracon = new OracleConnection(db.oradb);

            try
            {
                Oracon.Open();

                OracleCommand cmdSelect = new OracleCommand(SQL_SELECT_ALL, Oracon);
                cmdSelect.ExecuteNonQuery();

                OracleDataAdapter adapter = new OracleDataAdapter(cmdSelect);

                DataTable dt = new DataTable("Agencia Auxiliadora");
                adapter.Fill(dt);

                dgvAgenciaAuxiliadora.ItemsSource = dt.DefaultView;
                dgvAgenciaAuxiliadora.Columns[0].Header = "ID";
                dgvAgenciaAuxiliadora.Columns[1].Header = "Nome da Agencia";
                dgvAgenciaAuxiliadora.Columns[2].Header = "Status";

                dgvAgenciaAuxiliadora.Columns[2].Visibility = Visibility.Hidden;

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
            if (txtNomeAgencia.Text == "" || txtSigla.Text == "")
            {
                await this.ShowMessageAsync("Aviso", "Todos os campos com '*' são obrigatórios");
            }
            else
            {
                #region TO

                AgenciaAux objAgencia = new AgenciaAux();
                objAgencia.setNomeAgencia(txtNomeAgencia.Text);
				string sigla = txtSigla.Text.ToUpper();
                objAgencia.setSigla(sigla);

                #endregion

                OracleConnection Oracon = new OracleConnection(db.oradb);

                if (idAgencia == null)
                {
                    #region CADASTRO

                    try
                    {
                        Oracon.Open();

                        //Juntar Nome e Sigla
                        string nomeSigla = objAgencia.getNomeAgencia() + " " + "(" + objAgencia.getSigla() +")";

                        OracleCommand cmdInsert = new OracleCommand(SQL_INSERT, Oracon);
                        cmdInsert.Parameters.Add("nomesigla", nomeSigla);
                        cmdInsert.ExecuteNonQuery();

                        Oracon.Close();

                        await this.ShowMessageAsync("Aviso", "Agencia cadastrada com sucesso!");
                        btnLimpar_Click(null, null);
                        wndCadAgenciaAux_Loaded(null, null);

                    }
                    catch (OracleException ex)
                    {

                        System.Windows.MessageBox.Show(ex.Message);
                    }

                    #endregion
                }
                else
                {
                    #region ATUALIZAR

                    try
                    {
                        Oracon.Open();

                        //Juntar Nome e Sigla
                        string nomeSigla = txtNomeAgencia.Text + " " + "(" + txtSigla.Text + ")";

                        SQL_UPDATE = "update AGENCIA set nome_Agencia = :nomesigla where id_Agencia =" + idAgencia;
                        OracleCommand cmdUpdate = new OracleCommand(SQL_UPDATE, Oracon);
                        cmdUpdate.Parameters.Add("nomesigla", nomeSigla);
                        cmdUpdate.ExecuteNonQuery();

                        Oracon.Close();

                        await this.ShowMessageAsync("Aviso", "Agencia atualizada com sucesso!");
                        btnLimpar_Click(null, null);
                        wndCadAgenciaAux_Loaded(null, null);
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
            if (dgvAgenciaAuxiliadora.SelectedIndex != -1)
            {
                var mySettings = new MetroDialogSettings()
              {
                  AffirmativeButtonText = "Sim",
                  NegativeButtonText = "Cancelar",
                  ColorScheme = MetroDialogOptions.ColorScheme
              };

                MessageDialogResult result = await this.ShowMessageAsync("Atenção", "Você tem certeza que deseja remover a agencia?", MessageDialogStyle.AffirmativeAndNegative, mySettings);

                if (result == MessageDialogResult.Affirmative)
                {
                    OracleConnection Oracon = new OracleConnection(db.oradb);
                    try
                    {

                        //Abrir conexão com o banco de dados
                        Oracon.Open();


                        //obtendo valor da célula na coluna ID
                        object item = dgvAgenciaAuxiliadora.SelectedItem;
                        idAgencia = (dgvAgenciaAuxiliadora.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text;

                        //executando comando usando o ID como base para "apagar" um item
                        SQL_DELETE = "update AGENCIA set status=0 where id_Agencia =" + idAgencia;
                        OracleCommand deleteCommand = new OracleCommand(SQL_DELETE, Oracon);
                        deleteCommand.ExecuteNonQuery();

                        //Fechar conexão com o banco de dados
                        Oracon.Close();

                        System.Windows.Forms.MessageBox.Show("Agencia deletada com sucesso!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                        this.wndCadAgenciaAux_Loaded(null, null);
                        btnLimpar_Click(null, null);

                    }
                    catch (OracleException orclEx)
                    {

                        System.Windows.MessageBox.Show(orclEx.Message);
                    }
                }
            }
            else
            {
                await this.ShowMessageAsync("Aviso", "Selecione uma agencia para excluir!");
            }
        }

        #endregion

        private void btnLimpar_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            idAgencia = null;
            txtNomeAgencia.Text = "";
            txtSigla.Text = "";
        }

        private void btnCancelar_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.Close();
        }

        private void dgvAgenciaAuxiliadora_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
			 try
                {
                    //Coletando valores para atualização

                    object itemid = dgvAgenciaAuxiliadora.SelectedItem;
                    idAgencia = (dgvAgenciaAuxiliadora.SelectedCells[0].Column.GetCellContent(itemid) as TextBlock).Text;

                    object item1 = dgvAgenciaAuxiliadora.SelectedItem;
                    string nomesigla = (dgvAgenciaAuxiliadora.SelectedCells[1].Column.GetCellContent(item1) as TextBlock).Text;

                    int intnome = nomesigla.IndexOf("(");
                    string nome = nomesigla.Substring(0, intnome - 1);
					nome.Replace(" ", "");
                    string sigla = nomesigla.Substring(intnome + 1, (nomesigla.Length - intnome -2));

                    txtNomeAgencia.Text = nome;
                    txtSigla.Text = sigla;

                }
                catch (Exception ex)
                {

                    System.Windows.MessageBox.Show(ex.Message);
                }
        }

    }

}