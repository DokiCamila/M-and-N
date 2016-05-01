using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using MahApps.Metro.Behaviours;
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
using System.Windows.Media.Animation;
using sistemaCorporativo.FORMS.cadAgente;
using sistemaCorporativo.UTIL.databaseAdress;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;

namespace sistemaCorporativo.FORMS
{
    /// <summary>
    /// Interaction logic for MostrarFingerPrintsID.xaml
    /// </summary>
    public partial class MostrarFingerPrintsID : MetroWindow
    {

        //Instanciar
        CadAgente cad;
        public MostrarFingerPrintsID(CadAgente info)
        {
            InitializeComponent();
            cad = info;
        }

        //Endereço Banco
        databaseAddress db = new databaseAddress();
        #region agente informations
        //Id Agente
        private String IdAgente;
        //Impressões PATH
        private String Pol, PolD;
        private String Ind, IndD;
        private String Med, MedD;
        private String Anu, AnuD;
        private String Min, MinD;
        //Ficha Dactloscopica
        private String fichaDact;

        //Classificações
        private string ClassP, ClassPD;
        private string ClassI, ClassID;
        private string ClassM, ClassMD;
        private string ClassA, ClassAD;
        private string ClassMi, ClassMiD;

        #endregion

        #region SQL
        private String SQL_SELECT;
        private String SQL_TAKE_NAME;

        #endregion


