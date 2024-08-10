Public Class ItemButtons
    Enum ButtonTypes
        None = 0
        Hidden = 1
        Edit = 2
        Delete = 3
    End Enum
    Public Event ButtonClicked(ID As Guid, Button As HtmlElement, ButtonType As ButtonTypes)
    Sub New(ID As Guid, HiddenButton As HtmlElement, EditButton As HtmlElement, DeleteButton As HtmlElement)
        Me.IDValue = ID
        Me.HiddenButtonValue = HiddenButton
        Me.EditButtonValue = EditButton
        Me.DeleteButtonValue = DeleteButton
    End Sub
    Protected Friend WithEvents HiddenButtonValue As HtmlElement
    Protected Friend WithEvents DeleteButtonValue As HtmlElement
    Protected Friend WithEvents EditButtonValue As HtmlElement
    Private IDValue As Guid
    Public ReadOnly Property ID() As Guid
        Get
            Return IDValue
        End Get
    End Property

    Private Sub DeleteButtonValue_Click(sender As Object, e As System.Windows.Forms.HtmlElementEventArgs) Handles DeleteButtonValue.Click
        RaiseEvent ButtonClicked(Me.ID, sender, ButtonTypes.Delete)
    End Sub

    Private Sub EditButtonValue_Click(sender As Object, e As System.Windows.Forms.HtmlElementEventArgs) Handles EditButtonValue.Click
        RaiseEvent ButtonClicked(Me.ID, sender, ButtonTypes.Edit)
    End Sub

    Private Sub HiddenButtonValue_Click(sender As Object, e As System.Windows.Forms.HtmlElementEventArgs) Handles HiddenButtonValue.Click
        RaiseEvent ButtonClicked(Me.ID, sender, ButtonTypes.Hidden)
    End Sub
End Class
Public Class ItemButtonsCollection
    Inherits Dictionary(Of Guid, ItemButtons)
    Enum ButtonTypes
        None = 0
        Hidden = 1
        Edit = 2
        Delete = 3
    End Enum
    Public Event ButtonClicked(ID As Guid, Button As HtmlElement, ButtonType As ButtonTypes)
    Shadows Sub Add(ID As Guid, HiddenButton As HtmlElement, EditButton As HtmlElement, DeleteButton As HtmlElement)
        Dim Buttons As New ItemButtons(ID, HiddenButton, EditButton, DeleteButton)
        AddHandler Buttons.ButtonClicked, AddressOf onButtonClicked
        MyBase.Add(ID, Buttons)
    End Sub
    Shadows Sub Remove(ID As Guid)
        If MyBase.ContainsKey(ID) Then
            'RemoveHandler Me(ID).ButtonClicked, AddressOf onButtonClicked
            MyBase.Remove(ID)
        End If
    End Sub
    Private Sub onButtonClicked(ID As Guid, Button As System.Windows.Forms.HtmlElement, ButtonType As ButtonTypes)
        RaiseEvent ButtonClicked(ID, Button, ButtonType)
    End Sub
End Class