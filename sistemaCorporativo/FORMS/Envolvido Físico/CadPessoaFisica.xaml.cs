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
using sistemaCorporativo.UTIL.databaseAdress;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
using sistemaCorporativo.TO.EnvolvidoF;
using System.Data;
using System.Text.RegularExpressions;
using sistemaCorporativo.UTIL;

namespace sistemaCorporativo.FORMS
{
    /// <summary>
    /// Interaction logic for CadPessoaFisica.xaml
    /// </summary>
    public partial class CadPessoaFisica : MetroWindow
    {
        private String categoria = null;
        private String idEnvolvidoF = null;
        private String idCaso;
        public Boolean Edicao;
        public CadPessoaFisica(String infocat, string idEnvInfo, string idCasoInfo)
        {
            InitializeComponent();
            categoria = infocat;
            idEnvolvidoF = idEnvInfo;
            idCaso = idCasoInfo;
        }

        #region SQL SCRIPT
        //Envolvido F
        private string SQL_READENVOLVIDO = "SELECT * from ENVOLVIDO_F where id_Envolvido = ";
        private string SQL_INSERTENVOLVIDO = "INSERT into ENVOLVIDO_F (id_Envolvido, Primeiro_Nome, Ultimo_Nome, Conhecido_Como, Sexo, Dt_Nascimento, Dt_Morte, Altura, Largura, Peso, Raca, Cor_Cabelo, Cor_Olhos, Cor_Pele, CPF, RG, Estado_Civil, Naturalidade, Nacionalidade, Est_Ilegal, Observacoes, Num_Registro, Dt_Emissao, Dt_Validade, Cat_Hab, Estado, Status)"
                                           + "VALUES (seq_EnvolvidoF.NEXTVAL, :primeiroNome, :ultimoNome, :conhecidoComo, :sexo, :dtNascimento, :dtMorte, :altura, :largura, :peso, :raca, :corCabelo, :corOlhos, :corPele, :cpf, :rg, :estadoCivil, :naturalidade, :nacionalidade, :estIlegal, :observacoes, :numregistro, :dtEmissao, :dtValidade, :catHab, :estado, 1)";
        private string SQL_UPDATEENVOLVIDO = "update ENVOLVIDO_F set Primeiro_Nome = :primeiroNome, Ultimo_Nome = :ultimoNome, Conhecido_Como = :conhecidoComo, Sexo = :sexo, Dt_Nascimento = :dtNascimento, Dt_Morte = :dtMorte, Altura = :altura, Largura = :largura, Peso = :peso, Raca = :raca, Cor_Cabelo = :corCabelo, Cor_Olhos = :corOlhos, Cor_Pele = :corPele, CPF = :cpf, RG = :rg, Estado_Civil = :estadoCivil, Naturalidade = :naturalidade, Nacionalidade = :nacionalidade, Est_Ilegal = :estIlegal, Observacoes = :observacoes, Num_Registro = :numregistro, Dt_Emissao = :dtEmissao, Dt_Validade = :dtValidade, Cat_Hab = :catHab, Estado = :estado where id_Envolvido =";
        private string SQL_TAKELASTENVOLVIDO = "SELECT id_Envolvido FROM ENVOLVIDO_F WHERE id_Envolvido = ( SELECT MAX(id_Envolvido) FROM ENVOLVIDO_F)";

        //Contato
        private string SELECT_CONTATO = "select id_Contato, Descricao from CONTATO_ENV where status = 1 and id_EnvolvidoF = :idEnvolvido and tipo = :tipo";
        private string SQL_ADDCONTATO = "insert into CONTATO_ENV (id_Contato, id_EnvolvidoF, Descricao, Tipo, Status) VALUES (seq_ContatoEnv.NEXTVAL, :idEnvolvido, :descricao, :tipo, 1)";
        private string SQL_DELETECONTATO = "update CONTATO_ENV set status = 0 where id_Contato = ";

        //Endereco
        private string SELECT_ENDERECO = "select id_Endereco, CEP, Logradouro, Numero, Complemento, Bairro, Cidade, Uf from ENDERECO_ENV where status = 1 and id_EnvolvidoF = :idEnvolvido";
        private string SQL_ADDENDERECO = "insert into ENDERECO_ENV (id_Endereco, id_EnvolvidoF, CEP, Logradouro, Numero, Complemento, Bairro, Cidade, Uf, Status)"
                                       + "values (seq_EnderecoEnv.NEXTVAL, :idEnvolvido, :cep, :logradouro, :numero, :complemento, :bairro, :cidade, :uf, 1)";
        private string SQL_DELETEENDERECO = "update ENDERECO_ENV set status = 0 where id_Endereco = ";

        //Inserir em um caso
        private string SQL_INSERTONCASE = "INSERT into ENV_CASO (id_EnvolvidoF, id_Caso, categoria) VALUES (:idEnvolvido, :idCaso, :categoria)";
        #endregion

        //Imagens Impressões Digitais
        BitmapImage polD, indD, medD, anuD, minD, polE, indE, medE, anuE, minE;

        //Endereço do banco de dados
        databaseAddress db = new databaseAddress();

