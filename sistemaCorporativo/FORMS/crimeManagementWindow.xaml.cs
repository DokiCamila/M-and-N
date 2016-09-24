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
using sistemaCorporativo.UTIL.databaseAdress;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
using System.Data;
using System.Globalization;

namespace sistemaCorporativo
{
    /// <summary>
    /// Interaction logic for crimeManagementWindow.xaml
    /// </summary>
    public partial class CrimeManagementWindow : MetroWindow
    {
        public CrimeManagementWindow()
        {
            this.InitializeComponent();
        }

        #region SQL SCRIPT

        #region INVESTIGAÇÃO
        //Selecionar Investigações com Detalhes para exibir
        string SELECT_ALL_CASE = "select c.ID_CASO, c.NUMERO_CASO, c.TITULO_CASO, c.TIPO_CASO, c.DEP_LOCALIZACAO, c.CATEGORIA_CASO, c.AGENTE1, c.AGENTE2, c.AGENTE3, c.AGENTE4, c.DATA_ABERTURA, c.DATA_FECHAMENTO, c.STATUS_CASO, d.municipio, d.estado, d.ref_outraagencia from CASO c inner join DETALHES_CASO d on c.id_DetalheCaso = d.id_DetalheCaso where c.status = 1";
        //String deletar caso
        string SQL_DELETE_CASE = "update caso set status = 0 where id_Caso =";
        //String deletar detalhe do caso
        string SQL_DELETE_DETAILCASE = "update detalhes_caso set status = 0 where id_detalhecaso =";
        //String pegar id do detalhe pelo id do caso
        string SQL_TAKE_IDDETAIL = "select d.id_detalhecaso from caso c inner join detalhes_caso d on d.id_detalhecaso = c.id_detalhecaso and c.id_Caso = ";
        //String pegar caso e detalhe 
        string SELECT_CASEANDDEATIL = "select c.ID_CASO, c.NUMERO_CASO, c.TITULO_CASO, c.TIPO_CASO, c.DEP_LOCALIZACAO, c.CATEGORIA_CASO, c.AGENTE1, c.AGENTE2, c.AGENTE3, c.AGENTE4, c.DATA_ABERTURA, c.DATA_FECHAMENTO, c.STATUS_CASO, d.ID_DETALHECASO, d.TIPO_LAVAGEM, d.FORCA_TAREFA, d.MUNICIPIO, d.ESTADO, d.REF_OUTRAAGENCIA, d.GRANDE_JURI, d.INT_JURI, d.RELAT_JURI  from caso c inner join detalhes_caso d on c.id_Detalhecaso = d.id_Detalhecaso and c.id_Caso =";
        //String pegar o nome e o agente pelo id para editar o caso
        string SELECT_THISAGENTE = "select id_Agente, nome from AGENTE where status = 1 and administrador = 0 and id_Agente =";
        #endregion

        #endregion

        databaseAddress db = new databaseAddress();

        public void wndCrimeManagement_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            OracleConnection Oracon = new OracleConnection(db.oradb);

            Oracon.Open();

            #region CARREGAR INVESTIGAÇÕES

            OracleCommand selectall = new OracleCommand(SELECT_ALL_CASE, Oracon);
            selectall.ExecuteNonQuery();

            OracleDataAdapter adapter = new OracleDataAdapter(selectall);
            DataTable dt = new DataTable("Investigações");

            adapter.Fill(dt);
            dgvCasos.ItemsSource = dt.DefaultView;
            dgvCasos.Columns[0].Header = "ID";
            dgvCasos.Columns[1].Header = "Número doCaso";
            dgvCasos.Columns[2].Header = "Título";
            dgvCasos.Columns[3].Header = "Tipo";
            dgvCasos.Columns[4].Header = "Dept/Localização";
            dgvCasos.Columns[5].Header = "Categoria";
            dgvCasos.Columns[6].Header = "Agente Principal";
            dgvCasos.Columns[7].Header = "Agente 2";
            dgvCasos.Columns[8].Header = "Agente 3";
            dgvCasos.Columns[9].Header = "Agente 4";
            dgvCasos.Columns[10].Header = "Data de Abertura";
            dgvCasos.Columns[11].Header = "Data de Fechamento";
            dgvCasos.Columns[12].Header = "Status";
            dgvCasos.Columns[13].Header = "Município";
            dgvCasos.Columns[14].Header = "Estado";
            dgvCasos.Columns[15].Header = "Ref. Outra Agência";


            adapter.Update(dt);

            #endregion

            Oracon.Close();

        }

