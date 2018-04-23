# How to add custom buttons to FilterPanel in GridControl


In the example below you can see how to implement custom buttons for <a href="https://documentation.devexpress.com/WindowsForms/1424/Controls-and-Libraries/Data-Grid/Visual-Elements/View-Common-Elements/Filter-Panel">FilterPanel</a> by creating a GridControl descendant.<br><br>Custom buttons are not real controls, they are <a href="https://documentation.devexpress.com/WindowsForms/610/Controls-and-Libraries/Editors-and-Simple-Controls/Simple-Editors/Concepts/Editor-Buttons/Editor-Buttons-Overview">EditorButtons</a> that are usually used by ButtonEdit.<br>You can customize these buttons as necessary and handle their Click event.<br>You can also easily add/remove custom buttons by using the CustomFilterPanelButtons property of a GridView descendant. <br><br>
<p>This example has the second realization: <a href="https://www.devexpress.com/Support/Center/Example/Details/T375271/gridview-how-to-add-a-custom-button-to-the-filterpanel">GridView - How to add a custom button to the FilterPanel</a></p>
<p>In the current version of the example, there are several advantages:</p>
<p>- Possibility of setting the button’s alignment (left, right);</p>
<p>- Custom buttons do not affect the current layout of a filter panel and just take an empty space.</p>
<br><img src="https://raw.githubusercontent.com/DevExpress-Examples/how-to-add-custom-buttons-to-filterpanel-in-gridcontrol-t540359/17.1.4+/media/564b16ee-6e24-48fa-81e2-c251f1c80692.png">

<br/>


