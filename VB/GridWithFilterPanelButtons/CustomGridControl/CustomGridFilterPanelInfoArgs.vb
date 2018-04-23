Imports DevExpress.XtraGrid.Drawing
Imports System
Imports System.Linq

Namespace GridWithFilterPanelButtons
    Public Class CustomGridFilterPanelInfoArgs
        Inherits GridFilterPanelInfoArgs

        Public viewInfo As CustomGridControlViewInfo

        Public Sub New(ByVal info As CustomGridControlViewInfo)
            MyBase.New()
            viewInfo = info
        End Sub
    End Class
End Namespace
