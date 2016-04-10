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
using sistemaCorporativo.UTIL.databaseAdress;
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
using System.Drawing;
using sistemaCorporativo.UTIL.Adorners;


namespace sistemaCorporativo.FORMS.cadAgente
{
    /// <summary>
    /// Interaction logic for CadAgente.xaml
    /// </summary>
    public partial class CadAgente : MetroWindow
    {
        //Ferramenta para cropping
        CroppingAdorner _clp;
        FrameworkElement _felCur = null;
        System.Windows.Media.Brush _brOriginal;

        //Bitmap para Foto Perfil (LoadByFolder)
        BitmapImage FotoPerfil;



        public CadAgente()
        {
            InitializeComponent();
        }
       
  
        //Criar string com o comando para inserir
        private string SQL_INSERT = "insert into Agente(ID_AGENTE,NOME,SEXO,DATA_NASCIMENTO,RG,"
                     +"CPF,TIPO_SANGUINEO,ETNIA,ESTADO_CIVIL,CEP,LOGRADOURO,NUMERO,COMPLEMENTO,BAIRRO,"
                     + "CIDADE,UF,FOTOAGENTE,IMPRESSAOAGENTE, ID_CARGO,  STATUS, ADMINISTRADOR) values (seq_agente.NEXTVAL,:nome,:sexo,:data_nascimento,"
                     +":rg,:cpf,:tipo_sanguineo,:etnia,:estado_civil,:cep,:logradouro,:numero,:complemento,"
                     +":bairro,:cidade,:uf,:fotoagente,:impressaoagente, :cargo, '1','0')";
        //Criar string com o comando para listar
        private string SQL_SELECT_ALL = "select id_Agente, nome, sexo, data_Nascimento,rg, cpf,tipo_Sanguineo, etnia, estado_Civil, cep, logradouro, numero, complemento, bairro, cidade, uf, fotoAgente from agente where status = 1 and administrador = 0";
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
        public Boolean FingerInserido = false;
        public Boolean fotoInserida = false;
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
        public Boolean alterFinger = false;
        public Boolean alterPhoto = false;
        //Variável id para gerar Login's
        public string idAgente;
        //Cropped Bitmap para a foto do Agente
        CroppedBitmap fotoPerfil = new CroppedBitmap(); 
        //endereço banco
        databaseAddress db = new databaseAddress();


 
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
            
            OracleConnection Oracon = new OracleConnection(db.oradb);
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
                



                //Escondendo colunas
                //dgvConteudo.Columns[16].Visibility = Visibility.Hidden;
                
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
            FingerInserido = false;
            fotoInserida = false;
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
				
                CadFingerPrints cadFinger = new CadFingerPrints();
				cadFinger.Show();
                
                
                //alterFinger = true;

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
            fotoInserida = false;
           
          
        }

