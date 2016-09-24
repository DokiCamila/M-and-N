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
using Oracle.DataAccess.Client;
using sistemaCorporativo.UTIL.databaseAdress;
using System.Data;
using System.Text.RegularExpressions;
using sistemaCorporativo.TO.Denuncia;
using System.Windows.Media.Animation;

namespace sistemaCorporativo.FORMS
{
    /// <summary>
    /// Interaction logic for CadDenuncias.xaml
    /// </summary>
    public partial class CadDenuncia : MetroWindow
    {
        public CadDenuncia()
        {
            InitializeComponent();
        }

        private string idDenuncia;

        #region SQL STRINGS

        //Criar string com o comando para listar tds denuncias revisadas
        private string SQL_SELECT_REV = "select * from denuncia where status = 1 and revisada = 'sim'";
        //Criar string com o comando para listar tds denuncias não-revisadas
        private string SQL_SELECT_NOTREV = "select * from denuncia where status = 1 and revisada = 'não'";
        //Criar string com o comando para inserir
        private string SQL_INSERT = "insert into denuncia(ID_DENUNCIA, NOME_DENUNCIANTE, EMAIL, TIPO_DOCUMENTO, NUMERO_DOCUMENTO, EMISSOR_DOCUMENTO, CPF, CEP, LOGRADOURO, NUMERO, COMPLEMENTO, BAIRRO, CIDADE, ESTADO, TELEFONE, CELULAR, DENUNCIA, STATUS, REVISADA) values (seq_denuncia.NEXTVAL, :nome_denunciante, :email, :tipo_documento, :numero_documento, :emissor_documento, :cpf, :cep, :logradouro, :numero, :complemento, :bairro, :cidade, :estado, :telefone, :celular, :denuncia, 1, 'não')";
        //Criar string (sem o comando) para deletar
        private string SQL_DELETE = "update denuncia set status = 0 where id_denuncia =";
        //Criar string (sem o comando) para atualizar
        private string SQL_UPDATE = "update denuncia set NOME_DENUNCIANTE = :nome_denunciante, EMAIL = :email, TIPO_DOCUMENTO = :tipo_documento, NUMERO_DOCUMENTO = :numero_documento, EMISSOR_DOCUMENTO = :emissor_documento, CPF = :cpf, CEP = :cep, LOGRADOURO = :logradouro, NUMERO = :numero, COMPLEMENTO = :complemento, BAIRRO = :bairro, CIDADE = :cidade, ESTADO = :estado, TELEFONE = :telefone, CELULAR = :celular, DENUNCIA = :denuncia, STATUS = 1, REVISADA = :revisada where id_Denuncia = ";

        #endregion

        //endereço
        databaseAddress db = new databaseAddress();

        //A denúncia é Nao Revisada
        bool isNotRevised;

