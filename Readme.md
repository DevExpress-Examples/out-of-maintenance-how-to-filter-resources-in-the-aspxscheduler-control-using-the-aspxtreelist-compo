<!-- default file list -->
*Files to look at*:

* [CustomDataSource.cs](./CS/WebApplication1/CustomDataSource.cs) (VB: [CustomDataSource.vb](./VB/WebApplication1/CustomDataSource.vb))
* [CustomObjects.cs](./CS/WebApplication1/CustomObjects.cs) (VB: [CustomObjects.vb](./VB/WebApplication1/CustomObjects.vb))
* [Default.aspx](./CS/WebApplication1/Default.aspx) (VB: [Default.aspx](./VB/WebApplication1/Default.aspx))
* [Default.aspx.cs](./CS/WebApplication1/Default.aspx.cs) (VB: [Default.aspx.vb](./VB/WebApplication1/Default.aspx.vb))
<!-- default file list end -->
# How to filter resources in the ASPxScheduler control using the ASPxTreeList component


<p>At present, the ASPxScheduler control does not support the "resources tree" concept  (this functionality is currently available only for our WinForms SchedulerControl). <br>To emulate a "hierarchical resources" scenario, we suggested filtering child resources based on a parent resource using the ASPxComboBox control. <br><br>However, when there are several levels of resource hierarchies, you have to add an ASPxComboBox instance for each of the levels and implement filtering the "child" comboboxes items based on the "parent" combobox selected value.<br><br>This example demonstrates a simpler approach to emulate the "hierarchical resources" scenario by filtering ASPxScheduler resources using the <a href="https://documentation.devexpress.com/#AspNet/clsDevExpressWebASPxTreeListASPxTreeListtopic">ASPxTreeList</a> control.</p>

<br/>


