   M S B A M L   `   `   `  ����  elQuizer-CS!    elQuizer_CS.QuestionManager   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   -   5          �Name$	  ceb��5   	    � Title$ QuestionsManager��5   	   $	��350��6"   $	��700��6/    � WindowStartupLocation$ CenterScreen=�6;   $	��700��6`   $	��350��6o   .��5      � $��7q�6    � ColumnDefinitions 5   
   �� 5      $
��Auto �6   �� 5	      �� 5
      $
��Auto �6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      .+�5   
   �� $��Available Question Lists��6   $��0��64   $��0��6A   $��Center=�6Q   �� 5   
   -   5        $��	dirs_list��5      $��1��6#   $��0��60   $	��300��6@   $��0,7,0,0q�6O   6�   �� 5   
   -   5         ��Name$ list_name_txt��5      $��0��6'   $��1��64   $��7,0,0,0q�6D   $��Center=�6U   .��6y   List Name Here6�   �� 5   
   -   5         ��Name$ question_list��5      $��1��6*   $��1��67   $��7,7,0,0q�6G   6X   �� 5   
   $��1��6   $��2��6"   $��7,7,0,7q�62   $	��180��6C   .+�5      �� -   5        $��Add New Question��5      $��0,0,0,7q�60   $��27��6A   6M   �� 5      -   5        $��Export Current List��5      $��0,0,0,7q�63   $��27��6D   6P   �� 5      -   5        $��Import External List��5      $��0,0,0,7q�65   $��27��6F   6R   �� 5      -   5        $��Clear Current List��5      $��0,0,0,7q�63   $��27��6D   6P   �� 5      -	   5        $��Duplicate Selected List��5      $��0,0,0,7q�68   $��27��6I   6V   �� 5      -
   5        $��Delete Selected List��5      $��0,0,0,7q�65   $��27��6F   6S   5      5      5       