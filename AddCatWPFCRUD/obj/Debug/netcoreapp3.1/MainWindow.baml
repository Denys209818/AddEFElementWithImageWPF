   M S B A M L   `   `   `  ����H  DAddCatWPFCRUD, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:AddCatWPFCRUDAddCatWPFCRUD  5   	       AddCatWPFCRUD.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -localclr-namespace:AddCatWPFCRUD     5   	   -   5          � Title$  
MainWindow��5   	   $	��450��6   $	��800��6)   .��5	      ]� .+�5
   
   �� $��Right=�6   
�������$	��150��6D   .+�5      �� -   5        $��btnAddEl��5      $!��Додати елемент��6%   $
��5,10q�6>   $��30��6L   6X   5      &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ dgSimple��5         AutoGenerateColumns$ False��6%      Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ Ім'я��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$ 1* 6L   1  +System.Windows.Controls.DataGridBoundColumn   Binding 63   �� *Name+6L   4  .System.Windows.Controls.DataGridTemplateColumn  5      $# Дата Народження��6)   $ 3* 6B      CellTemplate 5      �� 5      .R�5      (  "System.Windows.Controls.DatePicker  $��0q�6K      SelectedDate 6)   �� *
Birthday+6K   5      5      	   RowDetailsTemplate	 5      �� 5      .R�5      � .+�5      �� -   5        $
��10,0q�5   !   
�������

 � Tag
 6/   �� *Id+.��5      �� -   5        $
��Fill=�5   n   $��64��6~   $��64��6�   ��6$   �� *	ImgPath+
 6?   �� *	ImgPath+6�   5      � 5       � ColumnDefinitions 5      �� 5    "   �� 5!   "    � RowDefinitions 5#      �� 5$   "   �� 5%   "   �� 5&   "   .+�5)      �� $
��ID: ��6(   $
��Bold"�64   $��0��6F   $��0��6S   �� 5*      $��Name: ��6(   $
��Bold"�66   $��1��6H   $��0��6U   �� 5+      $��
Birthday: ��6(   $
��Bold"�6:   $��2��6L   $��0��6Y   �� 5-      $��0��6<   $��1��6I   ��6(   �� *Id+6I   �� 5.      $��1��6>   $��1��6K   ��6(   �� *Name+6K   �� 5/      $��2��6B   $��1��6O   ��6(   �� *
Birthday+6O   50      51      52      54      55      56      