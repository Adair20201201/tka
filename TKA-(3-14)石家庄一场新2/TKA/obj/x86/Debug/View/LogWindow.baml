   M S B A M L   `   `   `  ����>  :TKA, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:TKA.ViewTKA.View  5   	       TKA.View.LogWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   'viewclr-namespace:TKA.View     5   	     � WindowStyle$
  None=�5   	    � 
ResizeMode$ NoResize=�5   	    � Title$ 查看日志��5   	   $	��500��6   $
��1000��6#    � 	Resources 5      '   !TKA.View.StartDateToTextConverter   StartDateToTextConverter%   TKA.View.EndDateToTextConverter  EndDateToTextConverter$   TKA.View.TrackMessageConverter  TrackMessageConverter  ButtonStyle �� 
TargetType �� Property �� Value �� 
TargetType J� SnapsToDevicePixels  SearchButtonStyle  ScrollButtons  VerticalScrollThumbs	 �� RadiusX
 �� RadiusY  HorizenScrollThumbs  VerticalScrollBarStyle �Name � RowDefinitions ��Name F�Name ]�Name ]� Thumb n�Name 	 StandardBrush 
 HorizenScrollBarStyle � ColumnDefinitions=  7System.Windows.Controls.Primitives.DataGridColumnHeader ��Name �� Padding ��Name*  $System.Windows.Controls.DataGridCell&   System.Windows.Controls.DataGrid �� Color ��Name   SelectAllCommand �� 
ResourceId �� TypeInTargetAssembly7  1System.Windows.Controls.DataGridHeadersVisibility
   All �� ConverterParameter!   HeadersVisibilityConverter �� 	Converter �� RelativeSource  �� AncestorTypeG  ASystem.Windows.Controls.Primitives.DataGridColumnHeadersPresenter!  Name"   Column# ��Name$ �� CanContentScroll% ��Name& �� ScrollableHeight' � Maximum( �� Orientation*) �� #ComputedVerticalScrollBarVisibility* �� ViewportHeight+ �� ViewportSize, � Value- �� Mode. �� ScrollableWidth,/ �� %ComputedHorizontalScrollBarVisibility0 �� ViewportWidth  ComboBoxToggleButton1 �� 	ClickMode2 d� 	IsChecked3 �� Path4 J� IsHitTestVisible5 �� SelectionBoxItem6 �� SelectionBoxItemTemplate7 �� IsDropDownOpen8 � AllowsTransparency9 � ActualWidth: �� MaxDropDownHeight; K� IsItemsHost-	  'System.Windows.Input.KeyboardNavigation< 	  DirectionalNavigation= �� Triggers> P� Property? P� Value@ �� 
TargetNameA P� 
SourceNameB �� TriggersC �� 
StartPointD �� EndPoint��%4)  &	        &	      &	 &     &	 9     &	 �    &	 �    &	 �    &	 .    &	 �    &	
 �
    '       '      '      7 
 &	 U    '�� �    7  '�� �%      5	   
   6(     5
   
   6&     5   
   6%   �� 5   
   " ��5      .�5      ��  � ��
 ���� 5      �� 5       � ��$ Center=�5      �� 5       � ��$ Center=�5      �� 5       � �� 5      �� 5      " ��6&   .R�5      �� #��z��6+   #��z��5   +   ��. # z 5   +   #��z��5   +   5      5      5       �� 5!   
   " ��6*   .�5"      ��  � �� �#3A9BB86+   �� 5#       � ��
 �����6+   �� 5$       � ��$ Microsoft YaHei)�6+   �� 5%       � ��$ 16(�6)   �� 5&       � �� 5'      �� 5(      " ��6&   .R�5)      �� #��z��6!   ._�5*      �� $��Center=�6/   ��. $��Center=�6g   5+      5,      5.      5/      �� 52   
   " ��53      .�54      ��  � �� 55      �� 56      " ��6&   .R�57      �� $��Border��6!   
������ ._�59      �� 5:      5;      5=      5>      �� 5?   
   " n�5@      .�5A      ��  � ��
 ���� 5B      �� 5C       � �� 5D      �� 5E      " n�6&   .R�5F      � $
��Grid��6   .+�5G      �� $��
Rectangle1��6(   $��7��5H   (   $
��Auto��5I   (   $��Stretch=�5J   (   $��Stretch=�5K   (   #��z��5L   (   $	 4��5M   (   $
 4��5N   (   5O      5P      5R      5S      �� 5T   
   " n�5U      .�5V      ��  � ��
 ���� 5W      �� 5X       � �� 5Y      �� 5Z      " n�6&   .R�5[      � $
��Grid��6   .+�5\      �� $��
Rectangle1��6(   $��7��5]   (   $
��Auto��5^   (   $��Stretch=�5_   (   $��Stretch=�5`   (   #��z��5a   (   $	 4��5b   (   $
 4��5c   (   5d      5e      5g      5h      �� 5i   
   " ��5j      .�5k      ��  � ��
 ���� 5l      �� 5m       � ��$ 12��5n      �� 5o       � �� 5p      �� 5q      " ��6&   .R�5r      � $ GridRoot��6   #��� ��5s      #��z��5t       5u      �� 5v   "   $��18��60   �� 5w   "   $��0.00001* �60   �� 5x   "   $��18��60   .+�5z      �� $ DecreaseRepeat��6+   $��ScrollBar.LineUpCommand��5{   +   #���  5|   +   .��5}   "   F� $ DecreaseArrow��6'   $��Center=�5~   '   $��Center=�5   '   +�����g5     �    ��j ���j ��j ��g5     ���#CCCCCC5�   '   5�      ]� 5�      $ 
PART_Track��6$   $��1��5�   $   |�.  ��.  5�   "   n� 5�   &   $ Thumb��6,   ���#CCCCCC#���  5�   ,   5�      �� 5�      $ IncreaseRepeat��6+   $��2��5�   +   $��ScrollBar.LineDownCommand��5�   +   #��� 	 5�   +   #���  5�   +   .��5�   "   F� $ IncreaseArrow��6'   $��Center=�5�   '   $��Center=�5�   '   +�����g5 ��j ���j     �        ��g5 ��j ���#CCCCCC5�   '   5�      5�      5�      5�      5�      �� 5�   
   " ��5�      .�5�      ��  � ��
 ���� 5�      �� 5�       � ��$ 10��5�      �� 5�       � �� 5�      �� 5�      " ��6&   .R�5�      � $ GridRoot��6   #��� ��5�      #��z��5�       5�      �� 5�   "   $��18��63   �� 5�   "   $��0.00001* �63   �� 5�   "   $��18��63   .+�5�      �� $ DecreaseRepeat��6+   $��ScrollBar.LineLeftCommand��5�   +   #���  5�   +   .��5�   "   F� $ DecreaseArrow��6'   $��Center=�5�   '   $��Center=�5�   '   +����    �g5 ���j     ���j ��j �    �g5 ���#CCCCCC5�   '   5�      ]� 5�      $ 
PART_Track��6$   $��1��5�   $   |�.  ��.  5�   "   n� 5�   &   $ Thumb��6,   ���#CCCCCC#���  5�   ,   5�      �� 5�      $ IncreaseRepeat��6+   $��2��5�   +   $ ��ScrollBar.LineRightCommand��5�   +   #��� 	 5�   +   #���  5�   +   .��5�   "   F� $ IncreaseArrow��6'   $��Center=�5�   '   $��Center=�5�   '   +����        ���j �g5 �    ��j �        ���#CCCCCC5�   '   5�      5�      5�      5�      5�      �� 5�   
   "  6   .�5�      ��  � �� �#0550765�      �� 5�       � �� �#FFFFFF5�      �� 5�       � ��$ 14(�5�      �� 5�       � ��$ Light"�5�      �� 5�       � ��$	 160��5�      �� 5�       � ��$ 33��5�      �� 5�       � ��$ Center=�5�      �� 5�       � ��$ Center=�5�      �� 5�       � �� 5�      �� 5�      "  6&   .R�5�      � .+�5�      �� $ HeaderBorder��6%   #��z��5�   %   $ 0q�5�   %   #��z��5�   %   $��0,0,1,0q�5�   %   ���#000000._�5�   "   �� $ HeaderContent��63   $��0,0,0,1q�5�   3   ��. #��z��5�   3   #��z��5�   3   # z 5�   3   5�      5�      5�      5�      5�      �� 5�   
   "  6   .�5�      ��  � ��
 ���� 5�      �� 5�       � ��$ 14(�5�      �� 5       � ��$ Microsoft TaHei)�5     �� 5      � �� �#64BFDF5     �� 5      � ��$ 33��5     �� 5      � ��$ Center=�5     �� 5      � ��$ Center=�5	     �� 5
      � �� 5     �� 5     "  6&   .R�5     �� #��z��6!   ._�5     �� $��Center=�6/   $��Center=�5  /    . 5  /   5     5     5     5     �� 5  
   "  6   .�5     ��  �@ VerticalGridLinesBrush 5     �� 5     $ #000000��6&   5     �� 5      �@ HeadersVisibility$ Column=�5     �� 5      �@ AutoGenerateColumns .  5      �� 5!      � ��
 ���� 5"     �� 5#      �@ RowBackground �	#8A1C1E1F5$     �� 5%       �@ AlternatingRowBackground
 ���� 5&     �� 5'      �@ RowHeaderWidth$ 1Q�5(     �� 5)      �@ CanUserAddRows .  5*     �� 5+      �@ CanUserReorderColumns .  5,     �� 5-      �@ CanUserResizeColumns .  5.     �� 5/      �@ CanUserSortColumns .  50     �� 51      �@ 
IsReadOnly . 52     �� 53      � �� 54     �� 55     "  6&   .R�56     �� $ DG_ScrollViewer��6'   |�.  ��58     �� 59  "   " ��62   .R�5:  &   �  5;  *   �� 5<  .   $
��Auto �6?   �� 5=  .   $��* �6?   �� 5>  .   $
��Auto �6?    5@  *   �� 5A  .   $
��Auto �6<   �� 5B  .   $��* �6<   �� 5C  .   $
��Auto �6<   .+�5E  *   �� #��Z 61   |�.  ��5G  1   C� *�� $" DataGridSelectAllButtonStyle��"  +y�5H  1   �� *HeadersVisibility+# Z # Z  �� "   ��5I  1   �� *CellsPanelHorizontalOffset+ �� "     5J  *   $!! PART_ColumnHeadersPresenter��6I   $��1��5K  I   y�5L  I   �� *HeadersVisibility+# Z" # Z  �� "   �� 5M  *   $!# PART_ScrollContentPresenter��6A   #$ z��5N  A   $��2��5O  A   $��1��5P  A   �� 5Q  *   $% PART_VerticalScrollBar��64   $��2��5R  4   #' z& 5S  4   $( Vertical=�5T  4   $��0��5U  4   $��2��5V  4   #y�z) 5W  4   #+ z* 5Y  4   #���  5[  -   , 5X  4   �� *VerticalOffset+$- OneWay=� �� *TemplatedParent+5[  -   � 5\  *   $��1��6/   $��2��5]  3    5^  .   �� 5_  2   ��6C   �� **(NonFrozenColumnsViewportHorizontalOffset+ �� "   �� 5`  2   $��* �6C   .+�5b  .   �� $% PART_HorizontalScrollBar��68   $��1��5c  <   #' z. 5d  <   $( 
Horizontal=�5e  <   #y�z/ 5f  <   #+ z0 5h  <   8��  , 5g  <   �� *HorizontalOffset+$- OneWay=� �� *TemplatedParent+5j  5   5k  +   5l  '   5m  #   .��5o     �� # z 6-   5p     5q     5s     5t     �� 5w  
   " d�67   .R�5x     �  5y     �� 5z     �� 5{     $��20 �6'   .+�5}     �� $ Border��5~     $��2��5     ���#3A9BB85�     F� 5�     $ Arrow��5�     $��1��5�     
�������$��Center=�5�     $��Center=�5�     "����        � 	=  	= � z     	5�     5�     5�     �� 5�  
   " ��6*   .�5�     ��  �@J�SnapsToDevicePixels . 64   �� 5�      �@�OverridesDefaultStyle . 66   �� 5�      � ��$
 Auto=�6K   �� 5�      � ��$
 Auto=�6I   �� 5�      � �� . 6>   �� 5�      � ��$ 91��6)   �� 5�      � ��$ 32��6*   �� 5�      � �� 5�     �� 5�     " ��6&   .R�5�     �  5�     �� 5�  "   �� 5�  "   $��20 �63   .+�5�     d� $��ToggleButton��5�  #   $��2��5�  #   8��  |�.  $1 Press=�5�  #   2 5�  #   �� $3 IsDropDownOpen�$- TwoWay=� �� *TemplatedParent+5�  #   �� 5�     $��ContentSite��5�  #   $��0��5�  #   4 .  #��z5 5�  #   #��z6 5�  #   #��z��5�  #   $��	10,3,23,3q�5�  #   $��Center=�5�  #   $
��Left=�5�  #   � 5�     $��Popup��5�  !   $��Bottom=�5�  !   #��z7 5�  !   8 . |�.  $��Slide=�5�  !   .��5�  "   � $��DropDown��5�  %    . #��z9 5�  %   #��z: 5�  %   .+�5�  &   �� $ DropDownBorder��5�  )   ���	#66000000$��1q�5�  )   ���#8888885�  )   �� 5�  &   $��4,6,4,6q�63    . .��5�  *   �� ; . $< 	Contained=�6H   5�  '   5�  #   5�     5�     = 5�     P� 5�     > �@��HasItems? .  . �5�  "   �� $@ DropDownBorder��6)    � ��$ 95��6Z   5�     P� 5�     > �@��
IsGrouping? . . �5�  "   ��  � �� .  6R   5�     P� 5�     $A Popup��6&   > �@�AllowsTransparency? . . �5�  "   �� $@ DropDownBorder��6)    �@��CornerRadius$ 4��6]   �� 5�  "   $@ DropDownBorder��6)    � ��$ 0,2,0,0q�6W   5�     P� 5�     > �@��
IsEditable? . . �5�  "   ��  � �� .  6>   �� 5�  "   $@ ContentSite��6)    � y�$ Hidden=�6X   5�     5�     5�     B 5�     5�     �� 5�  
   " ��6.   .�5�     ��  �@J�SnapsToDevicePixels . 64   �� 5�      �@�OverridesDefaultStyle . 66   �� 5�      � �� 5�     �� 5�     " ��6&   .R�5�     �  5�     �� 5�  "   $��* �60   �� 5�  "   $��1 �60   .+�5�     �� $��Border��5�  !   $��0��5�  %    . ._�5�  "   �� $��Center=�63   5�     F� 5�     ���#888888$��1��64   $��1��6H   ��5�  "   �� 5�  &   $	C 2,1&�63   $
