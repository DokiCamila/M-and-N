   M S B A M L   `   `   `  ����T  PMahApps.Metro, Version=1.1.2.0, Culture=neutral, PublicKeyToken=f4fb5a3c4d1e5b4fV;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.MetroMahApps.Metro.Controls  5      Z=clr-namespace:MahApps.Metro.Behaviours;assembly=MahApps.MetroMahApps.Metro.Behaviours  5       sistemaCorporativo+   %sistemaCorporativo.BuscarPessoaFisica   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      PControls;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.Metro     5      T
Behaviours=clr-namespace:MahApps.Metro.Behaviours;assembly=MahApps.Metro     5      P LAvalonControlsLibrary, Version=2.0.0.0, Culture=neutral, PublicKeyToken=nullICustom<http://schemas.AvalonControls/AvalonControlsLibrary/Controls  5      -   5        (   "MahApps.Metro.Controls.MetroWindow   Name$  wndBuscarPessoaFisica��5       � Title$ Buscar Pessoa Física��5	         AccentColorBrush#���   5
      $��1q�5      $��719.5��5      $	��519��6    � WindowStartupLocation$ CenterScreen=�6    � 
ResizeMode$ NoResize=�6B    � 	Resources 5        MaskedTextBoxStyle1 �� 
TargetType5  /AC.AvalonControlsLibrary.Controls.MaskedTextBox �� Property  TextBoxMetroContextMenu �� Value*   $MahApps.Metro.Controls.TextBoxHelper+   %MahApps.Metro.Controls.ControlsHelper  HighlightBrush  ContentFontFamily  ContentFontSize  ControlBackgroundBrush  	TextBrush  TextBoxBorderBrush 	 TextBoxFocusBorderBrush 
 TextBoxMouseOverBorderBrush  ValidationErrorTemplate  
BlackBrush �� 
TargetType	 � 	Resources  Key
 g� Duration	 W� To �� TargetProperty �� 
TargetName W� From ��Name J� SnapsToDevicePixels �Name � ColumnDefinitions ��Name �� Mode �� RelativeSource � RowDefinitions ��Name J� Opacity ��Name ��Name J� IsHitTestVisible �� Padding   	Watermark  FloatingWatermarkFontSize ��Name  ChromelessButtonStyle'    IsClearTextButtonBehaviorEnabled  �� Path! �� 	Converter  ControlsDisabledBrush" �� Triggers# �� Value$ �� Binding% �� 
TargetName& N� EnterActions' W� EasingFunction4  .System.Windows.Media.Animation.ExponentialEase7  1System.Windows.Media.Animation.EasingFunctionBase(   
EasingMode)   Exponent* N� ExitActions+ ]� 
Conditions, �� Property- �� Value. �� ElementName/ P� Property0 P� Value1 P� 
SourceName  
GrayBrush8  
WhiteBrush2 g� AutoReverse3 g� RepeatBehavior3  -System.Windows.Media.Effects.DropShadowEffect4   
BlurRadius  
BlackColor5   Color6   Opacity7   ShadowDepth��%3&  &	       �� 5      "  6&   .�5      ��  �@�ContextMenu# �  6#   �� 5       �@ IsMonitoring . 6;   �� 5       �@ ButtonWidth$ 22Q�6;   �� 5       �@��SelectionBrush# �  6&   �� 5       � ��# �  6"   �� 5       � ��# �  6    �� 5       � ��$ 1q�6'   �� 5       � ��$ 26��6!   �� 5       � ��# �  6"   �� 5       � ��# �  6"   �� 5       � ��$ 0q�6   �� 5       � ��# �  6#   �� 5       �@ FocusBorderBrush# � 	 6@   �� 5       �@ MouseOverBorderBrush# � 
 6D   �� 5       �@J�SnapsToDevicePixels . 6+   �� 5       �@=�ErrorTemplate# �  60   �� 5       �@��
CaretBrush# �  6"   �� 5        � �� 5!      �� 5"      "  6   	 5#      ���� 5$   	   enterGotFocus .��5%   
   W� $
 0:0:0.2D�6   $	 0.2X�6-   $ Opacity�66   $ Message��6Z   5&   
   �� 5'   	   exitGotFocus .��5(   
   W� $
 0:0:0.2D�6   $ Opacity�6-   $ Message��6Q   5)   
   �� 5*   	   enterHasText .��5+   
   W� $
 0:0:0.2D�6   $	 0.2X�6-   $ 0X�68   $ Opacity�6?   $ Message��6c   5,   
   �� 5-   	   exitHasText .��5.   
   W� $
 0:0:0.2D�6   $ Opacity�6-   $ Message��6Q   5/   
   50   	   .R�51      � .+�52   	   �� $
 Base��6   #��z��6   #��z��6J   #��z��6~   # z 6�   � 53   	   $ PART_InnerGrid��6   $��2q�6&    54   
   �� 55      $��* �6   �� 56      $ ButtonColumn��6   ��62   �� *'%(Controls:ControlsHelper.ButtonWidth)+$ OneWay=� �� *TemplatedParent+ 58   
   �� 59      $
��Auto �6   �� 5:      $ 	ButtonRow��6   $��* �6,   .+�5<   
   �� $ ReadOnlyVisualElement��6   $��2��6F   $ 0Q�6Z   $��1��6f   ��60   Z� 6f   �� 5=   
   $ PART_ContentHost��6   $��0q�61   $��0��6[   ��.  $��2q�6}   $��1��6�   $��Stretch=�6�   ��6E   Z� 6�   �� 5>   
   $ Message��6   $��0��6%   #��z��65   #��z��6_    .  $	��6,2q�6�   $	 0.6Q�6�   # z��6�   $��1��6�   #��z 6  $y�	Collapsed=�6A  #��z��6X  � 5?   
   $ FloatingMessageContainer��6   $��2��61   $��0��6E   $��0��6U    .  $	��5,0q�6y   $y�Visible=�6�   .+�5@      �� $ FloatingMessage��6   #��z��6.   #���  6X   #��z��6�   $	 0.6Q�6�   #��z 6�   #��z��6  z�5A      T� 5B      5D      5E      �� 5F   
   $ PART_ClearText��6   $��1��6�   #��z��6�   $��16(�6�   ��.  $��2��6&  #���  67  ��6)   �� *(&(Controls:TextBoxHelper.ButtonContent)+$ OneWay=� �� *TemplatedParent+��6�   �� *+)(Controls:TextBoxHelper.ButtonFontFamily)+$ OneWay=� �� *TemplatedParent+ 6{  �� **((Controls:TextBoxHelper.ClearTextButton)+$ OneWay=� �� *TemplatedParent+y�5G      �� 5H      $ OneWay=�6   $.  ((Controls:TextBoxHelper.ClearTextButton)�6"    6R   �� *TemplatedParent+! 5I      �� 5J      5L      5N      5O   
   �� 5P   	   $ DisabledVisualElement��6   #���  6/   #��z��6e   #���  6�    .  $ 0Q�6�   # z 6�   5Q   	   " 5R      �� 5S   	   $#  ��6T   $ 6   �� *Text+ �� *Self+.a�5T   
   �� $% Message��6'    � y�$ Visible=�6<   5U   
   �� 5V   	   $# False��6p   $ 6   �� *" (Controls:TextBoxHelper.HasText)+ �� *Self+& 5W   
   �� 5X      .��5Y      �� .��5Z      W� $
 0:0:0.2D�6   $ 0X�60   $ Height�67   $ FloatingMessageContainer��6Z   ' 5[        5\      $( 	EaseInOut=�6   $) 2Q�66   5^      W� 5_      $
 0:0:0.2D�6   $ 0X�60   $ Opacity�67   $ FloatingMessageContainer��6[   W� 5`      $
 0:0:0.2D�6   $ 20X�60   $8 2(UIElement.RenderTransform).(TranslateTransform.Y)�68   $ FloatingMessage��6�   ' 5a        5b      $( EaseIn=�6   $) 2Q�63   5d      5e      5f      * 5h   
   �� 5i      .��5j      �� .��5k      W� $
 0:0:0.2D�6   $ 15X�60   $ Height�68   $ FloatingMessageContainer��6[   ' 5l        5m      $( 	EaseInOut=�6   $) 2Q�66   5o      W� 5p      $
 0:0:0.2D�6   $ Opacity�60   $ FloatingMessageContainer��6T   W� 5q      $
 0:0:0.2D�6   $ 0X�60   $8 2(UIElement.RenderTransform).(TranslateTransform.Y)�67   $ FloatingMessage��6�   ' 5r        5s      $( EaseOut=�6   $) 2Q�64   5u      5v      5w      5y   
   �� 5z   	   $#  ��6r   $ 6   �� *$"(Controls:TextBoxHelper.Watermark)+ �� *Self+& 5{   
   �� 5|      .��5}      �� .��5~      W� $
 0:0:0.2D�6   $ 0X�60   $ Height�67   $ FloatingMessageContainer��6Z   ' 5        5�      $( 	EaseInOut=�6   $) 2Q�66   5�      W� 5�      $
 0:0:0.2D�6   $ 0X�60   $ Opacity�67   $ FloatingMessageContainer��6[   W� 5�      $
 0:0:0.2D�6   $ 20X�60   $8 2(UIElement.RenderTransform).(TranslateTransform.Y)�68   $ FloatingMessage��6�   ' 5�        5�      $( EaseIn=�6   $) 2Q�63   5�      5�      5�      * 5�   
   �� 5�      .��5�      �� .��5�      W� $
 0:0:0.2D�6   $ 15X�60   $ Height�68   $ FloatingMessageContainer��6[   ' 5�        5�      $( 	EaseInOut=�6   $) 2Q�66   5�      W� 5�      $
 0:0:0.2D�6   $ Opacity�60   $ FloatingMessageContainer��6T   W� 5�      $
 0:0:0.2D�6   $ 0X�60   $8 2(UIElement.RenderTransform).(TranslateTransform.Y)�67   $ FloatingMessage��6�   ' 5�        5�      $( EaseOut=�6   $) 2Q�64   5�      5�      5�      5�   
   �� 5�   	   $# False��6}   $ 6   �� */-(Controls:TextBoxHelper.UseFloatingWatermark)+ �� *Self+& 5�   
   �� 5�      .��5�      �� .��5�      W� $
 0:0:0.2D�6   $ 0X�60   $ Height�67   $ FloatingMessageContainer��6Z   ' 5�        5�      $( 	EaseInOut=�6   $) 2Q�66   5�      W� 5�      $
 0:0:0.2D�6   $ 0X�60   $ Opacity�67   $ FloatingMessageContainer��6[   W� 5�      $
 0:0:0.2D�6   $ 20X�60   $8 2(UIElement.RenderTransform).(TranslateTransform.Y)�68   $ FloatingMessage��6�   ' 5�        5�      $( EaseIn=�6   $) 2Q�63   5�      5�      5�      * 5�   
   �� 5�      .��5�      �� .��5�      W� $
 0:0:0.2D�6   $ 15X�60   $ Height�68   $ FloatingMessageContainer��6[   ' 5�        5�      $( 	EaseInOut=�6   $) 2Q�66   5�      W� 5�      $
 0:0:0.2D�6   $ Opacity�60   $ FloatingMessageContainer��6T   W� 5�      $
 0:0:0.2D�6   $ 0X�60   $8 2(UIElement.RenderTransform).(TranslateTransform.Y)�67   $ FloatingMessage��6�   ' 5�        5�      $( EaseOut=�6   $) 2Q�64   5�      5�      5�      5�   
   ]� 5�   	   + 5�   
   �� 5�      , �@��TextWrapping$- NoWrap=�6-   �� 5�      , �@��AcceptsReturn- .  6.   ./�5�   
   �� $% ButtonColumn��6"    6<   �� *ActualHeight+$. 	ButtonRow��$ OneWay=� � ��5�   
   �� 5�   	   $# False��6x   $ 6   �� **((Controls:TextBoxHelper.ClearTextButton)+ �� *Self+.a�5�   
   �� $% PART_ContentHost��6,    � ��$ 2��6J   �� 5�   
   $% Message��6,    � ��$ 2��6A   5�   
   P� 5�   	   / �@J�IsMouseOver0 . . �5�   
   �� $
% Base��6(    6:   �� *0.(Controls:ControlsHelper.MouseOverBorderBrush)+ �� *TemplatedParent+ � ��5�   
   P� 5�   	   / � {�0 .  . �5�   
   �� $% DisabledVisualElement��6$    �@J�Opacity$	 0.6Q�6G   5�   
   P� 5�   	   / �@��
IsReadOnly0 . . �5�   
   �� $% ReadOnlyVisualElement��6$    �@J�Opacity$ 1Q�6G   5�   
   P� 5�   	   / �@J�	IsFocused0 . . �5�   
   �� $
% Base��6(    6:   �� *,*(Controls:ControlsHelper.FocusBorderBrush)+ �� *TemplatedParent+ � ���� 5�   
   $% FloatingMessage��6'   # �   6D    � ���� 5�   
   $% FloatingMessage��6$    �@J�Opacity$ 1Q�6A   5�   
   P� 5�   	   $1 PART_ClearText��6(   / �@J�IsMouseOver0 . . �5�   
   �� $% PART_ClearText��6'   # �  6C    � ���� 5�   
   $% PART_ClearText��6'   # �   6C    � ��5�   
   P� 5�   	   $1 PART_ClearText��6&   / � ��0 . . �5�   
   �� $% PART_ClearText��6'   # �  6C    � ���� 5�   
   $% PART_ClearText��6'   # �  6C    � ��5�   
   ]� 5�   	   + 5�   
   �� 5�      , �@ HasText- .  6?   �� 5�      , �@J�	IsFocused- . 6*   & 5�   
   �� 5�      .��5�      �� .��5�      W� $
 0:0:0.2D�6   $	 0.2X�60   $ Opacity�69   $ Message��6]   5�      5�      * 5�   
   �� 5�      .��5�      �� .��5�      W� $
 0:0:0.2D�6   $ Opacity�60   $ Message��6T   5�      5�      5�   
   P� 5�   	   / �@ HasText0 . & 5�   
   �� 5�      .��5�      �� .��5�      W� $
 0:0:0.2D�6   $	 0.2X�60   $ 0X�6;   $ Opacity�6B   $ Message��6f   5�      5�      * 5�   
   �� 5      .��5     �� .��5     W� $
 0:0:0.2D�6   $ Opacity�60   $ Message��6T   5     5     5  
   P� 5  	   / �@ IsWaitingForData0 . & 5  
   �� 5	     .��5
     �� .��5     W� 2 . $
 0:0:2D�60   $ 0X�6A   $3 Forever��6J   $ 1X�6c   $ (Effect).Opacity�6j   $
 Base��6�   5     5     . �5  
   �� $
% Base��6#    �@J�Effect 5       5     $4 10Q�6   #5 �  6-   $6 0Q�6R   $7 0Q�6^   5     5  
   P� 5  	   / �@ IsWaitingForData0 .  . �5  
   �� $
% Base��6#    65   Z�  �@J�Effect5  
   5     5     5     5     .��5     � -   5        $ 
LayoutRoot��5     .+�5      � -   5        $ grdHead��5   	   $	��100��6   $	��Top=�6'   .+�5!     �� ���	#FF555555$	��100��6    $	��Top=�6-   �� 5"     $
��Left=�6   $��90��6&   $��	10,10,0,0q�62   $	��Top=�6E   $
��99.5��6]   $D��>/sistemaCorporativo;component/IMAGES/PesquisarPessoaFisica.png��6j   �� 5#     $��Busque por um Indivíduo��6   $
��Left=�6-   $��114.5,10,0,0q�6H   $	��Top=�6^   $
��Bold"�6v   �� 5$     $
��Left=�6   $��125,40.96,0,0q�6*   $
��Wrap=�6A   $����Digite o nome completo, parte do nome, ou a data de nascimento caso necessário, selecione a pessoa desejada do resultado de busca, ou clique em 'Add Novo' para adicionar um novo indivíduo.��6U   $	��Top=�6  $��49.04��61  $��422.46��6@  
�������6O  5%     � 5&     -   5        $ grdCorpo��5&  	   $
��Left=�6   $��	0,100,0,0q�66   $��711.5��6I   .+�5'     �� $��Nome:��6   $
��Left=�6   $��	10,10,0,0q�66   $	��Top=�6I   �� 5(     -   5        8 ��Name$8 txtNomeEnvolvido��5(     $
��Left=�6'   $��23��6B   $��60.22,9.96,0,0q�6N   9 �� TextWrapping$
9 Wrap=�6f   $	��Top=�6z   $	��236��6�   $��1��6�   �� 5)     $ ��(Nome, Sobrenome, Apelido)��6   $
��Left=�60   $��296.22,9.96,0,0q�6K   $	��Top=�6d   ���	#FFC3C3C36|   �� 5*     $��Data de Nascimento de:��6   $
��Left=�6,   $��10,40.96,0,0q�6G   $	��Top=�6]   �� 5+     $��à��6   $
��Left=�6   $��248.634,42.06,0,0q�62   $	��Top=�6M   �� 5,     $4��.(Deixe em branco para uma busca de data exata)��6   $
��Left=�6D   $��361.408,42.06,0,0q�6_   $	��Top=�6z   ���	#FFC3C3C36�   &   System.Windows.Controls.DataGrid  5-     -   5        :  Name$: dgvEnvolvido��5-     $
��Left=�6$   $��10,85.42,0,0q�6?   $	��Top=�6U   $��264.08��6m   $��691.5��6}   ;   
IsReadOnly; . 6�   �� 5.     -   5        $ 
btnAddNovo��5.       SquareButtonStyle#���  6    $
��Left=�6L   $��10,354.5,0,0q�6g   $	��Top=�6}   $��99��6�   $��27.96��6�   $��4��6�   .��5/     �� .+�50     �� $5��//sistemaCorporativo;component/IMAGES/addNew.png��6   $��11.002��6E   $��14.667��6U   
< �� Top$< -5.833��6d   = �� Left$= -39.126��6x   �� 51     $��Add Novo��6   $= -19.459��6    $< -7.781��66   52     53     �� 54     -   5        $ btnCancelar��54     #���  6!   $��626.5,354.5,10,0q�6M   $	��Top=�6g   $��6��6�   .��55     �� $��Cancelar��6   56  	   �� 57     -	   5        $ btnSelecionar��57     #���  6#   $
��Left=�6O   $��522.404,354.5,0,0q�6j   $	��Top=�6�   $��99��6�   $��27.96��6�   $��5��6�   .��58     �� .+�59     �� $4��./sistemaCorporativo;component/IMAGES/Check.png��6   $��15.96��6E   $��14.667��6T   $< -7.781��6c   $= -39.126��6w   �� 5:     $��
Selecionar��6   $= -19.459��6#   $< -7.781��69   5;     5<       5=     -
   5        >  Name$> 	txtDataDe��5=  h   $��26��6   $
��Left=�6&   $	��Top=�6A   $��91.667��6Y   ?   Mask$? 
00/00/0000��6|   @ �� CharacterCasing$@ Upper=�6�   $��151.967,40.92,0,0q�6�   $��2��6�   #���  6�   .��6    5>       MaskedTextBoxStyle2��%3&  &	       �� 5?     "  6)   .�5@     ��  �@�ContextMenu# �  6&   �� 5A      �@ IsMonitoring . 6>   �� 5B      �@ ButtonWidth$ 22Q�6>   �� 5C      �@��SelectionBrush# �  6)   �� 5D      � ��# �  6%   �� 5E      � ��# �  6#   �� 5F      � ��$ 1q�6*   �� 5G      � ��$ 26��6$   �� 5H      � ��# �  6%   �� 5I      � ��# �  6%   �� 5J      � ��$ 0q�6"   �� 5K      � ��# �  6&   �� 5L      �@ FocusBorderBrush# � 	 6C   �� 5M      �@ MouseOverBorderBrush# � 
 6G   �� 5N      �@J�SnapsToDevicePixels . 6.   �� 5O      �@=�ErrorTemplate# �  63   �� 5P      �@��
CaretBrush# �  6%   �� 5Q      � �� 5R  	   �� 5S  
   "  6   	 5T     ���� 5U     enterGotFocus .��5V     W� $
 0:0:0.2D�6   $	 0.2X�60   $ Opacity�69   $ Message��6]   5W     �� 5X     exitGotFocus .��5Y     W� $
 0:0:0.2D�6   $ Opacity�60   $ Message��6T   5Z     �� 5[     enterHasText .��5\     W� $
 0:0:0.2D�6   $	 0.2X�60   $ 0X�6;   $ Opacity�6B   $ Message��6f   5]     �� 5^     exitHasText .��5_     W� $
 0:0:0.2D�6   $ Opacity�60   $ Message��6T   5`     5a     .R�5b     � .+�5c     �� $
 Base��6   #��z��6!   #��z��6M   #��z��6�   # z 6�   � 5d     $ PART_InnerGrid��6   $��2q�6)    5e     �� 5f     $��* �6   �� 5g     $ ButtonColumn��6   ��65   �� *'%(Controls:ControlsHelper.ButtonWidth)+$ OneWay=� �� *TemplatedParent+ 5i     �� 5j     $
��Auto �6   �� 5k     $ 	ButtonRow��6   $��* �6/   .+�5m     �� $ ReadOnlyVisualElement��6   $��2��6I   $ 0Q�6]   $��1��6i   ��63   Z� 6i   �� 5n     $ PART_ContentHost��6   $��0q�64   $��0��6^   ��.  $��2q�6�   $��1��6�   $��Stretch=�6�   ��6H   Z� 6�   �� 5o     $ Message��6   $��0��6(   #��z��68   #��z��6b    .  $	��6,2q�6�   $	 0.6Q�6�   # z��6�   $��1��6�   #��z 6
  $y�	Collapsed=�6D  #��z��6[  � 5p     $ FloatingMessageContainer��6   $��2��64   $��0��6H   $��0��6X    .  $	��5,0q�6|   $y�Visible=�6�   .+�5q     �� $ FloatingMessage��6   #��z��61   #���  6[   #��z��6�   $	 0.6Q�6�   #��z 6�   #��z��6  z�5r     T� 5s     5u     5v     �� 5w     $ PART_ClearText��6   $��1��6�   #��z��6�   $��16(�6�   ��.  $��2��6)  #���  6:  ��6,   �� *(&(Controls:TextBoxHelper.ButtonContent)+$ OneWay=� �� *TemplatedParent+��6�   �� *+)(Controls:TextBoxHelper.ButtonFontFamily)+$ OneWay=� �� *TemplatedParent+ 6~  �� **((Controls:TextBoxHelper.ClearTextButton)+$ OneWay=� �� *TemplatedParent+y�5x     �� 5y     $ OneWay=�6   $.  ((Controls:TextBoxHelper.ClearTextButton)�6%    6U   �� *TemplatedParent+! 5z     �� 5{     5}     5     5�     �� 5�     $ DisabledVisualElement��6   #���  62   #��z��6h   #���  6�    .  $ 0Q�6�   # z 6�   5�     " 5�     �� 5�     $#  ��6W   $ 6   �� *Text+ �� *Self+.a�5�     �� $% Message��6*    � y�$ Visible=�6?   5�     �� 5�     $# False��6s   $ 6   �� *" (Controls:TextBoxHelper.HasText)+ �� *Self+& 5�     �� 5�     .��5�     �� .��5�     W� $
 0:0:0.2D�6    $ 0X�63   $ Height�6:   $ FloatingMessageContainer��6]   ' 5�       5�     $( 	EaseInOut=�6"   $) 2Q�69   5�     W� 5�     $
 0:0:0.2D�6    $ 0X�63   $ Opacity�6:   $ FloatingMessageContainer��6^   W� 5�     $
 0:0:0.2D�6    $ 20X�63   $8 2(UIElement.RenderTransform).(TranslateTransform.Y)�6;   $ FloatingMessage��6�   ' 5�       5�     $( EaseIn=�6"   $) 2Q�66   5�     5�     5�     * 5�     �� 5�     .��5�     �� .��5�     W� $
 0:0:0.2D�6    $ 15X�63   $ Height�6;   $ FloatingMessageContainer��6^   ' 5�       5�     $( 	EaseInOut=�6"   $) 2Q�69   5�     W� 5�     $
 0:0:0.2D�6    $ Opacity�63   $ FloatingMessageContainer��6W   W� 5�     $
 0:0:0.2D�6    $ 0X�63   $8 2(UIElement.RenderTransform).(TranslateTransform.Y)�6:   $ FloatingMessage��6�   ' 5�       5�     $( EaseOut=�6"   $) 2Q�67   5�     5�     5�     5�     �� 5�     $#  ��6u   $ 6   �� *$"(Controls:TextBoxHelper.Watermark)+ �� *Self+& 5�     �� 5�     .��5�     �� .��5�     W� $
 0:0:0.2D�6    $ 0X�63   $ Height�6:   $ FloatingMessageContainer��6]   ' 5�       5�     $( 	EaseInOut=�6"   $) 2Q�69   5�     W� 5�     $
 0:0:0.2D�6    $ 0X�63   $ Opacity�6:   $ FloatingMessageContainer��6^   W� 5�     $
 0:0:0.2D�6    $ 20X�63   $8 2(UIElement.RenderTransform).(TranslateTransform.Y)�6;   $ FloatingMessage��6�   ' 5�       5�     $( EaseIn=�6"   $) 2Q�66   5�     5�     5�     * 5�     �� 5�     .��5�     �� .��5�     W� $
 0:0:0.2D�6    $ 15X�63   $ Height�6;   $ FloatingMessageContainer��6^   ' 5�       5�     $( 	EaseInOut=�6"   $) 2Q�69   5�     W� 5�     $
 0:0:0.2D�6    $ Opacity�63   $ FloatingMessageContainer��6W   W� 5�     $
 0:0:0.2D�6    $ 0X�63   $8 2(UIElement.RenderTransform).(TranslateTransform.Y)�6:   $ FloatingMessage��6�   ' 5�       5�     $( EaseOut=�6"   $) 2Q�67   5�     5�     5�     5�     �� 5�     $# False��6�   $ 6   �� */-(Controls:TextBoxHelper.UseFloatingWatermark)+ �� *Self+& 5�     �� 5�     .��5�     �� .��5�     W� $
 0:0:0.2D�6    $ 0X�63   $ Height�6:   $ FloatingMessageContainer��6]   ' 5�       5�     $( 	EaseInOut=�6"   $) 2Q�69   5�     W� 5�     $
 0:0:0.2D�6    $ 0X�63   $ Opacity�6:   $ FloatingMessageContainer��6^   W� 5�     $
 0:0:0.2D�6    $ 20X�63   $8 2(UIElement.RenderTransform).(TranslateTransform.Y)�6;   $ FloatingMessage��6�   ' 5�       5�     $( EaseIn=�6"   $) 2Q�66   5�     5�     5�     * 5�     �� 5�     .��5�     �� .��5�     W� $
 0:0:0.2D�6    $ 15X�63   $ Height�6;   $ FloatingMessageContainer��6^   ' 5�       5�     $( 	EaseInOut=�6"   $) 2Q�69   5�     W� 5�     $
 0:0:0.2D�6    $ Opacity�63   $ FloatingMessageContainer��6W   W� 5�     $
 0:0:0.2D�6    $ 0X�63   $8 2(UIElement.RenderTransform).(TranslateTransform.Y)�6:   $ FloatingMessage��6�   ' 5�       5�     $( EaseOut=�6"   $) 2Q�67   5�     5�     5�     5�     ]� 5�     + 5�     �� 5�     , �@��TextWrapping$- NoWrap=�60   �� 5�     , �@��AcceptsReturn- .  61   ./�5�     �� $% ButtonColumn��6%    6?   �� *ActualHeight+$. 	ButtonRow��$ OneWay=� � ��5�     �� 5�     $# False��6{   $ 6   �� **((Controls:TextBoxHelper.ClearTextButton)+ �� *Self+.a�5�     �� $% PART_ContentHost��6/    � ��$ 2��6M   �� 5�     $% Message��6/    � ��$ 2��6D   5�     P� 5�     / �@J�IsMouseOver0 . . �5�     �� $
% Base��6+    6=   �� *0.(Controls:ControlsHelper.MouseOverBorderBrush)+ �� *TemplatedParent+ � ��5      P� 5     / � {�0 .  . �5     �� $% DisabledVisualElement��6'    �@J�Opacity$	 0.6Q�6J   5     P� 5     / �@��
IsReadOnly0 . . �5     �� $% ReadOnlyVisualElement��6'    �@J�Opacity$ 1Q�6J   5     P� 5     / �@J�	IsFocused0 . . �5     �� $
% Base��6+    6=   �� *,*(Controls:ControlsHelper.FocusBorderBrush)+ �� *TemplatedParent+ � ���� 5	     $% FloatingMessage��6*   # �   6G    � ���� 5
     $% FloatingMessage��6'    �@J�Opacity$ 1Q�6D   5     P� 5     $1 PART_ClearText��6+   / �@J�IsMouseOver0 . . �5     �� $% PART_ClearText��6*   # �  6F    � ���� 5     $% PART_ClearText��6*   # �   6F    � ��5     P� 5     $1 PART_ClearText��6)   / � ��0 . . �5     �� $% PART_ClearText��6*   # �  6F    � ���� 5     $% PART_ClearText��6*   # �  6F    � ��5     ]� 5     + 5     �� 5     , �@ HasText- .  6B   �� 5     , �@J�	IsFocused- . 6-   & 5     �� 5     .��5     �� .��5     W� $
 0:0:0.2D�6    $	 0.2X�63   $ Opacity�6<   $ Message��6`   5     5     * 5      �� 5!     .��5"     �� .��5#     W� $
 0:0:0.2D�6    $ Opacity�63   $ Message��6W   5$     5%     5'     P� 5(     / �@ HasText0 . & 5)     �� 5*     .��5+     �� .��5,     W� $
 0:0:0.2D�6    $	 0.2X�63   $ 0X�6>   $ Opacity�6E   $ Message��6i   5-     5.     * 50     �� 51     .��52     �� .��53     W� $
 0:0:0.2D�6    $ Opacity�63   $ Message��6W   54     55     57     P� 58     / �@ IsWaitingForData0 . & 59     �� 5:     .��5;     �� .��5<     W� 2 . $
 0:0:2D�63   $ 0X�6D   $3 Forever��6M   $ 1X�6f   $ (Effect).Opacity�6m   $
 Base��6�   5=     5>     . �5@     �� $
% Base��6&    �@J�Effect 5A       5B     $4 10Q�6    #5 �  60   $6 0Q�6U   $7 0Q�6a   5D     5E     P� 5F     / �@ IsWaitingForData0 .  . �5G     �� $
% Base��6&    68   Z�  �@J�Effect5H     5J     5L  	   5M     5N     .��6&   5O       5P     -   5        $> txtDataA��5P  h   $��26��6   $
��Left=�6&   $	��Top=�6A   $��91.667��6Y   $? 
00/00/0000��6{   $@ Upper=�6�   $��269.741,40.92,0,0q�6�   $��3��6�   #���  6�   {�.  6  5Q     5R     5S     