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
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using MahApps.Metro.Actions;
using MahApps.Metro.Behaviours;
using System.Drawing;
using AForge.Video;
using AForge.Video.DirectShow;
using AForge.Imaging.Filters;
using sistemaCorporativo.FORMS.cadAgente;
using System.IO;
using Microsoft.Win32;
using System.Drawing.Imaging;



namespace sistemaCorporativo.FORMS
{
    /// <summary>
    /// Interaction logic for WebcamWindow.xaml
    /// </summary>
    public partial class WebcamWindow : MetroWindow
    {

        //Istanciar com informações do agente
        CadAgente agente;
        public WebcamWindow(CadAgente agenteinfo)
        {
            InitializeComponent();
            agente = agenteinfo;
      
            
        }
        //Criar atributos com informações e Captura de frame
        private FilterInfoCollection webcam;
        private VideoCaptureDevice cam;
        //Criar variável para inverse da camera
        private Boolean inverse = false;
        //Criar variável para foto tirada
        private Boolean photoShot = false;

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {

            //Listar Dispositivos
            webcam = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo VideoCaptureDevice in webcam)
            {
                cmbDevices.Items.Add(VideoCaptureDevice.Name);
            }
            cmbDevices.SelectedIndex = 0;
            cam = new VideoCaptureDevice();

        }
        
        private void btnTirarFoto_Click(object sender, RoutedEventArgs e)
        {
            if (cam.IsRunning)
            {
                //Parar Camera
                cam.Stop();
                photoShot = true;
            }
            else
            {
                System.Windows.MessageBox.Show("Ligue a camera primeiro!", "Aviso");;
            }
            
        }

        private void btnComeçar_Click(object sender, RoutedEventArgs e)
        {
            if (photoShot == false)
            {
                //Iniciar a camera e mostrar no picture Box[form control] por meio do evento
                cam = new VideoCaptureDevice(webcam[cmbDevices.SelectedIndex].MonikerString);
                cam.NewFrame += new NewFrameEventHandler(cam_NewFrame);
                cam.Start();
                photoShot = false;
            }
                  
        }
        void cam_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            //Evento em que mostra o frame no picture box
            Bitmap bit = (Bitmap)eventArgs.Frame.Clone();
            pbCamera.Image = bit;

        } 
        
        private void btnPronto_Click(object sender, RoutedEventArgs e)
        {
            if (photoShot == true)
            {
                //Converter Bitmap To BitmapImage
                System.Drawing.Bitmap pp = new System.Drawing.Bitmap(pbCamera.Image);
                MemoryStream ms = new MemoryStream();
                pp.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                BitmapImage ppnew = new BitmapImage();
                ppnew.BeginInit();
                ms.Seek(0, SeekOrigin.Begin);
                ppnew.StreamSource = ms;
                ppnew.EndInit();

                //Abrir Cropping
                CortarImagem newCropWindow = new CortarImagem(agente, ppnew);
                newCropWindow.ShowDialog();
                this.Close();
               
            }
            else
            {
                System.Windows.MessageBox.Show("Tire uma foto antes!", "Aviso");
            }
            
        }

        private void btnParar_Click(object sender, RoutedEventArgs e)
        {
            //Parar camera se estiver rodando
            if (cam.IsRunning)
            {
                cam.Stop();   
            }
            pbCamera.Image = null;
            inverse = false;
            photoShot = false;
            
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }


        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            if (cam.IsRunning)
            {
                cam.Stop();
            }
            this.Close();
        }

        private void btnMirror_Click(object sender, RoutedEventArgs e)
        {
            if (photoShot == false)
            {
                if (cam.IsRunning)
                {
                    //Checar se o inverso é verdadeiro para espelhar
                    if (inverse == true)
                    {
                        cam.Stop();
                        btnComeçar_Click(sender, e);
                    }
                    else
                    {
                        cam.Stop();
                        cam = new VideoCaptureDevice(webcam[cmbDevices.SelectedIndex].MonikerString);
                        cam.NewFrame += new NewFrameEventHandler(cam_NewFrameMirror);
                        cam.Start();
                        inverse = true;
                       
                    }
                }
            }
            
        }

        private void cam_NewFrameMirror(object sender, NewFrameEventArgs eventArgs)
        {
            //Evento para espelhar espelhar
            try
            {
                Bitmap bitmirror = (Bitmap)eventArgs.Frame.Clone();

                Mirror filter = new Mirror(false, true);
                filter.ApplyInPlace(bitmirror);

                pbCamera.Image = bitmirror;


            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }
        }

     }
            
}
 

