using MahApps.Metro.Controls;
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
using MahApps.Metro.Behaviours;
using MahApps.Metro.Controls.Dialogs;
using System.IO;
using sistemaCorporativo.FORMS.cadAgente;
using System.Windows.Media.Animation;
using sistemaCorporativo.UTIL.Futronic;
using System.Threading;

namespace sistemaCorporativo.FORMS
{
    /// <summary>
    /// Interaction logic for CadFingerPrints.xaml
    /// </summary>
    public partial class CadFingerPrints : MetroWindow
    {
        
        //Dedos da mão
        public int dedos = 0;
        Boolean documento = false;
  
        //Instanciar
        CadAgente cad;
        public CadFingerPrints(CadAgente info)
        {
            InitializeComponent();
            cad = info;
            
            dispatcherTimer.Tick += dispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 1);
        }

        
        //Scanner Biométrico
        static Device d;
        Thread t = new Thread(Window);
        System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
		private bool imagemScanner = false;
        
        //Imagem da ID (same as the CadAgente)
        public BitmapSource polD, polE;
        public BitmapSource IndD, IndE;
        public BitmapSource MedD, MedE;
        public BitmapSource AnuD, AnuE;
        public BitmapSource MinD, MinE;

        //Grupo da ID (same as the CadAgente)
        public string grupoPolD, grupoPolE;
        public string grupoIndD, grupoIndE;
        public string grupoMedD, grupoMedE;
        public string grupoAnuD, grupoAnuE;
        public string grupoMinD, grupoMinE;
        public string ID;

        //Boolean que checa se foi selecionado alguma ID para inserir
        Boolean Passed;

