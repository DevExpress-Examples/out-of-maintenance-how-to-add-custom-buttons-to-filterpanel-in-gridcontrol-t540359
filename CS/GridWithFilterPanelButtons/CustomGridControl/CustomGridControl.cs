using DevExpress.Utils.Drawing;
using DevExpress.XtraEditors.Drawing;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Registrator;
using DevExpress.XtraGrid.Views.Base;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace GridWithFilterPanelButtons
{
    [ToolboxItem(true)]
    public class CustomGridControl : GridControl
    {
        CustomGridControlView gridControlView;
        CustomGridControlViewInfo gridControlViewInfo;

        EditorButtonObjectInfoArgs CheckMousePositionAndSetState(Point mousePosition, ObjectState state)
        {
            if(gridControlViewInfo != null)
            {
                var collection = gridControlViewInfo.LeftButtons.Concat(gridControlViewInfo.RightButtons);
                EditorButtonObjectInfoArgs button = collection.ToList().Find(p => p.Bounds.Contains(mousePosition));
                if(button != null)
                    button.State = state;
                return button;
            } else return null;
        }

        void HotTrackFilterPanelButtons(Point mousePosition)
        {
            if(gridControlViewInfo != null)
            {
                var collection = gridControlViewInfo.LeftButtons.Concat(gridControlViewInfo.RightButtons);
                EditorButtonObjectInfoArgs button = collection.ToList().Find(p => p.Bounds.Contains(mousePosition));
                if(button != null)
                {
                    if(button.State == ObjectState.Normal)
                    {
                        button.State = ObjectState.Hot;
                        gridControlView.InvalidateFilterPanel();
                    }
                } else
                {
                    collection.ToList().ForEach(p => p.State = ObjectState.Normal);
                    gridControlView.InvalidateFilterPanel();
                }
            }
        }

        protected override BaseView CreateDefaultView()
        {
            return CreateView("CustomGridControlView");
        }

        protected override void CreateMainView()
        {
            base.CreateMainView();
            gridControlView = (Views.FirstOrDefault() as CustomGridControlView);
            gridControlViewInfo = Views.FirstOrDefault().GetViewInfo() as CustomGridControlViewInfo;
        }
        protected override void OnMouseDown(MouseEventArgs ev)
        {
            base.OnMouseDown(ev);
            EditorButtonObjectInfoArgs button = CheckMousePositionAndSetState(ev.Location, ObjectState.Pressed);
            if(button != null)
                gridControlView.InvalidateFilterPanel();
        }

        protected override void OnMouseMove(MouseEventArgs ev)
        {
            base.OnMouseMove(ev);
            HotTrackFilterPanelButtons(ev.Location);
        }

        protected override void OnMouseUp(MouseEventArgs ev)
        {
            base.OnMouseUp(ev);
            EditorButtonObjectInfoArgs button = CheckMousePositionAndSetState(ev.Location, ObjectState.Normal);
            if(button != null)
                button.Button.PerformClick();
        }

        protected override void RegisterAvailableViewsCore(InfoCollection collection)
        {
            base.RegisterAvailableViewsCore(collection);
            collection.Add(new CustomGridControlViewInfoRegistrator());
        }
    }
}