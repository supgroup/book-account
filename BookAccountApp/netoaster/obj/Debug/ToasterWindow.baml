   M S B A M L   `   `   `  ����D  @netoaster, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:netoaster	netoaster  5   	       netoaster.ToasterWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -	netoasterclr-namespace:netoaster     5   	   $	��300��5        � SizeToContent$  Height=�5	       � WindowStyle$
 None=�5
       � ShowInTaskbar .   � AllowsTransparency . 
������ .��5      �� -   5        $��Notification��5      ��6!   �� *��  �� TypeInTargetAssembly   netoaster.ThemeKeys"   �� 
ResourceId$ 	ToastCard��+._�5   
   ]� $��5q�6   .+�5      �� $
��Left=�6   ��6.   �� *�� "  $ 	ToastIcon��+�� 5      ��6   �� *�� "  $ ToastIconCanvas��+.+�5      F� -   5         F�Name$	 Ico��5      $��38��6-   $��38��68    �� Left$ 19��6D   
 �� Top$ 19��6U   	 �� Stretch$
	 Fill=�6e   
������������2���!��x���!�@�e2��@�e�x���x�@�e�2�@�e���!�x����!2��2���!����V��hT�X��р�C�*�јV�X�������V���*���C�X��hT�јV���Q��C_)���@@�hT����Ѩ��hT��_)���@@��CѨ���V�6�   5      5      �� 5      $��Right=�6   .+�5      �� ��6   �� *Title+
 �� RelativeSource
 ��  �� AncestorType$ netoaster:ToasterWindowL� �� Mode$ FindAncestor=���5      �� *�� "  $ 
ToastTitle��+�� 5      ��6   �� *	Message+
 �� $ netoaster:ToasterWindowL�$ FindAncestor=���5      �� *�� "  $ ToastMessage��+5      5      5      5      