   M S B A M L   `   `   `  ����F  BMediaHotkey, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:MediaHotkeyMediaHotkey  5   	       MediaHotkey.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:MediaHotkey     5   	     � Title$  Media Helper��5   	   $��90��6   $	��320��6*    � Icon$ Res/play.png��66    � 
ResizeMode$ NoResize=�6J    � WindowStartupLocation$ CenterScreen=�6`    � ContextMenu 5	      �� 5
   
   .@�5      m� -   5        $��Minimize To Tray��5      61   m� 5      -   5        $��Run As Daemon��5      6.   m� 5      -   5         m�Name$ WindCM_Set_RAD��5      $��Daemon Startup: ��6E   m� 5      -   5        $ HK_ST_Daemon��5      $��Daemon Hotkey��6A   5      .��5      � .+�5   
   � -   5         �Name$ S_Panel��5      $��Center=�6%   $��Center=�6B    � Orientation$ Vertical=�6]   ��6t   �� *Height+ �� ElementName$ M_Panel����6�   �� *Width+$ M_Panel��.+�5      �� -   5        	 ��Name$	 HK_ST_PlayPause��5      
 J� Opacity$
 0Q�61   $��50��6=   $��False��6H    �� TextAlignment$ Center=�6U   $	��Top=�6l   $
��Left=�6�   $��0,16,0,0q�6�   �� 5      -   5        $	 
HK_ST_Prev��5      $
 0Q�6,   $��50��69   $��False��6D   $ Center=�6Q   $	��Top=�6h   $
��Left=�6�   $��0,16,0,0q�6�   �� 5      -   5        $	 
HK_ST_Next��5      $
 0Q�6,   $��50��69   $��False��6D   $ Center=�6Q   $	��Top=�6h   $
��Left=�6�   $��0,16,0,0q�6�   �� 5      -	   5        $	 
HK_ST_FVol��5      $
 0Q�6,   $��50��69   $��False��6D   $ Center=�6Q   $	��Top=�6h   $
��Left=�6�   $��0,16,0,0q�6�   �� 5      -
   5        $	 
HK_ST_LVol��5      $
 0Q�6,   $��50��69   $��False��6D   $ Center=�6Q   $	��Top=�6h   $
��Left=�6�   $��0,16,0,0q�6�   �� 5      -   5        $	 
HK_ST_MVol��5      $
 0Q�6,   $��50��69   $��False��6D   $ Center=�6Q   $	��Top=�6h   $
��Left=�6�   $��0,16,0,0q�6�   5      � 5   
   -   5        $ M_Panel��5      $��Center=�6%   $��Center=�6B   $ Vertical=�6]   .+�5      �� -   5         ��Name$ HK_PlayPause��5      $��50��6*   $��50��66   $��Res/play.png��6A   �� 5      -   5        $ HK_Prev��5      $��50��6%   $��50��61   $��Res/previous.png��6<   �� 5      -   5        $ HK_Next��5      $��50��6%   $��50��61   $��Res/next.png��6<   �� 5      -   5        $ HK_FVol��5      $��50��6%   $��50��61   $��Res/Fvol.png��6<   �� 5      -   5        $ HK_LVol��5      $��50��6%   $��50��61   $��Res/Lvol.png��6<   �� 5       -   5        $ HK_MVol��5       $��50��6%   $��50��61   $��Res/Mvol.png��6<   5!      5"      5#      