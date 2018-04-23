Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.Controls
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Windows.Forms

Namespace GridWithFilterPanelButtons
    Partial Public Class GridWithFilterPanelButtons
        Inherits XtraForm

        Private customGridControlView As CustomGridControlView

        Public Sub New()
            InitializeComponent()

            Dim customGridControl As New CustomGridControl()
            customGridControl.Dock = DockStyle.Fill
            customGridControlView = New CustomGridControlView()
            customGridControl.MainView = customGridControlView
            customGridControl.DataSource = New List(Of TestData)() From { _
                New TestData("Test1", True), _
                New TestData("Test2", False), _
                New TestData("Test3", True) _
            }
            customGridControlView.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.ShowAlways
            Dim btn As New EditorButton(ButtonPredefines.OK)
            AddCustomFilterPanelButton(ButtonPredefines.Redo, isLeft:= False)
            AddCustomFilterPanelButton(ButtonPredefines.Plus, isNeedClickEvent:= False)

            Dim button As New SimpleButton()
            AddHandler button.Click, AddressOf OnAddButton
            button.Dock = DockStyle.Left
            button.Text = "AddCustomFilterPanelButton"
            button.Width = 150

            Dim panelControl As New PanelControl()
            panelControl.Height = 30
            panelControl.Dock = DockStyle.Bottom
            panelControl.Controls.Add(button)
            Controls.Add(customGridControl)
            Controls.Add(panelControl)

        End Sub

        Private Sub AddCustomFilterPanelButton(ByVal buttonPredefine As ButtonPredefines, Optional ByVal isLeft As Boolean = True, Optional ByVal isNeedClickEvent As Boolean = True)
            Dim button As New EditorButton(buttonPredefine)
            If isNeedClickEvent Then
                AddHandler button.Click, AddressOf Button_Click
            End If
            button.IsLeft = isLeft
            customGridControlView.CustomFilterPanelButtons.Add(button)
        End Sub
        Private Sub Button_Click(ByVal sender As Object, ByVal e As EventArgs)
            MessageBox.Show("Test")
        End Sub

        Private Sub OnAddButton(ByVal sender As Object, ByVal e As EventArgs)
            AddCustomFilterPanelButton(ButtonPredefines.OK)
        End Sub
    End Class
End Namespace