        #region MENU LATERAL
        private void Menu_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            expDpci.IsExpanded = true;
            expCitacoes.IsExpanded = false;
        }

        private void btnInvestigacoes_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (!grdInvestigações.IsVisible)
            {
                grdInvestigações.IsEnabled = true;
                Storyboard animation = this.FindResource("grdInvestigaçõesAnim") as Storyboard;
                animation.Begin();
                grdInvestigações.Visibility = Visibility.Visible;
            }
        }

        #endregion

        #region GRID INVESTIGAÇÕES

        private void btnAddCaso_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            CadCaso addCaso = new CadCaso(this);
            addCaso.ShowDialog();
        }

        #region CRUD(D)
        private async void btnDelCaso_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (dgvCasos.SelectedIndex != -1)
            {
                var mySettings = new MetroDialogSettings()
              {
                  AffirmativeButtonText = "Sim",
                  NegativeButtonText = "Cancelar",
                  ColorScheme = MetroDialogOptions.ColorScheme
              };

                MessageDialogResult result = await this.ShowMessageAsync("Atenção", "Você tem certeza que deseja remover essa investigação?", MessageDialogStyle.AffirmativeAndNegative, mySettings);

                if (result == MessageDialogResult.Affirmative)
                {

                    try
                    {
                        OracleConnection Oracon = new OracleConnection(db.oradb);
                        //Abrir conexão com o banco de dados
                        Oracon.Open();

                        //obtendo valor da célula na coluna ID
                        object item = dgvCasos.SelectedItem;
                        string id = (dgvCasos.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text;
                        id.ToString();

                        //executando comando usando o ID como base para "apagar" um item
                        OracleCommand deleteCommand = new OracleCommand(SQL_DELETE_CASE + id, Oracon);
                        deleteCommand.ExecuteNonQuery();

                        //Pegar id do detalhe pelo id do caso
                        OracleCommand takeiddetail = new OracleCommand(SQL_TAKE_IDDETAIL + id, Oracon);
                        OracleDataReader dr = takeiddetail.ExecuteReader();
                        dr.Read();

                        string idDetail = dr[0].ToString();

                        //Deletando detalhe
                        OracleCommand deleteCommandDetail = new OracleCommand(SQL_DELETE_DETAILCASE + idDetail, Oracon);
                        deleteCommandDetail.ExecuteNonQuery();


                        //Fechar conexão com o banco de dados
                        Oracon.Close();

                        System.Windows.Forms.MessageBox.Show("Investigação deletada com sucesso!", "Aviso", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation, System.Windows.Forms.MessageBoxDefaultButton.Button1);
                        this.wndCrimeManagement_Loaded(null, null);

                    }
                    catch (OracleException orclEx)
                    {

                        System.Windows.MessageBox.Show(orclEx.Message);
                    }
                }


            }
            else
            {
                await this.ShowMessageAsync("Aviso", "Selecione uma investigação para excluir!");
            }
        }
        #endregion

        private void btnClose_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            grdInvestigações.IsEnabled = false;
            grdInvestigações.Visibility = Visibility.Hidden;
        }

        private void dgvCasos_AutoGeneratingColumn(object sender, System.Windows.Controls.DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyType == typeof(System.DateTime))
                (e.Column as DataGridTextColumn).Binding.StringFormat = "dd/MM/yyyy";
        }

        private async void dgvCasos_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (dgvCasos.SelectedIndex != -1)
            {
                OracleConnection Oracon = new OracleConnection(db.oradb);
                try
                {
                    Oracon.Open();

                    #region Enviando dados para a tela addcaso para editar
                    //Coletando valores para atualização

                    object itemid = dgvCasos.SelectedItem;
                    string idCaso = (dgvCasos.SelectedCells[0].Column.GetCellContent(itemid) as TextBlock).Text;
                    idCaso.ToString();



                    //Pegar id do detalhe pelo id do caso
                    OracleCommand takeiddetail = new OracleCommand(SQL_TAKE_IDDETAIL + idCaso, Oracon);
                    OracleDataReader dr = takeiddetail.ExecuteReader();
                    dr.Read();

                    string idDetail = dr[0].ToString();

                    //Fazer select do caso e detalhe selecionado para editar

                    OracleCommand selectallCommand = new OracleCommand(SELECT_CASEANDDEATIL + idCaso, Oracon);
                    OracleDataReader drcd = selectallCommand.ExecuteReader();

                    drcd.Read();

                    CadCaso editCaso = new CadCaso(this);
                    editCaso.modoEdicao = true;
                    editCaso.idCaso = idCaso;
                    editCaso.idDetalhe = idDetail;
                    editCaso.txtNumCaso.Text = drcd[1].ToString();
                    string numeroCaso = drcd[1].ToString();
                    editCaso.txtTituloCaso.Text = drcd[2].ToString();
                    string titulocaso = drcd[2].ToString();
                    editCaso.cmbTipoCaso.Text = drcd[3].ToString();
                    editCaso.cmbDeptLocal.Text = drcd[4].ToString();
                    editCaso.cmbCatCaso.Text = drcd[5].ToString();

                    //Agentes
                    string idagente1 = drcd[6].ToString();
                    OracleCommand selectagente1 = new OracleCommand(SELECT_THISAGENTE + idagente1, Oracon);
                    OracleDataReader readagente1 = selectagente1.ExecuteReader();
                    readagente1.Read();
                    editCaso.agente1 = (readagente1[0] + " " + readagente1[1]).ToString();

                    string idagente2 = drcd[7].ToString();

                    OracleCommand selectagente2 = new OracleCommand(SELECT_THISAGENTE + idagente2, Oracon);
                    OracleDataReader readagente2 = selectagente2.ExecuteReader();
                    readagente2.Read();
                    editCaso.agente2 = (readagente2[0] + " " + readagente2[1]).ToString();

                    string idagente3 = drcd[8].ToString();
                    if (idagente3 != "")
                    {
                        OracleCommand selectagente3 = new OracleCommand(SELECT_THISAGENTE + idagente3, Oracon);
                        OracleDataReader readagente3 = selectagente3.ExecuteReader();
                        readagente3.Read();
                        editCaso.agente3 = (readagente3[0] + " " + readagente3[1]).ToString();
                    }

                    string idagente4 = drcd[9].ToString();
                    if (idagente4 != "")
                    {
                        OracleCommand selectagente4 = new OracleCommand(SELECT_THISAGENTE + idagente4, Oracon);
                        OracleDataReader readagente4 = selectagente4.ExecuteReader();
                        readagente4.Read();
                        editCaso.agente4 = (readagente4[0] + " " + readagente4[1]).ToString();
                    }

                    //Data
                    string dataAbertura = drcd[10].ToString();
                    dataAbertura = formataData(dataAbertura);
                    editCaso.txtDataAbertura.Text = dataAbertura;

                    string dataFechamento = drcd[11].ToString();
                    if (dataFechamento != "")
                    {
                        dataFechamento = formataData(dataFechamento);
                        editCaso.txtDataFechamento.Text = dataFechamento;
                    }

                    editCaso.cmbStatusCaso.Text = drcd[12].ToString();
                    string tipolavagem = drcd[14].ToString();
                    if (tipolavagem != "")
                    {
                        editCaso.cmbTipoLavagem.Text = tipolavagem;
                        editCaso.cmbTipoLavagem.IsEnabled = true;

                    }

                    editCaso.txtForcaTarefa.Text = drcd[15].ToString();
                    editCaso.cmbMunicipio.Text = drcd[16].ToString();
                    editCaso.cmbEstado.Text = drcd[17].ToString();

                    string refoutraagencia = drcd[18].ToString();
                    if (refoutraagencia == "não")
                    {
                        editCaso.ckbRefOutraAgencia.IsChecked = false;
                        editCaso.cmbMunicipio.IsEnabled = true;
                        editCaso.cmbEstado.IsEnabled = false;
                    }
                    else
                    {
                        editCaso.ckbRefOutraAgencia.IsChecked = true;
                        editCaso.cmbMunicipio.IsEnabled = false;
                        editCaso.cmbEstado.IsEnabled = true;
                    }

                    string grandjuri = drcd[19].ToString();
                    if (grandjuri == "não")
                    {
                        editCaso.ckbGrandeJuri.IsChecked = false;
                    }
                    else
                    {
                        editCaso.ckbGrandeJuri.IsChecked = true;
                    }

                    string intjuri = drcd[20].ToString();
                    if (intjuri == "não")
                    {
                        editCaso.ckbInterJuri.IsChecked = false;
                    }
                    else
                    {
                        editCaso.ckbInterJuri.IsChecked = true;
                    }

                    string relatjuri = drcd[21].ToString();
                    if (relatjuri == "não")
                    {
                        editCaso.ckbRelJuri.IsChecked = false;
                    }
                    else
                    {
                        editCaso.ckbRelJuri.IsChecked = true;
                    }

                    editCaso.Title = "Editar Caso Nº " + numeroCaso + " | " + titulocaso;

                    editCaso.ShowDialog();

                    #endregion

                    Oracon.Close();
                }
                catch (OracleException ex)
                {

                    System.Windows.MessageBox.Show(ex.Message);
                }

            }
            else
            {
                await this.ShowMessageAsync("Aviso", "Selecione uma viatura para editar!");
            }
        }

        private static string formataData(string data)
        {

            data = data.Substring(0, data.IndexOf(" "));

            data = Convert.ToDateTime(data).ToString("dd/MM/yyyy");
            return data;
        }

        #endregion
    }
}