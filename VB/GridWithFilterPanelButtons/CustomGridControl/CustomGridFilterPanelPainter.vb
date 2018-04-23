Imports DevExpress.Skins
Imports DevExpress.Utils.Drawing
Imports DevExpress.XtraGrid.Drawing
Imports System
Imports System.Drawing
Imports System.Linq

Namespace GridWithFilterPanelButtons
    Public Class CustomGridFilterPanelPainter
        Inherits SkinGridFilterPanelPainter

        Public Sub New(ByVal provider As ISkinProvider)
            MyBase.New(provider)
        End Sub

        Protected Overrides Function CalcTextBounds(ByVal e As FilterPanelInfoArgsBase, ByVal client As Rectangle) As Rectangle
            Dim viewInfo As CustomGridControlViewInfo = (TryCast(e, CustomGridFilterPanelInfoArgs)).viewInfo
            Dim textBounds As Rectangle = MyBase.CalcTextBounds(e, client)
            Dim customizeButtonWidth As Integer = 0, activeButtonWidth As Integer = 0
            If e.ShowCustomizeButton Then
                customizeButtonWidth = viewInfo.GetButtonWidth(e.CustomizeButtonInfo.Bounds)
            End If
            If e.ShowActiveButton Then
                activeButtonWidth = viewInfo.GetButtonWidth(e.ActiveButtonInfo.Bounds)
            End If
            Dim textWidth As Integer = 0
            If e.DisplayText <> String.Empty Then
                Dim leftBorder As Integer = textBounds.X + textBounds.Width + viewInfo.GetButtonsWidth(viewInfo.LeftButtons)
                Dim rightBorder As Integer = e.Bounds.Width - (viewInfo.GetButtonsWidth(viewInfo.RightButtons) + customizeButtonWidth + activeButtonWidth)
                If leftBorder > rightBorder Then
                    textWidth = e.Bounds.Width - textBounds.X - viewInfo.GetButtonsWidth(viewInfo.LeftButtons) - viewInfo.GetButtonsWidth(viewInfo.RightButtons) - customizeButtonWidth - activeButtonWidth
                    If e.AllowMRU Then
                        textWidth -= e.MRUButtonInfo.Bounds.Width + viewInfo.Offset
                    End If
                    Return New Rectangle(textBounds.Location, New Size(textWidth, textBounds.Height))
                End If
            End If
            Return textBounds
        End Function
    End Class
End Namespace
