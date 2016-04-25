using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
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

namespace sistemaCorporativo.FORMS
{
    /// <summary>
    /// Interaction logic for ClassificarFingersPrints.xaml
    /// </summary>
    public partial class ClassificarFingersPrints : MetroWindow
    {
        //Instanciar
        CadFingerPrints cad;
        public ClassificarFingersPrints(CadFingerPrints info)
        {
            InitializeComponent();

            cad = info;
        }


        //Imagem (sources) da ID
        public BitmapSource polD, polE;
        public BitmapSource IndD, IndE;
        public BitmapSource MedD, MedE;
        public BitmapSource AnuD, AnuE;
        public BitmapSource MinD, MinE;
        
        //Grupo da ID (same as the cadFinger)
        public string grupoPolD, grupoPolE;
        public string grupoIndD, grupoIndE;
        public string grupoMedD, grupoMedE;
        public string grupoAnuD, grupoAnuE;
        public string grupoMinD, grupoMinE;

        //thisGroup
        private string thisGroup;
        //thisFinger
        private string thisFinger;
        //It is left?
        private Boolean IsLeft = false;

        private int _dedo;

        private void classFingerWindow_Loaded(object sender, RoutedEventArgs e)
        {
           
           //Receber Informações da Window Anterior
            polD = cad.imgPolD.Source as BitmapSource;
            polE = cad.imgPolE.Source as BitmapSource;
            IndD = cad.imgIndD.Source as BitmapSource;
            IndE = cad.imgIndE.Source as BitmapSource;
            MedD = cad.imgMedD.Source as BitmapSource;
            MedE = cad.imgMedE.Source as BitmapSource;
            AnuD = cad.imgAnuD.Source as BitmapSource;
            AnuE = cad.imgAnuE.Source as BitmapSource;
            MinD = cad.imgMinD.Source as BitmapSource;
            MinE = cad.imgMinE.Source as BitmapSource;
            _dedo = cad.dedos;
            loadSelectedFinger(_dedo);

        }

        private async void loadSelectedFinger(int dedo) 
        {
           switch (dedo)
           {
               case 1:
                   makeLoad(polD, dedo);
                   btnVoltar.IsEnabled = false;
                   thisFinger = "Polegar";
                   break;
               case 2:
                   makeLoad(IndD, dedo);
                   thisFinger = "Indicador";
                   break;
               case 3:
                   makeLoad(MedD, dedo);
                   thisFinger = "Médio";
                   break;
               case 4:
                   makeLoad(AnuD, dedo);
                   thisFinger = "Anular";
                   break;
               case 5:
                   makeLoad(MinD, dedo);
                   thisFinger = "Mínimo";
                   break;
               case 6:
                   makeLoad(polE, dedo);
                   thisFinger = "Polegar";
                   IsLeft = true;
                   break;
               case 7:
                   makeLoad(IndE, dedo);
                   thisFinger = "Indicador";
                   break;
               case 8:
                   makeLoad(MedE, dedo);
                   thisFinger = "Médio";
                   break;
               case 9:
                   makeLoad(AnuE, dedo);
                   thisFinger = "Anular";
                   break;
               case 10:
                   makeLoad(MinE, dedo);
                   thisFinger = "Mínimo";
                   break;
               default:
                   await this.ShowMessageAsync("Aviso","impressão não selecionada no formulário anterior");
                   break;
           } 

        }

        private void makeLoad(BitmapSource bs, int count) 
        {

            imgFingerPrint.Source = bs;

            if (count >= 2)
            {
                btnVoltar.IsEnabled = true;
            }
        }

