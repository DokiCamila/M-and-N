   M S B A M L   `   `   `  ����T  PMahApps.Metro, Version=1.1.2.0, Culture=neutral, PublicKeyToken=f4fb5a3c4d1e5b4fV;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.MetroMahApps.Metro.Controls  5   	   Z=clr-namespace:MahApps.Metro.Behaviours;assembly=MahApps.MetroMahApps.Metro.Behaviours  5   	    sistemaCorporativo/   )sistemaCorporativo.FORMS.gerarLoginWindow   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   PControls;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.Metro     5   	   T
Behaviours=clr-namespace:MahApps.Metro.Behaviours;assembly=MahApps.Metro     5   	   -   5          � Title$  Gerar Login��5   	    � WindowStartupLocation$ CenterScreen=�6   $��327.068��6B   $��386.841��6S    � 
ResizeMode$ NoResize=�6c      AccentColorBrush#���   5   	   $	��0,1q�5   	    � ShowInTaskbar .  .��5	      � .+�5
   
   �� $��153,110,0,-76q�6   $9��3/sistemaCorporativo;component/IMAGES/searchGray.png��6'   $	��262��6d   $	��Top=�6q   $
��Left=�6�   $	��178��6�   �� 5   
   $��246,132,0,0q�6   $:��4/sistemaCorporativo;component/IMAGES/quimic_Icon.png��6%   $
��Fill=�6c   $
��Left=�6r   $	��123��6�   $	��128��6�   $	��Top=�6�   G� 5   
   -   5         G�Name$ txtSenha��5   �   *   $MahApps.Metro.Controls.TextBoxHelper   	Watermark$ Senha��6      ClearTextButton . .   (MahApps.Metro.Controls.PasswordBoxHelper   CapsLockIcon$ capslock ativado��6m   $
��Left=�6�   $��
35,172,0,0q�6�   $	��Top=�6�   $	��186��6  6  �� 5   
   -   5         ��Name$ 
txtUsuario��5      $ Usuário��6&   $
��Left=�6Q   $��23��6l   $��
35,110,0,0q�6x   	 �� TextWrapping$	 NoWrap=�6�   $	��Top=�6�   $	��144��6�   �� 5   
   -   5        
 ��Name$
 btnGerarLogin��5      $��gerar login��6(     SquareButtonStyle#���  6>   $��181,238,0,0q�6j   $��37��6   $	��Top=�6�   $
��Left=�6�   $	��104��6�   6�   �� 5   
   $
��Left=�6   $	��100��6+   $��139,241,0,-45q�68   $	��Top=�6O   $	��100��6g    J� RenderTransformOrigin$ 0.463,0.503&�6s   �� 5   
   $��Gerar Login��6   $
��Left=�6&   $	��Top=�6A   $��	35,10,0,0q�6Y   $	��334��6l   $��67��6x   $��Segoe WP Light)�6�   $��36(�6�   �� 5   
   $��Pontos:��6   $
��Left=�6"   $��
35,223,0,0q�6=   $	��Top=�6Q   �� 5   
   -   5         ��Name$ 	lblPontos��5      $�� ��6#   $
��Left=�6.   $��
84,223,0,0q�6I   $	��Top=�6]   �� 5   
   $��Força:��6   $
��Left=�6!   $��
35,251,0,0q�6<   $	��Top=�6P   �� 5   
   -   5        $ lblForca��5      $�� ��6"   $
��Left=�6-   $��
76,251,0,0q�6H   $	��Top=�6\   5      5      