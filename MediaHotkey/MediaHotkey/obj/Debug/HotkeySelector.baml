   M S B A M L   `   `   `  ����F  BMediaHotkey, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:MediaHotkeyMediaHotkey  5   	        MediaHotkey.HotkeySelector   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:MediaHotkey     5   	     � Title$  Hotkey Selector��5   	   $��161.278��6!   $	��300��62    � WindowStartupLocation$ CenterOwner=�6>   .��5	      � .+�5
   
   �� -   5         ��Name$ 	textBlock��5
      $��Center=�6'   $��0,10,0,0q�6D   $
��Wrap=�6V   $��Hotkey��6j   $	��Top=�6x   �� 5   
   -   5         ��Name$ 	cb_hotkey��5      $��
10,30,10,0q�6&   $	��Top=�6:   �� 5   
   -   5        $ 
textBlock1��5      $��Center=�6(   $��0,57,0,0q�6E   $
��Wrap=�6W   $��Modifier��6k   $	��Top=�6{   �� 5   
   -   5        $ cb_mod��5      $��
10,78,10,0q�6#   $	��Top=�67   .@�5      �� $��NO MOD��6   �� 5      $
��CTRL��6   �� 5      $��SHIFT��6   �� 5      $	��ALT��6   �� 5      $	��WIN��6   5      �� 5   
   -   5         ��Name$ btn_done��5      $
��Done��6#   $��Center=�62   $��	0,105,0,0q�6O   $	��Top=�6b   $��75��6z   5      5      