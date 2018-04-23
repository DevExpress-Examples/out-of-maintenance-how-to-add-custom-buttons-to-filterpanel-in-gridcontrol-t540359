Imports DevExpress.Utils.Drawing
Imports DevExpress.XtraEditors.Drawing
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Registrator
Imports DevExpress.XtraGrid.Views.Base
Imports System
Imports System.ComponentModel
Imports System.Drawing
Imports System.Linq
Imports System.Windows.Forms

Namespace GridWithFilterPanelButtons
    <ToolboxItem(True)> _
    Public Class CustomGridControl
        Inherits GridControl

        Private gridControlView As CustomGridControlView
        Private gridControlViewInfo As CustomGridControlViewInfo

        Private Function CheckMousePositionAndSetState(ByVal mousePosition As Point, ByVal state As ObjectState) As EditorButtonObjectInfoArgs
            If gridControlViewInfo IsNot Nothing Then
                Dim collection = gridControlViewInfo.LeftButtons.Concat(gridControlViewInfo.RightButtons)
                Dim button As EditorButtonObjectInfoArgs = collection.ToList().Find(Function(p) p.Bounds.Contains(mousePosition))
                If button IsNot Nothing Then
                    button.State = state
                End If
                Return button
            Else
                Return Nothing
            End If
        End Function

        Private Sub HotTrackFilterPanelButtons(ByVal mousePosition As Point)
            If gridControlViewInfo IsNot Nothing Then
                Dim collection = gridControlViewInfo.LeftButtons.Concat(gridControlViewInfo.RightButtons)
                Dim button As EditorButtonObjectInfoArgs = collection.ToList().Find(Function(p) p.Bounds.Contains(mousePosition))
                If button IsNot Nothing Then
                    If button.State = ObjectState.Normal Then
                        button.State = ObjectState.Hot
                        gridControlView.InvalidateFilterPanel()
                    End If
                Else
                    collection.ToList().ForEach(Sub(p) p.State = ObjectState.Normal)
                    gridControlView.InvalidateFilterPanel()
                End If
            End If
        End Sub

        Protected Overrides Function CreateDefaultView() As BaseView
            Return CreateView("CustomGridControlView")
        End Function

        Protected Overrides Sub CreateMainView()
            MyBase.CreateMainView()
            gridControlView = (TryCast(Views.FirstOrDefault(), CustomGridControlView))
            gridControlViewInfo = TryCast(Views.FirstOrDefault().GetViewInfo(), CustomGridControlViewInfo)
        End Sub
        Protected Overrides Sub OnMouseDown(ByVal ev As MouseEventArgs)
            MyBase.OnMouseDown(ev)
            Dim button As EditorButtonObjectInfoArgs = CheckMousePositionAndSetState(ev.Location, ObjectState.Pressed)
            If button IsNot Nothing Then
                gridControlView.InvalidateFilterPanel()
            End If
        End Sub

        Protected Overrides Sub OnMouseMove(ByVal ev As MouseEventArgs)
            MyBase.OnMouseMove(ev)
            HotTrackFilterPanelButtons(ev.Location)
        End Sub

        Protected Overrides Sub OnMouseUp(ByVal ev As MouseEventArgs)
            MyBase.OnMouseUp(ev)
            Dim button As EditorButtonObjectInfoArgs = CheckMousePositionAndSetState(ev.Location, ObjectState.Normal)
            If button IsNot Nothing Then
                button.Button.PerformClick()
            End If
        End Sub

        Protected Overrides Sub RegisterAvailableViewsCore(ByVal collection As InfoCollection)
            MyBase.RegisterAvailableViewsCore(collection)
            collection.Add(New CustomGridControlViewInfoRegistrator())
        End Sub
    End Class
End Namespace