        private async void btnVoltar_Click(object sender, RoutedEventArgs e)
        {
            switch (_dedo)
           {
               case 2:
                   _dedo = 1;
                   if (grupoPolD != null)
                   {
                       switch (grupoPolD)
                       {
                           case "Arco":
                               rdbArco.IsChecked = true;
                               break;
                           case "Presilha Interna":
                               rdbPresInt.IsChecked = true;
                               break;
                           case "Presilha Externa":
                               rdbPresExt.IsChecked = true;
                               break;
                           case "Verticilo":
                               rdbVert.IsChecked = true;
                               break;
                           case "Amputado":
                               rdbAmpt.IsChecked = true;
                               break;
                           case "Cicatriz":
                               rdbCic.IsChecked = true;
                               break;
                       }
                   }
                   else
                   {
                       rdbArco.IsChecked = false;
                       rdbPresInt.IsChecked = false;
                       rdbPresExt.IsChecked = false;
                       rdbVert.IsChecked = false;
                       rdbAmpt.IsChecked = false;
                       rdbCic.IsChecked = false;
                   }
                   btnProximo.IsEnabled = false;
                   if (grupoIndD != null)
                   {
                       btnProximo.IsEnabled = true;
                   }
                   thisFinger = "Polegar";
                   makeLoad(polD, _dedo);
                   break;
               case 3:
                   _dedo = 2;
                   if (grupoIndD != null)
                   {
                       switch (grupoIndD)
                       {
                           case "1":
                               rdbArco.IsChecked = true;
                               break;
                           case "2":
                               rdbPresInt.IsChecked = true;
                               break;
                           case "3":
                               rdbPresExt.IsChecked = true;
                               break;
                           case "4":
                               rdbVert.IsChecked = true;
                               break;
                           case "0":
                               rdbAmpt.IsChecked = true;
                               break;
                           case "X":
                               rdbCic.IsChecked = true;
                               break;
                       }
                   }
                   else
                   {
                       rdbArco.IsChecked = false;
                       rdbPresInt.IsChecked = false;
                       rdbPresExt.IsChecked = false;
                       rdbVert.IsChecked = false;
                       rdbAmpt.IsChecked = false;
                       rdbCic.IsChecked = false;
                   }
                   btnProximo.IsEnabled = false;
                   if (grupoMedD != null)
                   {
                       btnProximo.IsEnabled = true;
                   }
                   thisFinger = "Indicador";
                   makeLoad(IndD, _dedo);
                   break;
               case 4:
                   _dedo = 3;
                   if (grupoMedD != null)
                   {
                       switch (grupoMedD)
                       {
                           case "1":
                               rdbArco.IsChecked = true;
                               break;
                           case "2":
                               rdbPresInt.IsChecked = true;
                               break;
                           case "3":
                               rdbPresExt.IsChecked = true;
                               break;
                           case "4":
                               rdbVert.IsChecked = true;
                               break;
                           case "0":
                               rdbAmpt.IsChecked = true;
                               break;
                           case "X":
                               rdbCic.IsChecked = true;
                               break;
                       }
                   }
                   else
                   {
                       rdbArco.IsChecked = false;
                       rdbPresInt.IsChecked = false;
                       rdbPresExt.IsChecked = false;
                       rdbVert.IsChecked = false;
                       rdbAmpt.IsChecked = false;
                       rdbCic.IsChecked = false;
                   }
                   btnProximo.IsEnabled = false;
                   if (grupoAnuD != null)
                   {
                       btnProximo.IsEnabled = true;
                   }
                   thisFinger = "Médio";
                   makeLoad(MedD, _dedo);
                   break;
               case 5:
                   _dedo = 4;
                   if (grupoAnuD != null)
                   {
                       switch (grupoAnuD)
                       {
                           case "1":
                               rdbArco.IsChecked = true;
                               break;
                           case "2":
                               rdbPresInt.IsChecked = true;
                               break;
                           case "3":
                               rdbPresExt.IsChecked = true;
                               break;
                           case "4":
                               rdbVert.IsChecked = true;
                               break;
                           case "0":
                               rdbAmpt.IsChecked = true;
                               break;
                           case "X":
                               rdbCic.IsChecked = true;
                               break;
                       }
                   }
                   else
                   {
                       rdbArco.IsChecked = false;
                       rdbPresInt.IsChecked = false;
                       rdbPresExt.IsChecked = false;
                       rdbVert.IsChecked = false;
                       rdbAmpt.IsChecked = false;
                       rdbCic.IsChecked = false;
                   }
                   btnProximo.IsEnabled = false;
                   if (grupoMinD != null)
                   {
                       btnProximo.IsEnabled = true;
                   }
                   thisFinger = "Anular";
                   makeLoad(AnuD, _dedo);
                   break;
               case 6:
                   _dedo = 5;
                   if (grupoMinD != null)
                   {
                       switch (grupoMinD)
                       {
                           case "1":
                               rdbArco.IsChecked = true;
                               break;
                           case "2":
                               rdbPresInt.IsChecked = true;
                               break;
                           case "3":
                               rdbPresExt.IsChecked = true;
                               break;
                           case "4":
                               rdbVert.IsChecked = true;
                               break;
                           case "0":
                               rdbAmpt.IsChecked = true;
                               break;
                           case "X":
                               rdbCic.IsChecked = true;
                               break;
                       }
                   }
                   else
                   {
                       rdbArco.IsChecked = false;
                       rdbPresInt.IsChecked = false;
                       rdbPresExt.IsChecked = false;
                       rdbVert.IsChecked = false;
                       rdbAmpt.IsChecked = false;
                       rdbCic.IsChecked = false;
                   }
                   btnProximo.IsEnabled = false;
                   if (grupoPolE != null)
                   {
                       btnProximo.IsEnabled = true;
                   }
                   thisFinger = "Mínimo";
                   makeLoad(MinD, _dedo);
                   IsLeft = false;
                   break;
               case 7:
                   _dedo = 6;
                   if (grupoPolE != null)
                   {
                       switch (grupoPolE)
                       {
                           case "Arco":
                               rdbArco.IsChecked = true;
                               break;
                           case "Presilha Interna":
                               rdbPresInt.IsChecked = true;
                               break;
                           case "Presilha Externa":
                               rdbPresExt.IsChecked = true;
                               break;
                           case "Verticilo":
                               rdbVert.IsChecked = true;
                               break;
                           case "Amputado":
                               rdbAmpt.IsChecked = true;
                               break;
                           case "Cicatriz":
                               rdbCic.IsChecked = true;
                               break;
                       }
                   }
                   else
                   {
                       rdbArco.IsChecked = false;
                       rdbPresInt.IsChecked = false;
                       rdbPresExt.IsChecked = false;
                       rdbVert.IsChecked = false;
                       rdbAmpt.IsChecked = false;
                       rdbCic.IsChecked = false;
                   }
                   btnProximo.IsEnabled = false;
                   if (grupoIndE != null)
                   {
                       btnProximo.IsEnabled = true;
                   }
                   thisFinger = "Polegar";
                   btnClassificar.IsEnabled = false;
                   makeLoad(polE, _dedo);
                   break;
               case 8:
                   _dedo = 7;
                   if (grupoIndE != null)
                   {
                       switch (grupoIndE)
                       {
                           case "1":
                               rdbArco.IsChecked = true;
                               break;
                           case "2":
                               rdbPresInt.IsChecked = true;
                               break;
                           case "3":
                               rdbPresExt.IsChecked = true;
                               break;
                           case "4":
                               rdbVert.IsChecked = true;
                               break;
                           case "0":
                               rdbAmpt.IsChecked = true;
                               break;
                           case "X":
                               rdbCic.IsChecked = true;
                               break;
                       }
                   }
                   else
                   {
                       rdbArco.IsChecked = false;
                       rdbPresInt.IsChecked = false;
                       rdbPresExt.IsChecked = false;
                       rdbVert.IsChecked = false;
                       rdbAmpt.IsChecked = false;
                       rdbCic.IsChecked = false;
                   }
                   btnProximo.IsEnabled = false;
                   if (grupoMedE != null)
                   {
                       btnProximo.IsEnabled = true;
                   }
                   thisFinger = "Indicador";
                   btnClassificar.IsEnabled = false;
                   makeLoad(IndE, _dedo);
                   break;
               case 9:
                   _dedo = 8;
                   if (grupoMedE != null)
                   {
                       switch (grupoMedE)
                       {
                           case "1":
                               rdbArco.IsChecked = true;
                               break;
                           case "2":
                               rdbPresInt.IsChecked = true;
                               break;
                           case "3":
                               rdbPresExt.IsChecked = true;
                               break;
                           case "4":
                               rdbVert.IsChecked = true;
                               break;
                           case "0":
                               rdbAmpt.IsChecked = true;
                               break;
                           case "X":
                               rdbCic.IsChecked = true;
                               break;
                       }
                   }
                   else
                   {
                       rdbArco.IsChecked = false;
                       rdbPresInt.IsChecked = false;
                       rdbPresExt.IsChecked = false;
                       rdbVert.IsChecked = false;
                       rdbAmpt.IsChecked = false;
                       rdbCic.IsChecked = false;
                   }
                   btnProximo.IsEnabled = false;
                   if (grupoAnuE != null)
                   {
                       btnProximo.IsEnabled = true;
                   }
                   thisFinger = "Médio";
                   btnClassificar.IsEnabled = false;
                   makeLoad(MedE, _dedo);
                   break;
               case 10:

                   btnFinalizar.IsEnabled = false;
                   btnFinalizar.Visibility = Visibility.Hidden;
                   btnProximo.IsEnabled = true;
                   btnProximo.Visibility = Visibility.Visible;
                   if (grupoAnuE != null)
                   {
                       switch (grupoAnuE)
                       {
                           case "1":
                               rdbArco.IsChecked = true;
                               break;
                           case "2":
                               rdbPresInt.IsChecked = true;
                               break;
                           case "3":
                               rdbPresExt.IsChecked = true;
                               break;
                           case "4":
                               rdbVert.IsChecked = true;
                               break;
                           case "0":
                               rdbAmpt.IsChecked = true;
                               break;
                           case "X":
                               rdbCic.IsChecked = true;
                               break;
                       }
                   }
                   else
                   {
                       rdbArco.IsChecked = false;
                       rdbPresInt.IsChecked = false;
                       rdbPresExt.IsChecked = false;
                       rdbVert.IsChecked = false;
                       rdbAmpt.IsChecked = false;
                       rdbCic.IsChecked = false;
                   }
                   btnProximo.IsEnabled = false;
                   if (grupoMinE != null)
                   {
                       btnProximo.IsEnabled = true;
                   }

                   thisFinger = "Anular";
                   _dedo = 9;
                   btnClassificar.IsEnabled = false;
                   makeLoad(AnuE, _dedo);
                   break;
               default:
                    await this.ShowMessageAsync("Aviso","Impossível voltar!");
                   break;
                    }
          
        }

