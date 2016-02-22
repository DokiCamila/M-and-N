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
using sistemaCorporativo.TO.Viatura;
using System.Windows.Forms;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
using System.Data;



namespace sistemaCorporativo.FORMS
{
    /// <summary>
    /// Interaction logic for CadViatura.xaml
    /// </summary>
    public partial class CadViatura : MetroWindow
    {
        public CadViatura()
        {
            InitializeComponent();
           
        }
       
        //Criar Variável para edições(atualizações) e exclusões;
        public string id;
        //Criar string com o endereço do banco
        private string oradb = "Data Source=(DESCRIPTION="
               + "(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521)))"
               + "(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=orcl.itb.com)));"
               + "User Id=matheus_23177;Password=123456;";
        //Criar string com o comando para listar
        private string SQL_SELECT_ALL = "select * from viatura where status = 1";
        //Criar string com o comando para inserir
        private string SQL_INSERT = "insert into viatura(ID_VIATURA, FABRICANTE_MODELO, PLACA, CHASSI, STATUS) values (seq_viatura.NEXTVAL, :fabricantemodelo, :placa, :chassi, '1')";
        //Criar string (sem o comando) para deletar
        private string SQL_DELETE;
        //Criar string (sem o comando) para atualizar
        private string SQL_UPDATE;

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

                DataTable dt = new DataTable("viatura");
                adapter.Fill(dt);
                dgvConteudo.ItemsSource = dt.DefaultView;
                dgvConteudo.Columns[0].Header = "ID";
                dgvConteudo.Columns[1].Header = "Fabricante Modelo";
                dgvConteudo.Columns[2].Header = "Placa";
                dgvConteudo.Columns[3].Header = "Chassi";
                dgvConteudo.Columns[4].Header = "Status";

                //Ocultar colunas
                dgvConteudo.Columns[4].Visibility = Visibility.Hidden;

                adapter.Update(dt);