        #region LOAD
        private void wndCadPessoaFisica_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            if (Edicao)
            {
                OracleConnection Oracon = new OracleConnection(db.oradb);

                Oracon.Open();
                OracleCommand cmdBuscarEnvolvido = new OracleCommand(SQL_READENVOLVIDO + idEnvolvidoF, Oracon);
                OracleDataReader rd = cmdBuscarEnvolvido.ExecuteReader();

                rd.Read();

                //Pegar dados
                txtPrimeiroNome.Text = rd[1].ToString();
                txtUltimoNome.Text = rd[2].ToString();
                txtConhecidoComo.Text = rd[3].ToString();
                string sexo = rd[4].ToString();
                if (sexo == "Masculino")
                {
                    rdbMasculino.IsChecked = true;
                }
                else
                {
                    rdbFeminino.IsChecked = true;
                }
                txtDataNascimento.Text = rd[5].ToString();
                txtDataMorte.Text = rd[6].ToString();
                txtAltura.Text = rd[7].ToString();
                txtLargura.Text = rd[8].ToString();
                txtPeso.Text = rd[9].ToString();
                cmbRaca.Text = rd[10].ToString();
                cmbCorCabelo.Text = rd[11].ToString();
                cmbCorOlhos.Text = rd[12].ToString();
                cmbCorPele.Text = rd[13].ToString();
                txtCpf.Text = rd[14].ToString();
                txtRg.Text = rd[15].ToString();
                cmbEstatoCivil.Text = rd[16].ToString();
                cmbNaturalidade.Text = rd[17].ToString();
                txtNacionalidade.Text = rd[18].ToString();
                int estilegal = Convert.ToInt32(rd[19].ToString());
                if (estilegal != 0)
                {
                    ckbEstrangeiroIlegal.IsChecked = true;
                }
                else
                {
                    ckbEstrangeiroIlegal.IsChecked = false;
                }
                txtObservacoes.Text = rd[20].ToString();
                txtNumRegistro.Text = rd[21].ToString();
                txtDataEmissaoCnh.Text = rd[22].ToString();
                txtDataValidadeCnh.Text = rd[23].ToString();
                cmbCatHab.Text = rd[24].ToString();
                cmbEstado.Text = rd[25].ToString();

                //Mudar titulo
                this.Title = "Editar Pessoa Física | " + rd[1].ToString() + " " + rd[2].ToString();
                Oracon.Close();

                //Carregar Dados Centrais
                lerContatos("Telefone");
                lerContatos("Celular");
                lerContatos("Email");
                lerContatos("Nome Usuario");
                //Carregar Endereços
                lerEndereco();

                if (categoria != null && Edicao == true)
                {
                    //Mudar Texto Botão
                    this.textBtnOk.Text = "Concluir";
                }

                //Desbloquear
                grbEndereco.IsEnabled = true;
                grdCentral.IsEnabled = true;
                grbQuadrilha.IsEnabled = true;

            }

        }

        private void lerContatos(string tipo)
        {
            OracleConnection Oracon = new OracleConnection(db.oradb);

            Oracon.Open();

            OracleCommand sqlSelectContatos = new OracleCommand(SELECT_CONTATO, Oracon);
            sqlSelectContatos.Parameters.Add("idEnvolvido", idEnvolvidoF);
            sqlSelectContatos.Parameters.Add("tipo", tipo);

            sqlSelectContatos.ExecuteNonQuery();

            OracleDataAdapter adapter = new OracleDataAdapter(sqlSelectContatos);
            DataTable dt = new DataTable("Contato");

            adapter.Fill(dt);
            if (tipo == "Telefone")
            {
                dgvTelefone.Columns.Clear();
                dgvTelefone.ItemsSource = dt.DefaultView;

                dgvTelefone.Columns[0].Header = "Nº";
                dgvTelefone.Columns[1].Header = "Número do Telefone";
                dgvTelefone.Columns[1].Width = new DataGridLength(1, DataGridLengthUnitType.Star);

                if (dgvTelefone.Items.Count == 0)
                {
                    dgvTelefone.Columns[1].Width = new DataGridLength(1, DataGridLengthUnitType.Star);
                }
            }
            else if (tipo == "Celular")
            {
                dgvCelular.Columns.Clear();
                dgvCelular.ItemsSource = dt.DefaultView;

                dgvCelular.Columns[0].Header = "Nº";
                dgvCelular.Columns[1].Header = "Número do Celular";
                dgvTelefone.Columns[1].Width = new DataGridLength(1, DataGridLengthUnitType.Star);

                if (dgvCelular.Items.Count == 0)
                {
                    dgvCelular.Columns[1].Width = new DataGridLength(1, DataGridLengthUnitType.Star);
                }
            }
            else if (tipo == "Email")
            {
                dgvEmail.Columns.Clear();
                dgvEmail.ItemsSource = dt.DefaultView;

                dgvEmail.Columns[0].Header = "Nº";
                dgvEmail.Columns[1].Header = "E-Mail";

                if (dgvEmail.Items.Count == 0)
                {
                    dgvEmail.Columns[1].Width = new DataGridLength(1, DataGridLengthUnitType.Star);
                }
            }
            else
            {
                dgvNomeUsuario.Columns.Clear();
                dgvNomeUsuario.ItemsSource = dt.DefaultView;

                dgvNomeUsuario.Columns[0].Header = "Nº";
                dgvNomeUsuario.Columns[1].Header = "Usuário";

                if (dgvNomeUsuario.Items.Count == 0)
                {
                    dgvNomeUsuario.Columns[1].Width = new DataGridLength(1, DataGridLengthUnitType.Star);
                }
            }

            adapter.Update(dt);
            Oracon.Close();
        }

