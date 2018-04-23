Imports System
Imports System.Linq

Namespace GridWithFilterPanelButtons
    Friend Class TestData
        Public Sub New(ByVal name As String, ByVal check As Boolean)
            Me.Name = name
            Me.Check = check
        End Sub

        Public Property Check() As Boolean
        Public Property Name() As String
    End Class
End Namespace