                //Fechar conexao com o banco de dados
                Oracon.Close();

            }
            catch (Exception ex)
            {

                System.Windows.MessageBox.Show(ex.Message);
            }   
        }

        private async void btnCadastrar_Click(object sender, RoutedEventArgs e)

        {

            //Checar Strings
            if (txtFabricante.Text == "" || txtPlaca.Text == "" || txtChassi.Text == "")
            {
                await this.ShowMessageAsync("Atenção", "Preencha todos os campos!");
            }
            else
            {
                string placa = txtPlaca.Text;
                string chassi = txtChassi.Text;

                //Checar quantidade de caracteres
                if (placa.Length == 8)
                {
                    //Checar quantidade de caracteres
                    if (chassi.Length == 17)
                    {
                        if (id == null)
                        {
                            //Transformar strings em maiuscula
                            placa = txtPlaca.Text.ToUpper();
                            txtPlaca.Text = placa;
                            chassi = txtChassi.Text.ToUpper();
                            txtChassi.Text = chassi;

                            
                            

                            Viatura objViatura = new Viatura();
                            objViatura.setFabricanteModelo(txtFabricante.Text);
                            objViatura.setPlaca(txtPlaca.Text);
                            objViatura.setChassi(txtChassi.Text);

                            OracleConnection Oracon = new OracleConnection(oradb);

                            //Ações 
                            try
                            {
                                //Abrir conexão com o banco de dados
                                Oracon.Open();

                                OracleCommand insertCommand = new OracleCommand(SQL_INSERT, Oracon);
                                insertCommand.Parameters.Add("fabricantemodelo", objViatura.getFabricanteModelo());
                                insertCommand.Parameters.Add("placa", objViatura.getPlaca());
                                insertCommand.Parameters.Add("chassi", objViatura.getChassi());
                                insertCommand.ExecuteNonQuery();

                                //Fechar conexão com o banco de dados
                                Oracon.Close();

                                await this.ShowMessageAsync("Aviso", "Viatura cadastrada com sucesso!");
                                this.MetroWindow_Loaded(null, null);
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
                            

                            Viatura objViatura = new Viatura();
                            objViatura.setFabricanteModelo(txtFabricante.Text);
                            objViatura.setPlaca(txtPlaca.Text);
                            objViatura.setChassi(txtChassi.Text);

                            OracleConnection Oracon = new OracleConnection(oradb);

                            //Ações 
                            try
                            {

                                //Criar string com o comando para atualizar
                                SQL_UPDATE = "update viatura set FABRICANTE_MODELO = :fabricantemodelo, PLACA = :placa, CHASSI = :chassi where id_Viatura ="+id;
                                //Excutar comandos SQL

                                //Abrir conexão com o banco de dados
                                Oracon.Open();

                                OracleCommand insertCommand = new OracleCommand(SQL_UPDATE, Oracon);
                                insertCommand.Parameters.Add("fabricantemodelo", objViatura.getFabricanteModelo());
                                insertCommand.Parameters.Add("placa", objViatura.getPlaca());
                                insertCommand.Parameters.Add("chassi", objViatura.getChassi());
                                insertCommand.ExecuteNonQuery();

                                //Fechar conexão com o banco de dados
                                Oracon.Close();

                                await this.ShowMessageAsync("Aviso", "Viatura atualizada com sucesso!"); 
                                this.MetroWindow_Loaded(null, null);
                                this.btnLimpar_Click(null, null);
                                gConsultar.IsSelected = true;

                            }
                            catch (Exception ex)
                            {

                                System.Windows.MessageBox.Show(ex.Message);
                            }
                        }
                        
                    }

                        else
                        {
                            await this.ShowMessageAsync("Aviso", "O chassi deve conter 17 caracteres!"); txtChassi.Focusable = true;
                            Keyboard.Focus(txtChassi);


                        }
                    }
                    else
                    {
                        await this.ShowMessageAsync("Aviso", "A placa deve conter 7 caracteres!"); 
                    txtPlaca.Focusable = true;
                        Keyboard.Focus(txtPlaca);
                    }

                }

            }
        
        private async void btnExcluir_Click(object sender, RoutedEventArgs e)
        {

            if (dgvConteudo.SelectedIndex != -1)
            {
                 var mySettings = new MetroDialogSettings()
               {
                AffirmativeButtonText = "Sim",
                NegativeButtonText ="Cancelar",
                ColorScheme = MetroDialogOptions.ColorScheme
                };

                MessageDialogResult result = await this.ShowMessageAsync("Atenção", "Você tem certeza que deseja remover a viatura?", MessageDialogStyle.AffirmativeAndNegative, mySettings);

                if (result == MessageDialogResult.Affirmative)
                {
                    OracleConnection Oracon = new OracleConnection(oradb);
                    try
                    {

                        //Abrir conexão com o banco de dados
                        Oracon.Open();


                        //obtendo valor da célula na coluna ID
                        object item = dgvConteudo.SelectedItem;
                        id = (dgvConteudo.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text;
                        id.ToString();

                        //executando comando usando o ID como base para "apagar" um item
                        SQL_DELETE = "update viatura set status=0 where ID_Viatura=" + id;
                        OracleCommand deleteCommand = new OracleCommand(SQL_DELETE, Oracon);
                        deleteCommand.ExecuteNonQuery();

                        //Fechar conexão com o banco de dados
                        Oracon.Close();

                        System.Windows.Forms.MessageBox.Show("Viatura deletada com sucesso!", "Aviso");
                        this.MetroWindow_Loaded(null, null);

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
            
        }

        private void btnLimpar_Click(object sender, RoutedEventArgs e)
        {
			
            txtFabricante.Text = "";
            txtPlaca.Text = "";
            txtChassi.Text = "";
            id = null;
			
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private async void btnEditar_Click(object sender, RoutedEventArgs e)
        {
            if (dgvConteudo.SelectedIndex != -1)
            {

                try
                {
                    //Coletando valores para atualização

                    object itemid = dgvConteudo.SelectedItem;
                    id = (dgvConteudo.SelectedCells[0].Column.GetCellContent(itemid) as TextBlock).Text;
                    id.ToString();

                    object item1 = dgvConteudo.SelectedItem;
                    string value1 = (dgvConteudo.SelectedCells[1].Column.GetCellContent(item1) as TextBlock).Text;
                    value1.ToString();
                    txtFabricante.Text = value1;

                    object item2 = dgvConteudo.SelectedItem;
                    string value2 = (dgvConteudo.SelectedCells[2].Column.GetCellContent(item2) as TextBlock).Text;
                    value2.ToString();
                    txtPlaca.Text = value2;

                    object item3 = dgvConteudo.SelectedItem;
                    string value3 = (dgvConteudo.SelectedCells[3].Column.GetCellContent(item3) as TextBlock).Text;
                    value1.ToString();
                    txtChassi.Text = value3;


                    gConsultar.IsSelected = false;
                    gCadastrar.IsSelected = true;
                }
                catch (Exception ex)
                {

                    System.Windows.MessageBox.Show(ex.Message);
                }
            }
            else
            {
                await this.ShowMessageAsync("Aviso", "Selecione uma viatura para editar!");
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
                btnExcluir_Click(sender, e);
            }
        }

        private void btnCancelar1_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.Close();
        }


    }
}
