using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace GridWithFilterPanelButtons
{
    public class CustomGridControlView : GridView
    {
        ObservableCollection<EditorButton> customFilterPanelButtons;

        public CustomGridControlView()
        {
            CustomFilterPanelButtons.CollectionChanged += OnCollectionChanged;
        }

        public CustomGridControlView(GridControl grid) : base(grid) { }

        private void OnCollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            OnPropertiesChanged();
        }

        protected override void Dispose(bool disposing)
        {
            CustomFilterPanelButtons.CollectionChanged -= OnCollectionChanged;
            base.Dispose(disposing);
        }

        protected override string ViewName => "CustomGridControlView";

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public ObservableCollection<EditorButton> CustomFilterPanelButtons {
            get {
                if(customFilterPanelButtons == null)
                    customFilterPanelButtons = new ObservableCollection<EditorButton>();
                return customFilterPanelButtons;
            }
        }
    }
}
