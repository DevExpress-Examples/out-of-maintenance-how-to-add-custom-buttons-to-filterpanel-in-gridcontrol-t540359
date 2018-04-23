Imports DevExpress.XtraEditors.Controls
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid
Imports System
Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports System.Linq

Namespace GridWithFilterPanelButtons
    Public Class CustomGridControlView
        Inherits GridView


        Private customFilterPanelButtons_Renamed As ObservableCollection(Of EditorButton)

        Public Sub New()
            AddHandler CustomFilterPanelButtons.CollectionChanged, AddressOf OnCollectionChanged
        End Sub

        Public Sub New(ByVal grid As GridControl)
            MyBase.New(grid)
        End Sub

        Private Sub OnCollectionChanged(ByVal sender As Object, ByVal e As System.Collections.Specialized.NotifyCollectionChangedEventArgs)
            OnPropertiesChanged()
        End Sub

        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            RemoveHandler CustomFilterPanelButtons.CollectionChanged, AddressOf OnCollectionChanged
            MyBase.Dispose(disposing)
        End Sub

        Protected Overrides ReadOnly Property ViewName() As String
            Get
                Return "CustomGridControlView"
            End Get
        End Property

        <DesignerSerializationVisibility(DesignerSerializationVisibility.Content)> _
        Public ReadOnly Property CustomFilterPanelButtons() As ObservableCollection(Of EditorButton)
            Get
                If customFilterPanelButtons_Renamed Is Nothing Then
                    customFilterPanelButtons_Renamed = New ObservableCollection(Of EditorButton)()
                End If
                Return customFilterPanelButtons_Renamed
            End Get
        End Property
    End Class
End Namespace