        private void windowMostrarFingerprints_Loaded(object sender, RoutedEventArgs e)
        {
            
            try
            {
                IdAgente = cad.id;

                #region PEGANDO DO BANCO
                //Conexão Banco de dados
                OracleConnection Oracon = new OracleConnection(db.oradb);
                Oracon.Open();

                //Inserindo Script
                SQL_SELECT = "select * from ID where id_Agente ='"+IdAgente+"'";

                //Comando
                OracleCommand selectall = new OracleCommand(SQL_SELECT, Oracon);
                
                SQL_TAKE_NAME = "select nome from Agente where id_Agente = '"+IdAgente+"'";
                OracleCommand takeName = new OracleCommand(SQL_TAKE_NAME, Oracon);

                //Leitor
                OracleDataReader read = selectall.ExecuteReader();
                read.Read();

                OracleDataReader take = takeName.ExecuteReader();
                take.Read();


                //Catch Values from Data Base
                //Direito - RIGHT
                PolD = Convert.ToString(read[2].ToString());
                IndD = Convert.ToString(read[3].ToString());
                MedD = Convert.ToString(read[4].ToString());
                AnuD = Convert.ToString(read[5].ToString());
                MinD = Convert.ToString(read[6].ToString());
                //Esquerdo - LEFT
                Pol = Convert.ToString(read[7].ToString());
                Ind = Convert.ToString(read[8].ToString());
                Med = Convert.ToString(read[9].ToString());
                Anu = Convert.ToString(read[10].ToString());
                Min = Convert.ToString(read[11].ToString());
                //Ficha
                fichaDact = Convert.ToString(read[12].ToString());

                //Nome
                lblNome.Content = Convert.ToString(take[0].ToString());

                Oracon.Close();
                #endregion

                #region MOSTRANDO
                //IMPRESSOES
                //RIGHT
                imgR1.Source = new BitmapImage(new Uri(PolD));
                imgR2.Source = new BitmapImage(new Uri(IndD));
                imgR3.Source = new BitmapImage(new Uri(MedD));
                imgR4.Source = new BitmapImage(new Uri(AnuD));
                imgR5.Source = new BitmapImage(new Uri(MinD));

                //LEFT
                imgL6.Source = new BitmapImage(new Uri(Pol));
                imgL7.Source = new BitmapImage(new Uri(Ind));
                imgL8.Source = new BitmapImage(new Uri(Med));
                imgL9.Source = new BitmapImage(new Uri(Anu));
                imgL10.Source = new BitmapImage(new Uri(Min));

                //Ficha Dacloscopica
                lblFichaDact.Content = fichaDact.ToString();

               

                #endregion

            }
            catch (Exception ex)
            {
                MessageBox.Show("Informações não carregadas!", "Erro!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }

        #region Selection Right (Direita)
        private void imgR1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //Visivel
            selectionR1.Visibility = Visibility.Visible;
            //Iniciar Animação
            Storyboard selection = windowMostrarFingerprints.FindResource("selectionR1") as Storyboard;
            selection.Begin();

            //Imagem
            imgRightHand.Source = new BitmapImage(new Uri("pack://application:,,,/IMAGES/handPolR.png"));

            //Clean
            selectionR2.Visibility = Visibility.Hidden;
            selectionR3.Visibility = Visibility.Hidden;
            selectionR4.Visibility = Visibility.Hidden;
            selectionR5.Visibility = Visibility.Hidden;

            //Clean
            selectionL6.Visibility = Visibility.Hidden;
            selectionL7.Visibility = Visibility.Hidden;
            selectionL8.Visibility = Visibility.Hidden;
            selectionL9.Visibility = Visibility.Hidden;
            selectionL10.Visibility = Visibility.Hidden;


            //Imagem
            imgLeftHand.Source = new BitmapImage(new Uri("pack://application:,,,/IMAGES/handWithoutSelection.png"));

            #region Show Informations
            //Mostrar Informações
            lblDedo.Content = "Polegar";
            lblMao.Content = "Direita";

            //Classificação
            ClassPD = fichaDact.Substring(0, 1);
            switch (ClassPD)
            {
                case "A":
                    lblClassificacao.Content = "Arco";
                    break;
                case "I":
                    lblClassificacao.Content = "Presilha Interna";
                    break;
                case "E":
                    lblClassificacao.Content = "Presilha Externa";
                    break;
                case "V":
                    lblClassificacao.Content = "Verticilo";
                    break;
                case "0":
                    lblClassificacao.Content = "Amputado";
                    break;
                case "X":
                    lblClassificacao.Content = "Cicatriz";
                    break;
            }
            #endregion

            imgFingerPrint.Source = new BitmapImage(new Uri(PolD));


        }

        private void imgR2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //Visivel
            selectionR2.Visibility = Visibility.Visible;
            //Iniciar Animação
            Storyboard selection = windowMostrarFingerprints.FindResource("selectionR2") as Storyboard;
            selection.Begin();

            //Imagem
            imgRightHand.Source = new BitmapImage(new Uri("pack://application:,,,/IMAGES/handIndR.png"));

            //Clean
            selectionR1.Visibility = Visibility.Hidden;
            selectionR3.Visibility = Visibility.Hidden;
            selectionR4.Visibility = Visibility.Hidden;
            selectionR5.Visibility = Visibility.Hidden;

            //Clean
            selectionL6.Visibility = Visibility.Hidden;
            selectionL7.Visibility = Visibility.Hidden;
            selectionL8.Visibility = Visibility.Hidden;
            selectionL9.Visibility = Visibility.Hidden;
            selectionL10.Visibility = Visibility.Hidden;


            //Imagem
            imgLeftHand.Source = new BitmapImage(new Uri("pack://application:,,,/IMAGES/handWithoutSelection.png"));

            #region Show Informations
            //Mostrar Informações
            lblDedo.Content = "Indicador";
            lblMao.Content = "Direita";

            //Classificação
            ClassID = fichaDact.Substring(1, 1);
            switch (ClassID)
            {
                case "1":
                    lblClassificacao.Content = "Arco";
                    break;
                case "2":
                    lblClassificacao.Content = "Presilha Interna";
                    break;
                case "3":
                    lblClassificacao.Content = "Presilha Externa";
                    break;
                case "4":
                    lblClassificacao.Content = "Verticilo";
                    break;
                case "0":
                    lblClassificacao.Content = "Amputado";
                    break;
                case "X":
                    lblClassificacao.Content = "Cicatriz";
                    break;
            }
            #endregion

            imgFingerPrint.Source = new BitmapImage(new Uri(IndD));
        }

        private void imgR3_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //Visivel
            selectionR3.Visibility = Visibility.Visible;
            //Iniciar Animação
            Storyboard selection = windowMostrarFingerprints.FindResource("selectionR3") as Storyboard;
            selection.Begin();

            //Imagem
            imgRightHand.Source = new BitmapImage(new Uri("pack://application:,,,/IMAGES/handMedR.png"));

            //Clean
            selectionR2.Visibility = Visibility.Hidden;
            selectionR1.Visibility = Visibility.Hidden;
            selectionR4.Visibility = Visibility.Hidden;
            selectionR5.Visibility = Visibility.Hidden;

            //Clean
            selectionL6.Visibility = Visibility.Hidden;
            selectionL7.Visibility = Visibility.Hidden;
            selectionL8.Visibility = Visibility.Hidden;
            selectionL9.Visibility = Visibility.Hidden;
            selectionL10.Visibility = Visibility.Hidden;


            //Imagem
            imgLeftHand.Source = new BitmapImage(new Uri("pack://application:,,,/IMAGES/handWithoutSelection.png"));

            #region Show Informations
            //Mostrar Informações
            lblDedo.Content = "Médio";
            lblMao.Content = "Direita";

            //Classificação
            ClassMD = fichaDact.Substring(2, 1);
            switch (ClassMD)
            {
                case "1":
                    lblClassificacao.Content = "Arco";
                    break;
                case "2":
                    lblClassificacao.Content = "Presilha Interna";
                    break;
                case "3":
                    lblClassificacao.Content = "Presilha Externa";
                    break;
                case "4":
                    lblClassificacao.Content = "Verticilo";
                    break;
                case "0":
                    lblClassificacao.Content = "Amputado";
                    break;
                case "X":
                    lblClassificacao.Content = "Cicatriz";
                    break;
            }
            #endregion

            imgFingerPrint.Source = new BitmapImage(new Uri(MedD));
        }