        private async void btnProximo_Click(object sender, RoutedEventArgs e)
        {
            switch (_dedo)
            {
                case 1:
                    if (cad.hasimage2)
                    {
                        thisFinger = "Indicador";
                        clearSelections();
                        thisGroup = null;
                        _dedo = 2;
                        makeLoad(IndD, _dedo);
                        if (grupoIndD != null)
                        {
                            switch (grupoIndD)
                            {
                                case "1":
                                    rdbArco.IsChecked = true;
                                    break;
                                case "2":
                                    rdbPresInt.IsChecked = true;
                                    break;
                                case "3":
                                    rdbPresExt.IsChecked = true;
                                    break;
                                case "4":
                                    rdbVert.IsChecked = true;
                                    break;
                                case "0":
                                    rdbAmpt.IsChecked = true;
                                    break;
                                case "X":
                                    rdbCic.IsChecked = true;
                                    break;
                            }
                            btnProximo.IsEnabled = true;
                        }
                        else
                        {
                            btnProximo.IsEnabled = false;
                        }
                    }
                    else
                    {
                        btnProximo.IsEnabled = false;
                    }
                    

                    break;
                case 2:
                    if (cad.hasimage3)
                    {
                        thisFinger = "Médio";
                        clearSelections();
                        thisGroup = null;
                        _dedo = 3;
                        makeLoad(MedD, _dedo);
                        if (grupoMedD != null)
                        {
                            switch (grupoMedD)
                            {
                                case "1":
                                    rdbArco.IsChecked = true;
                                    break;
                                case "2":
                                    rdbPresInt.IsChecked = true;
                                    break;
                                case "3":
                                    rdbPresExt.IsChecked = true;
                                    break;
                                case "4":
                                    rdbVert.IsChecked = true;
                                    break;
                                case "0":
                                    rdbAmpt.IsChecked = true;
                                    break;
                                case "X":
                                    rdbCic.IsChecked = true;
                                    break;
                            }
                            btnProximo.IsEnabled = true;
                        }
                        else
                        {
                            btnProximo.IsEnabled = false;
                        }
                    }
                    else
                    {
                        btnProximo.IsEnabled = false;
                    }

                    break;
                case 3:

                    if (cad.hasimage4)
                    {
                        thisFinger = "Anular";
                        clearSelections();
                        thisGroup = null;
                        _dedo = 4;
                        makeLoad(AnuD, _dedo);
                        if (grupoAnuD != null)
                        {
                            switch (grupoAnuD)
                            {
                                case "1":
                                    rdbArco.IsChecked = true;
                                    break;
                                case "2":
                                    rdbPresInt.IsChecked = true;
                                    break;
                                case "3":
                                    rdbPresExt.IsChecked = true;
                                    break;
                                case "4":
                                    rdbVert.IsChecked = true;
                                    break;
                                case "0":
                                    rdbAmpt.IsChecked = true;
                                    break;
                                case "X":
                                    rdbCic.IsChecked = true;
                                    break;
                            }
                            btnProximo.IsEnabled = true;
                        }
                        else
                        {
                            btnProximo.IsEnabled = false;
                        }
                    }
                    else
                    {
                        btnProximo.IsEnabled = false;
                    }
                   

                    break;
                case 4:
                    if (cad.hasimage5)
                    {
                        thisFinger = "Mínimo";
                        clearSelections();
                        thisGroup = null;
                        _dedo = 5;
                        makeLoad(MinD, _dedo);
                        if (grupoMinD != null)
                        {
                            switch (grupoMinD)
                            {
                                case "1":
                                    rdbArco.IsChecked = true;
                                    break;
                                case "2":
                                    rdbPresInt.IsChecked = true;
                                    break;
                                case "3":
                                    rdbPresExt.IsChecked = true;
                                    break;
                                case "4":
                                    rdbVert.IsChecked = true;
                                    break;
                                case "0":
                                    rdbAmpt.IsChecked = true;
                                    break;
                                case "X":
                                    rdbCic.IsChecked = true;
                                    break;
                            }
                            btnProximo.IsEnabled = true;
                        }
                        else
                        {
                            btnProximo.IsEnabled = false;
                        }
                    }
                    else
                    {
                        btnProximo.IsEnabled = false;
                    }
                    

                    break;
                case 5:
                    if (cad.hasimage6)
                    {
                        thisFinger = "Polegar";
                        IsLeft = true;
                        clearSelections();
                        thisGroup = null;
                        _dedo = 6;
                        makeLoad(polE, _dedo);
                        if (grupoPolE != null)
                        {
                            switch (grupoPolE)
                            {
                                case "Arco":
                                    rdbArco.IsChecked = true;
                                    break;
                                case "Presilha Interna":
                                    rdbPresInt.IsChecked = true;
                                    break;
                                case "Presilha Externa":
                                    rdbPresExt.IsChecked = true;
                                    break;
                                case "Verticilo":
                                    rdbVert.IsChecked = true;
                                    break;
                                case "Amputado":
                                    rdbAmpt.IsChecked = true;
                                    break;
                                case "Cicatriz":
                                    rdbCic.IsChecked = true;
                                    break;
                            }
                            btnProximo.IsEnabled = true;
                        }
                        else
                        {
                            btnProximo.IsEnabled = false;
                        }
                    }
                    else
                    {
                        btnProximo.IsEnabled = false;
                    }
                    

                    break;
                case 6:
                    if (cad.hasimage7)
                    {
                        thisFinger = "Indicador";
                        clearSelections();
                        thisGroup = null;
                        _dedo = 7;
                        makeLoad(IndE, _dedo);
                        if (grupoIndE != null)
                        {
                            switch (grupoIndE)
                            {
                                case "1":
                                    rdbArco.IsChecked = true;
                                    break;
                                case "2":
                                    rdbPresInt.IsChecked = true;
                                    break;
                                case "3":
                                    rdbPresExt.IsChecked = true;
                                    break;
                                case "4":
                                    rdbVert.IsChecked = true;
                                    break;
                                case "0":
                                    rdbAmpt.IsChecked = true;
                                    break;
                                case "X":
                                    rdbCic.IsChecked = true;
                                    break;
                            }
                            btnProximo.IsEnabled = true;
                        }
                        else
                        {
                            btnProximo.IsEnabled = false;
                        }
                    }
                    else
                    {
                        btnProximo.IsEnabled = false;
                    }

                    break;
                case 7:
                    if (cad.hasimage8)
                    {
                        thisFinger = "Médio";
                        clearSelections();
                        thisGroup = null;
                        _dedo = 8;
                        makeLoad(MedE, _dedo);
                        if (grupoMedE != null)
                        {
                            switch (grupoMedE)
                            {
                                case "1":
                                    rdbArco.IsChecked = true;
                                    break;
                                case "2":
                                    rdbPresInt.IsChecked = true;
                                    break;
                                case "3":
                                    rdbPresExt.IsChecked = true;
                                    break;
                                case "4":
                                    rdbVert.IsChecked = true;
                                    break;
                                case "0":
                                    rdbAmpt.IsChecked = true;
                                    break;
                                case "X":
                                    rdbCic.IsChecked = true;
                                    break;
                            }
                            btnProximo.IsEnabled = true;
                        }
                        else
                        {
                            btnProximo.IsEnabled = false;
                        }
                    }
                    else
                    {
                        btnProximo.IsEnabled = false;
                    }
                    

                    break;
                case 8:
                    if (cad.hasimage9)
                    {
                        thisFinger = "Anular";
                        clearSelections();
                        thisGroup = null;
                        _dedo = 9;
                        makeLoad(AnuE, _dedo);
                        if (grupoAnuE != null)
                        {
                            switch (grupoAnuE)
                            {
                                case "1":
                                    rdbArco.IsChecked = true;
                                    break;
                                case "2":
                                    rdbPresInt.IsChecked = true;
                                    break;
                                case "3":
                                    rdbPresExt.IsChecked = true;
                                    break;
                                case "4":
                                    rdbVert.IsChecked = true;
                                    break;
                                case "0":
                                    rdbAmpt.IsChecked = true;
                                    break;
                                case "X":
                                    rdbCic.IsChecked = true;
                                    break;
                            }
                            btnProximo.IsEnabled = true;
                        }
                        else
                        {
                            btnProximo.IsEnabled = false;
                        }
                    }
                    else
                    {
                        btnProximo.IsEnabled = false;
                    }
                   
                    break;
                case 9:
                    if (cad.hasimage10)
                    {
                        thisFinger = "Mínimo";
                        clearSelections();
                        thisGroup = null;
                        _dedo = 10;
                        makeLoad(MinE, _dedo);
                        if (grupoMinE != null)
                        {
                            switch (grupoMinE)
                            {
                                case "1":
                                    rdbArco.IsChecked = true;
                                    break;
                                case "2":
                                    rdbPresInt.IsChecked = true;
                                    break;
                                case "3":
                                    rdbPresExt.IsChecked = true;
                                    break;
                                case "4":
                                    rdbVert.IsChecked = true;
                                    break;
                                case "0":
                                    rdbAmpt.IsChecked = true;
                                    break;
                                case "X":
                                    rdbCic.IsChecked = true;
                                    break;
                            }
                            btnProximo.IsEnabled = false;
                            btnProximo.Visibility = Visibility.Hidden;
                            btnFinalizar.Visibility = Visibility.Visible;
                            btnFinalizar.IsEnabled = true;
                        }
                        else
                        {
                            btnProximo.IsEnabled = false;
                        }
                        //Arrumar os botões
                        btnClassificar.IsEnabled = false;
                        btnProximo.IsEnabled = false;
                    }
                    else
                    {
                        btnProximo.IsEnabled = false;
                    }
                    
                    

                    break;
                default:
                    await this.ShowMessageAsync("Aviso", "Impossível Avançar!");
                    break;
            }
           
        }

