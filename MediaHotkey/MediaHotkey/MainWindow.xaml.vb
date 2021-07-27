Imports System.ComponentModel

Class MainWindow
    Dim GH As GlobalHotkey
    Dim IntHelper As New Interop.WindowInteropHelper(Me)
    'this constant represents the hex value for the key to send to user32.dll
    Const APPCOMMAND_MEDIA_PLAY_PAUSE = &HE0000
    Const APPCOMMAND_VOLUME_MUTE As Integer = &H80000
    Const APPCOMMAND_VOLUME_DOWN As Integer = &H90000
    Const APPCOMMAND_VOLUME_UP As Integer = &HA0000
    Const APPCOMMAND_MEDIA_NEXTTRACK As Integer = &H110000
    Const APPCOMMAND_MEDIA_PREVIOUSTRACK As Integer = &H120000
    Const INTAPPCOMMAND_MEDIA_PLAY_PAUSE = 14
    Const INTAPPCOMMAND_VOLUME_MUTE As Integer = 8
    Const INTAPPCOMMAND_VOLUME_DOWN As Integer = 9
    Const INTAPPCOMMAND_VOLUME_UP As Integer = 10
    Const INTAPPCOMMAND_MEDIA_NEXTTRACK As Integer = 11
    Const INTAPPCOMMAND_MEDIA_PREVIOUSTRACK As Integer = 12
    'this constant represents which command. Sort of like the function in user32.dll we are calling.
    Const WM_APPCOMMAND = &H319
    'this declares the user32.dll call to SendMessageW we are making
    Declare Auto Function SendMessageW Lib "user32.dll" Alias "SendMessageW" (
    ByVal hWnd As Integer,
    ByVal Msg As Integer,
    ByVal wParam As Integer,
    ByVal lParam As Integer) As Integer
    Dim HKStates() As Boolean = {False, False, False, False, False, False, False}
    Dim DZeroAnim As New Animation.DoubleAnimation(0, New Duration(TimeSpan.FromMilliseconds(200)))
    Dim DOneAnim As New Animation.DoubleAnimation(1, New Duration(TimeSpan.FromMilliseconds(200)))
    Dim NIcon As New Forms.NotifyIcon With {.Icon = System.Drawing.SystemIcons.Shield, .Visible = False}
    Dim NIconMenu As New Forms.ContextMenuStrip

