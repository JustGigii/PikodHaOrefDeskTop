   M S B A M L   `   `   `  ���� J  FPikodAorfLayout, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:PikodAorfLayoutPikodAorfLayout  d `DotNetProjects.Input.Toolkit, Version=6.1.94.0, Culture=neutral, PublicKeyToken=79778c5f2eed289bgKclr-namespace:System.Windows.Controls;assembly=DotNetProjects.Input.ToolkitSystem.Windows.Controls     PikodAorfLayout.Setting   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     /localclr-namespace:PikodAorfLayout     _controlKclr-namespace:System.Windows.Controls;assembly=DotNetProjects.Input.Toolkit     -   $	��700��$	��925��  � WindowStyle$
  None=�
������  � 
ResizeMode$ NoResize=� � ShowInTaskbar$
 True��$y�Visible=� � AllowsTransparency$
 True�� � 	Resources  �� Source�� $ scroll.xaml?�%    .���� ���	#FFF5DE9C �� CornerRadius$ 20��._��  � ColumnDefinitions �� $	��200 ���  � RowDefinitions �� $��75 ��� .+��� -   $��0��$��1�����	#FFFFAB45$
��Left=�$��
655,0,0,39q�	 J� RenderTransformOrigin$	 0.5,0.5&�$��65��$ 4 20 4 4��._��� -   
 ��Name$
 image��$��/deleticon.png��$��5q�$	 	0.764,0.5&��� $��Center=�$��36(�
�������$��Hobo Std)�$��0,13,0,0q�$��1��$	��294�� �� TextAlignment$ Center=�$
��Bold"�:הגדרת אלפקציה�� $��1���� $
��Left=�$��75��$��52,0,0,0q�$��0��$	��Top=�$	��100��$��	/logo.png���� $��1��$��0��$ 20�����	#FFE88813$��4,4,4,4q����	#FFFFAB45$��0,10,0,0q��� $��Center=�$��0,10,0,0q�$��24(�
�������$��Hobo Std)�$��0��$��1��$	��200��$ Center=�$
��Bold"�$��	Underline{�:רשימת מעקב�� $��0��$��1��$��	0,40,0,24q�$��RightToLeft=�.��� $	��180��.+��� -    ��Name$ choose��$��0,0,0,10q����� ���� .R��� $��Bottom=����	#FFE88813$��0q�$��0,0,0,1q�$ 4��._��� $��18(�
�������$��Hobo Std)�$	��200��$
 Left=�$��5,0,0,0q����� (� $��1��$��1��$��445,25,20,460q�$��40��.��	� -    	�Name$ allcheak�� 	� 	GroupName$ ChoiseAlarm��$��1��$*��$להציג את כל  הישובים��$��RightToLeft=�
�������(� $��1��$��1��$��518,84,20,411q�$��40��.��	� -   $ DistrictsCheck��$ ChoiseAlarm��$��1��$ ��הצג לפי אזורים��$��RightToLeft=�
�������(� $��1��$��1��$��0,347,20,159q�$��Right=�$	��317��.��	� -   $ 	cityCheck��$ ChoiseAlarm��$��1��$��הצג ערים��$��RightToLeft=�
�������$��62��$	��123���� $��1��$��1��$ 10�����	#FFE88813$��4,4,4,4q����	#FFFFAB45$��216,134,275,303q�._��� -    ��Name$ DistrictsPanel��$��0,6,1,0q�$	��200��$	��Top=�$��RightToLeft=�.���� $	��229��.+��� -	   $ districtscontrolchoose��$��0 0 0 0q�$	��237������ ���� .R�� -
    �� �� $��55 �.+��� $��0 0 0 0q�$��1��$��/deleticon.png��$��25��$��25���� $��0��$��Hobo Std)�
���   �$��17(�$
 Left=�$��6 0 0 0q����� *name+�� -   $ districtscontrol��$��0 0 0 0q�$	��237������ ���� .R�� -    �� �� $��55 �.+��� $��0 0 0 0q�$��1��$��/addicon.png��$��25��$��25���� $��0��$��Hobo Std)�
�������$��17(�$
 Left=�$��6 0 0 0q����� *name+�� -   $��2q�$��1��$��
שמירה��$
��Left=�$��
29,553,0,0q�$��1��$	��Top=����	#FFFFAB45$��37��$��75��
����������	#FFE88813��  �� Orientation$ Vertical=�$	��Top=�$
��Left=�$��1��$��1��$��237,381,0,0q�.+��� -    ��Name$ autoTextBoxBorder��$	��220��$��50�����	#FFFFAB45���	#FFE88813$��4,4,4,4q�$ 3��._��� -    ��Name$ autoTextBox��$	��211��$��30��$��18(�$��Hobo Std)�
�������$��Center=�$��Center=�$��0q�$��Center=�$��Center=�$��0,0,0,0q�
������  �� TextAlignment$ Right=�$��...הכנס שם ישוב��� -    �Name$ autoListPopup��$y�	Collapsed=�$	��100�� � 	StaysOpen$ False��$��Bottom=�$
��Left=�$	��Top=�.���� -    ��Name$ autoList��$��Hobo Std)�
����������	#FFFFAB45$y�	Collapsed=�$	��250��$	��100��$��RightToLeft=��� -    ��Name$ clearbox��$��2q�$��1��$��
איפוס��$
��Left=�$��
88,387,0,0q�$��1��$	��Top=����	#FFFFAB45$��38��$��75��
����������	#FFE88813�� -    ��Name$ label��$��1��$��גודל מסך��$
��Left=�$��575,471,0,0q�$��1��$	��Top=�
�������$��Hobo Std)�$��24(��� -   $	 big��$��2q�$��1��$��גודל��$
��Left=�$��402,471,0,0q�$��1��$	��Top=����	#FFFFAB45$��37��$��75��
����������	#FFE88813$��16(��� -   $	 mid��$��2q�$��1��$��בינוני��$
��Left=�$��296,471,0,0q�$��1��$	��Top=����	#FFFFAB45$��37��$��74��
����������	#FFE88813$��16(��� -   $ small��$��2q�$��1��$��קטן��$
��Left=�$��188,471,0,0q�$��1��$	��Top=����	#FFFFAB45$��37��$��74��
����������	#FFE88813$��16(