        private void btnClassificar_Click(object sender, RoutedEventArgs e)
        {
            makeClassification(_dedo, thisFinger, thisGroup, IsLeft);
            btnClassificar.IsEnabled = false;
            btnProximo.IsEnabled = true;
            if (_dedo == 10)
            {
                 btnProximo.IsEnabled = false;
                 btnProximo.Visibility = Visibility.Hidden;
                 btnFinalizar.IsEnabled = true;
                 btnFinalizar.Visibility = Visibility.Visible;
            }
        }

        private void makeClassification(int dedo, string tF, string tG, Boolean iL) 
        {
            if (iL)
            {
                switch (tF)
                {
                    case "Polegar":
                        grupoPolE = tG;
                        break;
                    case "Indicador":

                        switch (tG)
                        {
                            case "Arco":
                                tG = "1";
                                break;
                            case "Presilha Interna":
                                tG = "2";
                                break;
                            case "Presilha Externa":
                                tG = "3";
                                break;
                            case "Verticilo":
                                tG = "4";
                                break;
                            case "Amputado":
                                tG = "0";
                                break;
                            case "Cicatriz":
                                tG = "X";
                                break;   
                        }
                        grupoIndE = tG;

                        break;

                    case "Médio":

                        switch (tG)
                        {
                            case "Arco":
                                tG = "1";
                                break;
                            case "Presilha Interna":
                                tG = "2";
                                break;
                            case "Presilha Externa":
                                tG = "3";
                                break;
                            case "Verticilo":
                                tG = "4";
                                break;
                            case "Amputado":
                                tG = "0";
                                break;
                            case "Cicatriz":
                                tG = "X";
                                break;
                        }
                        grupoMedE = tG;

                        break;

                    case "Anular":

                        switch (tG)
                        {
                            case "Arco":
                                tG = "1";
                                break;
                            case "Presilha Interna":
                                tG = "2";
                                break;
                            case "Presilha Externa":
                                tG = "3";
                                break;
                            case "Verticilo":
                                tG = "4";
                                break;
                            case "Amputado":
                                tG = "0";
                                break;
                            case "Cicatriz":
                                tG = "X";
                                break;
                        }
                        grupoAnuE = tG;

                        break;

                    case "Mínimo":

                        switch (tG)
                        {
                            case "Arco":
                                tG = "1";
                                break;
                            case "Presilha Interna":
                                tG = "2";
                                break;
                            case "Presilha Externa":
                                tG = "3";
                                break;
                            case "Verticilo":
                                tG = "4";
                                break;
                            case "Amputado":
                                tG = "0";
                                break;
                            case "Cicatriz":
                                tG = "X";
                                break;
                        }
                        grupoMinE = tG;

                        break;
                }
            }

            if (iL == false)
            {
                switch (tF)
                {
                    case "Polegar":
                        grupoPolD = tG;
                        break;
                    case "Indicador":

                        switch (tG)
                        {
                            case "Arco":
                                tG = "1";
                                break;
                            case "Presilha Interna":
                                tG = "2";
                                break;
                            case "Presilha Externa":
                                tG = "3";
                                break;
                            case "Verticilo":
                                tG = "4";
                                break;
                            case "Amputado":
                                tG = "0";
                                break;
                            case "Cicatriz":
                                tG = "X";
                                break;   
                        }
                        grupoIndD = tG;

                        break;

                    case "Médio":

                        switch (tG)
                        {
                            case "Arco":
                                tG = "1";
                                break;
                            case "Presilha Interna":
                                tG = "2";
                                break;
                            case "Presilha Externa":
                                tG = "3";
                                break;
                            case "Verticilo":
                                tG = "4";
                                break;
                            case "Amputado":
                                tG = "0";
                                break;
                            case "Cicatriz":
                                tG = "X";
                                break;
                        }
                        grupoMedD = tG;

                        break;

                    case "Anular":

                        switch (tG)
                        {
                            case "Arco":
                                tG = "1";
                                break;
                            case "Presilha Interna":
                                tG = "2";
                                break;
                            case "Presilha Externa":
                                tG = "3";
                                break;
                            case "Verticilo":
                                tG = "4";
                                break;
                            case "Amputado":
                                tG = "0";
                                break;
                            case "Cicatriz":
                                tG = "X";
                                break;
                        }
                        grupoAnuD = tG;

                        break;

                    case "Mínimo":

                        switch (tG)
                        {
                            case "Arco":
                                tG = "1";
                                break;
                            case "Presilha Interna":
                                tG = "2";
                                break;
                            case "Presilha Externa":
                                tG = "3";
                                break;
                            case "Verticilo":
                                tG = "4";
                                break;
                            case "Amputado":
                                tG = "0";
                                break;
                            case "Cicatriz":
                                tG = "X";
                                break;
                        }
                        grupoMinD = tG;

                        break;
                }
            
            }
   
        }

