   M S B A M L   `   `   `  ����K  Groadmoveinfinity, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:roadmoveinfinityroadmoveinfinity  5   	   "    roadmoveinfinity.UserDetails   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   0localclr-namespace:roadmoveinfinity     5   	     � Icon$  image/Fun_traffic.ico��5   	    � Title$ Fun Traffic��5	   	   $	��500��6   $	��500��6*   $	��500��66    � WindowStartupLocation$ CenterOwner=�6F   $	��500��6j   ��5
      �� 5   
    �� ImageSource$ /image/welcome_image.PNG��6    j� Stretch$ Uniform=�6<    j� 
AlignmentX$ Center=�6N    j� 
AlignmentY$ Bottom=�6b   6x   .��5      � -   5        $��	grid_main��5       � ColumnDefinitions 5   
   �� 5       � RowDefinitions 5   
   �� 5      $	��45* �6   �� 5      $
��224* �6   .+�5   
   �� $"��Welcome to Fun Traffic Game!��6   $��Center=�5      $��25��61   $��
55,10,57,0q�6=   $	��Top=�6Q   $��Showcard Gothic)�6i   $��24(�6�   $	��380��6�   ���	#FFF1F7F76�   �� 5   
   $��Player Name��6   $��357,62,17,382q�6'   $
��Bold"�6>   ���	#FFF8FFFE	 J� RenderTransformOrigin$	 0.484,0.565&�6g   $��Algerian)�6�   $��16(�6�   $��2��6�   �� 5   
   -   5        $��txt_playername��5      $��355,17,11,346q�6(   $	 0.5,0.5&�6?   $��1��6_   $��Viner Hand ITC)�6l   $��14(�6�   .��6�   z�5      V� 5      .~�5      �� �� 5      �� 5      
 �� Angle$
 0.513Q�6&   T� 5      5       .��5!   '   5"      �� 5$   
   -   5        $��Ok��5$      $��1��6   $��365,52,17,309q�6+   ���#FFFFFF$
��Bold"�6�   $��Snap ITC)�6�   $��16(�6�   ��6X   Z� 6�   �� 5%   
   $��Top 5 Score List��6   $	��144��6,   $��Center=�68   $��140,2,208,359q�6U   $��20(�6l   ���	#FF0C2C2C$	 0.5,0.5&�6�   $��1��6�   $��Viner Hand ITC)�6�   z�5&      V� 5'      .~�5(      �� �� 5)      �� 5*      $
 -12.374Q�6&   T� 5+      5,      5.      �� 5/   
   -   5         ��Name$ listBox_topscorelist��5/      $��197,10,158,278q�60   $��1��6H   $��Stretch=�50      $��0q�66   ���	#FF193232$	 0.5,0.5&�6w   $��Arial)�6�   $��Disabled=�51       � UseLayoutRounding .  ��50   J   Z� z�53      V� 54      .~�55      �� �� 56      �� 57      $
 -16.452Q�6&   T� 58      59      ��5;      �� 5<      .R�5=      �  5>      �� 5?      $��* �6/   �� 5@      $
��100* �6/   .+�5B      �� ��6$   �� *
playername+�� 5C      $��10,0,0,0q�6$   $��1��66   ��6F   �� *score+5D      5E      5G      �� 5H   
   $��FUN TRAFFIC��6   $��1��6'   $��330,287,42,76q�64   $
��Bold"�6K   ���	#FF031513$	 0.5,0.5&�6t   $��16(�6�   $��Viner Hand ITC)�6�   z�5I      V� 5J      .~�5K      �� �� 5L      �� 5M      $
 -21.324Q�6&   T� 5N      5O      5Q      5S      5T      