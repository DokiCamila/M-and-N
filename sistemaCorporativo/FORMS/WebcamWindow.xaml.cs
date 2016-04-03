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
        public WebcamWindow()
        {
            InitializeComponent();
            
        }
        //Criar atributos com informações e Captura de frame
        private FilterInfoCollection webcam;
        private VideoCaptureDevice cam;
        //Criar variável para inverse da camera
        private Boolean inverse = false;
        //Criar variável para foto tirada
        private Boolean photoShot = false;
        
        private async void btnTirarFoto_Click(object sender, RoutedEventArgs e)
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
            //Iniciar a camera e mostrar no picture Box[form control] por meio do evento
            cam = new VideoCaptureDevice(webcam[cmbDevices.SelectedIndex].MonikerString);
            cam.NewFrame += new NewFrameEventHandler(cam_NewFrame);
            cam.Start();
        }
        void cam_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            //Evento em que mostra o frame no picture box
            Bitmap bit = (Bitmap)eventArgs.Frame.Clone();
            pbCamera.Image = bit;

        } 
        
        private async void btnPronto_Click(object sender, RoutedEventArgs e)
        {
            if (photoShot == true)
            {
                //Capturar frame que parar e usar no source do cadagente
               
                
                System.Windows.Forms.SaveFileDialog saveFileDialog = new System.Windows.Forms.SaveFileDialog();
                saveFileDialog.InitialDirectory = "c:\fotos";
                saveFileDialog.FileName = "foto";
                saveFileDialog.Filter = "Images|*.png;*.bmp;*.jpg";
                ImageFormat format = ImageFormat.Png;

                if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    string ext = System.IO.Path.GetExtension(saveFileDialog.FileName);
                    switch (ext)
                    {
                        case ".jpg":
                            format = ImageFormat.Jpeg;
                            break;
                        case ".bmp":
                            format = ImageFormat.Bmp;
                            break;
                    }
                    pbCamera.Image.Save(saveFileDialog.FileName, format);
                    photoShot = false;
                    inverse = false;
                    this.Close();
                
                }
               
            }
            else
            {
                System.Windows.MessageBox.Show("Tire uma foto antes!", "Aviso");
            }
            
        }

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

        private void btnParar_Click(object sender, RoutedEventArgs e)
        {
            //Parar camera se estiver rodando
            if (cam.IsRunning)
            {
                cam.Stop();
                pbCamera.Image = null;
                inverse = false;
            }
            
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
            if (cam.IsRunning)
            {
                //Checar se esta o inverso é verdadeiro para espelhar
                if (inverse == true)
                {
                    cam.Stop();
                    btnComeçar_Click(sender, e);
                }
                else
                {
                    cam.Stop();
                    cam = new VideoCaptureDevice(webcam[cmbDevices.SelectedIndex].MonikerString);
                    cam.NewFrame += new NewFrameEventHandler(cam_NewFrameMirrortwo);
                    cam.Start();
                    inverse = true;
                } 
            }
           
        }

        private void cam_NewFrameMirrortwo(object sender, NewFrameEventArgs eventArgs)
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
 