        private void imgR4_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //Visivel
            selectionR4.Visibility = Visibility.Visible;
            //Iniciar Animação
            Storyboard selection = windowMostrarFingerprints.FindResource("selectionR4") as Storyboard;
            selection.Begin();

            //Imagem
            imgRightHand.Source = new BitmapImage(new Uri("pack://application:,,,/IMAGES/handAnuR.png"));

            //Clean
            selectionR2.Visibility = Visibility.Hidden;
            selectionR3.Visibility = Visibility.Hidden;
            selectionR1.Visibility = Visibility.Hidden;
            selectionR5.Visibility = Visibility.Hidden;

            //Clean
            selectionL6.Visibility = Visibility.Hidden;
            selectionL7.Visibility = Visibility.Hidden;
            selectionL8.Visibility = Visibility.Hidden;
            selectionL9.Visibility = Visibility.Hidden;
            selectionL10.Visibility = Visibility.Hidden;


            //Imagem
            imgLeftHand.Source = new BitmapImage(new Uri("pack://application:,,,/IMAGES/handWithoutSelection.png"));

            #region Show Informations
            //Mostrar Informações
            lblDedo.Content = "Anular";
            lblMao.Content = "Direita";

            //Classificação
            ClassAD = fichaDact.Substring(3, 1);
            switch (ClassAD)
            {
                case "1":
                    lblClassificacao.Content = "Arco";
                    break;
                case "2":
                    lblClassificacao.Content = "Presilha Interna";
                    break;
                case "3":
                    lblClassificacao.Content = "Presilha Externa";
                    break;
                case "4":
                    lblClassificacao.Content = "Verticilo";
                    break;
                case "0":
                    lblClassificacao.Content = "Amputado";
                    break;
                case "X":
                    lblClassificacao.Content = "Cicatriz";
                    break;
            }
            #endregion