        private void rdbArco_Checked(object sender, RoutedEventArgs e)
        {
            thisGroup = "Arco";
            btnClassificar.IsEnabled = true;
        }

        private void rdbPresInt_Checked(object sender, RoutedEventArgs e)
        {
            thisGroup = "Presilha Interna";
            btnClassificar.IsEnabled = true;
        }

        private void rdbPresExt_Checked(object sender, RoutedEventArgs e)
        {
            thisGroup = "Presilha Externa";
            btnClassificar.IsEnabled = true;
        }

        private void rdbVert_Checked(object sender, RoutedEventArgs e)
        {
            thisGroup = "Verticilo";
            btnClassificar.IsEnabled = true;
        }

        private void rdbAmpt_Checked(object sender, RoutedEventArgs e)
        {
            thisGroup = "Amputado";
            btnClassificar.IsEnabled = true;
        }

        private void rdbCic_Checked(object sender, RoutedEventArgs e)
        {
            thisGroup = "Cicatriz";
            btnClassificar.IsEnabled = true;
        }

        private async void btnFinalizar_Click(object sender, RoutedEventArgs e)
        {
            if (grupoPolD != null && grupoPolE != null && grupoIndD != null && grupoIndE != null && grupoMedD != null && grupoMedE != null && grupoAnuD != null && grupoAnuE != null && grupoMinD != null && grupoMinE != null)
            {
                //Update
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
                cad.btnFinalizar.IsEnabled = true;
                cad.lblClassificada.Content = "Impressões Classificadas!";
                //Green Color
                cad.lblClassificada.Foreground =  new SolidColorBrush(Color.FromRgb(38, 182, 24));
                
                //Fechar
                this.Close();
            }
            else
            {
                await this.ShowMessageAsync("Aviso", "Uma impressão digital não foi classificada!");
            }
        }

        private void clearSelections()
        {
            rdbArco.IsChecked = false;
            rdbPresInt.IsChecked = false;
            rdbPresExt.IsChecked = false;
            rdbVert.IsChecked = false;
            rdbAmpt.IsChecked = false;
            rdbCic.IsChecked = false;
        }

    }
}
