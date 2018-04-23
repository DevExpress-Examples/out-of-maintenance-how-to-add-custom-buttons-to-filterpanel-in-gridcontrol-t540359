using DevExpress.XtraGrid.Drawing;
using System;
using System.Linq;

namespace GridWithFilterPanelButtons
{
    public class CustomGridFilterPanelInfoArgs : GridFilterPanelInfoArgs
    {
        public CustomGridControlViewInfo viewInfo;

        public CustomGridFilterPanelInfoArgs(CustomGridControlViewInfo info) : base()
        {
            viewInfo = info;
        }
    }
}