#Region "Utils"

    Private Function STRToHotkeyCombo(str As String) As HotkeySelector.HotkeyCombo
        Try
            Dim SplitCombo = str.Split(">")
            If SplitCombo.Length > 0 Then
                Select Case SplitCombo(1)
                    Case 0 'NO MOD
                        Return New HotkeySelector.HotkeyCombo With {.Hotkey = SplitCombo(0), .Modifier = NOMOD}
                    Case 1 'CTRL
                        Return New HotkeySelector.HotkeyCombo With {.Hotkey = SplitCombo(0), .Modifier = CTRL}
                    Case 2 'SHIFT
                        Return New HotkeySelector.HotkeyCombo With {.Hotkey = SplitCombo(0), .Modifier = SHIFT}
                    Case 3 'ALT
                        Return New HotkeySelector.HotkeyCombo With {.Hotkey = SplitCombo(0), .Modifier = ALT}
                    Case 4 'WIN
                        Return New HotkeySelector.HotkeyCombo With {.Hotkey = SplitCombo(0), .Modifier = WIN}
                    Case Else
                        Return New HotkeySelector.HotkeyCombo With {.Hotkey = -1, .Modifier = NOMOD}
                End Select
            Else
                Return Nothing
            End If
        Catch
            Return New HotkeySelector.HotkeyCombo With {.Hotkey = -1, .Modifier = NOMOD}
        End Try
    End Function

    Private Function HotkeyComboToSTR(HK As HotkeySelector.HotkeyCombo) As String
        Return HK.Hotkey & ">" & HK.Modifier
    End Function

    Private Function MODToSTR(i As Integer) As String
        Select Case i
            Case 0 'NO MOD
                Return "NO MOD"
            Case 1 'CTRL
                Return "CTRL"
            Case 2 'SHIFT
                Return "SHIFT"
            Case 3 'ALT
                Return "ALT"
            Case 4 'WIN
                Return "WIN"
            Case Else
                Return Nothing
        End Select
    End Function

    Private Sub UpdateHotkey(i As Hotkeys)
        Dim HKCombo As HotkeySelector.HotkeyCombo
        Select Case i
            Case 0
                HKCombo = STRToHotkeyCombo(My.Settings.MEDIA_PLAYPAUSE)
                GH = New GlobalHotkey(HKCombo.Modifier, HKCombo.Hotkey, IntHelper.Handle, 0) 'PlayPause
                HKStates(0) = GH.Register()
                HK_ST_PlayPause.Text = HKStates(0) & Environment.NewLine & MODToSTR(HKCombo.Modifier) & "+" & HKCombo.Hotkey.ToString
            Case 1
                HKCombo = STRToHotkeyCombo(My.Settings.MEDIA_NEXT)
                GH = New GlobalHotkey(HKCombo.Modifier, HKCombo.Hotkey, IntHelper.Handle, 1) 'Next
                HKStates(1) = GH.Register()
                HK_ST_Next.Text = HKStates(1) & Environment.NewLine & MODToSTR(HKCombo.Modifier) & "+" & HKCombo.Hotkey.ToString
            Case 2
                HKCombo = STRToHotkeyCombo(My.Settings.MEDIA_PREV)
                GH = New GlobalHotkey(HKCombo.Modifier, HKCombo.Hotkey, IntHelper.Handle, 2) 'Previous
                HKStates(2) = GH.Register()
                HK_ST_Prev.Text = HKStates(2) & Environment.NewLine & MODToSTR(HKCombo.Modifier) & "+" & HKCombo.Hotkey.ToString
            Case 3
                HKCombo = STRToHotkeyCombo(My.Settings.VOL_UP)
                GH = New GlobalHotkey(HKCombo.Modifier, HKCombo.Hotkey, IntHelper.Handle, 3) 'V+
                HKStates(3) = GH.Register()
                HK_ST_FVol.Text = HKStates(3) & Environment.NewLine & MODToSTR(HKCombo.Modifier) & "+" & HKCombo.Hotkey.ToString
            Case 4
                HKCombo = STRToHotkeyCombo(My.Settings.VOL_DOWN)
                GH = New GlobalHotkey(HKCombo.Modifier, HKCombo.Hotkey, IntHelper.Handle, 4) 'V-
                HKStates(4) = GH.Register()
                HK_ST_LVol.Text = HKStates(4) & Environment.NewLine & MODToSTR(HKCombo.Modifier) & "+" & HKCombo.Hotkey.ToString
            Case 5
                HKCombo = STRToHotkeyCombo(My.Settings.VOL_MUTE)
                GH = New GlobalHotkey(HKCombo.Modifier, HKCombo.Hotkey, IntHelper.Handle, 5) 'V*
                HKStates(5) = GH.Register()
                HK_ST_MVol.Text = HKStates(5) & Environment.NewLine & MODToSTR(HKCombo.Modifier) & "+" & HKCombo.Hotkey.ToString
            Case 6
                HKCombo = STRToHotkeyCombo(My.Settings.DAEMON)
                GH = New GlobalHotkey(HKCombo.Modifier, HKCombo.Hotkey, IntHelper.Handle, 6) 'Daemon
                HKStates(6) = GH.Register()
        End Select
    End Sub

    Private Sub UnRegHotkey(i As Hotkeys)
        Dim HKCombo As HotkeySelector.HotkeyCombo
        Select Case i
            Case 0
                HKCombo = STRToHotkeyCombo(My.Settings.MEDIA_PLAYPAUSE)
                GH = New GlobalHotkey(HKCombo.Modifier, HKCombo.Hotkey, IntHelper.Handle, 0) 'PlayPause
                HKStates(0) = GH.Unregister()
                HK_ST_PlayPause.Text = HKStates(0) & Environment.NewLine & MODToSTR(HKCombo.Modifier) & "+" & HKCombo.Hotkey.ToString
            Case 1
                HKCombo = STRToHotkeyCombo(My.Settings.MEDIA_NEXT)
                GH = New GlobalHotkey(HKCombo.Modifier, HKCombo.Hotkey, IntHelper.Handle, 1) 'Next
                HKStates(1) = GH.Unregister()
                HK_ST_Next.Text = HKStates(1) & Environment.NewLine & MODToSTR(HKCombo.Modifier) & "+" & HKCombo.Hotkey.ToString
            Case 2
                HKCombo = STRToHotkeyCombo(My.Settings.MEDIA_PREV)
                GH = New GlobalHotkey(HKCombo.Modifier, HKCombo.Hotkey, IntHelper.Handle, 2) 'Previous
                HKStates(2) = GH.Unregister()
                HK_ST_Prev.Text = HKStates(2) & Environment.NewLine & MODToSTR(HKCombo.Modifier) & "+" & HKCombo.Hotkey.ToString
            Case 3
                HKCombo = STRToHotkeyCombo(My.Settings.VOL_UP)
                GH = New GlobalHotkey(HKCombo.Modifier, HKCombo.Hotkey, IntHelper.Handle, 3) 'V+
                HKStates(3) = GH.Unregister()
                HK_ST_FVol.Text = HKStates(3) & Environment.NewLine & MODToSTR(HKCombo.Modifier) & "+" & HKCombo.Hotkey.ToString
            Case 4
                HKCombo = STRToHotkeyCombo(My.Settings.VOL_DOWN)
                GH = New GlobalHotkey(HKCombo.Modifier, HKCombo.Hotkey, IntHelper.Handle, 4) 'V-
                HKStates(4) = GH.Unregister()
                HK_ST_LVol.Text = HKStates(4) & Environment.NewLine & MODToSTR(HKCombo.Modifier) & "+" & HKCombo.Hotkey.ToString
            Case 5
                HKCombo = STRToHotkeyCombo(My.Settings.VOL_MUTE)
                GH = New GlobalHotkey(HKCombo.Modifier, HKCombo.Hotkey, IntHelper.Handle, 5) 'V*
                HKStates(5) = GH.Unregister()
                HK_ST_MVol.Text = HKStates(5) & Environment.NewLine & MODToSTR(HKCombo.Modifier) & "+" & HKCombo.Hotkey.ToString
            Case 6
                HKCombo = STRToHotkeyCombo(My.Settings.DAEMON)
                GH = New GlobalHotkey(HKCombo.Modifier, HKCombo.Hotkey, IntHelper.Handle, 6) 'Daemon
                HKStates(6) = GH.Unregister()
        End Select
    End Sub

    Private Enum Hotkeys
        HK_PlayPause = 0
        HK_Next = 1
        HK_Prev = 2
        HK_VP = 3
        HK_VD = 4
        HK_VM = 5
        HK_DAEMON = 6
    End Enum
