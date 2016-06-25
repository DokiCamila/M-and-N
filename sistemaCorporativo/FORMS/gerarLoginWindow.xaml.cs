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
using MahApps.Metro.Actions;
using MahApps.Metro.Behaviours;
using sistemaCorporativo.FORMS.cadAgente;
using sistemaCorporativo.UTIL.checkPassword;
using sistemaCorporativo.UTIL.databaseAdress;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
using System.Data;

namespace sistemaCorporativo.FORMS
{
   
    /// <summary>
    /// Interaction logic for gerarLoginWindow.xaml
    /// </summary>
    public partial class gerarLoginWindow : MetroWindow
    {
        //Variável que vai receber o id pelo método construtor
        string id;
        public gerarLoginWindow(string idAgente)
        {
            InitializeComponent();
            id = idAgente;
        }

        //Variáveis de checagem (password)
        checkPassword verifica = new checkPassword();
        int placar;
        checkPassword.ForcaDaSenha strong;

        ///<Variável para selecionar o ultimo registro da tabela>
        //private string SELECT_LAST = "select id_Agente, nome from AGENTE where rownum =1 and status = 1 order by id_Agente desc";
        //Variável para Ler a linha relacionada ao id
        private string SQL_READ_AGENTE = "select * from AGENTE where id_Agente = :id";
        //Variável para criar o login
        private string SQL_INSERT_LOGIN = "insert into login_agente (id_LoginAgente, id_Agente, nome_User, senha_User, nivel_Acesso, status) values (seq_LoginAgente.NEXTVAL, :idAgente, :nomeUser, :senhaUser, :nivelAcesso, 1)";
         //Variável para criar o perfil
        private string SQL_INSERT_PROFILE = "insert into perfil_agente (id_PerfilAgente, id_Agente, nivel_Agente, xp, casos_Resolvidos, status) values(seq_PerfilAgente.NEXTVAL, :idAgente, 1, 0, 0, 1)";
        //String para checar se o login ja existe
        private string SQL_CHECK_LOGIN = "SELECT count(l.ID_AGENTE) as total FROM LOGIN_AGENTE l where l.ID_AGENTE = :id and status = 1";
        //Variável que receberá o nome do agente 
        private string nomeAgente;
        //Variável que receberá o cargo do agente 
        private string cargoAgente;


        //String com o endereço do banco
        databaseAddress db = new databaseAddress();

        //string NOME DE USUÁRIO
        private string nomeUser;

        int total;

