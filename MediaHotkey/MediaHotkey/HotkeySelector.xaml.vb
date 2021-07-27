Imports System.ComponentModel

Public Class HotkeySelector
    Public Property Result As HotkeyCombo
    Public Class HotkeyCombo
        Public Property Hotkey As Forms.Keys
        Public Property Modifier As Integer
    End Class
    Private Sub btn_done_Click(sender As Object, e As RoutedEventArgs) Handles btn_done.Click
        Result = New HotkeyCombo() With {.Hotkey = cb_hotkey.SelectedIndex, .Modifier = cb_mod.SelectedIndex}
        DialogResult = True
    End Sub

    Private Sub HotkeySelector_Initialized(sender As Object, e As EventArgs) Handles Me.Initialized
        cb_hotkey.Items.Clear()
        Dim KeyID As Int32 = 0
        For i As Integer = 0 To System.Enum.GetNames(GetType(Forms.Keys)).Count
            KeyID = i
            cb_hotkey.Items.Add(System.Enum.ToObject(GetType(Forms.Keys), KeyID).ToString & "(" & KeyID & ")")
        Next
    End Sub
End Class
