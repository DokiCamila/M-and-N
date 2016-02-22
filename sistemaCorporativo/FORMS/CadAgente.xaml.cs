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
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using MahApps.Metro.Behaviours;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
using sistemaCorporativo.TO.Agente;
using System.Data;
using System.IO;
using Microsoft.Win32;
using System.Diagnostics;
using System.Windows.Forms;
using sistemaCorporativo.FORMS;
using AC.AvalonControlsLibrary.Controls;
using AC.AvalonControlsLibrary.Core;
using System.Text.RegularExpressions;


namespace sistemaCorporativo.FORMS.cadAgente
{
    /// <summary>
    /// Interaction logic for CadAgente.xaml
    /// </summary>
    public partial class CadAgente : MetroWindow
    {
        public CadAgente()
        {
            InitializeComponent();
        }
        //Criar string com o endereço do banco
        private static string oradb = "Data Source=(DESCRIPTION="
             + "(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521)))"
             + "(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=orcl.itb.com)));"
             + "User Id=matheus_23177;Password=123456;";
  
        //Criar string com o comando para inserir
        private string SQL_INSERT = "insert into Agente(ID_AGENTE,NOME,SEXO,DATA_NASCIMENTO,RG,"
                     +"CPF,TIPO_SANGUINEO,ETNIA,ESTADO_CIVIL,CEP,LOGRADOURO,NUMERO,COMPLEMENTO,BAIRRO,"
                     + "CIDADE,UF,FOTOAGENTE,IMPRESSAOAGENTE,CARGO,STATUS) values (seq_agente.NEXTVAL,:nome,:sexo,:data_nascimento,"
                     +":rg,:cpf,:tipo_sanguineo,:etnia,:estado_civil,:cep,:logradouro,:numero,:complemento,"
                     +":bairro,:cidade,:uf,:fotoagente,:impressaoagente,:cargo,'1')";
        //Criar string com o comando para listar
        private string SQL_SELECT_ALL = "select * from agente where status = 1";
        //Criar Variável para edições(atualizações) e exclusões;
        public string id;
        //Criar string (sem o comando) para deletar
        private string SQL_DELETE;
        private string SQL_UPDATE_LOGIN;
        //Criar string (sem o comando) para atualizar
        private string SQL_UPDATE;
        //Criar obj com variaveis da classe PhotoAndFinger
        //Criar string para a foto e a impressão
        private string destinationPathFoto;
        private string destinationPathFinger;
        //Criar string para name foto e finger
        private string namefoto;
        private string namefinger;
        //Criar string para patch da Foto e da finger 
        private string fingerpath;
        private string imagepath;
        //Criar boolean para checar se foi upado uma finger
        private Boolean checkFinger = false;
        private Boolean checkFoto = false;
        //Criar boolean para checar se o armamento esta presente
        private Boolean Armamento = false;
        //Criar string para verificar se texto;
        private string verificarTexto = "^[ a-zA-Z á]*$";
        //Criar string para verificar se texto;
        private string verificarNum = "^[0-9]";
        //Criar string com o source da foto e digital
        private string fotoAgentesource;
        private string digitalsource;
        //Boolean para checar se a impressao e a foto foi alterada na atualização
        private Boolean alterFinger = false;
        private Boolean alterPhoto = false;
        //Variável id para gerar Login's
        public string idAgente;
 