#End Region
    Private Sub MainWindow_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        If My.Settings.RunAsDaemon Then WindowState = WindowState.Minimized : ShowInTaskbar = False
        Dim source As Interop.HwndSource = Interop.HwndSource.FromHwnd(IntHelper.Handle)
        source.AddHook(New Interop.HwndSourceHook(AddressOf WndProc))
        '--------------------------------Preparing Hotkeys-------------------------------------------------------
        Dim HKCombo = STRToHotkeyCombo(My.Settings.MEDIA_PLAYPAUSE)
        GH = New GlobalHotkey(HKCombo.Modifier, HKCombo.Hotkey, IntHelper.Handle, 0) 'PlayPause
        HKStates(0) = GH.Register()
        HK_ST_PlayPause.Text = HKStates(0) & Environment.NewLine & MODToSTR(HKCombo.Modifier) & "+" & HKCombo.Hotkey.ToString
        HKCombo = STRToHotkeyCombo(My.Settings.MEDIA_NEXT)
        GH = New GlobalHotkey(HKCombo.Modifier, HKCombo.Hotkey, IntHelper.Handle, 1) 'Next
        HKStates(1) = GH.Register()
        HK_ST_Next.Text = HKStates(1) & Environment.NewLine & MODToSTR(HKCombo.Modifier) & "+" & HKCombo.Hotkey.ToString
        HKCombo = STRToHotkeyCombo(My.Settings.MEDIA_PREV)
        GH = New GlobalHotkey(HKCombo.Modifier, HKCombo.Hotkey, IntHelper.Handle, 2) 'Previous
        HKStates(2) = GH.Register()
        HK_ST_Prev.Text = HKStates(2) & Environment.NewLine & MODToSTR(HKCombo.Modifier) & "+" & HKCombo.Hotkey.ToString
        HKCombo = STRToHotkeyCombo(My.Settings.VOL_UP)
        GH = New GlobalHotkey(HKCombo.Modifier, HKCombo.Hotkey, IntHelper.Handle, 3) 'V+
        HKStates(3) = GH.Register()
        HK_ST_FVol.Text = HKStates(3) & Environment.NewLine & MODToSTR(HKCombo.Modifier) & "+" & HKCombo.Hotkey.ToString
        HKCombo = STRToHotkeyCombo(My.Settings.VOL_DOWN)
        GH = New GlobalHotkey(HKCombo.Modifier, HKCombo.Hotkey, IntHelper.Handle, 4) 'V-
        HKStates(4) = GH.Register()
        HK_ST_LVol.Text = HKStates(4) & Environment.NewLine & MODToSTR(HKCombo.Modifier) & "+" & HKCombo.Hotkey.ToString
        HKCombo = STRToHotkeyCombo(My.Settings.VOL_MUTE)
        GH = New GlobalHotkey(HKCombo.Modifier, HKCombo.Hotkey, IntHelper.Handle, 5) 'V*
        HKStates(5) = GH.Register()
        HK_ST_MVol.Text = HKStates(5) & Environment.NewLine & MODToSTR(HKCombo.Modifier) & "+" & HKCombo.Hotkey.ToString
        HKCombo = STRToHotkeyCombo(My.Settings.DAEMON)
        GH = New GlobalHotkey(HKCombo.Modifier, HKCombo.Hotkey, IntHelper.Handle, 6) 'Daemon
        HKStates(6) = GH.Register()
        '--------------------------------------------------------------------------------------------------------
        '--------------------------------------------------------------------------------------------------------
        Dim TrayResurrect As New Forms.ToolStripMenuItem With {.Text = "Resurrect"}
        AddHandler TrayResurrect.Click, AddressOf Resurrect
        NIconMenu.Items.Add(TrayResurrect)
        NIcon.ContextMenuStrip = NIconMenu
        '--------------------------------------------------------------------------------------------------------
        WindCM_Set_RAD.Header = "Daemon Startup: " & My.Settings.RunAsDaemon
    End Sub
    Private Sub MainWindow_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        Dim source As Interop.HwndSource = Interop.HwndSource.FromHwnd(IntHelper.Handle)
        source.RemoveHook(New Interop.HwndSourceHook(AddressOf WndProc))
        Dim HKCombo = STRToHotkeyCombo(My.Settings.MEDIA_PLAYPAUSE)
        GH = New GlobalHotkey(HKCombo.Modifier, HKCombo.Hotkey, IntHelper.Handle, 0) 'PlayPause
        HKStates(0) = GH.Unregister()
        HK_ST_PlayPause.Text = HKStates(0) & Environment.NewLine & MODToSTR(HKCombo.Modifier) & "+" & HKCombo.Hotkey.ToString
        HKCombo = STRToHotkeyCombo(My.Settings.MEDIA_NEXT)
        GH = New GlobalHotkey(HKCombo.Modifier, HKCombo.Hotkey, IntHelper.Handle, 1) 'Next
        HKStates(1) = GH.Unregister()
        HK_ST_Next.Text = HKStates(1) & Environment.NewLine & MODToSTR(HKCombo.Modifier) & "+" & HKCombo.Hotkey.ToString
        HKCombo = STRToHotkeyCombo(My.Settings.MEDIA_PREV)
        GH = New GlobalHotkey(HKCombo.Modifier, HKCombo.Hotkey, IntHelper.Handle, 2) 'Previous
        HKStates(2) = GH.Unregister()
        HK_ST_Prev.Text = HKStates(2) & Environment.NewLine & MODToSTR(HKCombo.Modifier) & "+" & HKCombo.Hotkey.ToString
        HKCombo = STRToHotkeyCombo(My.Settings.VOL_UP)
        GH = New GlobalHotkey(HKCombo.Modifier, HKCombo.Hotkey, IntHelper.Handle, 3) 'V+
        HKStates(3) = GH.Unregister()
        HK_ST_FVol.Text = HKStates(3) & Environment.NewLine & MODToSTR(HKCombo.Modifier) & "+" & HKCombo.Hotkey.ToString
        HKCombo = STRToHotkeyCombo(My.Settings.VOL_DOWN)
        GH = New GlobalHotkey(HKCombo.Modifier, HKCombo.Hotkey, IntHelper.Handle, 4) 'V-
        HKStates(4) = GH.Unregister()
        HK_ST_LVol.Text = HKStates(4) & Environment.NewLine & MODToSTR(HKCombo.Modifier) & "+" & HKCombo.Hotkey.ToString
        HKCombo = STRToHotkeyCombo(My.Settings.VOL_MUTE)
        GH = New GlobalHotkey(HKCombo.Modifier, HKCombo.Hotkey, IntHelper.Handle, 5) 'V*
        HKStates(5) = GH.Unregister()
        HK_ST_MVol.Text = HKStates(5) & Environment.NewLine & MODToSTR(HKCombo.Modifier) & "+" & HKCombo.Hotkey.ToString
        HKCombo = STRToHotkeyCombo(My.Settings.DAEMON)
        GH = New GlobalHotkey(HKCombo.Modifier, HKCombo.Hotkey, IntHelper.Handle, 6) 'Daemon
        HKStates(6) = GH.Unregister()
    End Sub
    Protected Function WndProc(hwnd As IntPtr, msg As Integer, wParam As IntPtr, lParam As IntPtr, ByRef handled As Boolean) As IntPtr
        Select Case msg
            Case Constants.WM_HOTKEY_MSG_ID
                HandleHotkey(wParam.ToString)
        End Select
        Return IntPtr.Zero
    End Function
    Private Sub HandleHotkey(id As IntPtr)
        Select Case id
            Case 0 'PlayPause                
                'call the SendMessage function with the current window handle, the command we want to use, same handle, and the button we want to press
                SendMessageW(IntHelper.Handle, WM_APPCOMMAND, IntHelper.Handle, INTAPPCOMMAND_MEDIA_PLAY_PAUSE << 16)
            Case 1 'Next                
                'call the SendMessage function with the current window handle, the command we want to use, same handle, and the button we want to press
                SendMessageW(IntHelper.Handle, WM_APPCOMMAND, IntHelper.Handle, INTAPPCOMMAND_MEDIA_NEXTTRACK << 16)
            Case 2 'Previous                
                'call the SendMessage function with the current window handle, the command we want to use, same handle, and the button we want to press
                SendMessageW(IntHelper.Handle, WM_APPCOMMAND, IntHelper.Handle, INTAPPCOMMAND_MEDIA_PREVIOUSTRACK << 16)
            Case 3 'V +                
                'call the SendMessage function with the current window handle, the command we want to use, same handle, and the button we want to press
                SendMessageW(IntHelper.Handle, WM_APPCOMMAND, IntHelper.Handle, INTAPPCOMMAND_VOLUME_UP << 16)
            Case 4 'V -                
                'call the SendMessage function with the current window handle, the command we want to use, same handle, and the button we want to press
                SendMessageW(IntHelper.Handle, WM_APPCOMMAND, IntHelper.Handle, INTAPPCOMMAND_VOLUME_DOWN << 16)
            Case 5 'V *           
                'call the SendMessage function with the current window handle, the command we want to use, same handle, and the button we want to press
                SendMessageW(IntHelper.Handle, WM_APPCOMMAND, IntHelper.Handle, INTAPPCOMMAND_VOLUME_MUTE << 16)
            Case 6 'Call Daemon 😛
                Resurrect()
        End Select
    End Sub
