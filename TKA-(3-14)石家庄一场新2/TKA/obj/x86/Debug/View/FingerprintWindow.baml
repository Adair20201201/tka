   M S B A M L   `   `   `  ����>  :TKA, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:TKA.ViewTKA.View  5   	   -clr-namespace:TKA.ViewModelTKA.ViewModel  5   	        TKA.View.FingerprintWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   ,	converterclr-namespace:TKA.View     5   	   *vmclr-namespace:TKA.ViewModel     5   	     � WindowStyle$
  None=�5   	    � 
ResizeMode$ NoResize=�5   	   ���	#EE000000$	��300��5	   	   $	��500��5
   	    � Title$ 指纹��5   	    � WindowStartupLocation$ CenterScreen=�5   	    � DataContext 5      #   TKA.ViewModel.FingerViewModel  5   
    � 	Resources 5      +   %TKA.View.BooleanToVisibilityConverter    BooleanToVisibilityConverter"   TKA.View.DateToTextConverter  DateToTextConverter2   ,TKA.View.ReverseBooleanToVisibilityConverter ' #ReverseBooleanToVisibilityConverter  ButtonStyle �� 
TargetType �� Property �� Value	 �� 
TargetType
 J� SnapsToDevicePixels��%�  &	        &	      &	 &     &	 9       5   
   61     5   
   6(     5   
   68   �� 5   
   " ��5      .�5      ��  � ��
 ���� 5      �� 5       � ��$ Center=�5      �� 5       � ��$ Center=�5      �� 5       � �� 5       �� 5!      "	 ��6&   .R�5"      �� #��z��6+   #��z��5#   +   ��. #
 z
 5%   +   #��z��5&   +   5'      5)      5*      5,      .��5.      � ���	#EE000000 � RowDefinitions 5/   
   �� 50      $
��Auto �6   �� 51      $��* �6   .+�55   
   � -   5        $
��20,0q�55      $��60��56      
������ .+�5:      �� $��指纹录入��6   $��Center=�5;      $
��Left=�5<      
�������$��25(�5>      $8��2pack://application:,,,/Style/Font/#微软简老宋)�5?      �� 5I      $��1��6   $��Bottom=�5J      ���	#FF3e3e3e5K      5L      � 5O   
   $��1��6    � ColumnDefinitions 5P      �� 5Q      �� 5R      .+�5T      �� $��	主值班��6   $��16(�5U      ���#818283$	��119��5W      $	��188��5X      .��5Y      � .+�5Z      �� $��请录入指纹...��6    $��Center=�5[       $��Center=�5\       y�5]       �� *PremFingerprintCheckSuccess+ �� 	Converter# [ �� 5_      $)��#/TKA;component/Style/Image/Able.png��6   $��71��5`      $��82��5a      y�5b      �� *PremFingerprintCheckSuccess+# [  5c      5d      �� 5e      $��1��6   $��	副值班��5f      $��16(�5g      ���#818283$	��119��5i      $	��188��5j      .��5k      � .+�5l      �� $��请录入指纹...��6    $��Center=�5m       $��Center=�5n       y�5o       �� *ViceFingerprintCheckSuccess+# [ �� 5q      $)��#/TKA;component/Style/Image/Able.png��6   $��71��5r      $��82��5s      y�5t      �� *ViceFingerprintCheckSuccess+# [  5v      5w      5x      5y      5z      