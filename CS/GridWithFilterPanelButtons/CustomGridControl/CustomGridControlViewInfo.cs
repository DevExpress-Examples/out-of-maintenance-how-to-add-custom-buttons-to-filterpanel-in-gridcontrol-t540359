using DevExpress.Utils.Drawing;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Drawing;
using DevExpress.XtraEditors.ViewInfo;
using DevExpress.XtraGrid.Drawing;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace GridWithFilterPanelButtons
{
    public class CustomGridControlViewInfo : GridViewInfo
    {
        CustomGridControlView controlView;
        int сustomButtonsCountToDraw;
        bool drawMRU = true;
        Point leftStartPoint, rightStartPoint;
        EditorButtonObjectCollection rightButtons, leftButtons;
        Size sizeOfButton;

        public CustomGridControlViewInfo(GridView view) : base(view)
        {
            controlView = View as CustomGridControlView;
        }

        Rectangle GetButtonBounds(EditorButton button, CustomGridControlView controlView)
        {
            Point startPosition;
            if(button.IsLeft)
                startPosition = new Point(
                    LeftStartPoint.X + (SizeOfButton.Width + Offset) * controlView.CustomFilterPanelButtons.ToList().FindAll(p => p.IsLeft).IndexOf(button),
                    LeftStartPoint.Y);
            else startPosition = new Point(
                    RightStartPoint.X - (SizeOfButton.Width + Offset) * controlView.CustomFilterPanelButtons.ToList().FindAll(p => !p.IsLeft).IndexOf(button),
                    LeftStartPoint.Y);
            return new Rectangle(startPosition, SizeOfButton);
        }

        void GetButtonStates(List<ObjectState> states)
        {
            foreach(EditorButton button in controlView.CustomFilterPanelButtons)
            {
                if(button.IsLeft)
                    states.Add(LeftButtons.ToList().Find(p => p.Button == button).State);
                else states.Add(RightButtons.ToList().Find(p => p.Button == button).State);
            }
        }

        protected virtual EditorButtonObjectInfoArgs CreateButtonInfo(EditorButton button, int index)
        {
            return new EditorButtonObjectInfoArgs(button, PaintAppearance.FilterPanel);
        }

        protected override GridFilterPanelInfoArgs CreateFilterPanelInfo()
        {
            return new CustomGridFilterPanelInfoArgs(this);
        }

        public virtual void CalcButtons()
        {
            List<ObjectState> buttonStates = new List<ObjectState>();
            if(controlView.CustomFilterPanelButtons.Count == LeftButtons.Count + RightButtons.Count)
                GetButtonStates(buttonStates);
            ClearButtons();
            for(int n = 0; n < controlView.CustomFilterPanelButtons.Count; n++)
            {
                EditorButton currentButton = controlView.CustomFilterPanelButtons[n];
                if(!currentButton.Visible) continue;
                EditorButtonObjectInfoArgs currentButtonInfo = CreateButtonInfo(currentButton, n);
                if(buttonStates.Count != 0)
                    currentButtonInfo.State = buttonStates[n];
                else currentButtonInfo.State = ObjectState.Normal;
                currentButtonInfo.Bounds = GetButtonBounds(currentButton, controlView);
                if(currentButton.IsLeft)
                    LeftButtons.Add(currentButtonInfo);
                else
                    RightButtons.Add(currentButtonInfo);
            }
            int buttonWidth = GetCustomButtonWidth();
            if(buttonWidth > 0)
                сustomButtonsCountToDraw = (FilterPanel.Bounds.Width - GetCurrentActiveFilterButtonsWidth()) / buttonWidth;
            else сustomButtonsCountToDraw = 0;
        }

        public virtual void CheckMRUButton()
        {
            if((LeftButtons.Count > 0) || (RightButtons.Count > 0))
            {
                if(FilterPanel.AllowMRU)
                {
                    bool? isNeedToDrawMRU;
                    if((RightButtons.Count > 0) && (LeftButtons.Count > 0))
                    {
                        isNeedToDrawMRU = IsRightBoundCollidesLeft(
                                   new Rectangle(RightButtons.Last().Bounds.X - RightButtons.Last().Bounds.Width, 0, 0, 0),
                                   new Rectangle(LeftButtons.Last().Bounds.X, 0, GetButtonWidth(LeftButtons.Last().Bounds), 0));
                        if(isNeedToDrawMRU != null)
                            drawMRU = isNeedToDrawMRU.GetValueOrDefault();
                    } else
                    {
                        if((RightButtons.Count == 0) && (LeftButtons.Count > 0))
                        {
                            if(FilterPanel.ShowCustomizeButton)
                                isNeedToDrawMRU = IsRightBoundCollidesLeft(
                                   new Rectangle(FilterPanel.CustomizeButtonInfo.Bounds.X - SizeOfButton.Width, 0, 0, 0),
                                   new Rectangle(LeftButtons.Last().Bounds.X, 0, GetButtonWidth(LeftButtons.Last().Bounds), 0));
                            else isNeedToDrawMRU = IsRightBoundCollidesLeft(
                                   new Rectangle(FilterPanel.Bounds.X - SizeOfButton.Width, 0, 0, 0),
                                   new Rectangle(LeftButtons.Last().Bounds.X, 0, GetButtonWidth(LeftButtons.Last().Bounds), 0));
                            if(isNeedToDrawMRU != null)
                                drawMRU = isNeedToDrawMRU.GetValueOrDefault();
                        } else
                        {
                            isNeedToDrawMRU = IsRightBoundCollidesLeft(
                                   new Rectangle(RightButtons.Last().Bounds.X - RightButtons.Last().Bounds.Width, 0, 0, 0),
                                   new Rectangle(FilterPanel.TextBounds.X + FilterPanel.TextBounds.Width, 0, RightButtons.Last().Bounds.Width, 0));
                            if(isNeedToDrawMRU != null)
                                drawMRU = isNeedToDrawMRU.GetValueOrDefault();
                        }
                    }
                }
            }
        }
        public void ClearButtons()
        {
            leftButtons = null;
            rightButtons = null;
        }

        public virtual int GetButtonsWidth(EditorButtonObjectCollection collection)
        {
            int width = 0;
            foreach(EditorButtonObjectInfoArgs btn in collection)
                width += GetButtonWidth(btn.Bounds);
            return width;
        }

        public virtual int GetButtonWidth(Rectangle buttonBounds)
        {
            return buttonBounds.Width + Offset;
        }

        public int GetCurrentActiveFilterButtonsWidth()
        {
            int currentActiveButtons = 0;
            if(FilterPanel.ShowActiveButton) currentActiveButtons += GetButtonWidth(FilterPanel.ActiveButtonInfo.Bounds);
            if(FilterPanel.ShowCloseButton) currentActiveButtons += GetButtonWidth(FilterPanel.CloseButtonInfo.Bounds);
            if(FilterPanel.ShowCustomizeButton) currentActiveButtons += GetButtonWidth(FilterPanel.CustomizeButtonInfo.Bounds);
            if(FilterPanel.AllowMRU) currentActiveButtons += GetButtonWidth(FilterPanel.MRUButtonInfo.Bounds);
            return currentActiveButtons;
        }

        public int GetCustomButtonWidth()
        {
            int buttonWidth = 0;
            if(LeftButtons.Count > 0) buttonWidth = GetButtonWidth(LeftButtons[0].Bounds);
            else if(RightButtons.Count > 0)
                buttonWidth = GetButtonWidth(RightButtons[0].Bounds);
            return buttonWidth;
        }

        public virtual bool? IsRightBoundCollidesLeft(Rectangle leftBound, Rectangle rightBound)
        {
            int leftRightDistButtons = 2;
            if(leftBound.X <= rightBound.X + rightBound.Width * leftRightDistButtons)
                return false;
            if(leftBound.X >= rightBound.X + rightBound.Width * (leftRightDistButtons + 1))
                return true;
            return null;
        }

        public int CustomButtonsCountToDraw {
            get { return сustomButtonsCountToDraw; } }

        public virtual bool DrawMRU {
            get { return drawMRU; } }

        public override ObjectPainter FilterPanelPainter => new CustomGridFilterPanelPainter(GridControl.LookAndFeel);
        public virtual EditorButtonObjectCollection LeftButtons {
            get {
                if(leftButtons == null)
                    leftButtons = new EditorButtonObjectCollection();
                return leftButtons;
            }
        }

        public Point LeftStartPoint {
            get {
                if(FilterPanel.MRUButtonInfo.Bounds.Width > 0)
                    leftStartPoint = new Point(FilterPanel.MRUButtonInfo.Bounds.X + FilterPanel.MRUButtonInfo.Bounds.Width + Offset, FilterPanel.Bounds.Y + Offset);
                else
                if(!(FilterPanel.TextBounds.Width < 0))
                    leftStartPoint = new Point(FilterPanel.TextBounds.X + FilterPanel.TextBounds.Width + Offset, FilterPanel.Bounds.Y + Offset);
                else if(FilterPanel.ShowActiveButton)
                    leftStartPoint = new Point(FilterPanel.ActiveButtonInfo.Bounds.X + FilterPanel.ActiveButtonInfo.Bounds.Width + Offset, FilterPanel.Bounds.Y + Offset);
                else if(FilterPanel.ShowCloseButton)
                    leftStartPoint = new Point(FilterPanel.CloseButtonInfo.Bounds.X + FilterPanel.CloseButtonInfo.Bounds.Width + Offset, FilterPanel.Bounds.Y + Offset);
                else leftStartPoint = new Point(FilterPanel.Bounds.X + Offset, FilterPanel.Bounds.Y + Offset);
                return leftStartPoint;
            }
        }
        public virtual int Offset { get { return 3; } }
        public EditorButtonObjectCollection RightButtons {
            get {
                if(rightButtons == null)
                    rightButtons = new EditorButtonObjectCollection();
                return rightButtons;
            }
        }

        public Point RightStartPoint {
            get {
                if(FilterPanel.ShowCustomizeButton)
                    rightStartPoint = new Point(FilterPanel.CustomizeButtonInfo.Bounds.X - Offset - SizeOfButton.Width, FilterPanel.Bounds.Y + Offset);
                else rightStartPoint = new Point(FilterPanel.Bounds.Width - Offset * 2 - SizeOfButton.Width, FilterPanel.Bounds.Y + Offset);
                return rightStartPoint;
            }
        }

        public Size SizeOfButton {
            get {
                if(sizeOfButton.IsEmpty)
                    sizeOfButton = new Size(FilterPanel.Bounds.Height - Offset * 2, FilterPanel.Bounds.Height - Offset * 2);
                return sizeOfButton;
            }
        }
    }
}