            imgFingerPrint.Source = new BitmapImage(new Uri(AnuD));
        }

        private void imgR5_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //Visivel
            selectionR5.Visibility = Visibility.Visible;
            //Iniciar Animação
            Storyboard selection = windowMostrarFingerprints.FindResource("selectionR5") as Storyboard;
            selection.Begin();

            //Imagem
            imgRightHand.Source = new BitmapImage(new Uri("pack://application:,,,/IMAGES/handMidR.png"));

            //Clean
            selectionR2.Visibility = Visibility.Hidden;
            selectionR3.Visibility = Visibility.Hidden;
            selectionR4.Visibility = Visibility.Hidden;
            selectionR1.Visibility = Visibility.Hidden;

            //Clean
            selectionL6.Visibility = Visibility.Hidden;
            selectionL7.Visibility = Visibility.Hidden;
            selectionL8.Visibility = Visibility.Hidden;
            selectionL9.Visibility = Visibility.Hidden;
            selectionL10.Visibility = Visibility.Hidden;


            //Imagem
            imgLeftHand.Source = new BitmapImage(new Uri("pack://application:,,,/IMAGES/handWithoutSelection.png"));

            #region Show Informations
            //Mostrar Informações
            lblDedo.Content = "Mínimo";
            lblMao.Content = "Direita";

            //Classificação
            ClassMiD = fichaDact.Substring(4, 1);
            switch (ClassMiD)
            {
                case "1":
                    lblClassificacao.Content = "Arco";
                    break;
                case "2":
                    lblClassificacao.Content = "Presilha Interna";
                    break;
                case "3":
                    lblClassificacao.Content = "Presilha Externa";
                    break;
                case "4":
                    lblClassificacao.Content = "Verticilo";
                    break;
                case "0":
                    lblClassificacao.Content = "Amputado";
                    break;
                case "X":
                    lblClassificacao.Content = "Cicatriz";
                    break;
            }
            #endregion