        private async void btnGerarLogin_Click(object sender, RoutedEventArgs e)
        {
            total = 0;
            if (txtSenha.Password != "")
            {
                string forcaSenha = strong.ToString();
                if (forcaSenha == "Aceitável" || forcaSenha == "Forte" || forcaSenha == "Segura")
                {

                    //Updating nomeUser
                    nomeUser = txtUsuario.Text;

                    OracleConnection Oracon = new OracleConnection(db.oradb);
                    Oracon.Open();

                    OracleCommand check = new OracleCommand(SQL_CHECK_LOGIN, Oracon);
                    check.Parameters.Add("id", id);
                    OracleDataReader read = check.ExecuteReader();
                    read.Read();

                    total = Convert.ToInt32(read[0].ToString());

                    Oracon.Close();

                    if (total == 1)
                    {
                        await this.ShowMessageAsync("Aviso", "Esse agente já possui um login gerado!");
                    
                    }
                    else
                    {
                        Oracon.Open();

                        
                        OracleCommand insert = new OracleCommand(SQL_INSERT_LOGIN, Oracon);
                        insert.Parameters.Add("idAgente", id);
                        insert.Parameters.Add("nomeUser", nomeUser);
                        insert.Parameters.Add("senhaUser", txtSenha.Password);

                        //Set the Acess Level
                        switch (cargoAgente)
                        {
                            case "2":
                                insert.Parameters.Add("nivelAcesso", "1");
                                break;
                            case "3":
                                insert.Parameters.Add("nivelAcesso", "1");
                                break;
                            case "4":
                                insert.Parameters.Add("nivelAcesso", "1");
                                break;
                            case "5":
                                insert.Parameters.Add("nivelAcesso", "1");
                                break;
                        }
 
                        insert.ExecuteNonQuery();

                        OracleCommand profile = new OracleCommand(SQL_INSERT_PROFILE, Oracon);
                        profile.Parameters.Add("idAgente", id);
                        profile.ExecuteNonQuery();

                        Oracon.Close();

                        await this.ShowMessageAsync("Aviso", "Login gerado com sucesso!"); this.MetroWindow_Loaded(null, null);
                        this.Close();
                    } 
                }
                else
                {
                    var mySettings = new MetroDialogSettings()
                    {
                        AffirmativeButtonText = "Ok",
                        NegativeButtonText = "ver parâmetros",
                        ColorScheme = MetroDialogOptions.ColorScheme
                    };
                    MessageDialogResult result = await this.ShowMessageAsync("Aviso", "A senha informada não cumpre os parâmetros do sistema!", MessageDialogStyle.AffirmativeAndNegative, mySettings); 
                    if (result == MessageDialogResult.Negative)
                    {
                        MessageBox.Show("Seis pontos serão atribuídos para cada caractere na senha, até um máximo de sessenta pontos."
                        + " Cinco pontos serão concedidos se a senha inclui uma letra minúscula;"
                        + " Dez pontos serão atribuídos se mais de uma letra minúscula estiver presente;"
                        + " Cinco pontos serão concedidos, se a senha incluir uma letra maiúscula;" 
                        + " Dez pontos serão atribuídos se mais de uma letra maiúscula estiver presente;"
                        + " Cinco pontos serão concedidos, se a senha incluir um dígito numérico;" 
                        + " Dez pontos serão atribuídos, se mais de um dígito numérico estiver presente;"
                        + " Cinco pontos serão concedidos, se a senha incluir qualquer caractere diferente de uma letra ou um dígito, isto inclui símbolos e espaços em branco." 
                        + " Dez pontos serão concedidos, se houver dois ou mais de tais caracteres;"
                        + " Se houver caracteres repetidos na senha será atribuido 30 pontos que será subtraido da fórmula do cálculo no total dos pontos", "Parâmetros da senha para o DPISP");
                    }
                }
           }

            else
            {
                await this.ShowMessageAsync("Aviso", "Digite uma senha para criar o login!"); this.MetroWindow_Loaded(null, null);
            }
                                                                        
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            OracleConnection Oracon = new OracleConnection(db.oradb);
            Oracon.Open();

            OracleCommand select = new OracleCommand(SQL_READ_AGENTE, Oracon);
            select.Parameters.Add("id", id);
            OracleDataReader read = select.ExecuteReader();

            read.Read();

            nomeAgente = Convert.ToString(read[1].ToString());
           
            nomeUser = nomeAgente.Substring(0, nomeAgente.IndexOf(" ")) + "_" + id;

            //Buscar id do cargo e nome do cargo, pelo id do agente
            string SQL_SEARCH_CARGO = "select a.nome, a.id_cargo, c.nome_cargo from CARGO c inner join agente a on c.ID_CARGO = a.ID_CARGO and a.ID_AGENTE ='" + id + "' and a.status = 1";
            OracleCommand cmdCargo = new OracleCommand(SQL_SEARCH_CARGO, Oracon);
            OracleDataReader rCargo = cmdCargo.ExecuteReader();
            rCargo.Read();

            cargoAgente = Convert.ToString(rCargo[1].ToString());
            Oracon.Close();

            string usuario = nomeUser;
            usuario = usuario.ToLower();
            txtUsuario.Text = usuario;
            txtUsuario.IsReadOnly = true;
        }

        private void txtSenha_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (txtSenha.Password != string.Empty)
            {
                
                placar = verifica.geraPontosSenha(txtSenha.Password);
                strong = verifica.GetForcaDaSenha(txtSenha.Password);

                lblPontos.Content = placar.ToString() + " Pontos";
                lblForca.Content = strong.ToString();
            }
        }
    }
}
