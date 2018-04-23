<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebApplication1.Default" %>

<%@ Register Assembly="DevExpress.Web.ASPxTreeList.v15.1, Version=15.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxTreeList" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.ASPxScheduler.v15.1, Version=15.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxScheduler" TagPrefix="dxwschs" %>
<%@ Register Assembly="DevExpress.XtraScheduler.v15.1.Core, Version=15.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraScheduler" TagPrefix="cc1" %>

<%@ Register assembly="DevExpress.Web.v15.1, Version=15.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web" tagprefix="dx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <script type="text/javascript">
            function OnFilterScheduler(s, e) {
                clientScheduler.Refresh();
            }
        </script>
        <table>
            <tr>
                <td style="vertical-align:top">
                    <dx:ASPxTreeList ID="ASPxTreeList1" runat="server" 
                        DataSourceID="ObjectDataSourceResources"
                        ParentFieldName="ParentResID"
                        KeyFieldName="ResID">
                        <SettingsBehavior AutoExpandAllNodes="True" />
                        <SettingsSelection Enabled="True" Recursive="True" />
                    </dx:ASPxTreeList>
                    <dx:ASPxButton ID="ASPxButton1" runat="server" Text="Filter Scheduler" AutoPostBack="false">
                        <ClientSideEvents Click="OnFilterScheduler" />
                    </dx:ASPxButton>
                </td>
                <td>
                    <dxwschs:ASPxScheduler ID="ASPxScheduler1" runat="server" AppointmentDataSourceID="ObjectDataSourceAppointment"
                        ClientIDMode="AutoID" Start='<%#DateTime.Now%>' GroupType="Date" ClientInstanceName="clientScheduler"
                        ResourceDataSourceID="ObjectDataSourceResources" OnFilterResource="ASPxScheduler1_FilterResource" ActiveViewType="Timeline">
                        <Storage>
                            <Appointments AutoRetrieveId="True">
                                <Mappings
                                    AllDay="AllDay"
                                    AppointmentId="Id"
                                    Description="Description"
                                    End="EndTime"
                                    Label="Label"
                                    Location="Location"
                                    ReminderInfo="ReminderInfo"
                                    ResourceId="OwnerId"
                                    Start="StartTime"
                                    Status="Status"
                                    Subject="Subject"
                                    Type="EventType" />
                            </Appointments>
                            <Resources>
                                <Mappings
                                    Caption="Name"
                                    ResourceId="ResID" />
                            </Resources>
                        </Storage>

                        <Views>
                            <DayView>
                                <TimeRulers>
                                    <cc1:TimeRuler AlwaysShowTimeDesignator="False" ShowCurrentTime="Never"></cc1:TimeRuler>
                                </TimeRulers>
                                <DayViewStyles ScrollAreaHeight="600px">
                                </DayViewStyles>
                            </DayView>

                            <WorkWeekView>
                                <TimeRulers>
                                    <cc1:TimeRuler></cc1:TimeRuler>
                                </TimeRulers>
                            </WorkWeekView>
                            <TimelineView>
                                <TimelineViewStyles>
                                    <VerticalResourceHeader Width="100px">
                                    </VerticalResourceHeader>
                                </TimelineViewStyles>
                                <CellAutoHeightOptions Mode="FitToContent" />
                            </TimelineView>

                            <FullWeekView>
                                <TimeRulers>
                                    <cc1:TimeRuler></cc1:TimeRuler>
                                </TimeRulers>
                            </FullWeekView>
                        </Views>
                    </dxwschs:ASPxScheduler>
                    <asp:ObjectDataSource ID="ObjectDataSourceResources" runat="server" OnObjectCreated="ObjectDataSourceResources_ObjectCreated" SelectMethod="SelectMethodHandler" TypeName="WebApplication1.CustomResourceDataSource"></asp:ObjectDataSource>
                    <asp:ObjectDataSource ID="ObjectDataSourceAppointment" runat="server" DataObjectTypeName="WebApplication1.CustomAppointment" DeleteMethod="DeleteMethodHandler" InsertMethod="InsertMethodHandler" SelectMethod="SelectMethodHandler" TypeName="WebApplication1.CustomAppointmentDataSource" UpdateMethod="UpdateMethodHandler" OnObjectCreated="ObjectDataSourceAppointment_ObjectCreated"></asp:ObjectDataSource>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
