   M S B A M L   `   `   `  ����T  PMahApps.Metro, Version=1.1.2.0, Culture=neutral, PublicKeyToken=f4fb5a3c4d1e5b4fV;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.MetroMahApps.Metro.Controls  5   	   Z=clr-namespace:MahApps.Metro.Behaviours;assembly=MahApps.MetroMahApps.Metro.Behaviours  5   	    sistemaCorporativo>   8sistemaCorporativo.FORMS.principalScreen.PrincipalScreen   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5   	   JControls;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.Metro  5   	   N
Behaviours=clr-namespace:MahApps.Metro.Behaviours;assembly=MahApps.Metro  5   	   :d2http://schemas.microsoft.com/expression/blend/2008  5   	   Dmc;http://schemas.openxmlformats.org/markup-compatibility/2006  6F   -   5        (   "MahApps.Metro.Controls.MetroWindow   Name$  PrincipalScreen1��5   �    � Title$P JD.P.I.C.S.P Departamento Policial de Investigação Criminal de São Paulo��5   	   $	��729��6Y   $
��1361��6f      AccentColorBrush#���   5
      $��1q�5   	    � WindowStartupLocation$ CenterScreen=�5       � WindowState$ 	Maximized=�6(    � 	Resources 5        ButtonStyle1 �� 
TargetType �� Property �� Value �� 
TargetType	 ��Name  SquareButtonStyle
 �� Triggers P� Property P� Value �� 
TargetName  ButtonStyle2 J� SnapsToDevicePixels  ButtonStyle3  ButtonStyle4  ButtonStyle5  ButtonStyleClaro  ButtonStyle6 ��Name��%N
  &	       &	 �    &	 q    &	 �    &	 �    &	 �    &	 �    �� 5   
   " ��6%   .�5      ��  � �� 5      �� 5      " ��6&   .R�5      � .+�5      �� $	 button��6%   ���	#FF7A7979
�������$��claro��6`   ���	#FF020202#���  6�   5      
 5      P� 5       �@��	IsDefault .  6;   P� 5       �@��	IsDefault .  6;   P� 5       �@��	IsDefault .  6;   P� 5       �@��	IsDefault .  6;   P� 5       �@J�	IsFocused . . �5   "   �� $ button��6?    � �� �	#FF2724246S   �� 5   "   $ button��6?    � ��
 �����6S   5      P� 5       �@��	IsDefault .  6;   P� 5        �@��IsDefaulted . 6=   P� 5!       �@J�IsMouseOver . . �5"   "   �� $ button��6?    � �� �	#FFCBC6C66S   5#      P� 5$       �@��	IsDefault .  6;   P� 5%       � �� . . �5&   "   �� $ button��6?    � ��
 �����6S   �� 5'   "   $ button��6?    � �� �	#FF2120206S   5(      P� 5)       �@��	IsDefault .  6;   P� 5*       �@��	IsDefault .  6;   P� 5+       �@��	IsDefault .  6;   P� 5,       � {� .  6;   5.      50      51      �� 52   
   " ��6%   .�53      ��  � �� 54      �� 55      " ��6&   .R�56      � .+�57      �� ���	#FFF4F4F56(   �� 58      #��z��6/   ��. # z 6�   #��z��6�   $��claro��6  59      
 5:      P� 5;       �@J�	IsFocused . 6;   P� 5<       �@��IsDefaulted . 6=   P� 5=       �@J�IsMouseOver . 6=   P� 5>       � �� . 6;   P� 5?       � {� .  6;   5A      5C      5D      �� 5E   
   " ��6%   �� 5F   
   " ��6%   �� 5G   
   " ��6%   �� 5H   
   " ��6)   �� 5I   
   " ��6%   .�5J      ��  � �� 5K      �� 5L      " ��6&   .R�5M      � .+�5N      �� $ 	rectangle��6(   
����������	#FF6A6969$��2��6[   �� 5O      #��z��6/   ��. # z 6�   #��z��6�   $��claro��6  5P      
 5Q      P� 5R       �@��	IsDefault .  6;   P� 5S       �@J�	IsFocused . 6;   P� 5T       �@��IsDefaulted . 6=   P� 5U       �@J�IsMouseOver . . �5V   "   �� $ 	rectangle��69    � �� �	#FF7670706P   5W      P� 5X       �@��	IsDefault .  6;   P� 5Y       � �� . . �5Z   "   �� $ 	rectangle��69    � �� �	#FF302E2E6P   5[      P� 5\       �@��	IsDefault .  6;   P� 5]       � {� .  6;   5_      5a      �� 5b       � ��$
 Bold"�6+   5c      5d         RightWindowCommands 5e      +   %MahApps.Metro.Controls.WindowCommands  5f   
   .@�5g      �� -   5        $	 	btnPerfil��5g      .��5h      ��  �� Orientation$ 
Horizontal=�6   .+�5j      �� $��4 0 0 0q�6    $��Center=�5k      $��Perfil��5l      5m      5n      5o      .��5q      � -   5         �Name$ grdPrincipal��5q      ��5s   
   �� 5t       �� ImageSource$O I/sistemaCorporativo;component/IMAGES/Wallpaper Oficial SPPDI material.png��6    � RowDefinitions 5v   
   �� 5w      $
��221* �6   �� 5x      $	��23* �6   .+�5z   
   �� $
��Auto=�6   $��Disabled=�6<   $��2��6c   $
��Auto��6t   ��6�   ��  �� ElementName$ grdPrincipal�� �� Path$ ActualHeight�.��5{      � -   5        $ grdPrincipal1��5{      $
��Left=�6*   $	��514��6E   $��65,145,-310,0q�6R   $	��Top=�6i   $
��1596��6�   $��2��6�   .+�5}      �� $��Software��6   $
��Left=�6+   $��883.5,2.017,0,0q�6F   $	��Top=�6_   $��Segoe WP Light)�6w   $��29.333(�6�   !   MahApps.Metro.Controls.Tile  5      $��961,51.034,575.5,409.964q�6    $
��Auto��6B   $
��Auto��6P      
TiltFactor$ 2��6]    � ToolTip$ Configurações��6l   ��5�      �� 5�       �� Color# � ��6*   .��5�      �� $<��6/sistemaCorporativo;component/IMAGES/Settings_FLAT.png��6   $��	30,11,0,0q�6\   $
��69.5��6p   $��64.002��6}   5�      �� 5�      $��Ferramentas��6   $
��Left=�6.   $��566,2.017,0,0q�6I   $	��Top=�6`   $��Segoe WP Light)�6x   $��29.333(�6�   �� 5�      $��Cadastro��6   $
��Left=�6+   $��27,2.017,0,0q�6F   $	��Top=�6\   $��Segoe WP Light)�6t   $��29.333(�6�     5�      $��687,344.034,792.5,74.964q�6    $
��Auto��6B   $
��Auto��6P   $ 2��6]   $ Comparar Assinatura��6l   ��5�      �� 5�      # � ��6*   .��5�      �� $>��8/sistemaCorporativo;component/IMAGES/Assinatura_FLAT.png��6   $��
67,21,10,0q�6^   $��115.5��6s   $��140.002��6�   5�        5�      $��566,344.034,914,74.966q�6    $
��Auto��6@   $
��Auto��6N   $ 2��6[      Title$  ��6j   $ Analisar DNA��6s   ��5�      �� 5�      # � ��6*   .��5�      �� $7��1/sistemaCorporativo;component/IMAGES/dna_FLAT.png��6   $��
84,18,10,0q�6W   $	��120��6l   $	��150��6y   5�        5�      $��566,243.035,792.5,174.966q�6    $
��Auto��6C   $
��Auto��6Q   $ 2��6^   $ Reconhecimento Facial��6m   ��5�      �� 5�      # � ��6*   .��5�      �� $I��C/sistemaCorporativo;component/IMAGES/Reconhecimento_Facial_FLAT.png��6   $��
69,16,10,0q�6i   $��120.999��6~   $��237.5��6�   5�        5�      $��566,51.034,792.5,275.965q�6    $
��Auto��6B   $
��Auto��6P   $ 2��6]   $! Comparar Impressão Digital��6l   ��5�      �� 5�      # � ��6*   .��5�      �� $F��@/sistemaCorporativo;component/IMAGES/Finger_Print_tools_FLAT.png��6   $��146,27,10,0q�6f   $��280.001��6|   $��250.5��6�   5�        5�      -   5        $��242,243,1116,175q�5�       $
��Auto��6:   $
��Auto��6H   $ Cadastrar Ocorrência��6j   ��5�      �� 5�      # � ��6*   .��5�      �� $>��8/sistemaCorporativo;component/IMAGES/Ocorrencia_FLAT.png��6   $��
168,2,90,0q�6^   $��149.999��6s   $��270.5��6�   5�        5�      -   5        $!��242.5,344.034,1237.5,74.964q�5�       $
��Auto��6E   $
��Auto��6S   $  ��6u   $ Cadastrar Denuncia��6~   ��5�      �� 5�      # � ��6*   .��5�      �� $C��=/sistemaCorporativo;component/IMAGES/Denuncia_Normal_FLAT.png��6   $��
73,12,10,0q�6c   $	��121��6x   $	��250��6�   5�        5�      -   5        $!��363.5,344.034,1116.5,74.966q�5�       $
��Auto��6E   $
��Auto��6S   $! Cadastrar Denuncia Anônima��6u   ��5�      �� 5�      # � ��6*   .��5�      �� $D��>/sistemaCorporativo;component/IMAGES/Denuncia_Anonima_FLAT.png��6   $��
85,19,10,0q�6d   $	��131��6y   $	��250��6�   5�        5�      -   5        $��242,51.034,1116.5,275.965q�5�       $
��Auto��6C   $
��Auto��6Q   $ Cadastrar Viatura��6s   ��5�      �� 5�      # � ��6*   .��5�      �� $;��5/sistemaCorporativo;component/IMAGES/Viatura_FLAT.png��6   $��118,53,10,0q�6[   $	��229��6q   $	��240��6~   $
��Fill=�6�   5�        5�      -	   5        $��27,51.036,1359,74.964q�5�       $
��Auto��6?   $
��Auto��6M   $ Cadastrar Agente��6m   ��5�      �� 5�      # � ��6*   .��5�      �� $C��=/sistemaCorporativo;component/IMAGES/Police_man_menu_FLAT.png��6   $��169,105,10,10q�6c   $	��273��6z   $	��312��6�   5�        5�      $��883.5,51.034,653,409.964q�6    $
��Auto��6B   $
��Auto��6P   $ 2��6]   $ Sobre��6l   ��5�      �� 5�      # � ��6*   .��5�      �� $9��3/sistemaCorporativo;component/IMAGES/Sobre_FLAT.png��6   $��52,9,0,0q�6Y   $
��79.5��6k   $��75.002��6x   5�      5�      5�      o� 5�   
    o� 
IsMainMenu . $��29��6!   $	��Top=�6-   .@�5�      m� $��	Cadastrar��6   $��72.937��6*   .@�5�      m� $��Investigação��6   .@�5�      m� -
   5        $��Denuncia��5�      61   m� 5�      -   5        $��Denuncia Anônima��5�      69   m� 5�      -   5        $��Ocorrência��5�      63   5�      m� 5�      -   5        $��Departamento��5�      .@�5�      m� -   5        $��Agente��5�      6/   m� 5�      -   5        $��Viatura��5�      60   5�      5�      m� 5�      $��	Consultar��6   $��72.937��6*   .@�5�      m� $��Investigação��6   .@�5�      m� $��Crime��6   m� 5�      $��	Criminoso��6   5�      5�      m� 5�      $��Ajuda��6   $��56.46��6&   .@�5�      m� $��Sobre��6   5�      5�      5�         Flyouts 5�      +   %MahApps.Metro.Controls.FlyoutsControl  5�   
   -   5          Name$ FlyoutPrincipal��5�   "   .@�5�      #   MahApps.Metro.Controls.Flyout  -   5          Name$ flyoutAgente��5�      $��304.5��64   $��2��6B    J� 	AllowDrop .      AllowFocusElement  . !   IsPinned! .  "   AnimateOnPositionChange" . #   Theme$# Adapt=�6�   .��5�      �� .+�5�      �� $��Nome do Agente��6   $ �� Left$$ 68��65   
% �� Top$	% 162��6F   $��171.167��6W   $��Segoe WP Light)�6g   $��21.333(�6�   �� 5�      $��Nível:��6   $$ 10��6-   $% 234.22��6>   $��Segoe WP SemiLight)�6R   $��13.333(�6r   �� 5�      $��Cargo:��6   $$ 10��6-   $% 261.953��6>   $��Segoe WP SemiLight)�6S   $��13.333(�6s   �� 5�      -   5        $	 btnFzrLogoff��5�      $��Fazer logoff��63   $	% 619��6J   $��304.5��6[   $��34.333��6i   $��10.667(�6y   $��Segoe UI Light)�6�   ���	#FF262626
�������6�   )   #MahApps.Metro.Controls.ToggleSwitch  5�      -   5        &  Name$& tgsTema��5�   ,   '   Header$' 
Tema Claro��6=   $$ 10��6Q   $% 509.72��6b   $��64.28��6v   $��109.83��6�   $�� ��6�   �� 5�      $	��157��6   $$ 68��6)   $��168.667��6:   $?��9/sistemaCorporativo;component/IMAGES/User_Login adapt.png��6J   �� 5�      $��Casos resolvidos:��6   $$ 10��68   $% 289.686��6I   $��Segoe WP SemiLight)�6^   $��13.333(�6~   5�      5�      5�      5�      