        private void wndDenuncia_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            OracleConnection Oracon = new OracleConnection(db.oradb);
            try
            {
                //abrir conexao com o banco de dados
                Oracon.Open();

                #region LIST ALL NOT REVISED

                OracleCommand selectCommandNRev = new OracleCommand(SQL_SELECT_NOTREV, Oracon);
                selectCommandNRev.ExecuteNonQuery();

                OracleDataAdapter adapter = new OracleDataAdapter(selectCommandNRev);

                DataTable dt = new DataTable("Denuncia Não-Revisada");
                adapter.Fill(dt);
                dgvDenunciaNRev.ItemsSource = dt.DefaultView;
                dgvDenunciaNRev.Columns[0].Header = "ID";
                dgvDenunciaNRev.Columns[1].Header = "Nome Denunciante";
                dgvDenunciaNRev.Columns[2].Header = "Email";
                dgvDenunciaNRev.Columns[3].Header = "Tipo Documento";
                dgvDenunciaNRev.Columns[4].Header = "Numero Documento";
                dgvDenunciaNRev.Columns[5].Header = "Emissor";
                dgvDenunciaNRev.Columns[6].Header = "CPF";
                dgvDenunciaNRev.Columns[7].Header = "CEP";
                dgvDenunciaNRev.Columns[8].Header = "Logradouro";
                dgvDenunciaNRev.Columns[9].Header = "Numero";
                dgvDenunciaNRev.Columns[10].Header = "Complemento";
                dgvDenunciaNRev.Columns[11].Header = "Bairro";
                dgvDenunciaNRev.Columns[12].Header = "Cidade";
                dgvDenunciaNRev.Columns[13].Header = "UF";
                dgvDenunciaNRev.Columns[14].Header = "Telefone";
                dgvDenunciaNRev.Columns[15].Header = "Celular";
                dgvDenunciaNRev.Columns[16].Header = "Denúncia";
                dgvDenunciaNRev.Columns[17].Header = "Status";
                dgvDenunciaNRev.Columns[18].Header = "Revisada";

                //Esconder
                dgvDenunciaNRev.Columns[17].Visibility = Visibility.Hidden;

                adapter.Update(dt);
                #endregion

                #region LIST ALL REVISED

                OracleCommand selectCommandRev = new OracleCommand(SQL_SELECT_REV, Oracon);
                selectCommandRev.ExecuteNonQuery();

                OracleDataAdapter adapter2 = new OracleDataAdapter(selectCommandRev);

                DataTable dt2 = new DataTable("Denuncia Revisada");
                adapter2.Fill(dt2);

                dgvDenunciaRev.ItemsSource = dt2.DefaultView;
                dgvDenunciaRev.Columns[0].Header = "ID";
                dgvDenunciaRev.Columns[1].Header = "Nome Denunciante";
                dgvDenunciaRev.Columns[2].Header = "Email";
                dgvDenunciaRev.Columns[3].Header = "Tipo Documento";
                dgvDenunciaRev.Columns[4].Header = "Numero Documento";
                dgvDenunciaRev.Columns[5].Header = "Emissor";
                dgvDenunciaRev.Columns[6].Header = "CPF";
                dgvDenunciaRev.Columns[7].Header = "CEP";
                dgvDenunciaRev.Columns[8].Header = "Logradouro";
                dgvDenunciaRev.Columns[9].Header = "Numero";
                dgvDenunciaRev.Columns[10].Header = "Complemento";
                dgvDenunciaRev.Columns[11].Header = "Bairro";
                dgvDenunciaRev.Columns[12].Header = "Cidade";
                dgvDenunciaRev.Columns[13].Header = "UF";
                dgvDenunciaRev.Columns[14].Header = "Telefone";
                dgvDenunciaRev.Columns[15].Header = "Celular";
                dgvDenunciaRev.Columns[16].Header = "Denúncia";
                dgvDenunciaRev.Columns[17].Header = "Status";
                dgvDenunciaRev.Columns[18].Header = "Revisada";

                adapter2.Update(dt2);
                #endregion

                //Fechar conexao com o banco de dados
                Oracon.Close();
            }
            catch (OracleException ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }
        }

        #region CRUD

        private async void btnCadastrar_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            #region Validations
            //Checar se é anonima e todos estão preenchidos
            if (!rdbAnonSim.IsChecked == true)
            {
                //Não é anonima, realizar validações
                if (txtNome.Text == "" || cmbTipo.Text == "" || txtDocumento.Text == "" || cmbEmissor.Text == "")
                {
                    await this.ShowMessageAsync("Aviso", "Todos os campos com '*' são obrigatórios!");
                    return;
                }

                //Se Email for preenchido, realizar validação
                if (txtEmail.Text != "")
                {
                    bool certo = validaEmail(txtEmail.Text);
                    if (!certo)
                    {
                        await this.ShowMessageAsync("Aviso", "Email inválido!");
                        txtEmail.Focus();
                        return;
                    }
                }

                //Se CPF for preenchido, realizar validação
                if (txtCpf.Text != "___.___.___-__")
                {
                    int cpf = txtCpf.Text.IndexOf('_');
                    if (cpf != -1)
                    {
                        await this.ShowMessageAsync("Aviso", "Formato de CPF incorreto!");
                        txtCpf.Focus();
                        return;
                    }
                }
            }

            //Checar se estão preenchidos (os não anonimos)
            if (txtCep.Text == "" || txtLogradouro.Text == "" || txtNumero.Text == "" || txtBairro.Text == "" || txtCidade.Text == "" || cmbEstado.Text == "" || txtTelefone.Text == "" || txtCitDenuncia.Text == "")
            {
                await this.ShowMessageAsync("Aviso", "Todos os campos com '*' são obrigatórios");
                return;
            }
            else
            {
                //Validar CEP
                int cep = txtCep.Text.IndexOf('_');
                if (cep != -1)
                {
                    await this.ShowMessageAsync("Aviso", "Formato de CEP incorreto!");
                    txtCep.Focus();
                    return;
                }
                //Validar Telefone
                if (txtTelefone.Text.Length != 10)
                {
                    await this.ShowMessageAsync("Aviso", "Formato de telefone incorreto!");
                    txtTelefone.Focus();
                    return;
                }

            }

