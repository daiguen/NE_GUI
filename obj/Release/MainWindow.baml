   M S B A M L   `   `   `  ���� J  FNew_Enforce_GUI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:New_Enforce_GUINew_Enforce_GUI       New_Enforce_GUI.MainWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     /localclr-namespace:New_Enforce_GUI       � Icon$  Logo.ico�� � Title$ Enforce��$	��800��$	��600�� � 
ResizeMode$ NoResize=�
�������.��� .+�]� $	��Top=�$��25��.+�o� $	��Top=�$��25������  �� EndPoint$ 0.5,1&� �� 
StartPoint$ 0.5,0&�.���  � Color$ Black�� � Offset$ 1Q�� $ White��$
 0.99Q�.@�m� $
��File��.@�m� -   $��newMenuItem��$	��New�� m� InputGestureText$ Ctrl+N��m� -   $��openMenuItem��$
��Open��$ Ctrl+O��m� -   $��saveMenuItem��$
��Save��$ Ctrl+S��m� -   $��Save As���� m� -   $��exitMenuItem��$
��Exit��$ Alt+F4��m� $
��Help���� $��	Title.png��$��30,50,150,650q�]� $��30,150,30,170q�.+��� $	��Top=�.@��� $��Step1��.���� $��10,10,10,10q�.+��� $��Stroke Type��$��30��$	��300��$	��Top=�$
��Left=�$��0q�$��14(��� -   $��Stroke_Type_Val��$��30��$	��300��$	��Top=�$
��Left=�$��3q�$��14(��� $��Crank Rotation��$��30��$	��300��$	��Top=�$
��Left=�$��0q�$��14(��� -   $��CrankRot_Val��$��30��$	��300��$	��Top=�$
��Left=�$��3q�$��14(��� $	��RPM��$��30��$	��300��$	��Top=�$
��Left=�$��0q�$��14(��� -   $��RPM_Val��$��30��$	��300��$	��Top=�$
��Left=�$��3q�$��14(��� $��The Number of Cylinders��$��30��$	��300��$	��Top=�$
��Left=�$��0q�$��14(��� -	   $��
NumCyl_Val��$��30��$	��300��$	��Top=�$
��Left=�$��3q�$��14(��� $ ��Throw Angle / Firing Order��$��30��$	��300��$	��Top=�$
��Left=�$��0q�$��14(��� -
   $��	Order_Val��$��30��$	��300��$	��Top=�$
��Left=�$��3q�$��14(��� $��Bank Angle [deg]��$��30��$	��300��$	��Top=�$
��Left=�$��0q�$��14(��� -   $��BankAng_Val��$��30��$	��300��$	��Top=�$
��Left=�$��3q�$��14(��� $��Step2��.���� $��10,10,10,10q�.+��� $��1st Cylinder Postion [mm]��$��30��$	��300��$	��Top=�$
��Left=�$��0q�$��14(��� -   $��fCylPos_Val��$��30��$	��300��$	��Top=�$
��Left=�$��3q�$��14(��� $��Cylinder Distance [mm]��$��30��$	��300��$	��Top=�$
��Left=�$��0q�$��14(��� -   $��
CylDis_Val��$��30��$	��300��$	��Top=�$
��Left=�$��3q�$��14(��� $"��Crank Mass per Cylinder [kg]��$��30��$	��300��$	��Top=�$
��Left=�$��0q�$��14(��� -   $��CrankMass_Val��$��30��$	��300��$	��Top=�$
��Left=�$��3q�$��14(��� $��Piston Mass [kg]��$��30��$	��300��$	��Top=�$
��Left=�$��0q�$��14(��� -   $��	Pmass_Val��$��30��$	��300��$	��Top=�$
��Left=�$��3q�$��14(��� $��Piston Bore [mm]��$��30��$	��300��$	��Top=�$
��Left=�$��0q�$��14(��� -   $��	Pbore_Val��$��30��$	��300��$	��Top=�$
��Left=�$��3q�$��14(��� $��Step3��.���� $��10,10,10,10q�.+��� $��Conrod Mass [kg]��$��30��$	��300��$	��Top=�$
��Left=�$��0q�$��14(��� -   $��ConMass_Val��$��30��$	��300��$	��Top=�$
��Left=�$��3q�$��14(��� $��Conrod Inertia [kg*mm^2]��$��30��$	��300��$	��Top=�$
��Left=�$��0q�$��14(��� -   $��ConIner_Val��$��30��$	��300��$	��Top=�$
��Left=�$��3q�$��14(��� $��Conrod Length [mm]��$��30��$	��300��$	��Top=�$
��Left=�$��0q�$��14(��� -   $��
ConLen_Val��$��30��$	��300��$	��Top=�$
��Left=�$��3q�$��14(��� $��Conrod COG [mm]��$��30��$	��300��$	��Top=�$
��Left=�$��0q�$��14(��� -   $��
ConCOG_Val��$��30��$	��300��$	��Top=�$
��Left=�$��3q�$��14(��� $ ��Main Bearing Distance [mm]��$��30��$	��300��$	��Top=�$
��Left=�$��0q�$��14(��� -   $��MB_Val��$��30��$	��300��$	��Top=�$
��Left=�$��3q�$��14(��� $��Step4��.���� $��10,10,10,10q�.+��� $��1st Web Position [mm]��$��30��$	��300��$	��Top=�$
��Left=�$��0q�$��14(��� -   $��fWebPos_Val��$��30��$	��300��$	��Top=�$
��Left=�$��3q�$��14(��� $��Web Distance [mm]��$��30��$	��300��$	��Top=�$
��Left=�$��0q�$��14(��� -   $��
WebDis_Val��$��30��$	��300��$	��Top=�$
��Left=�$��3q�$��14(��� $��Web Mass [kg]��$��30��$	��300��$	��Top=�$
��Left=�$��0q�$��14(��� -   $��WebMass_Val��$��30��$	��300��$	��Top=�$
��Left=�$��3q�$��14(��� $��Stroke [mm]��$��30��$	��300��$	��Top=�$
��Left=�$��0q�$��14(��� -   $��
Stroke_Val��$��30��$	��300��$	��Top=�$
��Left=�$��3q�$��14(��� $��Web COG [mm]��$��30��$	��300��$	��Top=�$
��Left=�$��0q�$��14(��� -   $��
WebCOG_Val��$��30��$	��300��$	��Top=�$
��Left=�$��3q�$��14(��� $��Step5��.��� .+�]� $��30��$	��150��$��340,365,10,10q�.+��� -   $	��Add��$��30��$��70��$
��Left=�
�������$��14(��� -   $��Remove��$��30��$��70��$��Right=�
�������$��14(�]� $	��340��$	��480��$��10,10,10,50q�.+�&   System.Windows.Controls.DataGrid  -   $��
CWDataGrid���� $��Step6��.���� $��10,10,10,10q�.+��� $��Pressure��$��30��$	��300��$	��Top=�$
��Left=�$��0q�$��14(��� -   $��	Press_Val��$��30��$	��500��$	��Top=�$
��Left=�$��3q�$��14(��� -   $��	AddButton��$��Open Pressure File��$��30��$	��200��$��3q�$
��Left=�$��14(��� -    $��RemoveButton��$	��Run��$��30��$��25q�$��20(��� -!   $��StatusMessagBox��
�������$��30,630,30,50q���. $
��Auto=�.��]� $��Bottom=�$��20��.+��� $��Bottom=����� .R��  � ColumnDefinitions �� $	��500 ��� $
��Auto �.@��� $��0��.���� -"   $��StatusBarTextBlock���� $��1��.��� -#   $��NE_ProgressBar��	 � Value$	 0Q�$��70��$��12��