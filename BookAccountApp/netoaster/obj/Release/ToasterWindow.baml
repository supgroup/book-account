   M S B A M L   `   `   `  ���� D  @netoaster, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:netoaster	netoaster      netoaster.ToasterWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     -	netoasterclr-namespace:netoaster     $	��300��  � SizeToContent$  Height=� � WindowStyle$
 None=� � ShowInTaskbar .   � AllowsTransparency . 
������ .���� -   $��Notification������ *��  �� TypeInTargetAssembly   netoaster.ThemeKeys"   �� 
ResourceId$ 	ToastCard��+._�]� $��5q�.+��� $
��Left=����� *�� "  $ 	ToastIcon��+�� ���� *�� "  $ ToastIconCanvas��+.+�F� -    F�Name$	 Ico��$��38��$��38�� �� Left$ 19��
 �� Top$ 19��	 �� Stretch$
	 Fill=�
������������2���!��x���!�@�e2��@�e�x���x�@�e�2�@�e���!�x����!2��2���!����V��hT�X��р�C�*�јV�X�������V���*���C�X��hT�јV���Q��C_)���@@�hT����Ѩ��hT��_)���@@��CѨ���V��� $��Right=�.+��� ���� *Title+
 �� RelativeSource
 ��  �� AncestorType$ netoaster:ToasterWindowL� �� Mode$ FindAncestor=����� *�� "  $ 
ToastTitle��+�� ���� *	Message+
 �� $ netoaster:ToasterWindowL�$ FindAncestor=����� *�� "  $ ToastMessage��+