        private void lerEndereco()
        {

            OracleConnection Oracon = new OracleConnection(db.oradb);

            Oracon.Open();

            OracleCommand sqlSelectContatos = new OracleCommand(SELECT_ENDERECO, Oracon);
            sqlSelectContatos.Parameters.Add("idEnvolvido", idEnvolvidoF);

            sqlSelectContatos.ExecuteNonQuery();

            OracleDataAdapter adapter = new OracleDataAdapter(sqlSelectContatos);
            DataTable dt = new DataTable("Endereco");

            adapter.Fill(dt);
            dgvEndereco.Columns.Clear();
            dgvEndereco.ItemsSource = dt.DefaultView;

            dgvEndereco.Columns[0].Header = "ID";
            dgvEndereco.Columns[1].Header = "CEP";
            dgvEndereco.Columns[2].Header = "Logradouro";
            dgvEndereco.Columns[3].Header = "Número";
            dgvEndereco.Columns[4].Header = "Complemento";
            dgvEndereco.Columns[5].Header = "Bairro";
            dgvEndereco.Columns[6].Header = "Cidade";
            dgvEndereco.Columns[7].Header = "UF";
            adapter.Update(dt);
        }

        #endregion

        #region CRUD (C)

        private async void btnOk_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (txtPrimeiroNome.Text == "" || txtUltimoNome.Text == "" || txtDataNascimento.Text == "" || txtCpf.Text == "" || cmbNaturalidade.Text == "" || txtObservacoes.Text == "")
            {
                await this.ShowMessageAsync("Aviso", "Todos os campos com '*' são de preenchimento obrigatório!");
                return;
            }

            #region TO

            EnvolvidoF objEnvolvidoF = new EnvolvidoF();
            objEnvolvidoF.setPrimeiroNome(txtPrimeiroNome.Text);
            objEnvolvidoF.setUltimoNome(txtUltimoNome.Text);
            objEnvolvidoF.setConhecidoComo(txtConhecidoComo.Text);
            if (rdbMasculino.IsChecked == true)
            {
                objEnvolvidoF.setSexo("Masculino");
            }
            else
            {
                objEnvolvidoF.setSexo("Feminino");
            }
            objEnvolvidoF.setDtNascimento(txtDataNascimento.SelectedDate.Value);
            if (txtDataMorte.Text != "")
            {
                objEnvolvidoF.setDtMorte(txtDataMorte.SelectedDate.Value);
            }
            objEnvolvidoF.setAltura(Convert.ToDouble(txtAltura.Text));
            objEnvolvidoF.setLargura(Convert.ToDouble(txtLargura.Text));
            objEnvolvidoF.setPeso(Convert.ToDouble(txtPeso.Text));
            objEnvolvidoF.setRaca(cmbRaca.Text);
            objEnvolvidoF.setCorCabelo(cmbCorCabelo.Text);
            objEnvolvidoF.setCorOlhos(cmbCorOlhos.Text);
            objEnvolvidoF.setCorPele(cmbCorPele.Text);
            objEnvolvidoF.setCPF(txtCpf.Text);
            objEnvolvidoF.setRG(txtRg.Text);
            objEnvolvidoF.setEstadoCivil(cmbEstatoCivil.Text);
            objEnvolvidoF.setNaturalidade(cmbNaturalidade.Text);
            if (ckbEstrangeiroIlegal.IsChecked == true)
            {
                objEnvolvidoF.setEstIlegal(1);
            }
            else
            {
                objEnvolvidoF.setEstIlegal(0);
            }
            objEnvolvidoF.setObservacao(txtObservacoes.Text);
            objEnvolvidoF.setNumRegistro(txtNumRegistro.Text);
            if (txtDataEmissaoCnh.Text != "")
            {
                objEnvolvidoF.setDtEmissao(txtDataEmissaoCnh.SelectedDate.Value);
            }
            if (txtDataValidadeCnh.Text != "")
            {
                objEnvolvidoF.setDtValidade(txtDataValidadeCnh.SelectedDate.Value);
            }
            objEnvolvidoF.setCatHab(cmbCatHab.Text);
            objEnvolvidoF.setEstado(cmbEstado.Text);

            OracleConnection Oracon = new OracleConnection(db.oradb);

            #endregion

