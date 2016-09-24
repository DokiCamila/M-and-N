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
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
using sistemaCorporativo.FORMS;
using sistemaCorporativo.UTIL.databaseAdress;
using sistemaCorporativo.TO.Caso;
using System.Globalization;
using System.Data;
using System.Text.RegularExpressions;
using System.Windows.Controls.Primitives;
using sistemaCorporativo.TO.AgenciaAux;

namespace sistemaCorporativo
{
    /// <summary>
    /// Interaction logic for CadCaso.xaml
    /// </summary>
    public partial class CadCaso : MetroWindow
    {
        //Instanciar tela anterior
        CrimeManagementWindow crimeWindow;
        public CadCaso(CrimeManagementWindow info)
        {
            InitializeComponent();
            crimeWindow = info;
        }

        //Criar Variável para edições(atualizações);
        public Boolean modoEdicao = false;
        public string agente1;
        public string agente2;
        public string agente3;
        public string agente4;

        public string idCaso;
        public string idDetalhe;


        #region SQL STRINGS

        //String selecionar todos os agentes
        private string SELECT_ALL_AGENTE = "select id_Agente, nome from AGENTE where status = 1 and administrador = 0";
        //String selecionar todas as Agencias Auxiliadora
        private string SELECT_ALL_OUTRASAGENCIAS = "select nome_Agencia from AGENCIA where status = 1";
        //String selecionar todas as Unidades
        private string SELECT_ALL_UNIDADE = "select unidade_Local from UNIDADES where status = 1";
        //Script selecionar todos as Agencias Aux(Agencia, Nome Agente)
        private string SELECT_ALL_AGENCIASAUX= "select nome_Agencia, nome_Agente from AGENCIAAUX_DET where status =1 and id_DetalheCaso =";
        //String para inserir um  novo caso
        private string SQL_INSERT_CASO = "insert into caso(ID_CASO, NUMERO_CASO, TITULO_CASO, TIPO_CASO, DEP_LOCALIZACAO, CATEGORIA_CASO, AGENTE1, AGENTE2, AGENTE3, AGENTE4, DATA_ABERTURA, DATA_FECHAMENTO, STATUS_CASO, ID_DETALHECASO, STATUS)"
                                       + "values(seq_Caso.NEXTVAL, :numcaso, :titulocaso, :tipocaso, :deploc, :catcaso, :agente1, :agente2, :agente3, :agente4, :dataabertura, :datafechamento, :statuscaso, :iddetalhecaso, 1)";
        //String para inserir detalhes do caso
        private string SQL_INSERT_DETAIL = "insert into detalhes_caso(id_DETALHECASO, TIPO_LAVAGEM, FORCA_TAREFA, MUNICIPIO, ESTADO, REF_OUTRAAGENCIA, GRANDE_JURI, INT_JURI, RELAT_JURI, STATUS)"
                                         + "values (seq_DetalheCaso.NEXTVAL, :tipolavagem, :forcatarefa, :municipio, :estado, :refoutraagencia, :grandejuri, :intjuri, :relatjuri, 1)";
        //String para inserir Agencias Auxiliadoras
        private string SQL_INSERT_AGENCIASAUX = "insert into AGENCIAAUX_DET(id_AgenciaAuxDet, nome_Agencia, nome_Agente, id_DetalheCaso, status) values (seq_AgenciaAuxDet.NEXTVAL, :nomeAgencia, :nomeAgente, :idDetalheCaso, 1)";

        //String pegar o Id do ultimo detahle do caso
        private string SELECT_LAST_DETAIL = "SELECT id_DetalheCaso FROM DETALHES_CASO WHERE id_DetalheCaso = ( SELECT MAX(id_DetalheCaso) FROM DETALHES_CASO)";
        //String para atualizar caso 
        private string SQL_UPDATE_CASO = "update caso set NUMERO_CASO = :numcaso, TITULO_CASO = :titulocaso, TIPO_CASO = :tipocaso, DEP_LOCALIZACAO = :deploc, CATEGORIA_CASO = :catcaso, AGENTE1 = :agente1, AGENTE2 = :agente2, AGENTE3 = :agente3, AGENTE4 = :agente4, DATA_ABERTURA = :dataabertura, DATA_FECHAMENTO = :datafechamento, STATUS_CASO = :statuscaso where id_Caso =";
        //String para atualizar detalhes
        private string SQL_UPDATE_DETAIL = "update detalhes_caso set TIPO_LAVAGEM = :tipolavagem, FORCA_TAREFA = :forcatafera, MUNICIPIO = :municipio, ESTADO = :estado, REF_OUTRAAGENCIA = :refoutraagencia, GRANDE_JURI = :grandejuri, INT_JURI = :intjuri, RELAT_JURI = :relatjuri where id_Detalhecaso = :idDetalhe";


