using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxTreeList;
using DevExpress.XtraScheduler;

namespace WebApplication1 {
    public partial class Default : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {

        }

        protected void ObjectDataSourceResources_ObjectCreated(object sender, ObjectDataSourceEventArgs e) {
            if(Session["CustomResourceDataSource"] == null) {
                Session["CustomResourceDataSource"] = new CustomResourceDataSource(GetCustomResources());
            }
            e.ObjectInstance = Session["CustomResourceDataSource"];
        }

        BindingList<CustomResource> GetCustomResources() {
            BindingList<CustomResource> resources = new BindingList<CustomResource>();
            resources.Add(CreateCustomResource(1, -1, "Hong Kong"));
            resources.Add(CreateCustomResource(2, -1, "United States"));
            resources.Add(CreateCustomResource(3, 1, "Hotel A"));
            resources.Add(CreateCustomResource(4, 1, "Hotel B"));

            resources.Add(CreateCustomResource(6, 2, "Hotel 1"));
            resources.Add(CreateCustomResource(7, 2, "Hotel 2"));

            resources.Add(CreateCustomResource(8, 3, "A - Room 101"));
            resources.Add(CreateCustomResource(9, 3, "A - Room 102"));
            resources.Add(CreateCustomResource(10, 3, "A - Room 103"));
            resources.Add(CreateCustomResource(11, 4, "B - Room 101"));
            resources.Add(CreateCustomResource(12, 4, "B - Room 102"));
            resources.Add(CreateCustomResource(13, 4, "B - Room 103"));
            resources.Add(CreateCustomResource(14, 6, "1 - Room 101"));
            resources.Add(CreateCustomResource(15, 6, "1 - Room 102"));
            resources.Add(CreateCustomResource(16, 6, "1 - Room 103"));
            resources.Add(CreateCustomResource(17, 7, "2 - Room 101"));
            resources.Add(CreateCustomResource(18, 7, "2 - Room 102"));
            resources.Add(CreateCustomResource(19, 7, "2 - Room 103"));


            return resources;
        }

        private CustomResource CreateCustomResource(int res_id, int parent_id, string caption) {
            CustomResource cr = new CustomResource();
            cr.ResID = res_id;
            cr.ParentResID = parent_id;
            cr.Name = caption;
            return cr;
        }

        public Random RandomInstance = new Random();
        private CustomAppointment CreateCustomAppointment(string subject, object resourceId, int status, int label) {
            CustomAppointment apt = new CustomAppointment();
            apt.Subject = subject;
            apt.OwnerId = resourceId;
            Random rnd = RandomInstance;
            int rangeInMinutes = 60 * 24;
            apt.StartTime = DateTime.Today + TimeSpan.FromMinutes(rnd.Next(0, rangeInMinutes));
            apt.EndTime = apt.StartTime + TimeSpan.FromMinutes(rnd.Next(0, rangeInMinutes / 4));
            apt.Status = status;
            apt.Label = label;
            return apt;
        }

        protected void ObjectDataSourceAppointment_ObjectCreated(object sender, ObjectDataSourceEventArgs e) {
            if(Session["CustomAppointmentDataSource"] == null) {
                Session["CustomAppointmentDataSource"] = new CustomAppointmentDataSource(GetCustomAppointments());
            }
            e.ObjectInstance = Session["CustomAppointmentDataSource"];
        }

        BindingList<CustomAppointment> GetCustomAppointments() {
            BindingList<CustomAppointment> appointments = new BindingList<CustomAppointment>(); ;
            CustomResourceDataSource resources = Session["CustomResourceDataSource"] as CustomResourceDataSource;
            if(resources != null) {
                foreach(CustomResource item in resources.Resources) {
                    if(item.ResID > 7) {
                        string subjPrefix = item.Name + "'s ";
                        appointments.Add(CreateCustomAppointment(subjPrefix + "meeting", item.ResID, 2, 5));
                        appointments.Add(CreateCustomAppointment(subjPrefix + "travel", item.ResID, 3, 6));
                        appointments.Add(CreateCustomAppointment(subjPrefix + "phone call", item.ResID, 0, 10));
                    }
                }
            }
            return appointments;
        }

        protected void ASPxScheduler1_FilterResource(object sender, PersistentObjectCancelEventArgs e) {
            if(!ASPxTreeList1.IsCallback) {
                List<int> resourcesFilter = new List<int>();
                List<TreeListNode> selectedNodes = ASPxTreeList1.GetSelectedNodes();
                for(int i = 0; i < selectedNodes.Count; i++) {
                    if(!selectedNodes[i].HasChildren) {
                        resourcesFilter.Add(Convert.ToInt32(selectedNodes[i]["ResID"]));
                    }
                }
                e.Cancel = !resourcesFilter.Contains(Convert.ToInt32((e.Object as Resource).Id));                
            }
        }
    }
}