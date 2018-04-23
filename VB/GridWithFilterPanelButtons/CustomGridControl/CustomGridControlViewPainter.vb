Imports DevExpress.Utils.Drawing
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.XtraEditors.Drawing
Imports DevExpress.XtraEditors.ViewInfo
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Views.Grid.Drawing
Imports System
Imports System.Drawing
Imports System.Linq

Namespace GridWithFilterPanelButtons
    Public Class CustomGridControlViewPainter
        Inherits GridPainter

        Private viewInfo As CustomGridControlViewInfo
        Public Sub New(ByVal view As GridView)
            MyBase.New(view)
        End Sub

        Private Sub DrawButtons(ByVal cache As GraphicsCache, ByVal collection As EditorButtonObjectCollection)
            Dim view As CustomGridControlView = TryCast(viewInfo.View, CustomGridControlView)
            For Each button As EditorButtonObjectInfoArgs In collection
                If view.CustomFilterPanelButtons.IndexOf(button.Button) < viewInfo.CustomButtonsCountToDraw Then
                    If button.Cache Is Nothing Then
                        button.Cache = cache
                    End If
                    EditorButtonHelper.GetPainter(BorderStyles.Default).DrawObject(button)
                Else
                    Exit For
                End If
            Next button
        End Sub

        Protected Overrides Sub DrawFilterPanel(ByVal e As ViewDrawArgs)
            If viewInfo Is Nothing Then
                viewInfo = (TryCast(e.ViewInfo, CustomGridControlViewInfo))
            End If
            viewInfo.CheckMRUButton()
            If Not viewInfo.DrawMRU Then
                viewInfo.FilterPanel.MRUButtonInfo.Bounds = New Rectangle()
            End If
            MyBase.DrawFilterPanel(e)
            viewInfo.CalcButtons()
            DrawButtons(e.Cache, viewInfo.RightButtons)
            DrawButtons(e.Cache, viewInfo.LeftButtons)
        End Sub
    End Class
End Namespace