            #endregion

            #region TO
            Denuncia objDenuncia = new Denuncia();
            objDenuncia.setNome(txtNome.Text);
            objDenuncia.setEmail(txtEmail.Text);
            objDenuncia.setTipoDocumento(cmbTipo.Text);
            objDenuncia.setNumeroDocumento(txtDocumento.Text);
            if (cmbEmissor.Text != "")
            {
                string _emissor = cmbEmissor.Text.Substring(0, cmbEmissor.Text.IndexOf(" "));
                objDenuncia.setEmissor(_emissor);
            }
            else
            {
                objDenuncia.setEmissor(cmbEmissor.Text);
            }
            if (txtCpf.Text == "___.___.___-__")
            {
                objDenuncia.setCpf("");
            }
            else
            {
                objDenuncia.setCpf(txtCpf.Text);
            }
            objDenuncia.setCep(txtCep.Text);
            objDenuncia.setLogradouro(txtLogradouro.Text);
            objDenuncia.setNumero(txtNumero.Text);
            objDenuncia.setComplemento(txtComplemento.Text);
            objDenuncia.setBairro(txtBairro.Text);
            objDenuncia.setCidade(txtCidade.Text);
            objDenuncia.setEstado(cmbEstado.Text);
            objDenuncia.setTelefone(txtTelefone.Text);
            objDenuncia.setCelular(txtCelular.Text);
            objDenuncia.setDenuncia(txtCitDenuncia.Text);

            #endregion

