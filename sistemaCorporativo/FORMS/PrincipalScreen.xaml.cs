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
using sistemaCorporativo;
using sistemaCorporativo.FORMS.cadAgente;
using sistemaCorporativo.FORMS;
using sistemaCorporativo.UTIL.nameAndLastName;
using MahApps.Metro.Actions;
using MahApps.Metro.Converters;
using MahApps.Metro;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
using sistemaCorporativo.UTIL.databaseAdress;
using System.Windows.Media.Animation;


namespace sistemaCorporativo.FORMS.principalScreen
{
    /// <summary>
    /// Interaction logic for PrincipalScreen.xaml
    /// </summary>
    public partial class PrincipalScreen : MetroWindow
    {
        DateTime horaedata;
        private string user;
        public PrincipalScreen(string usuario)
        {
            
			System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += dispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();
			InitializeComponent();
			
            user = usuario.ToString();
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            //Hora e Data
            horaedata = DateTime.Now;
            txbDataHora.Text = "Data: " + horaedata.ToLongDateString() + " Hora: " + horaedata.ToLongTimeString();
        }

        //Strings para informações do usuario (FLYOUT)
        int casosResolvidos;
        string cargo;
        string idAgente;
        string name;
        int nivel;
        int xp;
        string ptsexperiencia;
        string ProfilePic;
        //Endereço database
        databaseAddress db = new databaseAddress();

        //Boolean TemaClaro
        public Boolean temaClaro = false;
        
        
        private void AgenteMenuItem_Click(object sender, RoutedEventArgs e)
        {
            CadAgente cadAgente = new CadAgente(this);
            cadAgente.ShowDialog();
        }
        

        private void OcorrenciaMenuItem_Click(object sender, RoutedEventArgs e)
        {
            CadOcorrencia cadOcorrencia = new CadOcorrencia();
            cadOcorrencia.ShowDialog();
        }
        
            private void DenunciaMenuItem_Click(object sender, RoutedEventArgs e)
        {
            CadDenuncia cadDenuncia = new CadDenuncia();
            cadDenuncia.ShowDialog();
        }

   
        private void btnPerfil_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            flyoutAgente.IsOpen = true;
			flyoutAgente.IsPinned = false;
        }

       
        private void tgsTema_Checked(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
            ThemeManager.ChangeAppTheme(Application.Current, "BaseLight");
            ImageBrush LightImage = new ImageBrush();
            Image image = new Image();
            image.Source = new BitmapImage(
                new Uri(
                   "pack://application:,,,/IMAGES/Wallpaper Oficial SPPDI material LIGHT.png"));
            LightImage.ImageSource = image.Source;
            grdPrincipal.Background = LightImage;
            LightImage.Stretch = Stretch.Fill;
            //Mudar a cor do botao Logoff
            SolidColorBrush solidLight = new SolidColorBrush();
            solidLight.Color = Color.FromArgb(255, 147, 147, 147);
            btnFzrLogoff.Background = solidLight;
            temaClaro = true;
            
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void tgsTema_IsCheckedChanged(object sender, System.EventArgs e)
        {
            try
            {
            ThemeManager.ChangeAppTheme(Application.Current, "BaseDark");
            ImageBrush DarkImage = new ImageBrush();
            Image imageDark = new Image();
            imageDark.Source = new BitmapImage(
                new Uri(
                   "pack://application:,,,/IMAGES/Wallpaper Oficial SPPDI material.png"));
            DarkImage.ImageSource = imageDark.Source;
            grdPrincipal.Background = DarkImage;
            DarkImage.Stretch = Stretch.Fill;
            //Mudar a cor do botao logoff
            SolidColorBrush solidDark= new SolidColorBrush();
            solidDark.Color = Color.FromArgb(255, 38, 38, 38);
            btnFzrLogoff.Background = solidDark;
            temaClaro = false;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void ViaturaMenuItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
             CadViatura cadViatura = new CadViatura();
             cadViatura.ShowDialog();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            
        }

        private void TileAgente_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
             CadAgente cadAgente = new CadAgente(this);
             cadAgente.ShowDialog();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            
        }

        private void TileViatura_Click_(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
            CadViatura cadViatura = new CadViatura();
            cadViatura.ShowDialog();
            }
            catch (Exception ex)
            {
                
                MessageBox.Show(ex.Message);
            }
        }
		
        private void TileInvestigacao_Click(object sender, System.Windows.RoutedEventArgs e)
        {
        	try
			{
				CrimeManagementSplash cms = new CrimeManagementSplash();
				cms.ShowDialog();
			}
			catch(Exception ex)
			{
                MessageBox.Show(ex.Message);
			}
        }