            imgFingerPrint.Source = new BitmapImage(new Uri(MinD));
        }

        #endregion 

        #region Selection Left (Esquerda)
        private void imgL6_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //Visivel
            selectionL6.Visibility = Visibility.Visible;
            //Iniciar Animação
            Storyboard selection = windowMostrarFingerprints.FindResource("selectionL6") as Storyboard;
            selection.Begin();

            //Imagem
            imgLeftHand.Source = new BitmapImage(new Uri("pack://application:,,,/IMAGES/handPol.png"));

            //Clean
            selectionL7.Visibility = Visibility.Hidden;
            selectionL8.Visibility = Visibility.Hidden;
            selectionL9.Visibility = Visibility.Hidden;
            selectionL10.Visibility = Visibility.Hidden;

            //Clean
            selectionR1.Visibility = Visibility.Hidden;
            selectionR2.Visibility = Visibility.Hidden;
            selectionR3.Visibility = Visibility.Hidden;
            selectionR4.Visibility = Visibility.Hidden;
            selectionR5.Visibility = Visibility.Hidden;


            //Imagem
            imgRightHand.Source = new BitmapImage(new Uri("pack://application:,,,/IMAGES/handWithoutSelectionR.png"));

            #region Show Informations
            //Mostrar Informações
            lblDedo.Content = "Polegar";
            lblMao.Content = "Esquerda";

            //Classificação
            ClassP = fichaDact.Substring(6, 1);
            switch (ClassP)
            {
                case "A":
                    lblClassificacao.Content = "Arco";
                    break;
                case "I":
                    lblClassificacao.Content = "Presilha Interna";
                    break;
                case "E":
                    lblClassificacao.Content = "Presilha Externa";
                    break;
                case "V":
                    lblClassificacao.Content = "Verticilo";
                    break;
                case "0":
                    lblClassificacao.Content = "Amputado";
                    break;
                case "X":
                    lblClassificacao.Content = "Cicatriz";
                    break;
            }
            #endregion

            imgFingerPrint.Source = new BitmapImage(new Uri(Pol));
        }

        private void imgL7_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //Visivel
            selectionL7.Visibility = Visibility.Visible;
            //Iniciar Animação
            Storyboard selection = windowMostrarFingerprints.FindResource("selectionL7") as Storyboard;
            selection.Begin();

            //Imagem
            imgLeftHand.Source = new BitmapImage(new Uri("pack://application:,,,/IMAGES/handInd.png"));

            //Clean
            selectionL6.Visibility = Visibility.Hidden;
            selectionL8.Visibility = Visibility.Hidden;
            selectionL9.Visibility = Visibility.Hidden;
            selectionL10.Visibility = Visibility.Hidden;

            //Clean
            selectionR1.Visibility = Visibility.Hidden;
            selectionR2.Visibility = Visibility.Hidden;
            selectionR3.Visibility = Visibility.Hidden;
            selectionR4.Visibility = Visibility.Hidden;
            selectionR5.Visibility = Visibility.Hidden;


            //Imagem
            imgRightHand.Source = new BitmapImage(new Uri("pack://application:,,,/IMAGES/handWithoutSelectionR.png"));

            #region Show Informations
            //Mostrar Informações
            lblDedo.Content = "Indicador";
            lblMao.Content = "Esquerda";

            //Classificação
            ClassI = fichaDact.Substring(7, 1);
            switch (ClassI)
            {
                case "1":
                    lblClassificacao.Content = "Arco";
                    break;
                case "2":
                    lblClassificacao.Content = "Presilha Interna";
                    break;
                case "3":
                    lblClassificacao.Content = "Presilha Externa";
                    break;
                case "4":
                    lblClassificacao.Content = "Verticilo";
                    break;
                case "0":
                    lblClassificacao.Content = "Amputado";
                    break;
                case "X":
                    lblClassificacao.Content = "Cicatriz";
                    break;
            }
            #endregion

            imgFingerPrint.Source = new BitmapImage(new Uri(Ind));
        }

        private void imgL8_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //Visivel
            selectionL8.Visibility = Visibility.Visible;
            //Iniciar Animação
            Storyboard selection = windowMostrarFingerprints.FindResource("selectionL8") as Storyboard;
            selection.Begin();

            //Imagem
            imgLeftHand.Source = new BitmapImage(new Uri("pack://application:,,,/IMAGES/handMed.png"));

            //Clean
            selectionL7.Visibility = Visibility.Hidden;
            selectionL6.Visibility = Visibility.Hidden;
            selectionL9.Visibility = Visibility.Hidden;
            selectionL10.Visibility = Visibility.Hidden;

            //Clean
            selectionR1.Visibility = Visibility.Hidden;
            selectionR2.Visibility = Visibility.Hidden;
            selectionR3.Visibility = Visibility.Hidden;
            selectionR4.Visibility = Visibility.Hidden;
            selectionR5.Visibility = Visibility.Hidden;


            //Imagem
            imgRightHand.Source = new BitmapImage(new Uri("pack://application:,,,/IMAGES/handWithoutSelectionR.png"));

            #region Show Informations
            //Mostrar Informações
            lblDedo.Content = "Médio";
            lblMao.Content = "Esquerda";

            //Classificação
            ClassM = fichaDact.Substring(8, 1);
            switch (ClassM)
            {
                case "1":
                    lblClassificacao.Content = "Arco";
                    break;
                case "2":
                    lblClassificacao.Content = "Presilha Interna";
                    break;
                case "3":
                    lblClassificacao.Content = "Presilha Externa";
                    break;
                case "4":
                    lblClassificacao.Content = "Verticilo";
                    break;
                case "0":
                    lblClassificacao.Content = "Amputado";
                    break;
                case "X":
                    lblClassificacao.Content = "Cicatriz";
                    break;
            }
            #endregion

            imgFingerPrint.Source = new BitmapImage(new Uri(Med));
        }

        private void imgL9_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //Visivel
            selectionL9.Visibility = Visibility.Visible;
            //Iniciar Animação
            Storyboard selection = windowMostrarFingerprints.FindResource("selectionL9") as Storyboard;
            selection.Begin();

            //Imagem
            imgLeftHand.Source = new BitmapImage(new Uri("pack://application:,,,/IMAGES/handAnu.png"));

            //Clean
            selectionL7.Visibility = Visibility.Hidden;
            selectionL8.Visibility = Visibility.Hidden;
            selectionL6.Visibility = Visibility.Hidden;
            selectionL10.Visibility = Visibility.Hidden;

            //Clean
            selectionR1.Visibility = Visibility.Hidden;
            selectionR2.Visibility = Visibility.Hidden;
            selectionR3.Visibility = Visibility.Hidden;
            selectionR4.Visibility = Visibility.Hidden;
            selectionR5.Visibility = Visibility.Hidden;


            //Imagem
            imgRightHand.Source = new BitmapImage(new Uri("pack://application:,,,/IMAGES/handWithoutSelectionR.png"));

            #region Show Informations
            //Mostrar Informações
            lblDedo.Content = "Anular";
            lblMao.Content = "Esquerda";

            //Classificação
            ClassA = fichaDact.Substring(9, 1);
            switch (ClassA)
            {
                case "1":
                    lblClassificacao.Content = "Arco";
                    break;
                case "2":
                    lblClassificacao.Content = "Presilha Interna";
                    break;
                case "3":
                    lblClassificacao.Content = "Presilha Externa";
                    break;
                case "4":
                    lblClassificacao.Content = "Verticilo";
                    break;
                case "0":
                    lblClassificacao.Content = "Amputado";
                    break;
                case "X":
                    lblClassificacao.Content = "Cicatriz";
                    break;
            }
            #endregion

            imgFingerPrint.Source = new BitmapImage(new Uri(Anu));
        }

        private void imgL10_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //Visivel
            selectionL10.Visibility = Visibility.Visible;
            //Iniciar Animação
            Storyboard selection = windowMostrarFingerprints.FindResource("selectionL10") as Storyboard;
            selection.Begin();

            //Imagem
            imgLeftHand.Source = new BitmapImage(new Uri("pack://application:,,,/IMAGES/handMid.png"));

            //Clean
            selectionL7.Visibility = Visibility.Hidden;
            selectionL8.Visibility = Visibility.Hidden;
            selectionL9.Visibility = Visibility.Hidden;
            selectionL6.Visibility = Visibility.Hidden;

            //Clean
            selectionR1.Visibility = Visibility.Hidden;
            selectionR2.Visibility = Visibility.Hidden;
            selectionR3.Visibility = Visibility.Hidden;
            selectionR4.Visibility = Visibility.Hidden;
            selectionR5.Visibility = Visibility.Hidden;


            //Imagem
            imgRightHand.Source = new BitmapImage(new Uri("pack://application:,,,/IMAGES/handWithoutSelectionR.png"));

            #region Show Informations
            //Mostrar Informações
            lblDedo.Content = "Mínimo";
            lblMao.Content = "Esquerda";

            //Classificação
            ClassMi = fichaDact.Substring(10, 1);
            switch (ClassMi)
            {
                case "1":
                    lblClassificacao.Content = "Arco";
                    break;
                case "2":
                    lblClassificacao.Content = "Presilha Interna";
                    break;
                case "3":
                    lblClassificacao.Content = "Presilha Externa";
                    break;
                case "4":
                    lblClassificacao.Content = "Verticilo";
                    break;
                case "0":
                    lblClassificacao.Content = "Amputado";
                    break;
                case "X":
                    lblClassificacao.Content = "Cicatriz";
                    break;
            }
            #endregion
            imgFingerPrint.Source = new BitmapImage(new Uri(Min));
        }

        #endregion


    }
}