            OracleConnection Oracon = new OracleConnection(db.oradb);
            if (idDenuncia == null)
            {
                #region CADASTRAR
                try
                {
                    Oracon.Open();

                    OracleCommand insertCommand = new OracleCommand(SQL_INSERT, Oracon);

                    insertCommand.Parameters.Add("nome_denunciante", objDenuncia.getNome());
                    insertCommand.Parameters.Add("email", objDenuncia.getEmail());
                    insertCommand.Parameters.Add("tipo_documento", objDenuncia.getTipoDocumento());
                    insertCommand.Parameters.Add("numero_documento", objDenuncia.getNumeroDocumento());
                    insertCommand.Parameters.Add("emissor_documento", objDenuncia.getEmissor());
                    insertCommand.Parameters.Add("cpf", objDenuncia.getCpf());
                    insertCommand.Parameters.Add("cep", objDenuncia.getCep());
                    insertCommand.Parameters.Add("logradouro", objDenuncia.getLogradouro());
                    insertCommand.Parameters.Add("numero", objDenuncia.getNumero());
                    insertCommand.Parameters.Add("complemento", objDenuncia.getComplemento());
                    insertCommand.Parameters.Add("bairro", objDenuncia.getBairro());
                    insertCommand.Parameters.Add("cidade", objDenuncia.getCidade());
                    insertCommand.Parameters.Add("estado", objDenuncia.getEstado());
                    insertCommand.Parameters.Add("telefone", objDenuncia.getTelefone());
                    insertCommand.Parameters.Add("celular", objDenuncia.getCelular());
                    insertCommand.Parameters.Add("denuncia", objDenuncia.getDenuncia());
                    insertCommand.ExecuteNonQuery();

                    Oracon.Close();

                    await this.ShowMessageAsync("Aviso", "Denúncia registrada com sucesso!");
                    this.wndDenuncia_Loaded(null, null);
                    this.btnLimpar_Click(null, null);
                    gConsultar.IsSelected = true;


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

                    OracleCommand updateCommand = new OracleCommand(SQL_UPDATE + idDenuncia, Oracon);

                    updateCommand.Parameters.Add("nome_denunciante", objDenuncia.getNome());
                    updateCommand.Parameters.Add("email", objDenuncia.getEmail());
                    updateCommand.Parameters.Add("tipo_documento", objDenuncia.getTipoDocumento());
                    updateCommand.Parameters.Add("numero_documento", objDenuncia.getNumeroDocumento());
                    updateCommand.Parameters.Add("emissor_documento", objDenuncia.getEmissor());
                    updateCommand.Parameters.Add("cpf", objDenuncia.getCpf());
                    updateCommand.Parameters.Add("cep", objDenuncia.getCep());
                    updateCommand.Parameters.Add("logradouro", objDenuncia.getLogradouro());
                    updateCommand.Parameters.Add("numero", objDenuncia.getNumero());
                    updateCommand.Parameters.Add("complemento", objDenuncia.getComplemento());
                    updateCommand.Parameters.Add("bairro", objDenuncia.getBairro());
                    updateCommand.Parameters.Add("cidade", objDenuncia.getCidade());
                    updateCommand.Parameters.Add("estado", objDenuncia.getEstado());
                    updateCommand.Parameters.Add("telefone", objDenuncia.getTelefone());
                    updateCommand.Parameters.Add("celular", objDenuncia.getCelular());
                    updateCommand.Parameters.Add("denuncia", objDenuncia.getDenuncia());
                    if (isNotRevised)
                    {
                        updateCommand.Parameters.Add("revisada", "não");
                    }
                    else
                    {
                        updateCommand.Parameters.Add("revisada", "sim");
                    }
                    updateCommand.ExecuteNonQuery();

                    Oracon.Close();

                    await this.ShowMessageAsync("Aviso", "Denúncia atualizada com sucesso");
                    this.wndDenuncia_Loaded(null, null);
                    this.btnLimpar_Click(null, null);
                    gConsultar.IsSelected = true;
                    if (!isNotRevised)
                    {
                        btnRevisada_Click(null, null);
                    }


                }
                catch (OracleException ex)
                {
                    MessageBox.Show(ex.Message);
                }

                #endregion
            }
        }
        public bool validaEmail(string email)
        {
            bool correto;
            Regex rg = new Regex(@"^[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})$");
            if (rg.IsMatch(email))
            {
                correto = true;
            }
            else
            {
                correto = false;
            }
            return correto;
        }

        private async void btnEditar_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                string SQL_SELECT_THIS = "select * from denuncia where id_denuncia = ";

                if (grdNRevisadas.IsVisible)
                {
                    #region É uma denuncia não revisada
                    if (dgvDenunciaNRev.SelectedIndex != -1)
                    {
                        btnLimpar_Click(null, null);
                        bringidfromGrid(dgvDenunciaNRev);

                        //Coletando valores para atualização
                        OracleConnection Oracon = new OracleConnection(db.oradb);
                        Oracon.Open();

                        OracleCommand selectall = new OracleCommand(SQL_SELECT_THIS + idDenuncia, Oracon);
                        OracleDataReader read = selectall.ExecuteReader();
                        read.Read();

                        //Coletando..
                        txtNome.Text = Convert.ToString(read[1].ToString());
                        if (txtNome.Text == "")
                        {
                            rdbAnonSim.IsChecked = true;
                        }
                        else
                        {
                            rdbAnonNao.IsChecked = true;
                        }
                        txtEmail.Text = Convert.ToString(read[2].ToString());
                        cmbTipo.Text = Convert.ToString(read[3].ToString());
                        txtDocumento.Text = Convert.ToString(read[4].ToString());
                        cmbEmissor.Text = Convert.ToString(read[5].ToString());
                        switch (cmbEmissor.Text)
                        {
                            case "CNT":
                                cmbEmissor.Text = "CNT (Carteira Nacional de Habilitação)";
                                break;
                            case "CTPS":
                                cmbEmissor.Text = "CTPS (Carteira de Trabaho e Previdência Social)";
                                break;
                            case "DIC":
                                cmbEmissor.Text = "DIC (Diretoria de Identificação Civil)";
                                break;
                            case "DPF":
                                cmbEmissor.Text = "DPF (Departamento da Policia Federal";
                                break;
                            case "DPT":
                                cmbEmissor.Text = "DPT (Departamento de Polícia Técnica Geral)";
                                break;
                            case "FGTS":
                                cmbEmissor.Text = "FGTS (Fundo de Garantia do Tempo de Serviço)";
                                break;
                            case "IFP":
                                cmbEmissor.Text = "IFP (Instituto Félix Pacheco)";
                                break;
                            case "IML":
                                cmbEmissor.Text = "IML (Instituto Médico-Legal)";
                                break;
                            case "IPF":
                                cmbEmissor.Text = "IPF (Instituto Pereira Faustino)";
                                break;
                            case "MAE":
                                cmbEmissor.Text = "MAE ( Ministério da Aeronáutica)";
                                break;
                            case "MEX":
                                cmbEmissor.Text = "MEX (Ministério do Exército)";
                                break;
                            case "MMA":
                                cmbEmissor.Text = "MMA (Ministério da Marinha)";
                                break;
                            case "MTE":
                                cmbEmissor.Text = "MTE (Ministério do Trabalho e Emprego)";
                                break;
                            case "OAB":
                                cmbEmissor.Text = "OAB (Ordem dos Advogados do Brasil)";
                                break;
                            case "PC":
                                cmbEmissor.Text = "PC (Policia Civil)";
                                break;
                            case "POM":
                                cmbEmissor.Text = "POM (Policia Militar)";
                                break;
                            case "SES":
                                cmbEmissor.Text = "SES (Carteira de Estrangeiro)";
                                break;
                            case "SJS":
                                cmbEmissor.Text = "SJS (Secretaria da Justiça e Segurança)";
                                break;
                            case "SJTS":
                                cmbEmissor.Text = "SJTS (Secretaria da Justiça do Trabalho e Segurança)";
                                break;
                            case "SSP":
                                cmbEmissor.Text = "SSP (Secretaria da Segurança Pública)";
                                break;
                            case "ZZZ":
                                cmbEmissor.Text = "ZZZ (Outros Inclusive exterior)";
                                break;
                        }
                        txtCpf.Text = Convert.ToString(read[6].ToString());
                        txtCep.Text = Convert.ToString(read[7].ToString());
                        txtLogradouro.Text = Convert.ToString(read[8].ToString());
                        txtNumero.Text = Convert.ToString(read[9].ToString());
                        txtComplemento.Text = Convert.ToString(read[10].ToString());
                        txtBairro.Text = Convert.ToString(read[11].ToString());
                        txtCidade.Text = Convert.ToString(read[12].ToString());
                        cmbEstado.Text = Convert.ToString(read[13].ToString());
                        txtTelefone.Text = Convert.ToString(read[14].ToString());
                        txtCelular.Text = Convert.ToString(read[15].ToString());
                        txtCitDenuncia.Text = Convert.ToString(read[16].ToString());

                        Oracon.Close();

                    }
                    else
                    {
                        await this.ShowMessageAsync("Aviso", "Selecione uma denúncia para editar!");
                        return;
                    }
                    #endregion
                }
                else
                {
                    #region É uma denuncia revisada
                    isNotRevised = false;
                    if (dgvDenunciaRev.SelectedIndex != -1)
                    {
                        btnLimpar_Click(null, null);
                        bringidfromGrid(dgvDenunciaRev);

                        //Coletando valores para atualização
                        OracleConnection Oracon = new OracleConnection(db.oradb);
                        Oracon.Open();

                        OracleCommand selectall = new OracleCommand(SQL_SELECT_THIS + idDenuncia, Oracon);
                        OracleDataReader read = selectall.ExecuteReader();
                        read.Read();

                        //Coletando..
                        txtNome.Text = Convert.ToString(read[1].ToString());
                        if (txtNome.Text == "")
                        {
                            rdbAnonSim.IsChecked = true;
                        }
                        else
                        {
                            rdbAnonNao.IsChecked = true;
                        }
                        txtEmail.Text = Convert.ToString(read[2].ToString());
                        cmbTipo.Text = Convert.ToString(read[3].ToString());
                        txtDocumento.Text = Convert.ToString(read[4].ToString());
                        cmbEmissor.Text = Convert.ToString(read[5].ToString());
                        switch (cmbEmissor.Text)
                        {
                            case "CNT":
                                cmbEmissor.Text = "CNT (Carteira Nacional de Habilitação)";
                                break;
                            case "CTPS":
                                cmbEmissor.Text = "CTPS (Carteira de Trabaho e Previdência Social)";
                                break;
                            case "DIC":
                                cmbEmissor.Text = "DIC (Diretoria de Identificação Civil)";
                                break;
                            case "DPF":
                                cmbEmissor.Text = "DPF (Departamento da Policia Federal";
                                break;
                            case "DPT":
                                cmbEmissor.Text = "DPT (Departamento de Polícia Técnica Geral)";
                                break;
                            case "FGTS":
                                cmbEmissor.Text = "FGTS (Fundo de Garantia do Tempo de Serviço)";
                                break;
                            case "IFP":
                                cmbEmissor.Text = "IFP (Instituto Félix Pacheco)";
                                break;
                            case "IML":
                                cmbEmissor.Text = "IML (Instituto Médico-Legal)";
                                break;
                            case "IPF":
                                cmbEmissor.Text = "IPF (Instituto Pereira Faustino)";
                                break;
                            case "MAE":
                                cmbEmissor.Text = "MAE ( Ministério da Aeronáutica)";
                                break;
                            case "MEX":
                                cmbEmissor.Text = "MEX (Ministério do Exército)";
                                break;
                            case "MMA":
                                cmbEmissor.Text = "MMA (Ministério da Marinha)";
                                break;
                            case "MTE":
                                cmbEmissor.Text = "MTE (Ministério do Trabalho e Emprego)";
                                break;
                            case "OAB":
                                cmbEmissor.Text = "OAB (Ordem dos Advogados do Brasil)";
                                break;
                            case "PC":
                                cmbEmissor.Text = "PC (Policia Civil)";
                                break;
                            case "POM":
                                cmbEmissor.Text = "POM (Policia Militar)";
                                break;
                            case "SES":
                                cmbEmissor.Text = "SES (Carteira de Estrangeiro)";
                                break;
                            case "SJS":
                                cmbEmissor.Text = "SJS (Secretaria da Justiça e Segurança)";
                                break;
                            case "SJTS":
                                cmbEmissor.Text = "SJTS (Secretaria da Justiça do Trabalho e Segurança)";
                                break;
                            case "SSP":
                                cmbEmissor.Text = "SSP (Secretaria da Segurança Pública)";
                                break;
                            case "ZZZ":
                                cmbEmissor.Text = "ZZZ (Outros Inclusive exterior)";
                                break;
                        }
                        txtCpf.Text = Convert.ToString(read[6].ToString());
                        txtCep.Text = Convert.ToString(read[7].ToString());
                        txtLogradouro.Text = Convert.ToString(read[8].ToString());
                        txtNumero.Text = Convert.ToString(read[9].ToString());
                        txtComplemento.Text = Convert.ToString(read[10].ToString());
                        txtBairro.Text = Convert.ToString(read[11].ToString());
                        txtCidade.Text = Convert.ToString(read[12].ToString());
                        cmbEstado.Text = Convert.ToString(read[13].ToString());
                        txtTelefone.Text = Convert.ToString(read[14].ToString());
                        txtCelular.Text = Convert.ToString(read[15].ToString());
                        txtCitDenuncia.Text = Convert.ToString(read[16].ToString());

                        Oracon.Close();
                    }
                    else
                    {
                        await this.ShowMessageAsync("Aviso", "Selecione uma denúncia para editar!");
                        return;
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            gConsultar.IsSelected = false;
            gCadastrar.IsSelected = true;
        }
        private void bringidfromGrid(DataGrid dgv)
        {
            object itemid = dgv.SelectedItem;
            idDenuncia = (dgv.SelectedCells[0].Column.GetCellContent(itemid) as TextBlock).Text;
            idDenuncia.ToString();
        }

        private async void btnExcluir_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (grdNRevisadas.IsVisible)
            {
                #region É uma denúncia não-revisada

                if (dgvDenunciaNRev.SelectedIndex != -1)
                {
                    var mySettings = new MetroDialogSettings()
                    {
                        AffirmativeButtonText = "Sim",
                        NegativeButtonText = "Cancelar",
                        ColorScheme = MetroDialogOptions.ColorScheme
                    };

                    MessageDialogResult result = await this.ShowMessageAsync("Atenção", "Você tem certeza que deseja remover a denúncia?", MessageDialogStyle.AffirmativeAndNegative, mySettings);

                    if (result == MessageDialogResult.Affirmative)
                    {
                        OracleConnection Oracon = new OracleConnection(db.oradb);
                        try
                        {

                            //Abrir conexão com o banco de dados
                            Oracon.Open();


                            //obtendo valor da célula na coluna ID
                            object item = dgvDenunciaNRev.SelectedItem;
                            idDenuncia = (dgvDenunciaNRev.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text;
                            idDenuncia.ToString();

                            //executando comando usando o ID como base para "apagar" um item
                            OracleCommand deleteCommand = new OracleCommand(SQL_DELETE + idDenuncia, Oracon);
                            deleteCommand.ExecuteNonQuery();

                            //Fechar conexão com o banco de dados
                            Oracon.Close();

                            System.Windows.Forms.MessageBox.Show("Denúncia deletada com sucesso!", "Aviso", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation, System.Windows.Forms.MessageBoxDefaultButton.Button1);
                            this.wndDenuncia_Loaded(null, null);
                            this.btnLimpar_Click(null, null);

                        }
                        catch (OracleException orclEx)
                        {

                            System.Windows.MessageBox.Show(orclEx.Message);
                        }
                    }


                }
                else
                {
                    await this.ShowMessageAsync("Aviso", "Selecione uma viatura para excluir!");
                }

                #endregion
            }
            else
            {
                #region É uma denúncia revisada

                if (dgvDenunciaRev.SelectedIndex != -1)
                {
                    var mySettings = new MetroDialogSettings()
                    {
                        AffirmativeButtonText = "Sim",
                        NegativeButtonText = "Cancelar",
                        ColorScheme = MetroDialogOptions.ColorScheme
                    };

                    MessageDialogResult result = await this.ShowMessageAsync("Atenção", "Você tem certeza que deseja remover a denúncia?", MessageDialogStyle.AffirmativeAndNegative, mySettings);

                    if (result == MessageDialogResult.Affirmative)
                    {
                        OracleConnection Oracon = new OracleConnection(db.oradb);
                        try
                        {

                            //Abrir conexão com o banco de dados
                            Oracon.Open();


                            //obtendo valor da célula na coluna ID
                            object item = dgvDenunciaRev.SelectedItem;
                            idDenuncia = (dgvDenunciaRev.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text;
                            idDenuncia.ToString();

                            //executando comando usando o ID como base para "apagar" um item
                            OracleCommand deleteCommand = new OracleCommand(SQL_DELETE + idDenuncia, Oracon);
                            deleteCommand.ExecuteNonQuery();

                            //Fechar conexão com o banco de dados
                            Oracon.Close();

                            System.Windows.Forms.MessageBox.Show("Denúncia deletada com sucesso!", "Aviso", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation, System.Windows.Forms.MessageBoxDefaultButton.Button1);
                            this.wndDenuncia_Loaded(null, null);
                            this.btnLimpar_Click(null, null);
                            gConsultar.IsSelected = true;

                        }
                        catch (OracleException orclEx)
                        {

                            System.Windows.MessageBox.Show(orclEx.Message);
                        }
                    }


                }
                else
                {
                    await this.ShowMessageAsync("Aviso", "Selecione uma denúncia para excluir!");
                }

                #endregion
            }
        }


        #endregion

        private void rdbAnonSim_Checked(object sender, System.Windows.RoutedEventArgs e)
        {
            gpbDadosPessoais.IsEnabled = false;
        }

        private void rdbAnonNao_Checked(object sender, System.Windows.RoutedEventArgs e)
        {
            gpbDadosPessoais.IsEnabled = true;
        }

        #region Eventos (validações controles)

        private void txtNumero_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if ((e.Key >= Key.A && e.Key <= Key.Z) || e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }

        private void txtDocumento_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }

        #endregion

        private void btnLimpar_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            idDenuncia = null;
            txtNome.Text = "";
            txtEmail.Text = "";
            cmbTipo.Text = "";
            txtDocumento.Text = "";
            cmbEmissor.Text = "";
            txtCpf.Text = "";
            txtCep.Text = "";
            txtLogradouro.Text = "";
            txtComplemento.Text = "";
            txtNumero.Text = "";
            txtBairro.Text = "";
            txtCidade.Text = "";
            cmbEstado.Text = "";
            txtTelefone.Text = "";
            txtCelular.Text = "";
            txtCitDenuncia.Text = "";
        }

        private void btnNRevisada_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            grdNRevisadas.Visibility = Visibility.Visible;
            grdRevisadas.Visibility = Visibility.Hidden;
            Storyboard Animation = this.FindResource("NRevisadaAnim") as Storyboard;
            Animation.Begin();
            btnRevisada.IsChecked = false;
            isNotRevised = true;
        }
        private void btnRevisada_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            grdNRevisadas.Visibility = Visibility.Hidden;
            grdRevisadas.Visibility = Visibility.Visible;
            Storyboard Animation = this.FindResource("RevisadaAnim") as Storyboard;
            Animation.Begin();
            btnNRevisada.IsChecked = false;
            isNotRevised = false;
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