        #endregion

        //Endereço banco de dados
        databaseAddress db = new databaseAddress();


        private void wndCadCaso_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            OracleConnection Oracon = new OracleConnection(db.oradb);

            Oracon.Open();

            #region Carrega combobox agente
            OracleCommand allagente = new OracleCommand(SELECT_ALL_AGENTE, Oracon);
            OracleDataReader dr = allagente.ExecuteReader();
            while (dr.Read())
            {
                string id = dr[0].ToString();
                string nome = dr[1].ToString();
                cmbAgente1.Items.Add(id + " " + nome);
                cmbAgente2.Items.Add(id + " " + nome);
                cmbAgente3.Items.Add(id + " " + nome);
                cmbAgente4.Items.Add(id + " " + nome);
            }
            #endregion

            Oracon.Close();



            if (modoEdicao)
            {
                //setar agentes
                cmbAgente1.Text = agente1;
                cmbAgente2.Text = agente2;
                cmbAgente3.Text = agente3;
                cmbAgente4.Text = agente4;

                #region Carrega Agencias Auxiliadoras GRID

                Oracon.Open();

                OracleCommand cmdSelect = new OracleCommand(SELECT_ALL_AGENCIASAUX + idDetalhe + "order by id_AgenciaAuxDet", Oracon);
                cmdSelect.ExecuteNonQuery();

                OracleDataAdapter adapter = new OracleDataAdapter(cmdSelect);
                DataTable dt = new DataTable("Agencia Auxiliadora");
                

                adapter.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    dgvOutrasAgencias.Columns.Clear();
                    dgvOutrasAgencias.ItemsSource = dt.DefaultView;

                    dgvOutrasAgencias.Columns[0].Header = "Nome Agencia";
                    dgvOutrasAgencias.Columns[1].Header = "Nome Agente";

                    adapter.Update(dt);
                }

                Oracon.Close();

				//desbloquear agencias auxiliadoras
                grbOutrasAgencias.IsEnabled = true;

                #endregion

                //Desbloquear TabItems 
                tabEnvolvidos.IsEnabled = true;
                tabEvidencias.IsEnabled = true;
                //tabCenaDoCrime.IsEnabled = true;
                tabAnexos.IsEnabled = true;
                tabRelatos.IsEnabled = true;
                tabCasosRel.IsEnabled = true;

