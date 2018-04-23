Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Registrator
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Views.Base.ViewInfo
Imports System

Namespace GridWithFilterPanelButtons

    Public Class CustomGridControlViewInfoRegistrator
        Inherits GridInfoRegistrator

        Public Overrides Function CreatePainter(ByVal view As BaseView) As BaseViewPainter
            Return New CustomGridControlViewPainter(TryCast(view, CustomGridControlView))
        End Function

        Public Overrides Function CreateView(ByVal grid As GridControl) As BaseView
            Return New CustomGridControlView(grid)
        End Function

        Public Overrides Function CreateViewInfo(ByVal view As BaseView) As BaseViewInfo
            Return New CustomGridControlViewInfo(TryCast(view, CustomGridControlView))
        End Function

        Public Overrides ReadOnly Property ViewName() As String
            Get
                Return "CustomGridControlView"
            End Get
        End Property
    End Class
End Namespace