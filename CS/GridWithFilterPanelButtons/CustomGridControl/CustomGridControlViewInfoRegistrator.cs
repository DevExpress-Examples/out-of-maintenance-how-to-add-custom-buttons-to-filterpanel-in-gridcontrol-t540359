using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Registrator;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Base.ViewInfo;
using System;

namespace GridWithFilterPanelButtons
{

    public class CustomGridControlViewInfoRegistrator : GridInfoRegistrator
    {
        public override BaseViewPainter CreatePainter(BaseView view)
        {
            return new CustomGridControlViewPainter(view as CustomGridControlView);
        }

        public override BaseView CreateView(GridControl grid)
        {
            return new CustomGridControlView(grid);
        }

        public override BaseViewInfo CreateViewInfo(BaseView view)
        {
            return new CustomGridControlViewInfo(view as CustomGridControlView);
        }

        public override string ViewName => "CustomGridControlView";
    }
}