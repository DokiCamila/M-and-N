   M S B A M L   `   `   `  ����T  PMahApps.Metro, Version=1.1.2.0, Culture=neutral, PublicKeyToken=f4fb5a3c4d1e5b4fV;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.MetroMahApps.Metro.Controls  5   	    sistemaCorporativo+   %sistemaCorporativo.FORMS.CortarImagem   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   PControls;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.Metro     5   	   -   5        (   "MahApps.Metro.Controls.MetroWindow   Name$  
fotoPerfil��5         WindowTransitionsEnabled .   � Title$ Foto Perfil��5   	   $��483.681��6   $��782.618��6.    � WindowStyle$ 
ToolWindow=�6Y    � 
ResizeMode$ NoResize=�6r    � WindowStartupLocation$ CenterScreen=�6�      AccentColorBrush#���   6�   $��1q�6�    � ShowInTaskbar .  .��5      �  � ColumnDefinitions 5	   
   �� 5
      �� 5      $
��Auto �6    � RowDefinitions 5   
   �� 5      �� 5      $
��Auto �6   .+�5   
   �� -   5        	 ��Name$	 myProfilePic��5   4   
 J� RenderTransformOrigin$
 0.722,0.713&�6   $��	5,5,170,0q�6J   $
��Fill=�6]   �� 5   
   $��Stretch=�6   $
��Auto��61   $��
633,0,17,0q�6U   $
 0.5,0.5&�6i   ��6?   Z�  � 	Resources 5       �� 
TargetType �� Property �� Value��%W   '��       �� 5      " ��62   .�5      ��  � ��$ 5,5,5,5q�6/   5      5      .+�5      �� -   5        $	 imgCrop��5      $	��130��6%   $��0,5,0,0q�62   �� 5      ���	#FFFFFFFF$��Editar��6.   $��0,5,0,0q�6>   ���	#FFFFFFFF.��5      �� $
��Auto��6   .+�5      	� -   5        $��Cortar Imagem��5   "    d� 	IsChecked$
 TrueY�6:   $
��Auto��6g   $
��Auto��6t   $
��Left=�6�   5       5!      �� 5"      $��0,8,0,0q�6   ���	#FFFFFFFF$��Cor (seleção)��6?   $��83��6V   .��5#      �� $
��Auto��6   .+�5$      	� -   5         	�Name$ rbRed��5$   "   $��Vermelho��61   $
 TrueY�6E   $��22��6l   	� 5%      -   5        $��Cinza��5%   "   $��22��6I   5&      5'      � 5(      $	��146��6   .+�5)      �� -   5         ��Name$ btnFinalizar��5)        SquareButtonStyle#���  6/   ���	#FF35A3CD$��	Finalizar��6r   $��Center=�6�   $��33��6�   $��4��6�   �� 5*      -   5        $ 	btnFechar��5*      #���  6,   $��Fechar��6X   $��Center=�6i   $��34��6�   $��4��6�   $��	0,94,0,18q�6�   5+      5-      ]� 5.   
   -	   5         ]�Name$ dckControls��5.   N   ��. $��5,5,6,5q�6)   $y�Hidden=�6:   6e   �� 5/   
   -
   5         ��Name$ tblkClippingRectangle��5/   C   $��Stretch=�6   $��5,0,0,5q�62   $	��Top=�6b   $
��Auto��6z   $
��Auto��6�   $��1��6�   ���	#FFFFFFFF$��ClippingRectangle��6�   $
��Wrap=�6�   51      52      