   M S B A M L   `   `   `  ����F  BRadioPlayer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:RadioPlayer.ViewsRadioPlayer.Views  5      ?$clr-namespace:RadioPlayer.ViewModelsRadioPlayer.ViewModels  5   �   #    RadioPlayer.Views.ChannelView   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      1localclr-namespace:RadioPlayer.Views     5      c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5      h dMicrosoft.Expression.Interactions, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bei9http://schemas.microsoft.com/expression/2010/interactions  6S   5
viewModels$clr-namespace:RadioPlayer.ViewModels  6�   $	��148��5   3   $	��300��6@   .��5	      � .+�5
   
   �� -   5          ��Name$  NameTextBox��5
      $��23��6'   $��
10,36,10,0q�63    �� TextWrapping$
 Wrap=�6G   $	��Top=�6[    J� RenderTransformOrigin$ 0.5,0.5&�6�   ��6s   �� *RadioChannel.ChannelName+.��6�   z�5      V� 5      .~�5      �� �� 5      �� 5       �� Angle$ -0.166Q�6&   T� 5       T� X$ -0.076Q�6)   5      .��5   '   5      �� 5   
   -   5         ��Name$ label��5      $��Name:��6   $
��Left=�6/   $��	10,10,0,0q�6J   $	��Top=�6]   $ 0.5,0.5&�6u   z�5      V� 5      .~�5      �� �� 5      �� 5      $ -0.166Q�6&   T� 5      $ -0.147Q�6)    T� Y$ 0.342Q�64   5      5      �� 5   
   -   5        $  
UrlTextBox��5      $��23��6&   $��
10,90,10,0q�62   $
 Wrap=�6F   $	��Top=�6Z   $ 0.5,0.5&�6�   ��6r   �� *RadioChannel.ChannelUri+.��6�   z�5      V� 5       .~�5!      �� �� 5"      �� 5#      $ -0.166Q�6&   T� 5$      $
 0.08Q�6)   5%      .��5&   '   5'      �� 5(   
   -   5        $ 
label_Copy��5(      $
��Url:��6$   $
��Left=�63   $��	10,64,0,0q�6N   $	��Top=�6a   $ 0.5,0.5&�6y   z�5)      V� 5*      .~�5+      �� �� 5,      �� 5-      $ -0.166Q�6&   T� 5.      $ 0.009Q�6)   $ 0.365Q�63   5/      51      �� 52   
   -   5         ��Name$ 
DoneButton��52      $
��Done��6%   $
��Left=�64   $��135,118,0,0q�6O   $��75��6d   $��20��6o   $	��Top=�6{   $ 0.5,0.5&�6�   z�53      V� 54      .~�55      �� �� 56      �� 57      $ -0.166Q�6&   T� 58      $ 0.156Q�6)   $ -0.065Q�63   59      .  (System.Windows.Interactivity.Interaction   Triggers 5;      /  )System.Windows.Interactivity.EventTrigger  5<      	   	EventName$	 Click��6!   .  (System.Windows.Interactivity.TriggerBase
   Actions.
 5=      >  8Microsoft.Expression.Interactivity.Core.CallMethodAction     
MethodName$ Close��6*      TargetObject 6=   �� 5>      5@      �� 5A   
   -   5        $ CancelButton��5A      $��Cancel��6'   $��215,118,0,0q�68   $��20��6M   $	��Top=�6Y    �� IsCancel . $
��Left=�6�   $��75��6�   $ 0.5,0.5&�6�   z�5B      V� 5C      .~�5D      �� �� 5E      �� 5F      $ -0.166Q�6&   T� 5G      $ 0.156Q�6)   $ -0.297Q�63   5H      5J      5L      5M      