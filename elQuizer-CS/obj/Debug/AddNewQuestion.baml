   M S B A M L   `   `   `  ����  elQuizer-CS     elQuizer_CS.AddNewQuestion   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   -   5          � Title$  Add New Question��5   	   $	��353��6"   $	��537��6/    � WindowStartupLocation$ CenterScreen=�6;    � WindowStyle$ 
ToolWindow=�6`   .��5      �  � RowDefinitions 5   
   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5
   
   �� .+�5      � $��7q�6    � ColumnDefinitions 5      �� 5      $
��Auto �6'   �� 5      $��* �6'   .+�5      �� $��	Question:��6   $
��Left=�6,   $	��Top=�6G   �� 5      -   5         ��Name$ question_txt��5       �� 	MaxLength$	 100��60   $��Center=�6@   $��1��6b   6�   5      �� 5      -   5         ��Name$ question_tabs��5      $��7,0,7,7q�60   .@�5      �� $��Short Answer��6   .��5      �� .+�5      � $��7q�6    5      �� 5   "   $
��Auto �63   �� 5   "   $��* �63   .+�5      �� $��Correct Answer:��6$   $
��Left=�6>   $	��Top=�6Y   �� 5      -   5        $ short_answer_txt��5   &   $��1��6@   $	 100��6P   $��Stretch=�6`   $��Center=�6|   6�   5      5      5       �� 5!      $��Fill The Blank��6   .��5"      �� .+�5#      � $��7q�6    5$      �� 5%   "   $
��Auto �63   �� 5&   "   $��* �63   .+�5(      �� $��Words:��6$   $
��Left=�65   $	��Top=�6P   � 5)      -   5         �Name$ blanks_wrap��5)   (   $��1��6=   5*      5+      5,      �� 5-      $��Mutlichoice��6   .��5.      �� .+�5/      � $��7q�6    50      �� 51   "   $
��Auto �63   �� 52   "   $��* �63   �� 53   "   $
��Auto �63   .+�55      �� $��New Choice:��6$   $
��Left=�6:   $	��Top=�6U   �� 56      -   5        $ 	mutli_txt��56   &   $��1��69   $��Stretch=�6I   $	 100��6e   $��Center=�6u   $��0,0,7,0q�6�   6�   �� 57      -   5        	 ��Name$	 	mutli_btn��57   %   $��2��68   $��50��6H   $��26��6S   $	��Top=�6_   .��6�   	Add (↵)6�   58      � 59      $��7q�6    5:      �� 5;   "   $
��Auto �63   �� 5<   "   $��* �63   .+�5>      �� $��Correct Choice:��6$   $
��Left=�6>   $	��Top=�6Y   �� 5?      -   5        $��
choices_sp��5?   )   $��1��6;   $��Center=�6K   $��	10,10,0,0q�6f   5@      5A      5B      �� 5C      $��
True False��6   .��5D      �� .+�5E      � $��7q�6    5F      �� 5G   "   $
��Auto �63   �� 5H   "   $��* �63   .+�5J      �� $��Correct Answer:��6$   $
��Left=�6>   $	��Top=�6Y   �� 5K      $��1��6)   $��Center=�69   $��	10,10,0,0q�6T   .+�5L   "   	� -	   5        
 	�Name$
 true_rb��5L   .   $
��True��6?   $��0,0,0,7q�6O    d� 	IsChecked$
 TrueY�6`   	� 5M   "   $��False��6.   $��0,0,0,0q�6>   5N      5O      5P      5Q      5R      5S      �� 5T   
   -
   5        $	 add_question_btn��5T      $��1��6+   $��7,0,7,7q�68   $��27��6I   .��6t   Add Question (Ctrl + S)6�   5U      5W      