#Region "UI"
    '------------------------------------------------------------------------------------------------------------
    Private Sub HK_PlayPause_MouseDown(sender As Object, e As MouseButtonEventArgs) Handles HK_PlayPause.MouseDown
        If e.LeftButton = MouseButtonState.Pressed Then
            Dim HKSelector As New HotkeySelector
            If HKSelector.ShowDialog Then
                UnRegHotkey(Hotkeys.HK_PlayPause)
                My.Settings.MEDIA_PLAYPAUSE = HotkeyComboToSTR(HKSelector.Result)
                My.Settings.Save()
                UpdateHotkey(Hotkeys.HK_PlayPause)
            End If
        End If
    End Sub

    Private Sub HK_PlayPause_MouseEnter(sender As Object, e As MouseEventArgs) Handles HK_PlayPause.MouseEnter
        'HK_PlayPause.BeginAnimation(OpacityProperty, DZeroAnim)
        'HK_ST_PlayPause.BeginAnimation(OpacityProperty, DOneAnim)
        Title = HK_ST_PlayPause.Text
    End Sub

    Private Sub HK_PlayPause_MouseLeave(sender As Object, e As MouseEventArgs) Handles HK_PlayPause.MouseLeave
        'HK_PlayPause.BeginAnimation(OpacityProperty, DOneAnim)
        'HK_ST_PlayPause.BeginAnimation(OpacityProperty, DZeroAnim)
        Title = "Media Hotkey"
    End Sub
    '------------------------------------------------------------------------------------------------------------
    '------------------------------------------------------------------------------------------------------------
    Private Sub HK_Prev_MouseDown(sender As Object, e As MouseButtonEventArgs) Handles HK_Prev.MouseDown
        If e.LeftButton = MouseButtonState.Pressed Then
            Dim HKSelector As New HotkeySelector
            If HKSelector.ShowDialog Then
                UnRegHotkey(Hotkeys.HK_Prev)
                My.Settings.MEDIA_PREV = HotkeyComboToSTR(HKSelector.Result)
                My.Settings.Save()
                UpdateHotkey(Hotkeys.HK_Prev)
            End If
        End If
    End Sub

    Private Sub HK_Prev_MouseEnter(sender As Object, e As MouseEventArgs) Handles HK_Prev.MouseEnter
        'HK_Prev.BeginAnimation(OpacityProperty, DZeroAnim)
        'HK_ST_Prev.BeginAnimation(OpacityProperty, DOneAnim)
        Title = HK_ST_Prev.Text
    End Sub

    Private Sub HK_Prev_MouseLeave(sender As Object, e As MouseEventArgs) Handles HK_Prev.MouseLeave
        'HK_Prev.BeginAnimation(OpacityProperty, DOneAnim)
        'HK_ST_Prev.BeginAnimation(OpacityProperty, DZeroAnim)
        Title = "Media Hotkey"
    End Sub
    '------------------------------------------------------------------------------------------------------------
    '------------------------------------------------------------------------------------------------------------
    Private Sub HK_Next_MouseDown(sender As Object, e As MouseButtonEventArgs) Handles HK_Next.MouseDown
        If e.LeftButton = MouseButtonState.Pressed Then
            Dim HKSelector As New HotkeySelector
            If HKSelector.ShowDialog Then
                UnRegHotkey(Hotkeys.HK_Next)
                My.Settings.MEDIA_NEXT = HotkeyComboToSTR(HKSelector.Result)
                My.Settings.Save()
                UpdateHotkey(Hotkeys.HK_Next)
            End If
        End If
    End Sub

    Private Sub HK_Next_MouseEnter(sender As Object, e As MouseEventArgs) Handles HK_Next.MouseEnter
        'HK_Next.BeginAnimation(OpacityProperty, DZeroAnim)
        'HK_ST_Next.BeginAnimation(OpacityProperty, DOneAnim)
        Title = HK_ST_Next.Text
    End Sub

    Private Sub HK_Next_MouseLeave(sender As Object, e As MouseEventArgs) Handles HK_Next.MouseLeave
        'HK_Next.BeginAnimation(OpacityProperty, DOneAnim)
        'HK_ST_Next.BeginAnimation(OpacityProperty, DZeroAnim)
        Title = "Media Hotkey"
    End Sub
    '------------------------------------------------------------------------------------------------------------
    '------------------------------------------------------------------------------------------------------------
    Private Sub HK_Lvol_MouseDown(sender As Object, e As MouseButtonEventArgs) Handles HK_LVol.MouseDown
        If e.LeftButton = MouseButtonState.Pressed Then
            Dim HKSelector As New HotkeySelector
            If HKSelector.ShowDialog Then
                UnRegHotkey(Hotkeys.HK_VD)
                My.Settings.VOL_DOWN = HotkeyComboToSTR(HKSelector.Result)
                My.Settings.Save()
                UpdateHotkey(Hotkeys.HK_VD)
            End If
        End If
    End Sub

    Private Sub HK_Lvol_MouseEnter(sender As Object, e As MouseEventArgs) Handles HK_LVol.MouseEnter
        'HK_LVol.BeginAnimation(OpacityProperty, DZeroAnim)
        'HK_ST_LVol.BeginAnimation(OpacityProperty, DOneAnim)
        Title = HK_ST_LVol.Text
    End Sub

    Private Sub HK_Lvol_MouseLeave(sender As Object, e As MouseEventArgs) Handles HK_LVol.MouseLeave
        'HK_LVol.BeginAnimation(OpacityProperty, DOneAnim)
        'HK_ST_LVol.BeginAnimation(OpacityProperty, DZeroAnim)
        Title = "Media Hotkey"
    End Sub
    '------------------------------------------------------------------------------------------------------------
    '------------------------------------------------------------------------------------------------------------
    Private Sub HK_Fvol_MouseDown(sender As Object, e As MouseButtonEventArgs) Handles HK_FVol.MouseDown
        If e.LeftButton = MouseButtonState.Pressed Then
            Dim HKSelector As New HotkeySelector
            If HKSelector.ShowDialog Then
                UnRegHotkey(Hotkeys.HK_VP)
                My.Settings.VOL_UP = HotkeyComboToSTR(HKSelector.Result)
                My.Settings.Save()
                UpdateHotkey(Hotkeys.HK_VP)
            End If
        End If
    End Sub

    Private Sub HK_Fvol_MouseEnter(sender As Object, e As MouseEventArgs) Handles HK_FVol.MouseEnter
        'HK_FVol.BeginAnimation(OpacityProperty, DZeroAnim)
        'HK_ST_FVol.BeginAnimation(OpacityProperty, DOneAnim)
        Title = HK_ST_FVol.Text
    End Sub

    Private Sub HK_Fvol_MouseLeave(sender As Object, e As MouseEventArgs) Handles HK_FVol.MouseLeave
        'HK_FVol.BeginAnimation(OpacityProperty, DOneAnim)
        'HK_ST_FVol.BeginAnimation(OpacityProperty, DZeroAnim)
        Title = "Media Hotkey"
    End Sub
    '------------------------------------------------------------------------------------------------------------
    '------------------------------------------------------------------------------------------------------------
    Private Sub HK_Mvol_MouseDown(sender As Object, e As MouseButtonEventArgs) Handles HK_MVol.MouseDown
        If e.LeftButton = MouseButtonState.Pressed Then
            Dim HKSelector As New HotkeySelector
            If HKSelector.ShowDialog Then
                UnRegHotkey(Hotkeys.HK_VM)
                My.Settings.VOL_MUTE = HotkeyComboToSTR(HKSelector.Result)
                My.Settings.Save()
                UpdateHotkey(Hotkeys.HK_VM)
            End If
        End If
    End Sub

    Private Sub HK_Mvol_MouseEnter(sender As Object, e As MouseEventArgs) Handles HK_MVol.MouseEnter
        'HK_MVol.BeginAnimation(OpacityProperty, DZeroAnim)
        'HK_ST_MVol.BeginAnimation(OpacityProperty, DOneAnim)
        Title = HK_ST_MVol.Text
    End Sub

    Private Sub HK_Mvol_MouseLeave(sender As Object, e As MouseEventArgs) Handles HK_MVol.MouseLeave
        'HK_MVol.BeginAnimation(OpacityProperty, DOneAnim)
        'HK_ST_MVol.BeginAnimation(OpacityProperty, DZeroAnim)
        Title = "Media Hotkey"
    End Sub
    '------------------------------------------------------------------------------------------------------------