        private void TileDenuncia_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
             CadDenuncia cadDenuncia = new CadDenuncia();
             cadDenuncia.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        	
        }
		
	    private void tileCadUnidade_Click(object sender, System.Windows.RoutedEventArgs e)
        {
			CadUnidades wndCadUnidades = new CadUnidades();
			wndCadUnidades.ShowDialog();
        }

        private void tileCadAgenAux_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            CadOutrasAgencias wndCadAgencia = new CadOutrasAgencias();
			wndCadAgencia.ShowDialog();
        }
		
        private void tilePessoaF_Click(object sender, System.Windows.RoutedEventArgs e)
        {
        	CadPessoaFisica wndPessoaF = new CadPessoaFisica(null, null, null);
			wndPessoaF.ShowDialog();
        }  

        private void btnFzrLogoff_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            LogoffSplash logofSplash = new LogoffSplash();
            logofSplash.ShowDialog();
            this.Close();
           
        }

        private void PrincipalScreen1_Loaded(object sender, RoutedEventArgs e)
        {
            OracleConnection Oracon = new OracleConnection(db.oradb);

            //String para buscar informações do usuario
            string SQL_SEARCH = "select id_Agente from login_Agente where nome_User = '" + user + "'";
            Oracon.Open();

            OracleCommand comando = new OracleCommand(SQL_SEARCH, Oracon);
            OracleDataReader read = comando.ExecuteReader();
            read.Read();
            idAgente = Convert.ToString(read[0].ToString());

            //String para buscar informações do usuario
            string SQL_SEARCH_ADVANCED = "select a.id_Agente, a.nome, l.nome_User from agente a Join LOGIN_AGENTE l on (l.id_Agente = '" + idAgente + "' and l.ID_AGENTE = a.ID_AGENTE and a.status != 0)";
            string SQL_PROFILE_SEARCH = "select nivel_Agente, casos_resolvidos, xp from perfil_Agente where id_Agente ='" + idAgente + "' and status = 1";
            OracleCommand cmdConsulta = new OracleCommand(SQL_SEARCH_ADVANCED, Oracon);
            OracleCommand cmdProfile = new OracleCommand(SQL_PROFILE_SEARCH, Oracon);
            OracleDataReader reader = cmdConsulta.ExecuteReader();
            OracleDataReader rProfile = cmdProfile.ExecuteReader();
            reader.Read();
            rProfile.Read();

            name = Convert.ToString(reader[1].ToString());
            nivel = Convert.ToInt32(rProfile[0]);
            casosResolvidos = Convert.ToInt32(rProfile[1].ToString());
            xp = (Convert.ToInt32(rProfile[2].ToString()));
            ptsexperiencia = (xp.ToString() + " XP");
            

            string SQL_SEARCH_CARGO = "select a.nome, a.id_cargo, c.nome_cargo from CARGO c inner join agente a on c.ID_CARGO = a.ID_CARGO and a.ID_AGENTE ='" + idAgente + "'";
            OracleCommand cmdCargo = new OracleCommand(SQL_SEARCH_CARGO, Oracon);
            OracleDataReader rCargo = cmdCargo.ExecuteReader();
            rCargo.Read();

            cargo = Convert.ToString(rCargo[2].ToString());

            //Take the profile Pic
            string SQL_TAKEPIC = "select fotoAgente from Agente where id_Agente ='"+idAgente+"'";

            OracleCommand takePic = new OracleCommand(SQL_TAKEPIC, Oracon);
            OracleDataReader takePicNow = takePic.ExecuteReader();
            takePicNow.Read();

            ProfilePic = Convert.ToString(takePicNow[0].ToString());

            Oracon.Close();

            name = nameAndLastName.FormataNome(name); 
			
			//Animation Dockbar
			Storyboard Animation = this.FindResource("dockbarAnimation") as Storyboard;
            Animation.Begin();
        }

        private void FlyoutAgente_Loaded(object sender, RoutedEventArgs e)
        {
            //Carregar Informações do Usuario
            lblNomeAgente.Content = name.ToString();
            lblNivel.Content = nivel.ToString();
            lblPtsExp.Content = ptsexperiencia.ToString();
            lblCargo.Content = cargo.ToString();
            lblCasosResolvidos.Content = casosResolvidos.ToString();
            agenteProfilePicture.Source = new BitmapImage(new Uri(ProfilePic));


            
        }

    }
}