        //Booleans para checar se existe impressao no controle Imagem
        public Boolean hasimage1 = false;
        public Boolean hasimage2 = false;
        public Boolean hasimage3 = false;
        public Boolean hasimage4 = false;
        public Boolean hasimage5 = false;
        public Boolean hasimage6 = false;
        public Boolean hasimage7 = false;
        public Boolean hasimage8 = false;
        public Boolean hasimage9 = false;
        public Boolean hasimage10 = false;

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            rdbPolD.IsChecked = true;
        }

        private void rdbPolD_Checked(object sender, RoutedEventArgs e)
        {
            dedos = 1;
            rdbIndD.IsChecked = false;
            rdbMedD.IsChecked = false;
            rdbAnuD.IsChecked = false;
            rdbMinD.IsChecked = false;
            rdbPolE.IsChecked = false;
            rdbIndE.IsChecked = false;
            rdbMedE.IsChecked = false;
            rdbAnuE.IsChecked = false;
            rdbMinE.IsChecked = false;
            if (hasimage1)
            {
                btnClassificar.IsEnabled = true;
            }
            else
            {
                btnClassificar.IsEnabled = false;
            }
        }

        private void rdbIndD_Checked(object sender, RoutedEventArgs e)
        {
            dedos = 2;
            rdbPolD.IsChecked = false;
            rdbMedD.IsChecked = false;
            rdbAnuD.IsChecked = false;
            rdbMinD.IsChecked = false;
            rdbPolE.IsChecked = false;
            rdbIndE.IsChecked = false;
            rdbMedE.IsChecked = false;
            rdbAnuE.IsChecked = false;
            rdbMinE.IsChecked = false;
            if (hasimage2)
            {
                btnClassificar.IsEnabled = true;
            }
            else
            {
                btnClassificar.IsEnabled = false;
            }
        }

        private void rdbMedD_Checked(object sender, RoutedEventArgs e)
        {
            dedos = 3;
            rdbIndD.IsChecked = false;
            rdbPolD.IsChecked = false;
            rdbAnuD.IsChecked = false;
            rdbMinD.IsChecked = false;
            rdbPolE.IsChecked = false;
            rdbIndE.IsChecked = false;
            rdbMedE.IsChecked = false;
            rdbAnuE.IsChecked = false;
            rdbMinE.IsChecked = false;
            if (hasimage3)
            {
                btnClassificar.IsEnabled = true;
            }
            else
            {
                btnClassificar.IsEnabled = false;
            }
        }

        private void rdbAnuD_Checked(object sender, RoutedEventArgs e)
        {
            dedos = 4;
            rdbIndD.IsChecked = false;
            rdbMedD.IsChecked = false;
            rdbPolD.IsChecked = false;
            rdbMinD.IsChecked = false;
            rdbPolE.IsChecked = false;
            rdbIndE.IsChecked = false;
            rdbMedE.IsChecked = false;
            rdbAnuE.IsChecked = false;
            rdbMinE.IsChecked = false;
            if (hasimage4)
            {
                btnClassificar.IsEnabled = true;
            }
            else
            {
                btnClassificar.IsEnabled = false;
            }
        }

        private void rdbMinD_Checked(object sender, RoutedEventArgs e)
        {
            dedos = 5;
            rdbIndD.IsChecked = false;
            rdbMedD.IsChecked = false;
            rdbAnuD.IsChecked = false;
            rdbPolD.IsChecked = false;
            rdbPolE.IsChecked = false;
            rdbIndE.IsChecked = false;
            rdbMedE.IsChecked = false;
            rdbAnuE.IsChecked = false;
            rdbMinE.IsChecked = false;
            if (hasimage5)
            {
                btnClassificar.IsEnabled = true;
            }
            else
            {
                btnClassificar.IsEnabled = false;
            }
        }

        private void rdbPolE_Checked(object sender, RoutedEventArgs e)
        {
            dedos = 6;
            rdbIndD.IsChecked = false;
            rdbMedD.IsChecked = false;
            rdbAnuD.IsChecked = false;
            rdbMinD.IsChecked = false;
            rdbPolD.IsChecked = false;
            rdbIndE.IsChecked = false;
            rdbMedE.IsChecked = false;
            rdbAnuE.IsChecked = false;
            rdbMinE.IsChecked = false;
            if (hasimage6)
            {
                btnClassificar.IsEnabled = true;
            }
            else
            {
                btnClassificar.IsEnabled = false;
            }
        }

        private void rdbIndE_Checked(object sender, RoutedEventArgs e)
        {
            dedos = 7;
            rdbIndD.IsChecked = false;
            rdbMedD.IsChecked = false;
            rdbAnuD.IsChecked = false;
            rdbMinD.IsChecked = false;
            rdbPolE.IsChecked = false;
            rdbPolD.IsChecked = false;
            rdbMedE.IsChecked = false;
            rdbAnuE.IsChecked = false;
            rdbMinE.IsChecked = false;
            if (hasimage7)
            {
                btnClassificar.IsEnabled = true;
            }
            else
            {
                btnClassificar.IsEnabled = false;
            }
        }

        private void rdbMedE_Checked(object sender, RoutedEventArgs e)
        {
            dedos = 8;
            rdbIndD.IsChecked = false;
            rdbMedD.IsChecked = false;
            rdbAnuD.IsChecked = false;
            rdbMinD.IsChecked = false;
            rdbPolE.IsChecked = false;
            rdbIndE.IsChecked = false;
            rdbPolD.IsChecked = false;
            rdbAnuE.IsChecked = false;
            rdbMinE.IsChecked = false;
            if (hasimage8)
            {
                btnClassificar.IsEnabled = true;
            }
            else
            {
                btnClassificar.IsEnabled = false;
            }
        }

        private void rdbAnuE_Checked(object sender, RoutedEventArgs e)
        {
            dedos = 9;
            rdbIndD.IsChecked = false;
            rdbMedD.IsChecked = false;
            rdbAnuD.IsChecked = false;
            rdbMinD.IsChecked = false;
            rdbPolE.IsChecked = false;
            rdbIndE.IsChecked = false;
            rdbMedE.IsChecked = false;
            rdbPolD.IsChecked = false;
            rdbMinE.IsChecked = false;
            if (hasimage9)
            {
                btnClassificar.IsEnabled = true;
            }
            else
            {
                btnClassificar.IsEnabled = false;
            }
        }

        private void rdbMinE_Checked(object sender, RoutedEventArgs e)
        {
            dedos = 10;
            rdbIndD.IsChecked = false;
            rdbMedD.IsChecked = false;
            rdbAnuD.IsChecked = false;
            rdbMinD.IsChecked = false;
            rdbPolE.IsChecked = false;
            rdbIndE.IsChecked = false;
            rdbMedE.IsChecked = false;
            rdbAnuE.IsChecked = false;
            rdbPolD.IsChecked = false;
            if (hasimage10)
            {
                btnClassificar.IsEnabled = true;
            }
            else
            {
                btnClassificar.IsEnabled = false;
            }
        }


        private void itmAbrirDoc_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog ofd = new System.Windows.Forms.OpenFileDialog();
            ofd.Filter = "Images|*.png;*.jpg;*.jpeg;*.bmp;*.gif";
            List<string> allowableFileTypes = new List<string>();
            allowableFileTypes.AddRange(new string[] { ".png", ".jpg", ".jpeg", ".bmp", ".gif" });
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (!ofd.FileName.Equals(String.Empty))
                {
                    FileInfo f = new FileInfo(ofd.FileName);
                    if (allowableFileTypes.Contains(f.Extension.ToLower()))
                    {
                        this.UcImageCropper.ImageUrl = f.FullName;
						rdbDocumento.IsChecked = true;
                        documento = true;
                        
                    }
                    else
                    {
                        MessageBox.Show("Tipo de arquivo inválido");
                    }
                }
                else
                {
                    MessageBox.Show("Você precisa selecionar uma foto do documento para continuar");
                }
            }
        }


        private async void btnInserir_Click(object sender, RoutedEventArgs e)
        {
            
            if (documento || imagemScanner)
            {
                        switch (dedos)
                        {
                            case 1:
                                inserirImpressao(imgPolD, polD);
                                if (Passed == true)
	                               {
                                      hasimage1 = true;
		                              rdbIndD.IsChecked = true;
	                               }
                                
                                break;
                            case 2:
                                inserirImpressao(imgIndD, IndD);
                                if (Passed == true)
	                               {
                                      hasimage2 = true;
		                              rdbMedD.IsChecked = true;
	                               }
                                break;
                            case 3:
                                inserirImpressao(imgMedD, MedD);
                                if (Passed == true)
	                               {
                                      hasimage3 = true;
		                              rdbAnuD.IsChecked = true;
	                               }
                               
                                break;
                            case 4:
                                inserirImpressao(imgAnuD, AnuD);
                                if (Passed == true)
	                               {
                                      hasimage4 = true;
		                              rdbMinD.IsChecked = true;
	                               }
                                
                                break;
                            case 5:
                                inserirImpressao(imgMinD, MinD);
                                if (Passed == true)
	                               {
                                      hasimage5 = true;
		                              rdbPolE.IsChecked = true;
	                               }
                                
                                break;
                            case 6:
                                inserirImpressao(imgPolE, polE);
                                if (Passed == true)
	                               {
                                      hasimage6 = true;
		                              rdbIndE.IsChecked = true;
	                               }
                                
                                break;
                            case 7:
                                inserirImpressao(imgIndE, IndE);
                                if (Passed == true)
	                               {
                                      hasimage7 = true;
		                              rdbMedE.IsChecked = true;
	                               }                           
                                break;
                            case 8:
                                inserirImpressao(imgMedE, MedE);
                                if (Passed == true)
	                               {
                                      hasimage8 = true;
		                              rdbAnuE.IsChecked = true;
	                               } 
                               
                                break;
                            case 9:
                                inserirImpressao(imgAnuE, AnuE);
                                if (Passed == true)
	                               {
                                      hasimage9 = true;
		                              rdbMinE.IsChecked = true;
	                               } 
                                
                                break;
                            case 10:
                                inserirImpressao(imgMinE, MinE);
                                if (Passed == true)
	                               {
                                      hasimage10 = true;
		                              rdbPolD.IsChecked = true;
	                               } 
                                
                                break;
                            default:
                                await this.ShowMessageAsync("Aviso", "Selecione um dedo!");
                                break;
                        }
            }
            else
            {
                await this.ShowMessageAsync("Aviso", "Abra um documento e selecione uma impressão para inserir!");
            }
            
        }

        private async void inserirImpressao(Image impressao, BitmapSource insertinto) 
        {
            if (documento)
            {
                //Inserir via Documento
                this.UcImageCropper.SaveCroppedImage();
                if (this.UcImageCropper.bmpPopup != null)
                {
                    impressao.Source = this.UcImageCropper.bmpPopup;
                    insertinto = this.UcImageCropper.bmpPopup;
                    this.UcImageCropper.bmpPopup = null;
                    this.UcImageCropper.createSelectionCanvas();
                    Passed = true;
                }
                else
                {
                    Passed = false;
                    await this.ShowMessageAsync("Aviso", "Selecione uma impressão digital no documento para continuar!");

                } 
            }
            else
            {
                //Inserir via Leitor
                impressao.Source = imgFingerLeitor.Source;
                insertinto = imgFingerLeitor.Source as BitmapSource;
				Passed = true;
            }
                                
        }

        private void btnClassificar_Click(object sender, RoutedEventArgs e)
        {
            ClassificarFingersPrints classFingers = new ClassificarFingersPrints(this);
            classFingers.ShowDialog();
        }

        private void MetroWindow_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                btnInserir_Click(sender, e);
            }
        }
        private void itmSair_Click(object sender, RoutedEventArgs e)
        {
			if(dispatcherTimer.IsEnabled)
			{
				dispatcherTimer.Stop();
			}
            this.Close();
        }

        private void btnFinalizar_Click(object sender, RoutedEventArgs e)
        {
            //receives the source
            polD = imgPolD.Source as BitmapSource;
            polE = imgPolE.Source as BitmapSource;
            IndD = imgIndD.Source as BitmapSource;
            IndE = imgIndE.Source as BitmapSource;
            MedD = imgMedD.Source as BitmapSource;
            MedE = imgMedE.Source as BitmapSource;
            AnuD = imgAnuD.Source as BitmapSource;
            AnuE = imgAnuE.Source as BitmapSource;
            MinD = imgMinD.Source as BitmapSource;
            MinE = imgMinE.Source as BitmapSource;

            //Update imagens para criação da ID
            cad.polD = polD as BitmapImage;
            cad.polE = polE as BitmapImage;
            cad.IndD = IndD as BitmapImage;
            cad.IndE = IndE as BitmapImage;
            cad.MedD = MedD as BitmapImage;
            cad.MedE = MedE as BitmapImage;
            cad.AnuD = AnuD as BitmapImage;
            cad.AnuE = AnuE as BitmapImage;
            cad.MinD = MinD as BitmapImage;
            cad.MinE = MinE as BitmapImage;

            //Update Informações do grupo das impressões da ID
            cad.grupoPolD = grupoPolD;
            cad.grupoPolE = grupoPolE;
            cad.grupoIndD = grupoIndD;
            cad.grupoIndE = grupoIndE;
            cad.grupoMedD = grupoMedD;
            cad.grupoMedE = grupoMedE;
            cad.grupoAnuD = grupoAnuD;
            cad.grupoAnuE = grupoAnuE;
            cad.grupoMinD = grupoMinD;
            cad.grupoMinE = grupoMinE;

            //Take the ID, checking the text into the string
            if (grupoPolD == "Presilha Interna" || grupoPolD == "Presilha Externa")
            {
                if (grupoPolE == "Presilha Interna" || grupoPolE == "Presilha Externa")
                {
                    
                    ID = (grupoPolD.Substring(9, 1) + grupoIndD.Substring(0, 1) + grupoMedD.Substring(0, 1) + grupoAnuD.Substring(0, 1) + grupoMinD.Substring(0, 1) + "/" + grupoPolE.Substring(9, 1) + grupoIndE.Substring(0, 1) + grupoMedE.Substring(0, 1) + grupoAnuE.Substring(0, 1) + grupoMinE.Substring(0, 1));
                }
                else
                {
                    
                    ID = (grupoPolD.Substring(9, 1)+ grupoIndD.Substring(0, 1) + grupoMedD.Substring(0, 1) + grupoAnuD.Substring(0, 1) + grupoMinD.Substring(0, 1) + "/" + grupoPolE.Substring(0, 1) + grupoIndE.Substring(0, 1) + grupoMedE.Substring(0, 1) + grupoAnuE.Substring(0, 1) + grupoMinE.Substring(0, 1));
                }
                
            }
            else
            {
                if (grupoPolE == "Presilha Interna" || grupoPolE == "Presilha Externa")
                {
                    grupoPolE.ToUpper();
                    ID = (grupoPolD.Substring(0, 1) + grupoIndD.Substring(0, 1) + grupoMedD.Substring(0, 1) + grupoAnuD.Substring(0, 1) + grupoMinD.Substring(0, 1) + "/" + grupoPolE.Substring(9, 1).ToUpper() + grupoIndE.Substring(0, 1) + grupoMedE.Substring(0, 1) + grupoAnuE.Substring(0, 1) + grupoMinE.Substring(0, 1));
                }
                else
                {
                    ID = (grupoPolD.Substring(0, 1) + grupoIndD.Substring(0, 1) + grupoMedD.Substring(0, 1) + grupoAnuD.Substring(0, 1) + grupoMinD.Substring(0, 1) + "/" + grupoPolE.Substring(0, 1) + grupoIndE.Substring(0, 1) + grupoMedE.Substring(0, 1) + grupoAnuE.Substring(0, 1) + grupoMinE.Substring(0, 1));  
                }
                
            }
             
             cad.IdentificacaoDac = ID;
             cad.lblId.Content = cad.IdentificacaoDac;
             cad.FingerInserido = true;

            //Retornando valor ao padrão para não bugar os eventos do Image control
             cad.editMode = false;

             if (cad.id != null)
             {
                 cad.alterFinger = true;
             }

            //Close Window
            btnCancelar_Click(null, null);
            cad.imgDigitalFront.Source = new BitmapImage(new Uri("pack://application:,,,/IMAGES/impressao Digital Recurso Checked transp.png"));
            Storyboard fingerAnimation = cad.FindResource("RotationImageFingerPrint") as Storyboard;
            fingerAnimation.Begin();

        }

        private void SC_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            //Controlando o scroll
            if (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl))
            {
                scDocumento.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;
                ScrollViewer scrollviewer = sender as ScrollViewer;
                if (e.Delta > 0)
                {
                    scrollviewer.LineLeft();
                }
                else
                {
                    scrollviewer.LineRight();
                }
                e.Handled = true;
            }
            else
            {
                scDocumento.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;
                ScrollViewer scrollviewer = sender as ScrollViewer;
                if (e.Delta > 0)
                {
                    scrollviewer.LineUp();
                }
                else
                {
                    scrollviewer.LineDown();
                }
                e.Handled = true;
            }
            

            
        }

        #region Inserir Via Leitor Biométrico
        private void rdbLeitor_Checked(object sender, System.Windows.RoutedEventArgs e)
        {
			btnIniciarLeitor.Visibility = Visibility.Visible;
            btnIniciarLeitor.IsEnabled = true;
			
			grdLeitor.Visibility = Visibility.Visible;
			grdLeitor.IsEnabled = true;
			
			scDocumento.Visibility = Visibility.Hidden;
			scDocumento.IsEnabled = false;
			
			Storyboard animation = this.FindResource("leitorAnimation") as Storyboard;
            animation.Begin();
			
			lblStatusText.Visibility = Visibility.Visible;
			lblStatusLeitor.Visibility = Visibility.Visible;

            
			documento = false;
        }

        private void run()
        {
			Device d = new Device();
            if (!d.Init())
            {
                statusWrite("Dispositivo Desconectado!");
                return;
            }
            statusWrite("Dispositivo Conectado!");


            System.Drawing.Bitmap fingerprint = new System.Drawing.Bitmap(d.ExportBitMapFrame());

           
            //Show
            imgFingerLeitor.Source = ConvertToBI(fingerprint);
			if(!imagemScanner)
			{
				imagemScanner = true;
				
			}
			
            d.SetDiodesStatus(true, true);

            d.SetDiodesStatus(false, false);
            d.Dispose();

           
        }

        private BitmapImage ConvertToBI(System.Drawing.Bitmap bmp)
        {
            //Convert
            //Converter Bitmap To BitmapImage
            MemoryStream ms = new MemoryStream();
            bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
            BitmapImage fingerprintBitmapImage = new BitmapImage();
            fingerprintBitmapImage.BeginInit();
            ms.Seek(0, SeekOrigin.Begin);
            fingerprintBitmapImage.StreamSource = ms;
            fingerprintBitmapImage.EndInit();

            return fingerprintBitmapImage;
        }

        private void statusWrite(string text)
        {
            lblStatusLeitor.Content = text;
        }

        static void Wait(int n)
        {
            int x = 0;
            for (int i = 0; i < n; i++)
                x += i;
            
        }

        static void Window()
        {
            while (d.Connected)
            {
                Thread.Sleep(10000);
                if (d.IsFinger())
                {
                }
            }
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            //Iniciar Captura via Leitor
            run();
        }
		
		private void btnIniciarLeitor_Click(object sender, System.Windows.RoutedEventArgs e)
        {
        	dispatcherTimer.Start();
			rdbDocumento.IsEnabled = false;
			itmAbrirDoc.IsEnabled = false;
			btnParar.IsEnabled = true;
			btnParar.Visibility = Visibility.Visible;
			btnIniciarLeitor.IsEnabled = false;
			btnIniciarLeitor.Visibility = Visibility.Hidden;
        }

        #endregion

        private void rdbDocumento_Checked(object sender, System.Windows.RoutedEventArgs e)
        {
        	scDocumento.Visibility = Visibility.Visible;
			scDocumento.IsEnabled = true;
			
			grdLeitor.Visibility = Visibility.Hidden;
			grdLeitor.IsEnabled = false;
			
			Storyboard animation = this.FindResource("documentoAnimation") as Storyboard;
            animation.Begin();
			
			lblStatusText.Visibility = Visibility.Hidden;
			lblStatusLeitor.Visibility = Visibility.Hidden;
			
			if(dispatcherTimer.IsEnabled)
			{
				dispatcherTimer.Stop();
			}	
			imgFingerLeitor.Source = null;
			documento = true;
			imagemScanner = false;
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
			if(dispatcherTimer.IsEnabled)
			{
				dispatcherTimer.Stop();
			}
            this.Close();
        }

        private void btnParar_Click(object sender, System.Windows.RoutedEventArgs e)
        {
        	dispatcherTimer.Stop();
			rdbDocumento.IsEnabled = true;
			itmAbrirDoc.IsEnabled = true;
			btnParar.IsEnabled = false;
			btnParar.Visibility = Visibility.Hidden;
			btnIniciarLeitor.IsEnabled = true;
			btnIniciarLeitor.Visibility = Visibility.Visible;
			imagemScanner = false;
        }
    }
}