                //Mudar algumas Propriedades da tela
                btnArquivar.Content = "Salvar";
            }
        }

        #region CRUD (CU)

        #region CASO
        private async void btnArquivar_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            #region Validations

            if (txtNumCaso.Text == "" || cmbTipoCaso.Text == "" || cmbDeptLocal.Text == "" || cmbCatCaso.Text == "" || cmbAgente1.Text == "" || cmbAgente2.Text == "" || txtDataAbertura.Text == "" || cmbStatusCaso.Text == "")
            {
                await this.ShowMessageAsync("Aviso", "Todos os campos com '*' são obrigatórios para a abertura do caso!");
                return;
            }

            //Checar se é Lavagem 
            if (cmbTipoLavagem.IsEnabled)
            {
                if (cmbTipoLavagem.Text == "")
                {
                    await this.ShowMessageAsync("Aviso", "Especifique a tipologia da lavagem!");
                    return;
                }
            }

            //Checar se é referida a outra agência
            if (ckbRefOutraAgencia.IsChecked == false)
            {
                if (cmbMunicipio.Text == "")
                {
                    await this.ShowMessageAsync("Aviso", "Todos os campos com '*' são obrigatórios para a abertura do caso!");
                    cmbMunicipio.Focus();
                    return;
                }
            }
            else if (cmbEstado.Text == "")
            {
                await this.ShowMessageAsync("Aviso", "Todos os campos com '*' são obrigatórios para a abertura do caso!");
                cmbEstado.Focus();
            }

            #endregion

            #region TO

            Caso objCaso = new Caso();
            objCaso.setNumeroCaso(txtNumCaso.Text);
            objCaso.setTituloCaso(txtTituloCaso.Text);
            objCaso.setTipoCaso(cmbTipoCaso.Text);
            objCaso.setDepLoc(cmbDeptLocal.Text);
            objCaso.setCategoriaCaso(cmbCatCaso.Text);
            string idagente1 = cmbAgente1.Text.Substring(0, cmbAgente1.Text.IndexOf(" "));
            objCaso.setAgente1(idagente1);
            string idagente2 = cmbAgente2.Text.Substring(0, cmbAgente2.Text.IndexOf(" "));
            objCaso.setAgente2(idagente2);
            if (cmbAgente3.Text != "")
            {
                if (cmbAgente3.Text != null)
                {
                    string idagente3 = cmbAgente3.Text.Substring(0, cmbAgente3.Text.IndexOf(" "));
                    objCaso.setAgente3(idagente3);
                }

            }
            if (cmbAgente4.Text != "")
            {
                if (cmbAgente4.Text != null)
                {
                    string idagente4 = cmbAgente4.Text.Substring(0, cmbAgente4.Text.IndexOf(" "));
                    objCaso.setAgente4(idagente4);
                }

            }
            int data = txtDataAbertura.Text.IndexOf('_');

            //Validar data
            if (data != -1)
            {
                await this.ShowMessageAsync("Aviso", "Formato de data Incorreto!");
                txtDataAbertura.Focus();
                return;
            }
            string dataabertura = txtDataAbertura.Text.Replace("/", "-");
            dataabertura = formataData(dataabertura);
            bool eValida = dataValida(dataabertura);
            if (!eValida)
            {
                await this.ShowMessageAsync("Aviso", "Formato de data Incorreto!");
                txtDataAbertura.Focus();
                return;
            }
            objCaso.setDataAbertura(dataabertura);


            objCaso.setStatus(cmbStatusCaso.Text);
            if (txtDataFechamento.Text != "__/__/____")
            {
                string datafechamento = txtDataFechamento.Text.Replace("/", "-");
                datafechamento = formataData(datafechamento);
                bool eValida2 = dataValida(datafechamento);
                if (!eValida2)
                {
                    await this.ShowMessageAsync("Aviso", "Formato de data Incorreto!");
                    txtDataFechamento.Focus();
                    return;
                }
                objCaso.setDataFechamento(datafechamento);
            }

            objCaso.setTipoLavagem(cmbTipoLavagem.Text);
            objCaso.setForcaTarefa(txtForcaTarefa.Text);
            objCaso.setMunicipio(cmbMunicipio.Text);
            objCaso.setEstado(cmbEstado.Text);
            if (ckbRefOutraAgencia.IsChecked == true)
            {
                objCaso.setRefOutraAgencia("sim");
            }
            else
            {
                objCaso.setRefOutraAgencia("não");
            }

            if (ckbGrandeJuri.IsChecked == true)
            {
                objCaso.setGrandeJuri("sim");
            }
            else
            {
                objCaso.setGrandeJuri("não");
            }

            if (ckbInterJuri.IsChecked == true)
            {
                objCaso.setIntervencaoJuri("sim");
            }
            else
            {
                objCaso.setIntervencaoJuri("não");
            }

            if (ckbRelJuri.IsChecked == true)
            {
                objCaso.setRelJuri("sim");
            }
            else
            {
                objCaso.setRelJuri("não");
            }



            #endregion

            OracleConnection Oracon = new OracleConnection(db.oradb);

            if (idCaso == null)
            {
                #region CADASTRAR

                try
                {

                    Oracon.Open();

                    //Inserir Detalhe
                    OracleCommand insertCommandDetail = new OracleCommand(SQL_INSERT_DETAIL, Oracon);
                    insertCommandDetail.Parameters.Add("tipolavagem", objCaso.getTipoLavagem());
                    insertCommandDetail.Parameters.Add("forcatarefa", objCaso.getForcaTarefa());
                    insertCommandDetail.Parameters.Add("municipio", objCaso.getMunicipio());
                    insertCommandDetail.Parameters.Add("estado", objCaso.getEstado());
                    insertCommandDetail.Parameters.Add("refoutraagencia", objCaso.getRefOutraAgencia());
                    insertCommandDetail.Parameters.Add("grandejuri", objCaso.getGrandeJuri());
                    insertCommandDetail.Parameters.Add("intjuri", objCaso.getIntervencaoJuri());
                    insertCommandDetail.Parameters.Add("relatjuri", objCaso.getRelJuri());
                    insertCommandDetail.ExecuteNonQuery();


                    //Pegar o ID Desse detalhe
                    OracleCommand lastIdDetail = new OracleCommand(SELECT_LAST_DETAIL, Oracon);
                    OracleDataReader dr = lastIdDetail.ExecuteReader();
                    dr.Read();

                    string idDetail = dr[0].ToString();

                    //Inserir Caso
                    OracleCommand insertCommandCase = new OracleCommand(SQL_INSERT_CASO, Oracon);
                    insertCommandCase.Parameters.Add("numcaso", objCaso.getNumeroCaso());
                    insertCommandCase.Parameters.Add("titulocaso", objCaso.getTituloCaso());
                    insertCommandCase.Parameters.Add("tipocaso", objCaso.getTipoCaso());
                    insertCommandCase.Parameters.Add("deploc", objCaso.getDepLoc());
                    insertCommandCase.Parameters.Add("catcaso", objCaso.getCategoriaCaso());
                    insertCommandCase.Parameters.Add("agente1", objCaso.getAgente1());
                    insertCommandCase.Parameters.Add("agente2", objCaso.getAgente2());
                    insertCommandCase.Parameters.Add("agente3", objCaso.getAgente3());
                    insertCommandCase.Parameters.Add("agente4", objCaso.getAgente4());
                    insertCommandCase.Parameters.Add("dataabertura", objCaso.getDataAbertura());
                    insertCommandCase.Parameters.Add("datafechamento", objCaso.getDataFechamento());
                    insertCommandCase.Parameters.Add("statuscaso", objCaso.getStatus());
                    insertCommandCase.Parameters.Add("iddetalhecaso", idDetail);
                    insertCommandCase.ExecuteNonQuery();

                    Oracon.Close();

                    await this.ShowMessageAsync("Aviso", "Caso registrado com sucesso!");
                    btnLimpar_Click(null, null);
                    crimeWindow.wndCrimeManagement_Loaded(null, null);



                }
                catch (OracleException ex)
                {
                    MessageBox.Show(ex.Message);
                }

                #endregion
            }
            else
            {
                #region ATUALIZAR

                try
                {

                    Oracon.Open();

                    //Atualizar detalhe
                    OracleCommand updateCommand = new OracleCommand(SQL_UPDATE_DETAIL, Oracon);
                    updateCommand.Parameters.Add("tipolavagem", objCaso.getTipoLavagem());
                    updateCommand.Parameters.Add("forcatarefa", objCaso.getForcaTarefa());
                    updateCommand.Parameters.Add("municipio", objCaso.getMunicipio());
                    updateCommand.Parameters.Add("estado", objCaso.getEstado());
                    updateCommand.Parameters.Add("refoutraagencia", objCaso.getRefOutraAgencia());
                    updateCommand.Parameters.Add("grandejuri", objCaso.getGrandeJuri());
                    updateCommand.Parameters.Add("intjuri", objCaso.getIntervencaoJuri());
                    updateCommand.Parameters.Add("relatjuri", objCaso.getRelJuri());
                    updateCommand.Parameters.Add("idDetalhe", idDetalhe);
                    updateCommand.ExecuteNonQuery();


                    //Inserir Caso
                    OracleCommand updateCommandCase = new OracleCommand(SQL_UPDATE_CASO + idCaso, Oracon);
                    updateCommandCase.Parameters.Add("numcaso", objCaso.getNumeroCaso());
                    updateCommandCase.Parameters.Add("titulocaso", objCaso.getTituloCaso());
                    updateCommandCase.Parameters.Add("tipocaso", objCaso.getTipoCaso());
                    updateCommandCase.Parameters.Add("deploc", objCaso.getDepLoc());
                    updateCommandCase.Parameters.Add("catcaso", objCaso.getCategoriaCaso());
                    updateCommandCase.Parameters.Add("agente1", objCaso.getAgente1());
                    updateCommandCase.Parameters.Add("agente2", objCaso.getAgente2());
                    updateCommandCase.Parameters.Add("agente3", objCaso.getAgente3());
                    updateCommandCase.Parameters.Add("agente4", objCaso.getAgente4());
                    updateCommandCase.Parameters.Add("dataabertura", objCaso.getDataAbertura());
                    updateCommandCase.Parameters.Add("datafechamento", objCaso.getDataFechamento());
                    updateCommandCase.Parameters.Add("statuscaso", objCaso.getStatus());
                    updateCommandCase.ExecuteNonQuery();


                    Oracon.Close();

                    await this.ShowMessageAsync("Aviso", "Caso de Nº " + objCaso.getNumeroCaso() + " atualizado com sucesso!");
                    btnLimpar_Click(null, null);
                    crimeWindow.wndCrimeManagement_Loaded(null, null);


                }
                catch (OracleException ex)
                {
                    MessageBox.Show(ex.Message);
                }

                #endregion
            }

        }

        private static string formataData(string data)
        {
            int mes = Convert.ToInt32(data.Substring(3, data.IndexOf("-")));

            string monthname = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(mes);

            string day = data.Substring(0, data.IndexOf("-"));
            string year = data.Substring(6, 4);
            string newdata = day + "-" + monthname + "-" + year;

            return newdata;
        }
        private bool dataValida(string data)
        {
            DateTime resultado = DateTime.MinValue;

            if (DateTime.TryParse(data, out resultado))
                return true;

            return false;
        }

        #endregion

        #region AGENCIA AUXILIADORA
        private async void btnAddAgenAux_Click(object sender, RoutedEventArgs e)
        {
            if (cmbNomeAgenciaAux.Text == "" || txtNomeAgente.Text == "")
            {
                await this.ShowMessageAsync("Aviso", "Preencha os campos Nome da Agencia e Nome do Agente para adicionar!");
            }
            else
            {
                #region TO
                AgenciaAux objAgenciaAux = new AgenciaAux();

                objAgenciaAux.setNomeAgencia(cmbNomeAgenciaAux.Text);
                objAgenciaAux.setNomeAgente(txtNomeAgente.Text);

                #endregion

                OracleConnection Oracon = new OracleConnection(db.oradb);
                Oracon.Open();
                OracleCommand cmdInsert = new OracleCommand(SQL_INSERT_AGENCIASAUX, Oracon);
                cmdInsert.Parameters.Add("nomeAgencia", objAgenciaAux.getNomeAgencia());
                cmdInsert.Parameters.Add("nomeAgente", objAgenciaAux.getNomeAgente());
                cmdInsert.Parameters.Add("idDetalheCaso", idDetalhe);

                cmdInsert.ExecuteNonQuery();
                Oracon.Close();
                this.wndCadCaso_Loaded(null, null);
            }  
        }
		private async void btnDelAgenAux_Click(object sender, System.Windows.RoutedEventArgs e)
        {
        	
        }

        #endregion

        #endregion

        #region EVENT padrões antes do cadastro

        private void cmbCatCaso_DropDownClosed(object sender, System.EventArgs e)
        {
            if (cmbCatCaso.Text == "Lavagem de Dinheiro / Inv. Financeira")
            {
                cmbTipoLavagem.IsEnabled = true;
            }
            else
            {
                cmbTipoLavagem.IsEnabled = false;
                cmbTipoLavagem.Text = "";
            }
        }

        private void cmbStatusCaso_DropDownClosed(object sender, System.EventArgs e)
        {
            if (cmbStatusCaso.Text == "Aberto")
            {
                txtDataFechamento.IsEnabled = false;
            }
        }

        private void ckbRefOutraAgencia_Checked(object sender, System.Windows.RoutedEventArgs e)
        {
            cmbMunicipio.IsEnabled = false;
            cmbMunicipio.Text = "";
            cmbEstado.IsEnabled = true;
        }

        private void ckbRefOutraAgencia_Unchecked(object sender, System.Windows.RoutedEventArgs e)
        {
            cmbEstado.IsEnabled = false;
            cmbEstado.Text = "";
            cmbMunicipio.IsEnabled = true;
        }

        private void cmbNomeAgenciaAux_DropDownOpened(object sender, System.EventArgs e)
        {
            if (cmbNomeAgenciaAux.Items.Count != 0)
            {
                while (cmbNomeAgenciaAux.HasItems)
                {
                    int item = 0;
                    cmbNomeAgenciaAux.Items.RemoveAt(item);
                    item++;
                }
            }

            OracleConnection Oracon = new OracleConnection(db.oradb);

            Oracon.Open();

            OracleCommand cmdSelect = new OracleCommand(SELECT_ALL_OUTRASAGENCIAS, Oracon);
            OracleDataReader dr = cmdSelect.ExecuteReader();

            while (dr.Read())
            {
                string nomeagencia = dr[0].ToString();

                cmbNomeAgenciaAux.Items.Add(nomeagencia);
            }

            Oracon.Close();
        }

        private void cmbDeptLocal_DropDownOpened(object sender, System.EventArgs e)
        {
            if (cmbDeptLocal.Items.Count != 0)
            {
                while (cmbDeptLocal.HasItems)
                {
                    int item = 0;
                    cmbDeptLocal.Items.RemoveAt(item);
                    item++;
                }
            }

            OracleConnection Oracon = new OracleConnection(db.oradb);

            Oracon.Open();

            OracleCommand cmdSelect = new OracleCommand(SELECT_ALL_UNIDADE, Oracon);
            OracleDataReader dr = cmdSelect.ExecuteReader();

            while (dr.Read())
            {
                string unidadeloc = dr[0].ToString();

                cmbDeptLocal.Items.Add(unidadeloc);
            }

            Oracon.Close();
        }

        #endregion

        #region Envolvidos
        private void btnCadPessoaFisica_Click(object sender, RoutedEventArgs e)
        {
            TipoPessoaFisicaWindow wndTipoPesoaFisica = new TipoPessoaFisicaWindow();
            wndTipoPesoaFisica.ShowDialog();
        }

        #endregion

        private void btnLimpar_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            txtNumCaso.Text = "";
            txtTituloCaso.Text = "";
            cmbTipoCaso.Text = "";
            cmbDeptLocal.Text = "";
            cmbCatCaso.Text = "";
            cmbAgente1.Text = "";
            cmbAgente2.Text = "";
            cmbAgente3.Text = "";
            cmbAgente4.Text = "";
            txtDataAbertura.Text = "";
            txtDataFechamento.Text = "";
            cmbStatusCaso.Text = "";
            cmbNomeAgenciaAux.Text = "";
            txtNomeAgente.Text = "";
            while (dgvOutrasAgencias.Items.Count != 0)
            {
                dgvOutrasAgencias.Items.RemoveAt(0);
            }
            cmbMunicipio.Text = "";
            cmbEstado.Text = "";
            cmbTipoLavagem.Text = "";
            txtForcaTarefa.Text = "";
            ckbRefOutraAgencia.IsChecked = false;
            ckbGrandeJuri.IsChecked = false;
            ckbGrandeJuri.IsChecked = false;
            ckbInterJuri.IsChecked = false;
            ckbRelJuri.IsChecked = false;
        }

        private void btnNovaAgenciaAux_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            CadOutrasAgencias newagenciaAuxWnd = new CadOutrasAgencias();
            newagenciaAuxWnd.ShowDialog();
        }

        private void btnNovaUnidade_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            CadUnidades newUnidadeWnd = new CadUnidades();
            newUnidadeWnd.ShowDialog();
        }

        private void btnCancelar_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.Close();
        }
    }
}