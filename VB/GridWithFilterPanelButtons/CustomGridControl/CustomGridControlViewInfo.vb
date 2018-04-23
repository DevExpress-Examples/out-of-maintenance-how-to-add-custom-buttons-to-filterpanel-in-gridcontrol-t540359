Imports DevExpress.Utils.Drawing
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.XtraEditors.Drawing
Imports DevExpress.XtraEditors.ViewInfo
Imports DevExpress.XtraGrid.Drawing
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo
Imports System
Imports System.Collections.Generic
Imports System.Drawing
Imports System.Linq

Namespace GridWithFilterPanelButtons
    Public Class CustomGridControlViewInfo
        Inherits GridViewInfo

        Private controlView As CustomGridControlView
        Private сustomButtonsCountToDraw As Integer

        Private drawMRU_Renamed As Boolean = True


        Private leftStartPoint_Renamed, rightStartPoint_Renamed As Point


        Private rightButtons_Renamed, leftButtons_Renamed As EditorButtonObjectCollection

        Private sizeOfButton_Renamed As Size

        Public Sub New(ByVal view As GridView)
            MyBase.New(view)
            controlView = TryCast(View, CustomGridControlView)
        End Sub

        Private Function GetButtonBounds(ByVal button As EditorButton, ByVal controlView As CustomGridControlView) As Rectangle
            Dim startPosition As Point
            If button.IsLeft Then
                startPosition = New Point(LeftStartPoint.X + (SizeOfButton.Width + Offset) * controlView.CustomFilterPanelButtons.ToList().FindAll(Function(p) p.IsLeft).IndexOf(button), LeftStartPoint.Y)
            Else
                startPosition = New Point(RightStartPoint.X - (SizeOfButton.Width + Offset) * controlView.CustomFilterPanelButtons.ToList().FindAll(Function(p) Not p.IsLeft).IndexOf(button), LeftStartPoint.Y)
            End If
            Return New Rectangle(startPosition, SizeOfButton)
        End Function

        Private Sub GetButtonStates(ByVal states As List(Of ObjectState))
            For Each button As EditorButton In controlView.CustomFilterPanelButtons
                If button.IsLeft Then
                    states.Add(LeftButtons.ToList().Find(Function(p) p.Button Is button).State)
                Else
                    states.Add(RightButtons.ToList().Find(Function(p) p.Button Is button).State)
                End If
            Next button
        End Sub

        Protected Overridable Function CreateButtonInfo(ByVal button As EditorButton, ByVal index As Integer) As EditorButtonObjectInfoArgs
            Return New EditorButtonObjectInfoArgs(button, PaintAppearance.FilterPanel)
        End Function

        Protected Overrides Function CreateFilterPanelInfo() As GridFilterPanelInfoArgs
            Return New CustomGridFilterPanelInfoArgs(Me)
        End Function

        Public Overridable Sub CalcButtons()
            Dim buttonStates As New List(Of ObjectState)()
            If controlView.CustomFilterPanelButtons.Count = LeftButtons.Count + RightButtons.Count Then
                GetButtonStates(buttonStates)
            End If
            ClearButtons()
            For n As Integer = 0 To controlView.CustomFilterPanelButtons.Count - 1
                Dim currentButton As EditorButton = controlView.CustomFilterPanelButtons(n)
                If Not currentButton.Visible Then
                    Continue For
                End If
                Dim currentButtonInfo As EditorButtonObjectInfoArgs = CreateButtonInfo(currentButton, n)
                If buttonStates.Count <> 0 Then
                    currentButtonInfo.State = buttonStates(n)
                Else
                    currentButtonInfo.State = ObjectState.Normal
                End If
                currentButtonInfo.Bounds = GetButtonBounds(currentButton, controlView)
                If currentButton.IsLeft Then
                    LeftButtons.Add(currentButtonInfo)
                Else
                    RightButtons.Add(currentButtonInfo)
                End If
            Next n
            Dim buttonWidth As Integer = GetCustomButtonWidth()
            If buttonWidth > 0 Then
                сustomButtonsCountToDraw = (FilterPanel.Bounds.Width - GetCurrentActiveFilterButtonsWidth()) \ buttonWidth
            Else
                сustomButtonsCountToDraw = 0
            End If
        End Sub

        Public Overridable Sub CheckMRUButton()
            If (LeftButtons.Count > 0) OrElse (RightButtons.Count > 0) Then
                If FilterPanel.AllowMRU Then
                    Dim isNeedToDrawMRU? As Boolean
                    If (RightButtons.Count > 0) AndAlso (LeftButtons.Count > 0) Then
                        isNeedToDrawMRU = IsRightBoundCollidesLeft(New Rectangle(RightButtons.Last().Bounds.X - RightButtons.Last().Bounds.Width, 0, 0, 0), New Rectangle(LeftButtons.Last().Bounds.X, 0, GetButtonWidth(LeftButtons.Last().Bounds), 0))
                        If isNeedToDrawMRU IsNot Nothing Then
                            drawMRU_Renamed = isNeedToDrawMRU.GetValueOrDefault()
                        End If
                    Else
                        If (RightButtons.Count = 0) AndAlso (LeftButtons.Count > 0) Then
                            If FilterPanel.ShowCustomizeButton Then
                                isNeedToDrawMRU = IsRightBoundCollidesLeft(New Rectangle(FilterPanel.CustomizeButtonInfo.Bounds.X - SizeOfButton.Width, 0, 0, 0), New Rectangle(LeftButtons.Last().Bounds.X, 0, GetButtonWidth(LeftButtons.Last().Bounds), 0))
                            Else
                                isNeedToDrawMRU = IsRightBoundCollidesLeft(New Rectangle(FilterPanel.Bounds.X - SizeOfButton.Width, 0, 0, 0), New Rectangle(LeftButtons.Last().Bounds.X, 0, GetButtonWidth(LeftButtons.Last().Bounds), 0))
                            End If
                            If isNeedToDrawMRU IsNot Nothing Then
                                drawMRU_Renamed = isNeedToDrawMRU.GetValueOrDefault()
                            End If
                        Else
                            isNeedToDrawMRU = IsRightBoundCollidesLeft(New Rectangle(RightButtons.Last().Bounds.X - RightButtons.Last().Bounds.Width, 0, 0, 0), New Rectangle(FilterPanel.TextBounds.X + FilterPanel.TextBounds.Width, 0, RightButtons.Last().Bounds.Width, 0))
                            If isNeedToDrawMRU IsNot Nothing Then
                                drawMRU_Renamed = isNeedToDrawMRU.GetValueOrDefault()
                            End If
                        End If
                    End If
                End If
            End If
        End Sub
        Public Sub ClearButtons()
            leftButtons_Renamed = Nothing
            rightButtons_Renamed = Nothing
        End Sub

        Public Overridable Function GetButtonsWidth(ByVal collection As EditorButtonObjectCollection) As Integer
            Dim width As Integer = 0
            For Each btn As EditorButtonObjectInfoArgs In collection
                width += GetButtonWidth(btn.Bounds)
            Next btn
            Return width
        End Function

        Public Overridable Function GetButtonWidth(ByVal buttonBounds As Rectangle) As Integer
            Return buttonBounds.Width + Offset
        End Function

        Public Function GetCurrentActiveFilterButtonsWidth() As Integer
            Dim currentActiveButtons As Integer = 0
            If FilterPanel.ShowActiveButton Then
                currentActiveButtons += GetButtonWidth(FilterPanel.ActiveButtonInfo.Bounds)
            End If
            If FilterPanel.ShowCloseButton Then
                currentActiveButtons += GetButtonWidth(FilterPanel.CloseButtonInfo.Bounds)
            End If
            If FilterPanel.ShowCustomizeButton Then
                currentActiveButtons += GetButtonWidth(FilterPanel.CustomizeButtonInfo.Bounds)
            End If
            If FilterPanel.AllowMRU Then
                currentActiveButtons += GetButtonWidth(FilterPanel.MRUButtonInfo.Bounds)
            End If
            Return currentActiveButtons
        End Function

        Public Function GetCustomButtonWidth() As Integer
            Dim buttonWidth As Integer = 0
            If LeftButtons.Count > 0 Then
                buttonWidth = GetButtonWidth(LeftButtons(0).Bounds)
            ElseIf RightButtons.Count > 0 Then
                buttonWidth = GetButtonWidth(RightButtons(0).Bounds)
            End If
            Return buttonWidth
        End Function

        Public Overridable Function IsRightBoundCollidesLeft(ByVal leftBound As Rectangle, ByVal rightBound As Rectangle) As Boolean?
            Dim leftRightDistButtons As Integer = 2
            If leftBound.X <= rightBound.X + rightBound.Width * leftRightDistButtons Then
                Return False
            End If
            If leftBound.X >= rightBound.X + rightBound.Width * (leftRightDistButtons + 1) Then
                Return True
            End If
            Return Nothing
        End Function

        Public ReadOnly Property CustomButtonsCountToDraw() As Integer
            Get
                Return сustomButtonsCountToDraw
            End Get
        End Property

        Public Overridable ReadOnly Property DrawMRU() As Boolean
            Get
                Return drawMRU_Renamed
            End Get
        End Property

        Public Overrides ReadOnly Property FilterPanelPainter() As ObjectPainter
            Get
                Return New CustomGridFilterPanelPainter(GridControl.LookAndFeel)
            End Get
        End Property
        Public Overridable ReadOnly Property LeftButtons() As EditorButtonObjectCollection
            Get
                If leftButtons_Renamed Is Nothing Then
                    leftButtons_Renamed = New EditorButtonObjectCollection()
                End If
                Return leftButtons_Renamed
            End Get
        End Property

        Public ReadOnly Property LeftStartPoint() As Point
            Get
                If FilterPanel.MRUButtonInfo.Bounds.Width > 0 Then
                    leftStartPoint_Renamed = New Point(FilterPanel.MRUButtonInfo.Bounds.X + FilterPanel.MRUButtonInfo.Bounds.Width + Offset, FilterPanel.Bounds.Y + Offset)
                Else
                If Not (FilterPanel.TextBounds.Width < 0) Then
                    leftStartPoint_Renamed = New Point(FilterPanel.TextBounds.X + FilterPanel.TextBounds.Width + Offset, FilterPanel.Bounds.Y + Offset)
                ElseIf FilterPanel.ShowActiveButton Then
                    leftStartPoint_Renamed = New Point(FilterPanel.ActiveButtonInfo.Bounds.X + FilterPanel.ActiveButtonInfo.Bounds.Width + Offset, FilterPanel.Bounds.Y + Offset)
                ElseIf FilterPanel.ShowCloseButton Then
                    leftStartPoint_Renamed = New Point(FilterPanel.CloseButtonInfo.Bounds.X + FilterPanel.CloseButtonInfo.Bounds.Width + Offset, FilterPanel.Bounds.Y + Offset)
                Else
                    leftStartPoint_Renamed = New Point(FilterPanel.Bounds.X + Offset, FilterPanel.Bounds.Y + Offset)
                End If
                End If
                Return leftStartPoint_Renamed
            End Get
        End Property
        Public Overridable ReadOnly Property Offset() As Integer
            Get
                Return 3
            End Get
        End Property
        Public ReadOnly Property RightButtons() As EditorButtonObjectCollection
            Get
                If rightButtons_Renamed Is Nothing Then
                    rightButtons_Renamed = New EditorButtonObjectCollection()
                End If
                Return rightButtons_Renamed
            End Get
        End Property

        Public ReadOnly Property RightStartPoint() As Point
            Get
                If FilterPanel.ShowCustomizeButton Then
                    rightStartPoint_Renamed = New Point(FilterPanel.CustomizeButtonInfo.Bounds.X - Offset - SizeOfButton.Width, FilterPanel.Bounds.Y + Offset)
                Else
                    rightStartPoint_Renamed = New Point(FilterPanel.Bounds.Width - Offset * 2 - SizeOfButton.Width, FilterPanel.Bounds.Y + Offset)
                End If
                Return rightStartPoint_Renamed
            End Get
        End Property

        Public ReadOnly Property SizeOfButton() As Size
            Get
                If sizeOfButton_Renamed.IsEmpty Then
                    sizeOfButton_Renamed = New Size(FilterPanel.Bounds.Height - Offset * 2, FilterPanel.Bounds.Height - Offset * 2)
                End If
                Return sizeOfButton_Renamed
            End Get
        End Property
    End Class
End Namespace