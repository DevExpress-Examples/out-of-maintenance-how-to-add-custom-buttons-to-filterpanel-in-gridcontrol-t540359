<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/128625283/17.1.4%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/T540359)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->
<!-- default file list -->
*Files to look at*:

* [CustomGridControl.cs](./CS/GridWithFilterPanelButtons/CustomGridControl/CustomGridControl.cs) (VB: [CustomGridControl.vb](./VB/GridWithFilterPanelButtons/CustomGridControl/CustomGridControl.vb))
* [CustomGridControlView.cs](./CS/GridWithFilterPanelButtons/CustomGridControl/CustomGridControlView.cs) (VB: [CustomGridControlView.vb](./VB/GridWithFilterPanelButtons/CustomGridControl/CustomGridControlView.vb))
* [CustomGridControlViewInfo.cs](./CS/GridWithFilterPanelButtons/CustomGridControl/CustomGridControlViewInfo.cs) (VB: [CustomGridControlViewInfoRegistrator.vb](./VB/GridWithFilterPanelButtons/CustomGridControl/CustomGridControlViewInfoRegistrator.vb))
* [CustomGridControlViewInfoRegistrator.cs](./CS/GridWithFilterPanelButtons/CustomGridControl/CustomGridControlViewInfoRegistrator.cs) (VB: [CustomGridControlViewInfoRegistrator.vb](./VB/GridWithFilterPanelButtons/CustomGridControl/CustomGridControlViewInfoRegistrator.vb))
* [CustomGridControlViewPainter.cs](./CS/GridWithFilterPanelButtons/CustomGridControl/CustomGridControlViewPainter.cs) (VB: [CustomGridControlViewPainter.vb](./VB/GridWithFilterPanelButtons/CustomGridControl/CustomGridControlViewPainter.vb))
* [CustomGridFilterPanelInfoArgs.cs](./CS/GridWithFilterPanelButtons/CustomGridControl/CustomGridFilterPanelInfoArgs.cs) (VB: [CustomGridFilterPanelInfoArgs.vb](./VB/GridWithFilterPanelButtons/CustomGridControl/CustomGridFilterPanelInfoArgs.vb))
* [CustomGridFilterPanelPainter.cs](./CS/GridWithFilterPanelButtons/CustomGridControl/CustomGridFilterPanelPainter.cs) (VB: [CustomGridFilterPanelPainter.vb](./VB/GridWithFilterPanelButtons/CustomGridControl/CustomGridFilterPanelPainter.vb))
* [Program.cs](./CS/GridWithFilterPanelButtons/Program.cs) (VB: [Program.vb](./VB/GridWithFilterPanelButtons/Program.vb))
* [TestData.cs](./CS/GridWithFilterPanelButtons/TestData.cs) (VB: [TestData.vb](./VB/GridWithFilterPanelButtons/TestData.vb))
* [XtraForm1.cs](./CS/GridWithFilterPanelButtons/XtraForm1.cs) (VB: [XtraForm1.vb](./VB/GridWithFilterPanelButtons/XtraForm1.vb))
<!-- default file list end -->
# How to add custom buttons to FilterPanel in GridControl


In the example below you can see how to implement custom buttons for <a href="https://documentation.devexpress.com/WindowsForms/1424/Controls-and-Libraries/Data-Grid/Visual-Elements/View-Common-Elements/Filter-Panel">FilterPanel</a> by creating a GridControl descendant.<br><br>Custom buttons are not real controls, they are <a href="https://documentation.devexpress.com/WindowsForms/610/Controls-and-Libraries/Editors-and-Simple-Controls/Simple-Editors/Concepts/Editor-Buttons/Editor-Buttons-Overview">EditorButtons</a> that are usually used by ButtonEdit.<br>You can customize these buttons as necessary and handle their Click event.<br>You can also easily add/remove custom buttons by using the CustomFilterPanelButtons property of a GridView descendant. <br><br>
<p>This example has the second realization: <a href="https://www.devexpress.com/Support/Center/Example/Details/T375271/gridview-how-to-add-a-custom-button-to-the-filterpanel">GridView - How to add a custom button to the FilterPanel</a></p>
<p>In the current version of the example, there are several advantages:</p>
<p>- Possibility of setting the button’s alignment (left, right);</p>
<p>- Custom buttons do not affect the current layout of a filter panel and just take an empty space.</p>
<br><img src="https://raw.githubusercontent.com/DevExpress-Examples/how-to-add-custom-buttons-to-filterpanel-in-gridcontrol-t540359/17.1.4+/media/564b16ee-6e24-48fa-81e2-c251f1c80692.png">

<br/>