D 78,1&�6D   5�     5�     = 5�     P� 5�     > �@��IsHighlighted? . . �5�  "   �� $@ Border��6)    � �� �	#EE6666665�  )   5�     5�     5�     5�     5�     .��5�     � ���	#EE000000 5�  
   �� 5      $
��Auto �6   �� 5     $
��Auto �6   �� 5     $��* �6   .+�5  
   � -   5        $
��20,0q�5     $��60��5     
������ .+�5	     �� $��日志��6   $��Center=�5
     $
��Left=�5     
�������$��25(�5     $8��2pack://application:,,,/Style/Font/#微软简老宋)�5     �� 5     -   5        E ��Name$E 	btn_close��5     $��Right=�5     $	��Top=�5     #��[ 5     .��5     �� $8��2pack://application:,,,/Style/Image/CloseButton.png��6   $��46��5     $��41��5     5     �� 5     $��1��6   $��Bottom=�5     ���#3e3e3e5     5     �� 5  
   F �� Orientation$F 
Horizontal=�6   $��1��5     $��20,10q�5     .+�5      �� $��起始日期:��6   $��16(�5!     
�������$��Microsoft YaHei)�5#     $��Center=�5$     $��0,0,5,0q�5%     �� 5&     ���#888888$��1q�5'     $	��210��5(     $��32��5)     $��0,0,20,0q�5*     ._�5+     �  5,     �� 5-     $��* �6+   �� 5.     $
��Auto �6+   .+�50     �� -   5        G ��Name$G 	StartTime��52      $��Center=�50      $��Center=�51      $��16(�53      
���������55      �� H �� ElementName$H PopupStartcal��$3 SelectedDate�# [  �� 56     -   5        $E StartTimeButton��56     #��[ 57     $��30��58     $��1��59     .��5;     �� $5��/pack://application:,,,/Style/Image/Calendar.png��6    $��26��5<      $��24��5=      5>     5?     5@     �� 5B     $��终止日期:��6   $��16(�5C     
�������$��Microsoft YaHei)�5E     $��Center=�5F     $��0,0,5,0q�5G     �� 5H     ���#888888$��1q�5I     $	��210��5J     $��32��5K     $��0,0,60,0q�5L     ._�5M     �  5N     �� 5O     $��* �6+   �� 5P     $
��Auto �6+   .+�5R     �� -   5        $G EndTime��5T      $��Center=�5R      $��Center=�5S      $��16(�5U      
���������5W      �� $H PopupEndcal��$3 SelectedDate�# [ �� 5X     -   5        $E EndTimeButton��5X     #��[ 5Y     $��30��5Z     $��1��5[     .��5]     �� $5��/pack://application:,,,/Style/Image/Calendar.png��6    $��26��5^      $��24��5_      5`     5a     5b     �� 5c     -   5        I ��Name$I MoldBox��5c     $��1q�5d     
�������$��16(�5f     $��91��5g     $��32��5h     $��Microsoft YaHei)�5i     $��0,0,5,0q�5j     $��Center=�5k     J J� 	AllowDropJ .  .@�5m     �� $��全部��6   K �� 
IsSelectedK . 6@   �� 5n     $��警告��6   6.   �� 5o     $��操作��6   6.   5p     5q     �� 5r  
   -   5        $E 
btn_Getout��5r     $��1��5s     $��Right=�5t     $��20,10q�5u     $��导出��5v     #��[ 5w     $��80��5x     $��32��5y     5z     �� 5{  
   -	   5        $E 
btn_search��5{     $��1��5|     $��Right=�5}     $��120,10q�5~     $��搜索��5     #��[ 5�     $��80��5�     $��32��5�     5�       5�  
   -
   5        $��LogGrid��5�     $��2��5�     $��20,10q�5�     L   AutoGenerateColumnsL .  M   
IsReadOnlyM . N   ColumnsN 5�     4
  .System.Windows.Controls.DataGridTemplateColumn
  5�     ,  &System.Windows.Controls.DataGridColumnO   Header$O 类型��6)   P   Width5  /System.Windows.Controls.DataGridLengthConverter$P 2* 5�  )   Q 
  CellTemplateQ 5�     �� 5�     .R�5�     �� ��6(   �� *ItemArray[1]+# [ 5�     5�     
  5�     $O 时间��6)   $P 3* 5�  )   Q 5�     �� 5�     .R�5�     �� ��6(   �� *ItemArray[2]+R �� StringFormat$R yyyy/MM/dd HH:mm:ss��5�     5�     
  5�     $O 事件��6)   $P 5* 5�  )   Q 5�     �� 5�     .R�5�     �� ��6(   �� *ItemArray[3]+5�     5�     5�     � 5�  
   -   5        S �Name$S 
PopupStart��5�     $��Right=�5�     T � 	StaysOpenT .  U � PlacementTargetU 5�     �� $H StartTimeButton��.��5�     &   System.Windows.Controls.Calendar  -   5        V  Name$V PopupStartcal��5�     5�     5�     � 5�  
   -   5        $S PopupEnd��5�     $��Right=�5�     T .  U 5�     �� $H EndTimeButton��.��5�       -   5        $V PopupEndcal��5�     5�     5�     5�     5�     