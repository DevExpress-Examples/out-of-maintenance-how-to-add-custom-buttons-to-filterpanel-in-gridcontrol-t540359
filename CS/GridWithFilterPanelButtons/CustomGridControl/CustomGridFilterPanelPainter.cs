using DevExpress.Skins;
using DevExpress.Utils.Drawing;
using DevExpress.XtraGrid.Drawing;
using System;
using System.Drawing;
using System.Linq;

namespace GridWithFilterPanelButtons
{
    public class CustomGridFilterPanelPainter : SkinGridFilterPanelPainter
    {
        public CustomGridFilterPanelPainter(ISkinProvider provider) : base(provider) { }

        protected override Rectangle CalcTextBounds(FilterPanelInfoArgsBase e, Rectangle client)
        {
            CustomGridControlViewInfo viewInfo = (e as CustomGridFilterPanelInfoArgs).viewInfo;
            Rectangle textBounds = base.CalcTextBounds(e, client);
            int customizeButtonWidth = 0, activeButtonWidth = 0;
            if(e.ShowCustomizeButton)
                customizeButtonWidth = viewInfo.GetButtonWidth(e.CustomizeButtonInfo.Bounds);
            if(e.ShowActiveButton)
                activeButtonWidth = viewInfo.GetButtonWidth(e.ActiveButtonInfo.Bounds);
            int textWidth = 0;
            if(e.DisplayText != string.Empty)
            {
                int leftBorder = textBounds.X + textBounds.Width + viewInfo.GetButtonsWidth(viewInfo.LeftButtons);
                int rightBorder = e.Bounds.Width - (viewInfo.GetButtonsWidth(viewInfo.RightButtons) + customizeButtonWidth + activeButtonWidth);
                if(leftBorder > rightBorder)
                {
                    textWidth = e.Bounds.Width - textBounds.X - viewInfo.GetButtonsWidth(viewInfo.LeftButtons) - viewInfo.GetButtonsWidth(viewInfo.RightButtons) - customizeButtonWidth - activeButtonWidth;
                    if(e.AllowMRU) textWidth -= e.MRUButtonInfo.Bounds.Width + viewInfo.Offset;
                    return new Rectangle(textBounds.Location, new Size(textWidth, textBounds.Height));
                }
            }
            return textBounds;
        }
    }
}
