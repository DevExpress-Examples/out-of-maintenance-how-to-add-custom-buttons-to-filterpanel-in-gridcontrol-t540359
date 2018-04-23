using DevExpress.Utils.Drawing;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Drawing;
using DevExpress.XtraEditors.ViewInfo;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.Drawing;
using System;
using System.Drawing;
using System.Linq;

namespace GridWithFilterPanelButtons
{
    public class CustomGridControlViewPainter : GridPainter
    {
        CustomGridControlViewInfo viewInfo;
        public CustomGridControlViewPainter(GridView view) : base(view) { }

        void DrawButtons(GraphicsCache cache, EditorButtonObjectCollection collection)
        {
            CustomGridControlView view = viewInfo.View as CustomGridControlView;
            foreach(EditorButtonObjectInfoArgs button in collection)
            {
                if(view.CustomFilterPanelButtons.IndexOf(button.Button) < viewInfo.CustomButtonsCountToDraw)
                {
                    if(button.Cache == null)
                        button.Cache = cache;
                    EditorButtonHelper.GetPainter(BorderStyles.Default).DrawObject(button);
                } else break;
            }
        }

        protected override void DrawFilterPanel(ViewDrawArgs e)
        {
            if(viewInfo == null)
                viewInfo = (e.ViewInfo as CustomGridControlViewInfo);
            viewInfo.CheckMRUButton();
            if(!viewInfo.DrawMRU)
                viewInfo.FilterPanel.MRUButtonInfo.Bounds = new Rectangle();
            base.DrawFilterPanel(e);
            viewInfo.CalcButtons();
            DrawButtons(e.Cache, viewInfo.RightButtons);
            DrawButtons(e.Cache, viewInfo.LeftButtons);
        }
    }
}