            if (idEnvolvidoF == null)
            {
                #region CADASTRO

                try
                {
                    Oracon.Open();

                    OracleCommand cmdInsert = new OracleCommand(SQL_INSERTENVOLVIDO, Oracon);
                    cmdInsert.Parameters.Add("primeiroNome", objEnvolvidoF.getPrimeiroNome());
                    cmdInsert.Parameters.Add("ultimoNome", objEnvolvidoF.getUltimoNome());
                    cmdInsert.Parameters.Add("conhecidoComo", objEnvolvidoF.getConhecidoComo());
                    cmdInsert.Parameters.Add("sexo", objEnvolvidoF.getSexo());
                    cmdInsert.Parameters.Add("dtNascimento", objEnvolvidoF.getDtNascimento());
                    if (txtDataMorte.Text == "")
                    {
                        cmdInsert.Parameters.Add("dtMorte", null);
                    }
                    else
                    {
                        cmdInsert.Parameters.Add("dtMorte", objEnvolvidoF.getDtMorte());
                    }
                    cmdInsert.Parameters.Add("altura", objEnvolvidoF.getAltura());
                    cmdInsert.Parameters.Add("largura", objEnvolvidoF.getLargura());
                    cmdInsert.Parameters.Add("peso", objEnvolvidoF.getPeso());
                    cmdInsert.Parameters.Add("raca", objEnvolvidoF.getRaca());
                    cmdInsert.Parameters.Add("corCabelo", objEnvolvidoF.getCorCabelo());
                    cmdInsert.Parameters.Add("corOlhos", objEnvolvidoF.getCorOlhos());
                    cmdInsert.Parameters.Add("corPele", objEnvolvidoF.getCorPele());
                    cmdInsert.Parameters.Add("cpf", objEnvolvidoF.getCPF());
                    cmdInsert.Parameters.Add("rg", objEnvolvidoF.getRG());
                    cmdInsert.Parameters.Add("estadoCivil", objEnvolvidoF.getEstadoCivil());
                    cmdInsert.Parameters.Add("naturalidade", objEnvolvidoF.getNaturalidade());
                    cmdInsert.Parameters.Add("nacionalidade", objEnvolvidoF.getNacionalidade());
                    cmdInsert.Parameters.Add("estIlegal", objEnvolvidoF.getEstIlegal());
                    cmdInsert.Parameters.Add("observacoes", objEnvolvidoF.getObservacao());
                    cmdInsert.Parameters.Add("numregistro", objEnvolvidoF.getNumRegistro());
                    if (txtDataEmissaoCnh.Text == "")
                    {
                        cmdInsert.Parameters.Add("dtEmissao", null);
                    }
                    else
                    {
                        cmdInsert.Parameters.Add("dtEmissao", objEnvolvidoF.getDtEmissao());
                    }

                    if (txtDataValidadeCnh.Text == "")
                    {
                        cmdInsert.Parameters.Add("dtValidade", null);
                    }
                    else
                    {
                        cmdInsert.Parameters.Add("dtValidade", objEnvolvidoF.getDtValidade());
                    }
                    cmdInsert.Parameters.Add("catHab", objEnvolvidoF.getCatHab());
                    cmdInsert.Parameters.Add("estado", objEnvolvidoF.getEstado());
                    cmdInsert.ExecuteNonQuery();

                    OracleCommand sqlTakeLastPerson = new OracleCommand(SQL_TAKELASTENVOLVIDO, Oracon);
                    OracleDataReader reader = sqlTakeLastPerson.ExecuteReader();

                    reader.Read();
                    idEnvolvidoF = reader[0].ToString();

                    Oracon.Close();

                    Edicao = true;

                    await this.ShowMessageAsync("Aviso", "Pessoa Física cadastrada com sucesso!");
                    this.wndCadPessoaFisica_Loaded(null, null);

                }
                catch (Exception ex)
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

                    OracleCommand cmdUpdate = new OracleCommand(SQL_UPDATEENVOLVIDO + idEnvolvidoF, Oracon);
                    cmdUpdate.Parameters.Add("primeiroNome", objEnvolvidoF.getPrimeiroNome());
                    cmdUpdate.Parameters.Add("ultimoNome", objEnvolvidoF.getUltimoNome());
                    cmdUpdate.Parameters.Add("conhecidoComo", objEnvolvidoF.getConhecidoComo());
                    cmdUpdate.Parameters.Add("sexo", objEnvolvidoF.getSexo());
                    cmdUpdate.Parameters.Add("dtNascimento", objEnvolvidoF.getDtNascimento());
                    if (txtDataMorte.Text == "")
                    {
                        cmdUpdate.Parameters.Add("dtMorte", null);
                    }
                    else
                    {
                        cmdUpdate.Parameters.Add("dtMorte", objEnvolvidoF.getDtMorte());
                    }
                    cmdUpdate.Parameters.Add("altura", objEnvolvidoF.getAltura());
                    cmdUpdate.Parameters.Add("largura", objEnvolvidoF.getLargura());
                    cmdUpdate.Parameters.Add("peso", objEnvolvidoF.getPeso());
                    cmdUpdate.Parameters.Add("raca", objEnvolvidoF.getRaca());
                    cmdUpdate.Parameters.Add("corCabelo", objEnvolvidoF.getCorCabelo());
                    cmdUpdate.Parameters.Add("corOlhos", objEnvolvidoF.getCorOlhos());
                    cmdUpdate.Parameters.Add("corPele", objEnvolvidoF.getCorPele());
                    cmdUpdate.Parameters.Add("cpf", objEnvolvidoF.getCPF());
                    cmdUpdate.Parameters.Add("rg", objEnvolvidoF.getRG());
                    cmdUpdate.Parameters.Add("estadoCivil", objEnvolvidoF.getEstadoCivil());
                    cmdUpdate.Parameters.Add("naturalidade", objEnvolvidoF.getNaturalidade());
                    cmdUpdate.Parameters.Add("nacionalidade", objEnvolvidoF.getNacionalidade());
                    cmdUpdate.Parameters.Add("estIlegal", objEnvolvidoF.getEstIlegal());
                    cmdUpdate.Parameters.Add("observacoes", objEnvolvidoF.getObservacao());
                    cmdUpdate.Parameters.Add("numregistro", objEnvolvidoF.getNumRegistro());
                    if (txtDataEmissaoCnh.Text == "")
                    {
                        cmdUpdate.Parameters.Add("dtEmissao", null);
                    }
                    else
                    {
                        cmdUpdate.Parameters.Add("dtEmissao", objEnvolvidoF.getDtEmissao());
                    }

                    if (txtDataValidadeCnh.Text == "")
                    {
                        cmdUpdate.Parameters.Add("dtValidade", null);
                    }
                    else
                    {
                        cmdUpdate.Parameters.Add("dtValidade", objEnvolvidoF.getDtValidade());
                    }
                    cmdUpdate.Parameters.Add("catHab", objEnvolvidoF.getCatHab());
                    cmdUpdate.Parameters.Add("estado", objEnvolvidoF.getEstado());
                    cmdUpdate.ExecuteNonQuery();

                    Oracon.Close();

                    if (categoria != null)
                    {
                        inserirNoCaso();
                        this.Close();
                    }
                    else
                    {
                        await this.ShowMessageAsync("Aviso", "Pessoa Física atualizada com sucesso!");
                        this.wndCadPessoaFisica_Loaded(null, null);
                    }


                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show(ex.Message);
                }