#End Region
    Private Async Sub MinimizeToTray()
        BeginAnimation(OpacityProperty, New Animation.DoubleAnimation(0, New Duration(TimeSpan.FromMilliseconds(500))))
        Await Task.Delay(500)
        Hide()
        Opacity = 1
        ShowInTaskbar = False
        NIcon.Visible = True
    End Sub
    Private Sub Resurrect()
        Show()
        ShowInTaskbar = True
        NIcon.Visible = False
    End Sub
    Private Sub SendToDaemon()
        MessageBox.Show(Me, "You can resurrect the app using SHIFT+F8", "Media Helper", MessageBoxButton.OK, MessageBoxImage.Information)
        Hide()
        ShowInTaskbar = False
    End Sub
    Private Sub ToggleStartup()
        My.Settings.RunAsDaemon = Not My.Settings.RunAsDaemon
        My.Settings.Save()
        WindCM_Set_RAD.Header = "Daemon Startup: " & My.Settings.RunAsDaemon
    End Sub
    Private Sub SetDaemonHK()
        Dim HKSelector As New HotkeySelector
        If HKSelector.ShowDialog Then
            UnRegHotkey(Hotkeys.HK_DAEMON)
            My.Settings.DAEMON = HotkeyComboToSTR(HKSelector.Result)
            My.Settings.Save()
            UpdateHotkey(Hotkeys.HK_DAEMON)
        End If
    End Sub
End Class