        private void rdbArmNao_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                txtRegistro.IsEnabled = false;
                cmbTipo.IsEnabled = false;
                txtMarca.IsEnabled = false;
                txtCalibre.IsEnabled = false;
                txtNumSerie.IsEnabled = false;
                txtDataExpedicao.IsEnabled = false;
                Armamento = false;
            }
            catch (Exception ex)
            {

                System.Windows.MessageBox.Show(ex.Message);
            }

        }

        private void rdbArmSim_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                txtRegistro.IsEnabled = true;
                cmbTipo.IsEnabled = true;
                txtMarca.IsEnabled = true;
                txtCalibre.IsEnabled = true;
                txtNumSerie.IsEnabled = true;
                txtDataExpedicao.IsEnabled = true;
                Armamento = true;
            }
            catch (Exception ex)
            {

                System.Windows.MessageBox.Show(ex.Message);
            }
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            OracleConnection Oracon = new OracleConnection(oradb);
            try
            {
                //abrir conexao com o banco de dados
                Oracon.Open();

                OracleCommand createCommand = new OracleCommand(SQL_SELECT_ALL, Oracon);
                createCommand.ExecuteNonQuery();

                OracleDataAdapter adapter = new OracleDataAdapter(createCommand);
                DataTable dt = new DataTable("agente");
                adapter.Fill(dt);
                dgvConteudo.ItemsSource = dt.DefaultView;
                dgvConteudo.Columns[0].Header = "ID";
                dgvConteudo.Columns[1].Header = "Nome Completo";
                dgvConteudo.Columns[2].Header = "Sexo";
                dgvConteudo.Columns[3].Header = "Data de Nascimento";
                dgvConteudo.Columns[4].Header = "RG";
                dgvConteudo.Columns[5].Header = "CPF";
                dgvConteudo.Columns[6].Header = "Tipo Sanguíneo";
                dgvConteudo.Columns[7].Header = "Raça";
                dgvConteudo.Columns[8].Header = "Estado Civil";
                dgvConteudo.Columns[9].Header = "CEP";
                dgvConteudo.Columns[10].Header = "Logradouro";
                dgvConteudo.Columns[11].Header = "Numero";
                dgvConteudo.Columns[12].Header = "Complemento";
                dgvConteudo.Columns[13].Header = "Bairro";
                dgvConteudo.Columns[14].Header = "Cidade";
                dgvConteudo.Columns[15].Header = "UF";
                dgvConteudo.Columns[16].Header = "Foto Agente";
                dgvConteudo.Columns[17].Header = "Impressão Digital";
                dgvConteudo.Columns[18].Header = "Cargo";
                dgvConteudo.Columns[19].Header = "STATUS";


                //Escondendo colunas
                dgvConteudo.Columns[16].Visibility = Visibility.Hidden;
                dgvConteudo.Columns[17].Visibility = Visibility.Hidden;
                dgvConteudo.Columns[19].Visibility = Visibility.Hidden;

                adapter.Update(dt);

                Oracon.Close();

            }
            catch (Exception ex)
            {

                System.Windows.MessageBox.Show(ex.Message);
            }

            //Variaveis de Foto e Digital nao podem ser nulas
            destinationPathFoto = "pack://application:,,,/IMAGES/User_Profile.png";
            destinationPathFinger = "pack://application:,,,/IMAGES/Finger_Print.png";
            // Variaveis para o padrão
            namefoto = "";
            namefinger = "";
            fingerpath = "";
            imagepath = "";
            checkFinger = false;
            checkFoto = false;
            Armamento = false;
            fotoAgentesource = "";
            digitalsource = "";
            alterFinger = false;
            alterPhoto = false;
        }

        private void btnCarregar_Click(object sender, RoutedEventArgs e)
        {
            Storyboard photoAnimation = this.FindResource("OpenFolderAndCam") as Storyboard;
            photoAnimation.Begin();
            cnvPhoto.IsEnabled = true;
            cnvPhoto.Visibility = Visibility.Visible;

        }

        public void btnCarregarDigital_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                
                String caminhoImpressao;
                Microsoft.Win32.OpenFileDialog op = new Microsoft.Win32.OpenFileDialog();
                op.Title = "Selecione uma impressão digital!";
                op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
                  "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
                  "Portable Network Graphic (*.png)|*.png";
                if (op.ShowDialog() == true)
                {
                    //Receber Imagem
                    imgDigital.Source = new BitmapImage(new Uri(op.FileName));

                    //Coletar informações da imagem
                    caminhoImpressao = op.FileName.ToString();
                    fingerpath = caminhoImpressao.ToString();
                    var imageFinger = new System.IO.FileInfo(fingerpath);
                    checkFinger = true;
                    //Houve alteração na digital
                    if (id != null)
                    {
                        alterFinger = true;
                    }

                }


            }
            catch (Exception ex)
            {

                System.Windows.MessageBox.Show(ex.Message);
            }
        }

        private void btnExcluir_Click(object sender, RoutedEventArgs e)
        {
            //Voltar ao padrão da foto de perfil
            imgFoto.Source = new BitmapImage(new Uri("pack://application:,,,/IMAGES/User_Profile.png"));
            destinationPathFoto = "pack://application:,,,/IMAGES/User_Profile.png";
            checkFoto = false;
          
        }

        private void btnExcluirDigital_Click(object sender, RoutedEventArgs e)
        {
            //Voltar ao padrão da digital
            imgDigital.Source = new BitmapImage(new Uri("pack://application:,,,/IMAGES/Finger_Print.png"));
            destinationPathFinger = "pack://application:,,,/IMAGES/Finger_Print.png";
            checkFinger = false;

        }

        private static String GetDestinationPath(string filename, string foldername)
        {
            String appStartPath = System.IO.Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);

            appStartPath = String.Format(appStartPath + "\\{0}\\" + filename, foldername);
            return appStartPath;
        }

        private async void btnCadastrar_Click(object sender, RoutedEventArgs e)
        {
            
            //Checar se todos os campos foram preenchidos
            if (txtNome.Text == "" || txtNascimento.Text =="" || txtRg.Text=="" || txtCpf.Text == "" || cmbTipoSangue.Text=="" || cmbEtnia.Text=="" || cmbEstadoCivil.Text=="" || cmbCargo.Text =="" || txtCep.Text=="" || txtLogradouro.Text=="" || txtNumero.Text=="" || txtBairro.Text=="" || txtCidade.Text=="" || cmbUf.Text=="")
            {
              await this.ShowMessageAsync("Aviso", "Todos os campos são obrigatórios para o cadastramento do agente!");   
            }
            else
            {
                //Checar o formato do nascimento
                int nasc = txtNascimento.Text.IndexOf('_');
                if (nasc != -1)
                {
                    await this.ShowMessageAsync("Aviso", "Formato de data incorreto!");
                    txtNascimento.Focus();
                }
                else
	            {
                    //Checar o formato do RG com indexOf
                    int rg = txtRg.Text.IndexOf('_');
                    if (rg != -1)
                    {
                        await this.ShowMessageAsync("Aviso", "Formato de RG incorreto!");
                        txtRg.Focus();
                    }
                    else
                    {
                        //Checar o formato do CPF com indeOf
                        int cpf = txtCpf.Text.IndexOf('_');
                        if (cpf != -1)
                        {
                            await this.ShowMessageAsync("Aviso", "Formato de CPF incorreto!");
                            txtCpf.Focus();
                        }
                        else
                        {
                            int cep = txtCep.Text.IndexOf('_');
                            if (cep != -1)
                            {
                                await this.ShowMessageAsync("Aviso", "Formato de CEP incorreto!");txtCep.Focus();
                            }
                            else
                            {
                                //Regex CEP
                                string ceptxt = txtCep.Text;
                                if (Regex.IsMatch(ceptxt, verificarNum))
                                {
                                        //Checar Numero
                                        string numeroCasa = txtNumero.Text;
                                        if (Regex.IsMatch(numeroCasa, verificarNum))
                                        {
                                                //Checar Cidade
                                                string cidadetxt = txtCidade.Text;
                                                if (Regex.IsMatch(cidadetxt, verificarTexto))
                                                {
                                                    //Checar se o armamento esta presente
                                                    if (Armamento == true)
                                                    {
                                                        if (txtRegistro.Text == "" || cmbTipo.Text == "" || txtMarca.Text == "" || txtCalibre.Text == "" || txtNumSerie.Text == "" || txtDataExpedicao.Text == "")
                                                        {
                                                            await this.ShowMessageAsync("Aviso", "Preencha todos os campos relacionados ao armamento!");
                                                        }
                                                    }
                                                    else
                                                    {
                                                        //Checar se a impressão foi inserida (OBRIGATORIO)
                                                        if (checkFinger == false)
                                                        {
                                                            await this.ShowMessageAsync("Aviso", "A impressão digital é obrigatória para o cadastro do agente!");
                                                        }
                                                        else
                                                        {
                                                            if (id == null)
                                                            {
                                                                //C-A-D-A-S-T-R-A-R
                                                                //Checar se foi upado uma Foto
                                                                if (checkFoto == true)
                                                                {
                                                                    //--FOTO PERFIL
                                                                    //Criar a pasta para armazenar fotos do perfil
                                                                    //Pegar o folder da aplicação
                                                                    var applicationPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
                                                                    var dir = new System.IO.DirectoryInfo(System.IO.Path.Combine(applicationPath, "ProfilePicture"));
                                                                    if (!dir.Exists)
                                                                        dir.Create();
                                                                    //Copiar para o diretório do sistema
                                                                    namefoto = System.IO.Path.GetFileName(imagepath);
                                                                    destinationPathFoto = GetDestinationPath(namefoto, "ProfilePicture");
                                                                    File.Copy(imagepath, destinationPathFoto, true);

                                                                }

                                                                //--IMPRESSÃODIGITAL
                                                                //Criar a pasta para armazenar a impressão
                                                                //Pegar o folder da aplicação
                                                                var applicationPathF = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
                                                                var dirf = new System.IO.DirectoryInfo(System.IO.Path.Combine(applicationPathF, "FingerPrints"));
                                                                if (!dirf.Exists)
                                                                    dirf.Create();
                                                                //Copiar para o diretório do sistema
                                                                namefinger = System.IO.Path.GetFileName(fingerpath);
                                                                destinationPathFinger = GetDestinationPath(namefinger, "FingerPrints");
                                                                File.Copy(fingerpath, destinationPathFinger, true);

                                                                //Acessar a classe TO 
                                                                Agente objAgente = new Agente();
                                                                objAgente.setNome(txtNome.Text);
                                                                if (rdbMasc.IsChecked == true)
                                                                {
                                                                    objAgente.setSexo("Masculino");
                                                                }
                                                                else
                                                                {
                                                                    objAgente.setSexo("Feminino");
                                                                }

                                                                //Coletar dados digitados
                                                                objAgente.setNascismento(txtNascimento.Text);
                                                                objAgente.setRg(txtRg.Text);
                                                                objAgente.setCpf(txtCpf.Text);
                                                                objAgente.settipoSanguineo(cmbTipoSangue.Text);
                                                                objAgente.setEtnia(cmbEtnia.Text);
                                                                objAgente.setestadoCivil(cmbEstadoCivil.Text);
                                                                objAgente.setCargo(cmbCargo.Text);
                                                                objAgente.setcep(txtCep.Text);
                                                                objAgente.setLogradouro(txtLogradouro.Text);
                                                                objAgente.setNumero(txtNumero.Text);
                                                                objAgente.setComplemento(txtComplemento.Text);
                                                                objAgente.setBairro(txtBairro.Text);
                                                                objAgente.setCidade(txtCidade.Text);
                                                                objAgente.setuf(cmbUf.Text);
                                                                objAgente.setFoto(destinationPathFoto);
                                                                objAgente.setimpressaodigital(destinationPathFinger);

                                                                //Criando Conexão Com o banco de dados
                                                                OracleConnection Oracon = new OracleConnection(oradb);

                                                                //Ações 
                                                                try
                                                                {
                                                                    //Abrir conexão com o banco de dados e inserir dados digitados
                                                                    Oracon.Open();
                                                                    OracleCommand insertCommand = new OracleCommand(SQL_INSERT, Oracon);
                                                                    insertCommand.Parameters.Add("nome", objAgente.getNome());
                                                                    insertCommand.Parameters.Add("sexo", objAgente.getSexo());
                                                                    insertCommand.Parameters.Add("data_nascimento", objAgente.getNascimento());
                                                                    insertCommand.Parameters.Add("rg", objAgente.getRg());
                                                                    insertCommand.Parameters.Add("cpf", objAgente.getCpf());
                                                                    insertCommand.Parameters.Add("tipo_sanguineo", objAgente.gettipoSanguineo());
                                                                    insertCommand.Parameters.Add("etnia", objAgente.getEtnia());
                                                                    insertCommand.Parameters.Add("estado_civil", objAgente.getestadoCivil());
                                                                    insertCommand.Parameters.Add("cep", objAgente.getcep());
                                                                    insertCommand.Parameters.Add("logradouro", objAgente.getLogradouro());
                                                                    insertCommand.Parameters.Add("numero", objAgente.getNumero());
                                                                    insertCommand.Parameters.Add("complemento", objAgente.getComplemento());
                                                                    insertCommand.Parameters.Add("bairro", objAgente.getBairro());
                                                                    insertCommand.Parameters.Add("cidade", objAgente.getCidade());
                                                                    insertCommand.Parameters.Add("uf", objAgente.getuf());
                                                                    insertCommand.Parameters.Add("fotoagente", objAgente.getFoto());
                                                                    insertCommand.Parameters.Add("impressaoagente", objAgente.getimpressaDigital());
                                                                    insertCommand.Parameters.Add("cargo", objAgente.getCargo());
                                                                    //ESSE CAMPO ABAIXO NÃO É OBRIGATÓRIO POIS SERÁ PREENCHIDO AUTOMATICAMENTE
                                                                    //insertCommand.Parameters.Add("status", objAgente.getStatus());
                                                                    insertCommand.ExecuteNonQuery();


                                                                    //Fechar conexão com o banco de dados
                                                                    Oracon.Close();
                                                                    await this.ShowMessageAsync("Aviso", "Agente cadastrado com sucesso!"); this.MetroWindow_Loaded(null, null);
                                                                    this.btnLimpar_Click(null, null);
                                                                    gConsultar.IsSelected = true;

                                                                }
                                                                catch (OracleException orclEx)
                                                                {
                                                                    System.Windows.MessageBox.Show(orclEx.Message);
                                                                }

                                                            }
                                                            else
                                                            {
                                                                //Checar Foto (alteração)
                                                                if (alterPhoto == true)
                                                                {
                                                                    //--FOTO PERFIL
                                                                    //Criar a pasta para armazenar fotos do perfil
                                                                    //Pegar o folder da aplicação
                                                                    var applicationPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
                                                                    var dir = new System.IO.DirectoryInfo(System.IO.Path.Combine(applicationPath, "ProfilePicture"));
                                                                    if (!dir.Exists)
                                                                        dir.Create();
                                                                    //Copiar para o diretório do sistema
                                                                    namefoto = System.IO.Path.GetFileName(imagepath);
                                                                    destinationPathFoto = GetDestinationPath(namefoto, "ProfilePicture");
                                                                    File.Copy(imagepath, destinationPathFoto, true);

                                                                }

                                                                //Checar digital (alteração)
                                                                if (alterFinger == true)
                                                                {
                                                                    //--IMPRESSÃODIGITAL
                                                                    //Criar a pasta para armazenar a impressão
                                                                    //Pegar o folder da aplicação
                                                                    var applicationPathF = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
                                                                    var dirf = new System.IO.DirectoryInfo(System.IO.Path.Combine(applicationPathF, "FingerPrints"));
                                                                    if (!dirf.Exists)
                                                                        dirf.Create();
                                                                    //Copiar para o diretório do sistema
                                                                    namefinger = System.IO.Path.GetFileName(fingerpath);
                                                                    destinationPathFinger = GetDestinationPath(namefinger, "FingerPrints");
                                                                    File.Copy(fingerpath, destinationPathFinger, true);

                                                                }

                                                                //A-T-U-A-L-I-Z-A-R
                                                                //Checar se foi upado uma Foto
                                                                SQL_UPDATE = "update agente set NOME = :nome, SEXO = :sexo, DATA_NASCIMENTO = :data_nascimento, RG = :rg, CPF = :cpf, TIPO_SANGUINEO = :tipo_sanguineo, ETNIA = :etnia, ESTADO_CIVIL = :estado_civil, CEP = :cep, LOGRADOURO = :logradouro, NUMERO = :numero, COMPLEMENTO = :complemento, BAIRRO = :bairro, CIDADE = :cidade, UF = :uf, FOTOAGENTE = :fotoagente, IMPRESSAOAGENTE = :impressaoagente, CARGO = :cargo where id_Agente=" + id;

                                                                //Acessar a classe TO 
                                                                Agente objAgente = new Agente();
                                                                objAgente.setNome(txtNome.Text);
                                                                if (rdbMasc.IsChecked == true)
                                                                {
                                                                    objAgente.setSexo("Masculino");
                                                                }
                                                                else
                                                                {
                                                                    objAgente.setSexo("Feminino");
                                                                }

                                                                //Coletar dados digitados
                                                                objAgente.setNascismento(txtNascimento.Text);
                                                                objAgente.setRg(txtRg.Text);
                                                                objAgente.setCpf(txtCpf.Text);
                                                                objAgente.settipoSanguineo(cmbTipoSangue.Text);
                                                                objAgente.setEtnia(cmbEtnia.Text);
                                                                objAgente.setestadoCivil(cmbEstadoCivil.Text);
                                                                objAgente.setCargo(cmbCargo.Text);
                                                                objAgente.setcep(txtCep.Text);
                                                                objAgente.setLogradouro(txtLogradouro.Text);
                                                                objAgente.setNumero(txtNumero.Text);
                                                                objAgente.setComplemento(txtComplemento.Text);
                                                                objAgente.setBairro(txtBairro.Text);
                                                                objAgente.setCidade(txtCidade.Text);
                                                                objAgente.setuf(cmbUf.Text);
                                                                objAgente.setFoto(destinationPathFoto.ToString());
                                                                objAgente.setimpressaodigital(destinationPathFinger.ToString());

                                                                //Criando Conexão Com o banco de dados
                                                                OracleConnection Oracon = new OracleConnection(oradb);

                                                                //Ações 
                                                                try
                                                                {
                                                                    //Abrir conexão com o banco de dados e inserir dados digitados
                                                                    Oracon.Open();
                                                                    OracleCommand insertCommand = new OracleCommand(SQL_UPDATE, Oracon);
                                                                    insertCommand.Parameters.Add("nome", objAgente.getNome());
                                                                    insertCommand.Parameters.Add("sexo", objAgente.getSexo());
                                                                    insertCommand.Parameters.Add("data_nascimento", objAgente.getNascimento());
                                                                    insertCommand.Parameters.Add("rg", objAgente.getRg());
                                                                    insertCommand.Parameters.Add("cpf", objAgente.getCpf());
                                                                    insertCommand.Parameters.Add("tipo_sanguineo", objAgente.gettipoSanguineo());
                                                                    insertCommand.Parameters.Add("etnia", objAgente.getEtnia());
                                                                    insertCommand.Parameters.Add("estado_civil", objAgente.getestadoCivil());
                                                                    insertCommand.Parameters.Add("cep", objAgente.getcep());
                                                                    insertCommand.Parameters.Add("logradouro", objAgente.getLogradouro());
                                                                    insertCommand.Parameters.Add("numero", objAgente.getNumero());
                                                                    insertCommand.Parameters.Add("complemento", objAgente.getComplemento());
                                                                    insertCommand.Parameters.Add("bairro", objAgente.getBairro());
                                                                    insertCommand.Parameters.Add("cidade", objAgente.getCidade());
                                                                    insertCommand.Parameters.Add("uf", objAgente.getuf());
                                                                    insertCommand.Parameters.Add("fotoagente", objAgente.getFoto());
                                                                    insertCommand.Parameters.Add("impressaoagente", objAgente.getimpressaDigital());
                                                                    insertCommand.Parameters.Add("cargo", objAgente.getCargo());
                                                                    //ESSE CAMPO ABAIXO NÃO É OBRIGATÓRIO POIS SERÁ PREENCHIDO AUTOMATICAMENTE
                                                                    //insertCommand.Parameters.Add("status", objAgente.getStatus());
                                                                    insertCommand.ExecuteNonQuery();

                                                                    //Fechar conexão com o banco de dados
                                                                    Oracon.Close();
                                                                    await this.ShowMessageAsync("Aviso", "Agente atualizado com sucesso!");
                                                                    this.MetroWindow_Loaded(null, null);
                                                                    this.btnLimpar_Click(null, null);
                                                                    gConsultar.IsSelected = true;

                                                                }
                                                                catch (OracleException orclEx)
                                                                {
                                                                    System.Windows.MessageBox.Show(orclEx.Message);
                                                                }
                                                            }

                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    await this.ShowMessageAsync("Aviso", "Cidade incorreta!");
                                                    txtCidade.Focus();
                                                }
                                        }
                                        else
                                        {
                                            await this.ShowMessageAsync("Aviso", "Formato de número incorreto!");
                                            txtNumero.Focus();
                                        }
                                }
                                else
                                {
                                    await this.ShowMessageAsync("Aviso", "Apenas numeros!, CEP!");
                                    txtCep.Focus();
                                }
                            }
                        }
                    }
                    
	            }       
               
            } 
           
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnCancelar1_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnLoadFolder_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                String caminhoFoto;
                Microsoft.Win32.OpenFileDialog opf = new Microsoft.Win32.OpenFileDialog();
                opf.Title = "Selecione uma foto para o perfil!";
                opf.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
                  "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
                  "Portable Network Graphic (*.png)|*.png";
                if (opf.ShowDialog() == true)
                {
                    //Receber Imagem
                    imgFoto.Source = new BitmapImage(new Uri(opf.FileName));

                    //Coletar informações da imagem
                    caminhoFoto = opf.FileName.ToString();
                    imagepath = caminhoFoto.ToString();
                    var imageFoto = new System.IO.FileInfo(imagepath);
                    checkFoto = true;
                    cnvPhoto.IsEnabled = false;
                    cnvPhoto.Visibility = Visibility.Hidden;
                    alterPhoto = true;

                }


            }
            catch (Exception ex)
            {

                System.Windows.MessageBox.Show(ex.Message);
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            cnvPhoto.IsEnabled = false;
            cnvPhoto.Visibility = Visibility.Hidden;
        }

        private void btnLoadCam_Click(object sender, RoutedEventArgs e)
        {
            WebcamWindow webCam = new WebcamWindow();
            cnvPhoto.IsEnabled = false;
            cnvPhoto.Visibility = Visibility.Hidden;
            webCam.ShowDialog();
            
        }

        private void btnLimpar_Click(object sender, RoutedEventArgs e)
        {
            txtNome.Text = "";
            rdbMasc.IsChecked = true;
            txtNascimento.Text = "";
            txtRg.Text = "";
            txtCpf.Text = "";
            cmbTipoSangue.Text = "";
            cmbEtnia.Text = "";
            cmbEstadoCivil.Text = "";
            cmbCargo.Text = "";
            txtCep.Text = "";
            txtLogradouro.Text = "";
            txtNumero.Text = "";
            txtComplemento.Text = "";
            txtBairro.Text = "";
            txtCidade.Text = "";
            cmbUf.Text = "";

            imgFoto.Source = new BitmapImage(new Uri("pack://application:,,,/IMAGES/User_Profile.png"));
            destinationPathFoto = "pack://application:,,,/IMAGES/User_Profile.png";
            checkFoto = false;
            imgDigital.Source = new BitmapImage(new Uri("pack://application:,,,/IMAGES/Finger_Print.png"));
            destinationPathFinger = "pack://application:,,,/IMAGES/Finger_Print.png";
            checkFinger = false;

            txtRegistro.Text = "";
            cmbTipo.Text = "";
            txtMarca.Text = "";
            txtCalibre.Text = "";
            txtNumSerie.Text = "";
            txtDataExpedicao.Text = "";
            id = "";



        }

        private async void btnExcluirAgente_Click(object sender, RoutedEventArgs e)
        {
            if (dgvConteudo.SelectedIndex != -1)
            {
                var mySettings = new MetroDialogSettings()
               {
                AffirmativeButtonText = "Sim",
                NegativeButtonText ="Cancelar",
                ColorScheme = MetroDialogOptions.ColorScheme
                };

                MessageDialogResult result = await this.ShowMessageAsync("Atenção", "Você tem certeza que deseja remover o agente?", MessageDialogStyle.AffirmativeAndNegative, mySettings);

                if (result == MessageDialogResult.Affirmative)
                {
                    OracleConnection Oracon = new OracleConnection(oradb);
                    try
                    {
                        //Abrir a conexão com o banco de dados
                        Oracon.Open();

                        //Obtendo o valor da célula na coluna ID
                        object item = dgvConteudo.SelectedItem;

                        id = (dgvConteudo.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text;
                        id.ToString();

                        //executando comando usando o ID como base para "apagar" um item
                        SQL_DELETE = "update agente set status=0 where ID_AGENTE=" + id;
                        SQL_UPDATE_LOGIN = "update login_Agente set status = 0 where ID_AGENTE ="+id;

                        OracleCommand deleteCommand = new OracleCommand(SQL_DELETE, Oracon);
                        OracleCommand updateLogin = new OracleCommand(SQL_UPDATE_LOGIN, Oracon);
                        deleteCommand.ExecuteNonQuery();
                        updateLogin.ExecuteNonQuery();

                        //Fechar conexão com o banco de dados
                        Oracon.Close();

                        System.Windows.Forms.MessageBox.Show("Agente deletado com sucesso!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                        this.MetroWindow_Loaded(null, null);

                    }
                    catch (Exception ex)
                    {

                        System.Windows.Forms.MessageBox.Show(ex.Message);
                    }
                }

            }
            else
            {
               await this.ShowMessageAsync("Aviso", "Selecione um agente para excluir!");
            }
        }

        private async void btnEditar_Click(object sender, RoutedEventArgs e)
        {
            if (dgvConteudo.SelectedIndex != -1)
            {
                try
                {
                    
                    //Padrão do checkPhoto
                    checkFoto = false;

                    //Coletando valores para atualização

                    object itemid = dgvConteudo.SelectedItem;
                    id = (dgvConteudo.SelectedCells[0].Column.GetCellContent(itemid) as TextBlock).Text;
                    id.ToString();

                    object item1 = dgvConteudo.SelectedItem;
                    string value1 = (dgvConteudo.SelectedCells[1].Column.GetCellContent(item1) as TextBlock).Text;
                    value1.ToString();
                    txtNome.Text = value1;

                    object item2 = dgvConteudo.SelectedItem;
                    string value2 = (dgvConteudo.SelectedCells[2].Column.GetCellContent(item2) as TextBlock).Text;
                    value2.ToString();
                    if (value2 == "Masculino")
                    {
                        rdbMasc.IsChecked = true;
                    }
                    else
                    {
                        rdbFem.IsChecked = true;
                    }

                    object item3 = dgvConteudo.SelectedItem;
                    string value3 = (dgvConteudo.SelectedCells[3].Column.GetCellContent(item3) as TextBlock).Text;
                    value3.ToString();
                    txtNascimento.Text = value3;

                    object item4 = dgvConteudo.SelectedItem;
                    string value4 = (dgvConteudo.SelectedCells[4].Column.GetCellContent(item4) as TextBlock).Text;
                    value4.ToString();
                    txtRg.Text = value4;

                    object item5 = dgvConteudo.SelectedItem;
                    string value5 = (dgvConteudo.SelectedCells[5].Column.GetCellContent(item5) as TextBlock).Text;
                    value5.ToString();
                    txtCpf.Text = value5;

                    object item6 = dgvConteudo.SelectedItem;
                    string value6 = (dgvConteudo.SelectedCells[6].Column.GetCellContent(item6) as TextBlock).Text;
                    value6.ToString();
                    cmbTipoSangue.Text = value6;

                    object item7 = dgvConteudo.SelectedItem;
                    string value7 = (dgvConteudo.SelectedCells[7].Column.GetCellContent(item7) as TextBlock).Text;
                    value7.ToString();
                    cmbEtnia.Text = value7;

                    object item8 = dgvConteudo.SelectedItem;
                    string value8 = (dgvConteudo.SelectedCells[8].Column.GetCellContent(item8) as TextBlock).Text;
                    value8.ToString();
                    cmbEstadoCivil.Text = value8;

                    object item9 = dgvConteudo.SelectedItem;
                    string value9 = (dgvConteudo.SelectedCells[9].Column.GetCellContent(item9) as TextBlock).Text;
                    value9.ToString();
                    txtCep.Text = value9;

                    object item10 = dgvConteudo.SelectedItem;
                    string value10 = (dgvConteudo.SelectedCells[10].Column.GetCellContent(item10) as TextBlock).Text;
                    value10.ToString();
                    txtLogradouro.Text = value10;

                    object item11 = dgvConteudo.SelectedItem;
                    string value11 = (dgvConteudo.SelectedCells[11].Column.GetCellContent(item11) as TextBlock).Text;
                    value11.ToString();
                    txtNumero.Text = value11;

                    object item12 = dgvConteudo.SelectedItem;
                    string value12 = (dgvConteudo.SelectedCells[12].Column.GetCellContent(item12) as TextBlock).Text;
                    value12.ToString();
                    txtComplemento.Text = value12;

                    object item13 = dgvConteudo.SelectedItem;
                    string value13 = (dgvConteudo.SelectedCells[13].Column.GetCellContent(item13) as TextBlock).Text;
                    value13.ToString();
                    txtBairro.Text = value13;

                    object item14 = dgvConteudo.SelectedItem;
                    string value14 = (dgvConteudo.SelectedCells[14].Column.GetCellContent(item14) as TextBlock).Text;
                    value14.ToString();
                    txtCidade.Text = value14;

                    object item15 = dgvConteudo.SelectedItem;
                    string value15 = (dgvConteudo.SelectedCells[15].Column.GetCellContent(item15) as TextBlock).Text;
                    value15.ToString();
                    cmbUf.Text = value15;

                    string SQL_SELECT_THIS = "select * from agente where id_Agente='"+id+"'";
                    OracleConnection Oracon = new OracleConnection(oradb);
                    Oracon.Open();
                    OracleCommand selectall = new OracleCommand(SQL_SELECT_THIS, Oracon);
                    OracleDataReader read = selectall.ExecuteReader();
                    read.Read();
                    fotoAgentesource = Convert.ToString(read[16].ToString());
                    digitalsource = Convert.ToString(read[17].ToString());
                    Oracon.Close();

                    ImageSource photoProfile = new BitmapImage(new Uri(fotoAgentesource));
                    imgFoto.Source = photoProfile;
                    destinationPathFoto = photoProfile.ToString();

                    ImageSource fingerPrint = new BitmapImage(new Uri(digitalsource));
                    imgDigital.Source = fingerPrint;
                    destinationPathFinger = fingerPrint.ToString();
                    checkFinger = true;

                    object item18 = dgvConteudo.SelectedItem;
                    string value18 = (dgvConteudo.SelectedCells[18].Column.GetCellContent(item18) as TextBlock).Text;
                    value18.ToString();
                    cmbCargo.Text = value18;

                    gConsultar.IsSelected = false;
                    gCadastrar.IsSelected = true;

                }
                catch (Exception ex)
                {

                    System.Windows.Forms.MessageBox.Show(ex.Message);
                } 
            }
            else
            {
                await this.ShowMessageAsync("Aviso", "Selecione um agente para excluir!");
            }
        }

        private void MetroWindow_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            //Cadastrar por tecla
            if (e.Key == Key.Enter)
            {
                btnCadastrar_Click(sender, e);
            }
            //Apagar por tecla
            if (e.Key == Key.Delete)
            {
                btnExcluirAgente_Click(sender, e);
            }
        }

        private async void btnGerarLogin_Click(object sender, RoutedEventArgs e)
        {
            if (dgvConteudo.SelectedIndex != -1)
            {
                object linha = dgvConteudo.SelectedItem;
                idAgente = (dgvConteudo.SelectedCells[0].Column.GetCellContent(linha) as TextBlock).Text;
                gerarLoginWindow gerarLogin = new gerarLoginWindow(idAgente);
                gerarLogin.ShowDialog();
            }
            else
            {
                await this.ShowMessageAsync("Aviso", "Selecione um agente para gerar login!"); }
            
        }

        private void txtRg_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            KeyConverter kv = new KeyConverter();

            if (e.Key == Key.Tab) return;
            if ((char.IsNumber((string)kv.ConvertTo(e.Key, typeof(string)), 0) == false))
            {
                e.Handled = true;
            }
        }


    }

}  

 