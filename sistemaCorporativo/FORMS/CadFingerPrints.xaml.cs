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



namespace sistemaCorporativo.FORMS
{
    /// <summary>
    /// Interaction logic for CadFingerPrints.xaml
    /// </summary>
    public partial class CadFingerPrints : MetroWindow
    {
        
        
        //Boolean Nothing
        Boolean nothing = true;
        
        //Dedos da mão
        private int dedos = 0;

        private bool isDragging = false;
        private Point anchorPoint = new Point();

        public CadFingerPrints()
        {
            InitializeComponent();

            cnvDocumento.MouseLeftButtonDown += new MouseButtonEventHandler(imgDocumento_MouseLeftButtonDown);
            cnvDocumento.MouseMove += new MouseEventHandler(imgDocumento_MouseMove);
            cnvDocumento.MouseLeftButtonUp += new MouseButtonEventHandler(imgDocumento_MouseLeftButtonUp);
        }

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
        }


        private void itmAbrirDoc_Click(object sender, RoutedEventArgs e)
        {
                Microsoft.Win32.OpenFileDialog op = new Microsoft.Win32.OpenFileDialog();
                op.Title = "Selecione uma impressão digital!";
                op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
                  "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
                  "Portable Network Graphic (*.png)|*.png";
                if (op.ShowDialog() == true)
                {
                    //Receber Documento
                    imgDocumento.Source = new BitmapImage(new Uri(op.FileName));
                    cnvDocumento.Cursor = Cursors.Cross;
                    nothing = false;

                }
        }

        private void btnRemoverDoc_Click(object sender, RoutedEventArgs e)
        {
            imgDocumento.Source = new BitmapImage(new Uri("pack://application:,,,/IMAGES/nothing.png"));
            cnvDocumento.Cursor = Cursors.Arrow;
            nothing = true;
        }

        private void imgDocumento_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (nothing == false)
            {
                if (isDragging == false)
                {
                    anchorPoint.X = e.GetPosition(BackPanel).X;
                    anchorPoint.Y = e.GetPosition(BackPanel).Y;
                    isDragging = true;
                }
            }
            
        }

        private void imgDocumento_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                double x = e.GetPosition(BackPanel).X;
                double y = e.GetPosition(BackPanel).Y;
                selectionRectangle.SetValue(Canvas.LeftProperty, Math.Min(x, anchorPoint.X));
                selectionRectangle.SetValue(Canvas.TopProperty, Math.Min(y, anchorPoint.Y));
                selectionRectangle.Width = Math.Abs(x - anchorPoint.X);
                selectionRectangle.Height = Math.Abs(y - anchorPoint.Y);

                if (selectionRectangle.Visibility != Visibility.Visible)
                    selectionRectangle.Visibility = Visibility.Visible;
            }
        }

        private void imgDocumento_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (isDragging)
            {
                isDragging = false;
                if (selectionRectangle.Width > 0)
                {
                   //Botao Liberado
                }
                if (selectionRectangle.Visibility != Visibility.Visible)
                    selectionRectangle.Visibility = Visibility.Visible;
            }
        }

        private void RestRect()
        {
            selectionRectangle.Visibility = Visibility.Collapsed;
            isDragging = false;
        }

        private async void btnInserir_Click(object sender, RoutedEventArgs e)
        {
            if (imgDocumento.Source != null && selectionRectangle.Visibility == Visibility.Visible)
            {
                switch (dedos)
                {
                    case 1:
                        inserirImpressao(imgPolD);
                        rdbIndD.IsChecked = true;
                        break;
                    case 2:
                        inserirImpressao(imgIndD);
                        rdbMedD.IsChecked = true;
                        break;
                    case 3:
                        inserirImpressao(imgMedD);
                        rdbAnuD.IsChecked = true;
                        break;
                    case 4:
                        inserirImpressao(imgAnuD);
                        rdbMinD.IsChecked = true;
                        break;
                    case 5:
                        inserirImpressao(imgMinD);
                        rdbPolE.IsChecked = true;
                        break;
                    case 6:
                        inserirImpressao(imgPolE);
                        rdbIndE.IsChecked = true;
                        break;
                    case 7:
                        inserirImpressao(imgIndE);
                        rdbMedE.IsChecked = true;
                        break;
                    case 8:
                        inserirImpressao(imgMedE);
                        rdbAnuE.IsChecked = true;
                        break;
                    case 9:
                        inserirImpressao(imgAnuE);
                        rdbMinE.IsChecked = true;
                        break;
                    case 10:
                        inserirImpressao(imgMinE);
                        break;
                    default:
                        await this.ShowMessageAsync("Aviso", "Selecione um dedo!");
                        break;
                }
                RestRect();

            }
            else
            {
                await this.ShowMessageAsync("Aviso", "Abra um documento e selecione uma impressão para inserir");
            }
            
        }

        private void inserirImpressao(Image impressao) 
        {
            if (imgDocumento.Source != null)
           {
               var imagePosition = imgDocumento.TransformToAncestor(imgDocumento).Transform(new Point(0, 0));
               Rect rect1 = new Rect(Canvas.GetLeft(selectionRectangle), Canvas.GetTop(selectionRectangle), selectionRectangle.Width, selectionRectangle.Height);
               System.Windows.Int32Rect rcFrom = new System.Windows.Int32Rect();
               rcFrom.X = (int)((rect1.X) * (imgDocumento.Source.Width) / (imgDocumento.Width));
               rcFrom.Y = (int)((rect1.Y) * (imgDocumento.Source.Height) / (imgDocumento.Height));
               rcFrom.Width = (int)((rect1.Width) * (imgDocumento.Source.Width) / (imgDocumento.Width));
               rcFrom.Height = (int)((rect1.Height) * (imgDocumento.Source.Height) / (imgDocumento.Height));
               BitmapSource bs = new CroppedBitmap(imgDocumento.Source as BitmapSource, rcFrom);
               impressao.Source = bs; 
            }
        }

        private void btnClassificar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MetroWindow_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                btnInserir_Click(sender, e);
                RestRect();
            }
        }
        private void itmSair_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
   
    }
}
