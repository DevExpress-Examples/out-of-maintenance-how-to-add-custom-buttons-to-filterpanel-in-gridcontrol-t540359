using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace GridWithFilterPanelButtons
{
    public partial class GridWithFilterPanelButtons : XtraForm
    {
        CustomGridControlView customGridControlView;

        public GridWithFilterPanelButtons()
        {
            InitializeComponent();

            CustomGridControl customGridControl = new CustomGridControl();
            customGridControl.Dock = DockStyle.Fill;
            customGridControlView = new CustomGridControlView();
            customGridControl.MainView = customGridControlView;
            customGridControl.DataSource = new List<TestData>() { new TestData("Test1", true), new TestData("Test2", false), new TestData("Test3", true) };
            customGridControlView.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.ShowAlways;
            EditorButton btn = new EditorButton(ButtonPredefines.OK);
            AddCustomFilterPanelButton(ButtonPredefines.Redo, isLeft: false);
            AddCustomFilterPanelButton(ButtonPredefines.Plus, isNeedClickEvent: false);

            SimpleButton button = new SimpleButton();
            button.Click += OnAddButton;
            button.Dock = DockStyle.Left;
            button.Text = "AddCustomFilterPanelButton";
            button.Width = 150;

            PanelControl panelControl = new PanelControl();
            panelControl.Height = 30;
            panelControl.Dock = DockStyle.Bottom;
            panelControl.Controls.Add(button);
            Controls.Add(customGridControl);
            Controls.Add(panelControl);

        }

        void AddCustomFilterPanelButton(ButtonPredefines buttonPredefine, bool isLeft = true, bool isNeedClickEvent = true)
        {
            EditorButton button = new EditorButton(buttonPredefine);
            if(isNeedClickEvent)
                button.Click += Button_Click;
            button.IsLeft = isLeft;
            customGridControlView.CustomFilterPanelButtons.Add(button);
        }
        private void Button_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Test");
        }

        private void OnAddButton(object sender, EventArgs e)
        {
            AddCustomFilterPanelButton(ButtonPredefines.OK);
        }
    }
}