        private void btnExcluirDigital_Click(object sender, RoutedEventArgs e)
        {
            //Voltar ao padrão da digital
            imgDigital.Source = new BitmapImage(new Uri("pack://application:,,,/IMAGES/Finger_Print.png"));
            destinationPathFinger = "pack://application:,,,/IMAGES/Finger_Print.png";
            FingerInserido = false;

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
                                                        if (FingerInserido == false)
                                                        {
                                                            await this.ShowMessageAsync("Aviso", "A impressão digital é obrigatória para o cadastro do agente!");
                                                        }
                                                        else
                                                        {
                                                            if (id == null)
                                                            {
                                                                //C-A-D-A-S-T-R-A-R
                                                                //--FotoPerfil
                                                                //Checar se foi upado uma Foto
                                                                if (fotoInserida == true)
                                                                {
                                                                    fotoPerfil = imgFoto.Source as CroppedBitmap;
                                                                    //Criar Diretório onde serão armazenadas as fotos de perfis
                                                                    string subPath = "ImagesData";
                                                                    string path = System.IO.Path.Combine(subPath, "ProfilePictures");
                                                                    bool exists = System.IO.Directory.Exists(path);

                                                                    if (!exists)
                                                                        System.IO.Directory.CreateDirectory(path);

                                                                    //Nome
                                                                    string fileName = (id.ToString()+".png");
                                                                    //Combinar
                                                                    path = System.IO.Path.Combine(path, fileName);

                                                                    using (System.IO.FileStream stream = System.IO.File.Create(path))
                                                                    {
                                                                        //Guardando o valor do image control no Bitmap FotoPerfil        
                                                                        PngBitmapEncoder encoder = new PngBitmapEncoder();
                                                                        encoder.Frames.Add(BitmapFrame.Create(fotoPerfil));
                                                                        encoder.Save(stream);
                                                                    }
                                                                    //Pegar o path Bin
                                                                    string pathToDataBase;
                                                                    pathToDataBase = System.IO.Path.GetDirectoryName(
                                                                    System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);

                                                                    //GO TO THE DATABASE
                                                                    destinationPathFoto = (pathToDataBase + @"\" + path);
                                                                  
                                                                }

                                                                //--IMPRESSÃODIGITAL
                                                                //Criar a pasta para armazenar a impressão
                                                                //Pegar o folder da aplicação
                                                                ///Obs O IF DO FINGERPRINT INSERIDO Ñ ESTÁ AQUI POR QUE ELE JA PASSOU NO PENULTIMO IF
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
                                                                OracleConnection Oracon = new OracleConnection(db.oradb);

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

                                                                    string cargo = cmbCargo.Text;
                                                                    switch (cargo)
                                                                    {
                                                                        case "Policial":
                                                                            insertCommand.Parameters.Add("cargo", "2");
                                                                            break;
                                                                        case "Técnico Criminal":
                                                                            insertCommand.Parameters.Add("cargo", "3");
                                                                            break;
                                                                        case "Cientista Forense":
                                                                            insertCommand.Parameters.Add("cargo", "4");
                                                                            break;
                                                                        case "Biologo Forense":
                                                                            insertCommand.Parameters.Add("cargo", "5");
                                                                            break;
                                                                        default:
                                                                            await this.ShowMessageAsync("Aviso", "Cargo Inválido!");   
                                                                            break;
                                                                    }
                                                                    
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
                                                                //A-T-U-A-L-I-Z-A-R
                                                                //--FotoPerfil
                                                                //Checar Foto (alteração)
                                                                if (alterPhoto == true)
                                                                {
                                                            
                                                                    fotoPerfil = imgFoto.Source as CroppedBitmap;
                                                                    //Criar Diretório onde serão armazenadas as fotos de perfis
                                                                    string subPath = "ImagesData";
                                                                    string path = System.IO.Path.Combine(subPath, "ProfilePictures");
                                                                    bool exists = System.IO.Directory.Exists(path);

                                                                    if (!exists)
                                                                        System.IO.Directory.CreateDirectory(path);

                                                                    //Nome
                                                                    string fileName = (id.ToString()+".png");
                                                                    //Combinar
                                                                    path = System.IO.Path.Combine(path, fileName);

                                                                    using (System.IO.FileStream stream = System.IO.File.Create(path))
                                                                    {
                                                                        //Guardando o valor do image control no Bitmap FotoPerfil        
                                                                        PngBitmapEncoder encoder = new PngBitmapEncoder();
                                                                        encoder.Frames.Add(BitmapFrame.Create(fotoPerfil));
                                                                        encoder.Save(stream);
                                                                    }
                                                                    
                                                                    //Pegar o path Bin
                                                                    string pathToDataBase;
                                                                    pathToDataBase = System.IO.Path.GetDirectoryName(
                                                                    System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
       
                                                                    //GO TO THE DATABASE
                                                                    destinationPathFoto = (pathToDataBase+@"\"+path);
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

                                                                
                                                                //Checar se foi upado uma Foto
                                                                SQL_UPDATE = "update agente set NOME = :nome, SEXO = :sexo, DATA_NASCIMENTO = :data_nascimento, RG = :rg, CPF = :cpf, TIPO_SANGUINEO = :tipo_sanguineo, ETNIA = :etnia, ESTADO_CIVIL = :estado_civil, CEP = :cep, LOGRADOURO = :logradouro, NUMERO = :numero, COMPLEMENTO = :complemento, BAIRRO = :bairro, CIDADE = :cidade, UF = :uf, FOTOAGENTE = :fotoagente, IMPRESSAOAGENTE = :impressaoagente, ID_CARGO = :cargo where id_Agente=" + id;

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
                                                                OracleConnection Oracon = new OracleConnection(db.oradb);

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

                                                                    string cargo = cmbCargo.Text;
                                                                    switch (cargo)
                                                                    {
                                                                        case "Policial":
                                                                            insertCommand.Parameters.Add("cargo", "2");
                                                                            break;
                                                                        case "Técnico Criminal":
                                                                            insertCommand.Parameters.Add("cargo", "3");
                                                                            break;
                                                                        case "Cientista Forense":
                                                                            insertCommand.Parameters.Add("cargo", "4");
                                                                            break;
                                                                        case "Biologo Forense":
                                                                            insertCommand.Parameters.Add("cargo", "5");
                                                                            break;
                                                                        default:
                                                                            await this.ShowMessageAsync("Aviso", "Cargo Inválido!");
                                                                            break;
                                                                    }
                                                                    
                                                                    

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
               
                Microsoft.Win32.OpenFileDialog opf = new Microsoft.Win32.OpenFileDialog();
                opf.Title = "Selecione uma foto para o perfil!";
                opf.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
                  "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
                  "Portable Network Graphic (*.png)|*.png";
                if (opf.ShowDialog() == true)
                {
                    cnvPhoto.IsEnabled = false;
                    cnvPhoto.Visibility = Visibility.Hidden;

                    FotoPerfil = new BitmapImage(new Uri(opf.FileName));
                    fotoInserida = true;

                    //Abrir Janela Foto Perfil
                    CortarImagem newWindowCrop = new CortarImagem(this, FotoPerfil);
                    newWindowCrop.ShowDialog();
                    


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
            WebcamWindow webCam = new WebcamWindow(this);
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
            fotoInserida = false;
            imgDigital.Source = new BitmapImage(new Uri("pack://application:,,,/IMAGES/Finger_Print.png"));
            destinationPathFinger = "pack://application:,,,/IMAGES/Finger_Print.png";
            FingerInserido = false;

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
                    OracleConnection Oracon = new OracleConnection(db.oradb);
                    try
                    {
                        //Abrir a conexão com o banco de dados
                        Oracon.Open();

                        //Obtendo o valor da célula na coluna ID
                        object item = dgvConteudo.SelectedItem;

                        id = (dgvConteudo.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text;
                        id.ToString();

                        //executando comando usando o ID como base para "apagar" um item
                        SQL_DELETE = "update agente set status = 0 where ID_AGENTE=" + id;
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
                    fotoInserida = false;
                    alterPhoto = false;

                    //Coletando valores para atualização

                    object itemid = dgvConteudo.SelectedItem;
                    id = (dgvConteudo.SelectedCells[0].Column.GetCellContent(itemid) as TextBlock).Text;
                    id.ToString();

                    string SQL_SELECT_THIS = "select * from agente where id_Agente='"+id+"'";
                    OracleConnection Oracon = new OracleConnection(db.oradb);
                    Oracon.Open();
                    OracleCommand selectall = new OracleCommand(SQL_SELECT_THIS, Oracon);
                    OracleDataReader read = selectall.ExecuteReader();
                    read.Read();
                    txtNome.Text = Convert.ToString(read[1].ToString());

                    string sexo = Convert.ToString(read[2].ToString());
                    if (sexo == "Masculino")
                    {
                        rdbMasc.IsChecked = true;
                    }
                    else
                    {
                        rdbFem.IsChecked = true;
                    }

                    txtNascimento.Text = Convert.ToString(read[3].ToString());
                    txtRg.Text = Convert.ToString(read[4].ToString());
                    txtCpf.Text = Convert.ToString(read[5].ToString());
                    cmbTipoSangue.Text = Convert.ToString(read[6].ToString());
                    cmbEtnia.Text = Convert.ToString(read[7].ToString());
                    cmbEstadoCivil.Text = Convert.ToString(read[8].ToString());

                    string cargo = Convert.ToString(read[17].ToString());
                    switch (cargo)
                    {
                        case "2" :
                            cmbCargo.Text = "Policial";
                            break;
                        case "3":
                            cmbCargo.Text = "Técnico Criminal";
                            break;
                        case "4":
                            cmbCargo.Text = "Cientista Forense";
                            break;
                        case "5":
                            cmbCargo.Text = "Biologo Forense";
                            break;
                        default:
                             await this.ShowMessageAsync("Aviso", "Cargo Inválido!");
                            break;
                    }

                    txtCep.Text = Convert.ToString(read[9].ToString());
                    txtLogradouro.Text = Convert.ToString(read[10].ToString());
                    txtNumero.Text = Convert.ToString(read[11].ToString());
                    txtComplemento.Text = Convert.ToString(read[12].ToString());
                    txtBairro.Text = Convert.ToString(read[13].ToString());
                    txtCidade.Text = Convert.ToString(read[14].ToString());
                    cmbUf.Text = Convert.ToString(read[15].ToString());

                    fotoAgentesource = Convert.ToString(read[16].ToString());
                   
                    Oracon.Close();

                    if (fotoAgentesource != "")
                    {
                        ImageSource photoProfile = new BitmapImage(new Uri(fotoAgentesource));
                        imgFoto.Source = photoProfile;
                        destinationPathFoto = photoProfile.ToString();
                    }

                    if (digitalsource != "")
                    {
                        
                    }

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

 