                #endregion
            }
        }

        private void inserirNoCaso()
        {
            OracleConnection Oracon = new OracleConnection(db.oradb);

            Oracon.Open();
            OracleCommand cmdInsereNoCaso = new OracleCommand(SQL_INSERTONCASE, Oracon);
            cmdInsereNoCaso.Parameters.Add("idEnvolvido", idEnvolvidoF);
            cmdInsereNoCaso.Parameters.Add("idCaso", idCaso);
            cmdInsereNoCaso.Parameters.Add("categoria", categoria);
            cmdInsereNoCaso.ExecuteNonQuery();
            Oracon.Close();
        }

        #endregion

        #region CRUD (CD) CADASTROS SECUNDARIOS

        #region Telefone Fixo
        private async void btnAddTelFixo_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                if (txtTelefoneFixo.Text == "(__)____-____")
                {
                    await this.ShowMessageAsync("Aviso", "Preencha o campo Telefone fixo para adicionar!");
                    txtTelefoneFixo.Focusable = true;
                    return;
                }
                int temunderline = txtTelefoneFixo.Text.IndexOf("_");
                if (temunderline != -1)
                {
                    await this.ShowMessageAsync("Aviso", "Número de Telefone Inválido!");
                    txtTelefoneFixo.Focusable = true;
                    return;
                }
                else
                {
                    #region TO
                    ContatoEnvF contato = new ContatoEnvF();

                    contato.setDescricao(txtTelefoneFixo.Text);
                    contato.setTipo("Telefone");

                    #endregion

                    OracleConnection Oracon = new OracleConnection(db.oradb);
                    Oracon.Open();
                    OracleCommand cmdInsert = new OracleCommand(SQL_ADDCONTATO, Oracon);
                    cmdInsert.Parameters.Add("idEnvolvido", idEnvolvidoF);
                    cmdInsert.Parameters.Add("descricao", contato.getDescricao());
                    cmdInsert.Parameters.Add("tipo", contato.getTipo());

                    cmdInsert.ExecuteNonQuery();
                    Oracon.Close();
                    txtTelefoneFixo.Text = "";
                    lerContatos("Telefone");

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void btnDelTelFixo_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                if (dgvTelefone.SelectedIndex != -1)
                {

                    object item = dgvTelefone.SelectedItem;
                    string idTelefone = (dgvTelefone.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text;

                    OracleConnection Oracon = new OracleConnection(db.oradb);
                    Oracon.Open();

                    OracleCommand cmdDelete = new OracleCommand(SQL_DELETECONTATO + idTelefone, Oracon);

                    cmdDelete.ExecuteNonQuery();
                    Oracon.Close();
                    lerContatos("Telefone");
                }
                else
                {
                    await this.ShowMessageAsync("Aviso", "Selecione um telefone para deletar!");
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        #endregion

        #region Celular

        private async void btnAddCelular_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                if (txtCelular.Text == "(__)_____-____")
                {
                    await this.ShowMessageAsync("Aviso", "Preencha o campo Celular para adicionar!");
                    txtCelular.Focusable = true;
                    return;
                }
                int temunderline = txtCelular.Text.IndexOf("_");
                if (temunderline != -1)
                {
                    await this.ShowMessageAsync("Aviso", "Número de Celular Inválido!");
                    txtCelular.Focusable = true;
                    return;
                }
                else
                {
                    #region TO
                    ContatoEnvF contato = new ContatoEnvF();

                    contato.setDescricao(txtCelular.Text);
                    contato.setTipo("Celular");

                    #endregion

                    OracleConnection Oracon = new OracleConnection(db.oradb);
                    Oracon.Open();
                    OracleCommand cmdInsert = new OracleCommand(SQL_ADDCONTATO, Oracon);
                    cmdInsert.Parameters.Add("idEnvolvido", idEnvolvidoF);
                    cmdInsert.Parameters.Add("descricao", contato.getDescricao());
                    cmdInsert.Parameters.Add("tipo", contato.getTipo());

                    cmdInsert.ExecuteNonQuery();
                    Oracon.Close();
                    txtCelular.Text = "";
                    lerContatos("Celular");

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void btnDelCelular_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                if (dgvCelular.SelectedIndex != -1)
                {

                    object item = dgvCelular.SelectedItem;
                    string idCelular = (dgvCelular.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text;

                    OracleConnection Oracon = new OracleConnection(db.oradb);
                    Oracon.Open();

                    OracleCommand cmdDelete = new OracleCommand(SQL_DELETECONTATO + idCelular, Oracon);

                    cmdDelete.ExecuteNonQuery();
                    Oracon.Close();
                    lerContatos("Celular");
                }
                else
                {
                    await this.ShowMessageAsync("Aviso", "Selecione um celular para deletar!");
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        #endregion

        #region Email

        private async void btnAddEmail_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                if (txtEmail.Text == "")
                {
                    await this.ShowMessageAsync("Aviso", "Preencha o campo Email para adicionar!");
                    txtEmail.Focusable = true;
                    return;
                }
                bool emailValido = validaEmail(txtEmail.Text);
                if (!emailValido)
                {
                    await this.ShowMessageAsync("Aviso", "Email inválido!");
                    txtEmail.Focusable = true;
                    return;
                }
                else
                {
                    #region TO
                    ContatoEnvF contato = new ContatoEnvF();

                    contato.setDescricao(txtEmail.Text);
                    contato.setTipo("Email");

                    #endregion

                    OracleConnection Oracon = new OracleConnection(db.oradb);
                    Oracon.Open();
                    OracleCommand cmdInsert = new OracleCommand(SQL_ADDCONTATO, Oracon);
                    cmdInsert.Parameters.Add("idEnvolvido", idEnvolvidoF);
                    cmdInsert.Parameters.Add("descricao", contato.getDescricao());
                    cmdInsert.Parameters.Add("tipo", contato.getTipo());

                    cmdInsert.ExecuteNonQuery();
                    Oracon.Close();
                    txtEmail.Text = "";
                    lerContatos("Email");

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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

        private async void btnDelEmail_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                if (dgvEmail.SelectedIndex != -1)
                {

                    object item = dgvEmail.SelectedItem;
                    string idEmail = (dgvEmail.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text;

                    OracleConnection Oracon = new OracleConnection(db.oradb);
                    Oracon.Open();

                    OracleCommand cmdDelete = new OracleCommand(SQL_DELETECONTATO + idEmail, Oracon);

                    cmdDelete.ExecuteNonQuery();
                    Oracon.Close();
                    lerContatos("Email");
                }
                else
                {
                    await this.ShowMessageAsync("Aviso", "Selecione um email para deletar!");
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        #endregion

        #region NomeUsuario

        private async void btnAddNomeUsuario_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                if (txtNomeUsuario.Text == "")
                {
                    await this.ShowMessageAsync("Aviso", "Preencha o campo Nome de Usuário para adicionar!");
                    txtNomeUsuario.Focusable = true;
                    return;
                }
                else
                {
                    #region TO
                    ContatoEnvF contato = new ContatoEnvF();

                    contato.setDescricao(txtNomeUsuario.Text);
                    contato.setTipo("Nome Usuario");

                    #endregion

                    OracleConnection Oracon = new OracleConnection(db.oradb);
                    Oracon.Open();
                    OracleCommand cmdInsert = new OracleCommand(SQL_ADDCONTATO, Oracon);
                    cmdInsert.Parameters.Add("idEnvolvido", idEnvolvidoF);
                    cmdInsert.Parameters.Add("descricao", contato.getDescricao());
                    cmdInsert.Parameters.Add("tipo", contato.getTipo());

                    cmdInsert.ExecuteNonQuery();
                    Oracon.Close();
                    txtNomeUsuario.Text = "";
                    lerContatos("Nome Usuario");

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void btnDelNomeUsuario_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                if (dgvNomeUsuario.SelectedIndex != -1)
                {

                    object item = dgvNomeUsuario.SelectedItem;
                    string idNomeUsuario = (dgvNomeUsuario.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text;

                    OracleConnection Oracon = new OracleConnection(db.oradb);
                    Oracon.Open();

                    OracleCommand cmdDelete = new OracleCommand(SQL_DELETECONTATO + idNomeUsuario, Oracon);

                    cmdDelete.ExecuteNonQuery();
                    Oracon.Close();
                    lerContatos("Nome Usuario");
                }
                else
                {
                    await this.ShowMessageAsync("Aviso", "Selecione um nome de usuário para deletar!");
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        #endregion

        #region Endereco

        private async void btnAddEndereco_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtCep.Text == "_____-___" || txtLogradouro.Text == "" || txtNumero.Text == "" || txtCidade.Text == "" || txtBairro.Text == "" || cmbUf.Text == "")
                {
                    await this.ShowMessageAsync("Aviso", "Todos os dados com '*' devem ser preenchidos para adicionar um endereço!");
                    txtCep.Focusable = true;
                    return;
                }

                #region TO
                EnderecoEnvF endereco = new EnderecoEnvF();

                endereco.setCep(txtCep.Text);
                endereco.setLogradouro(txtLogradouro.Text);
                endereco.setNumero(txtNumero.Text);
                endereco.setComplemento(txtComplemento.Text);
                endereco.setBairro(txtBairro.Text);
                endereco.setCidade(txtCidade.Text);
                endereco.setUf(cmbUf.Text);

                #endregion

                OracleConnection Oracon = new OracleConnection(db.oradb);
                Oracon.Open();
                OracleCommand cmdInsert = new OracleCommand(SQL_ADDENDERECO, Oracon);
                cmdInsert.Parameters.Add("idEnvolvido", idEnvolvidoF);
                cmdInsert.Parameters.Add("cep", endereco.getCep());
                cmdInsert.Parameters.Add("logradouro", endereco.getLogradouro());
                cmdInsert.Parameters.Add("numero", endereco.getNumero());
                cmdInsert.Parameters.Add("complemento", endereco.getComplemento());
                cmdInsert.Parameters.Add("bairro", endereco.getBairro());
                cmdInsert.Parameters.Add("cidade", endereco.getCidade());
                cmdInsert.Parameters.Add("uf", endereco.getUf());

                cmdInsert.ExecuteNonQuery();
                Oracon.Close();
                txtCep.Text = "";
                txtLogradouro.Text = "";
                txtNumero.Text = "";
                txtComplemento.Text = "";
                txtBairro.Text = "";
                txtCidade.Text = "";
                cmbUf.Text = "";
                lerEndereco();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void btnDelEndereco_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dgvEndereco.SelectedIndex != -1)
                {

                    object item = dgvEndereco.SelectedItem;
                    string idEndereco = (dgvEndereco.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text;

                    OracleConnection Oracon = new OracleConnection(db.oradb);
                    Oracon.Open();

                    OracleCommand cmdDelete = new OracleCommand(SQL_DELETEENDERECO + idEndereco, Oracon);

                    cmdDelete.ExecuteNonQuery();
                    Oracon.Close();
                    lerEndereco();
                }
                else
                {
                    await this.ShowMessageAsync("Aviso", "Selecione um endereço para deletar!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion

        #endregion

        #region Eventos

        private void txtCep_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.Key == Key.Enter)
            {
                searchByCep objCEP = new searchByCep();
                OracleConnection Oracon = new OracleConnection(db.oradb);

                Oracon.Open();
                OracleDataReader drDados;
                string cep = txtCep.Text.Replace("-", "");
                objCEP.cep = cep;
                drDados = objCEP.ProcurarCEP(Oracon);
                if (drDados.Read())
                {

                    txtLogradouro.Text = Convert.ToString
                        (drDados["descricao"]);

                    cmbUf.Text = Convert.ToString
                        (drDados["UF"]);

                    txtBairro.Text = Convert.ToString
                        (drDados["Bairro"]);

                    txtCidade.Text = Convert.ToString
                        (drDados["Cidade"]);
                }
                Oracon.Close();

            }
        }

        #endregion

        #region ID'S TAB

        private void btnFolderPolD_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog opf = new Microsoft.Win32.OpenFileDialog();
            opf.Title = "Selecione uma foto para o perfil!";
            opf.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            if (opf.ShowDialog() == true)
            {
                imgPolD.Source = new BitmapImage(new Uri(opf.FileName));
                polD = new BitmapImage(new Uri(opf.FileName));
                btnFolderPolD.Visibility = Visibility.Hidden;
                btnFingerPolD.Visibility = Visibility.Hidden;
                btnFecharPolD.Visibility = Visibility.Visible;
                
            }
        }

        private void btnFolderIndD_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog opf = new Microsoft.Win32.OpenFileDialog();
            opf.Title = "Selecione uma foto para o perfil!";
            opf.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            if (opf.ShowDialog() == true)
            {
                imgIndD.Source = new BitmapImage(new Uri(opf.FileName));
                indD = new BitmapImage(new Uri(opf.FileName));
                btnFolderIndD.Visibility = Visibility.Hidden;
                btnFingerIndD.Visibility = Visibility.Hidden;
                btnFecharIndD.Visibility = Visibility.Visible;
            }
        }

        private void btnFolderMedD_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog opf = new Microsoft.Win32.OpenFileDialog();
            opf.Title = "Selecione uma foto para o perfil!";
            opf.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            if (opf.ShowDialog() == true)
            {
                imgMedD.Source = new BitmapImage(new Uri(opf.FileName));
                medD = new BitmapImage(new Uri(opf.FileName));
                btnFolderMedD.Visibility = Visibility.Hidden;
                btnFingerMedD.Visibility = Visibility.Hidden;
                btnFecharMedD.Visibility = Visibility.Visible;
            }
        }

        private void btnFolderAnuD_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog opf = new Microsoft.Win32.OpenFileDialog();
            opf.Title = "Selecione uma foto para o perfil!";
            opf.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            if (opf.ShowDialog() == true)
            {
                imgAnuD.Source = new BitmapImage(new Uri(opf.FileName));
                anuD = new BitmapImage(new Uri(opf.FileName));
                btnFolderAnuD.Visibility = Visibility.Hidden;
                btnFingerAnuD.Visibility = Visibility.Hidden;
                btnFecharAnuD.Visibility = Visibility.Visible;
            }
        }

        private void btnFolderMinD_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog opf = new Microsoft.Win32.OpenFileDialog();
            opf.Title = "Selecione uma foto para o perfil!";
            opf.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            if (opf.ShowDialog() == true)
            {
                imgMinD.Source = new BitmapImage(new Uri(opf.FileName));
                minD = new BitmapImage(new Uri(opf.FileName));
                btnFolderMinD.Visibility = Visibility.Hidden;
                btnFingerMinD.Visibility = Visibility.Hidden;
                btnFecharMinD.Visibility = Visibility.Visible;
            }
        }

        private void btnFolderPolE_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog opf = new Microsoft.Win32.OpenFileDialog();
            opf.Title = "Selecione uma foto para o perfil!";
            opf.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            if (opf.ShowDialog() == true)
            {
                imgPolE.Source = new BitmapImage(new Uri(opf.FileName));
                polE = new BitmapImage(new Uri(opf.FileName));
                btnFolderPolE.Visibility = Visibility.Hidden;
                btnFingerPolE.Visibility = Visibility.Hidden;
                btnFecharPolE.Visibility = Visibility.Visible;
            }
        }

        private void btnFolderIndE_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog opf = new Microsoft.Win32.OpenFileDialog();
            opf.Title = "Selecione uma foto para o perfil!";
            opf.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            if (opf.ShowDialog() == true)
            {
                imgIndE.Source = new BitmapImage(new Uri(opf.FileName));
                indE = new BitmapImage(new Uri(opf.FileName));
                btnFolderIndE.Visibility = Visibility.Hidden;
                btnFingerIndE.Visibility = Visibility.Hidden;
                btnFecharIndE.Visibility = Visibility.Visible;
            }
        }

        private void btnFolderMedE_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog opf = new Microsoft.Win32.OpenFileDialog();
            opf.Title = "Selecione uma foto para o perfil!";
            opf.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            if (opf.ShowDialog() == true)
            {
                imgMedE.Source = new BitmapImage(new Uri(opf.FileName));
                medE = new BitmapImage(new Uri(opf.FileName));
                btnFolderMedE.Visibility = Visibility.Hidden;
                btnFingerMedE.Visibility = Visibility.Hidden;
                btnFecharMedE.Visibility = Visibility.Visible;
            }
        }

        private void btnFolderAnuE_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog opf = new Microsoft.Win32.OpenFileDialog();
            opf.Title = "Selecione uma foto para o perfil!";
            opf.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            if (opf.ShowDialog() == true)
            {
                imgAnuE.Source = new BitmapImage(new Uri(opf.FileName));
                anuE = new BitmapImage(new Uri(opf.FileName));
                btnFolderAnuE.Visibility = Visibility.Hidden;
                btnFingerAnuE.Visibility = Visibility.Hidden;
                btnFecharAnuE.Visibility = Visibility.Visible;
            }
        }

        private void btnFolderMinE_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog opf = new Microsoft.Win32.OpenFileDialog();
            opf.Title = "Selecione uma foto para o perfil!";
            opf.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            if (opf.ShowDialog() == true)
            {
                imgMinE.Source = new BitmapImage(new Uri(opf.FileName));
                minE = new BitmapImage(new Uri(opf.FileName));
                btnFolderMinE.Visibility = Visibility.Hidden;
                btnFingerMinE.Visibility = Visibility.Hidden;
                btnFecharMinE.Visibility = Visibility.Visible;
            }
        }
        #endregion

        private void ckbMembroQuadrilha_Checked(object sender, System.Windows.RoutedEventArgs e)
        {
            // TODO: Add event handler implementation here.
        }

        private void btnFechar_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnFecharPolD_Click(object sender, RoutedEventArgs e)
        {
            btnFolderPolD.Visibility = Visibility.Visible;
            btnFingerPolD.Visibility = Visibility.Visible;
            btnFecharPolD.Visibility = Visibility.Hidden;
            imgPolD.Source = new BitmapImage(new Uri("pack://application:,,,/IMAGES/nothing.png"));
        }

        private void btnFecharIndD_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            btnFolderIndD.Visibility = Visibility.Visible;
            btnFingerIndD.Visibility = Visibility.Visible;
            btnFecharIndD.Visibility = Visibility.Hidden;
            imgIndD.Source = new BitmapImage(new Uri("pack://application:,,,/IMAGES/nothing.png"));
        }

        private void btnFecharMedD_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            btnFolderMedD.Visibility = Visibility.Visible;
            btnFingerMedD.Visibility = Visibility.Visible;
            btnFecharMedD.Visibility = Visibility.Hidden;
            imgMedD.Source = new BitmapImage(new Uri("pack://application:,,,/IMAGES/nothing.png"));
        }

        private void btnFecharAnuD_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            btnFolderAnuD.Visibility = Visibility.Visible;
            btnFingerAnuD.Visibility = Visibility.Visible;
            btnFecharAnuD.Visibility = Visibility.Hidden;
            imgAnuD.Source = new BitmapImage(new Uri("pack://application:,,,/IMAGES/nothing.png"));
        }

        private void btnFecharMinD_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            btnFolderMinD.Visibility = Visibility.Visible;
            btnFingerMinD.Visibility = Visibility.Visible;
            btnFecharMinD.Visibility = Visibility.Hidden;
            imgMinD.Source = new BitmapImage(new Uri("pack://application:,,,/IMAGES/nothing.png"));
        }

        private void btnFecharPolE_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            btnFolderPolE.Visibility = Visibility.Visible;
            btnFingerPolE.Visibility = Visibility.Visible;
            btnFecharPolE.Visibility = Visibility.Hidden;
            imgPolE.Source = new BitmapImage(new Uri("pack://application:,,,/IMAGES/nothing.png"));
        }

        private void btnFecharIndE_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            btnFolderIndE.Visibility = Visibility.Visible;
            btnFingerIndE.Visibility = Visibility.Visible;
            btnFecharIndE.Visibility = Visibility.Hidden;
            imgIndE.Source = new BitmapImage(new Uri("pack://application:,,,/IMAGES/nothing.png"));
        }

        private void btnFecharMedE_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            btnFolderMedE.Visibility = Visibility.Visible;
            btnFingerMedE.Visibility = Visibility.Visible;
            btnFecharMedE.Visibility = Visibility.Hidden;
            imgMedE.Source = new BitmapImage(new Uri("pack://application:,,,/IMAGES/nothing.png"));
        }

        private void btnFecharAnuE_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            btnFolderAnuE.Visibility = Visibility.Visible;
            btnFingerAnuE.Visibility = Visibility.Visible;
            btnFecharAnuE.Visibility = Visibility.Hidden;
            imgAnuE.Source = new BitmapImage(new Uri("pack://application:,,,/IMAGES/nothing.png"));
        }

        private void btnFecharMinE_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            btnFolderMinE.Visibility = Visibility.Visible;
            btnFingerMinE.Visibility = Visibility.Visible;
            btnFecharMinE.Visibility = Visibility.Hidden;
            imgMinE.Source = new BitmapImage(new Uri("pack://application:,,,/IMAGES/nothing.png"));
        }



    }
}
