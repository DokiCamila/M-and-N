   M S B A M L   `   `   `  ����T  PMahApps.Metro, Version=1.1.2.0, Culture=neutral, PublicKeyToken=f4fb5a3c4d1e5b4fV;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.MetroMahApps.Metro.Controls  5   	   Z=clr-namespace:MahApps.Metro.Behaviours;assembly=MahApps.MetroMahApps.Metro.Behaviours  5   	   [ WSystem.Windows.Forms, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089Y@clr-namespace:System.Windows.Forms;assembly=System.Windows.FormsSystem.Windows.Forms 5   	    sistemaCorporativo+   %sistemaCorporativo.FORMS.WebcamWindow   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   PControls;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.Metro     5   	   T
Behaviours=clr-namespace:MahApps.Metro.Behaviours;assembly=MahApps.Metro     5   	   Owf@clr-namespace:System.Windows.Forms;assembly=System.Windows.Forms     5   	   -   5          � Title$  Camera��5   	   $��508.33��6   $��373.17��6(   (   "MahApps.Metro.Controls.MetroWindow   ShowCloseButton .   � 
ResizeMode$ NoResize=�5   !    � WindowStartupLocation$ CenterScreen=�67    � WindowStyle$ 
ToolWindow=�6x    � ShowInTaskbar .  .��5
      �  � RowDefinitions 5   
   �� 5      $
��453* �6   �� 5      $
��140* �6    � ColumnDefinitions 5   
   �� 5      $	��44* �6   �� 5      $
��195* �6   �� 5      $
��195* �6   �� 5      $	��44* �6   .+�5   
   7  1System.Windows.Forms.Integration.WindowsFormsHost  $��4��6   $��
11,44,11,0q�6/   $	��314��6C   $	��Top=�6P      Child. 5      %  System.Windows.Forms.PictureBox  -   5        pbCamera��5   0   5      �� 5   
   -   5        	 ��Name$	 btnTirarFoto��5         MetroCircleButtonStyle#���   6'   $�� ��6X   $
��Left=�6c   $��
108,30,0,0q�6~   $��80��6�   ���	#FFB21F1F
 � ToolTip$
 Tirar foto!��6�   $��74��6�   $	��Top=�6�   $��2��6	  $��1��6  $��1��6-  �� 5   
   -   5        $	 btnComeçar��5        SquareButtonStyle#���  6%   $��Começar��6Q   $
��Left=�6c   $��	10,30,0,0q�6~   $��96��6�   $��33��6�   $	��Top=�6�   $��2��6�   $��1��6�   �� 5   
   -   5        $	 	btnPronto��5        AccentedSquareButtonStyle#���  6$   $��pronto��6X   $
��Left=�6i   $��	77,30,0,0q�6�   $��96��6�   $��33��6�   $	��Top=�6�   $��1��6�   $��2��6�   $��2��6�   �� 5   
   -   5        $	 btnParar��5      #���  6#   $��Parar��6O   $
��Left=�6_   $��	10,68,0,0q�6z   $��96��6�   $��33��6�   $	��Top=�6�   $��1��6�   $��2��6�   �� 5   
   -   5        $	 btnClose��5      #���   6#   $��X��6T   $
��Left=�6`   $	��Top=�6{   $��33��6�   $��32��6�   $��1,0,0,0q�6�   $��3��6�   $
 Fechar��6�   �� 5   
   -   5         ��Name$ 
cmbDevices��5      $��4��6'   $
��Left=�6;   $��
11,363,0,0q�6V   $	��345��6j   $��26��6v   $	��Top=�6�   $��2��6�   �� 5   
   -	   5        $	 	btnMirror��5      #���   6$   $�� ��6U   $
��Left=�6`   $	��Top=�6{   $��41��6�   $��41��6�   $��
108,63,0,0q�6�   $��2��6�   $��1��6�   $
 Girar na horizontal��6�   ��5      �� 5        �� ImageSource$3 -/sistemaCorporativo;component/IMAGES/Turn.png��6   5"      5#      5$      