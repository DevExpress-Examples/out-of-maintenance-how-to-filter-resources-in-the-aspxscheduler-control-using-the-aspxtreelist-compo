Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Linq
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports DevExpress.Web.ASPxTreeList
Imports DevExpress.XtraScheduler

Namespace WebApplication1
	Partial Public Class [Default]
		Inherits System.Web.UI.Page
		Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)

		End Sub

		Protected Sub ObjectDataSourceResources_ObjectCreated(ByVal sender As Object, ByVal e As ObjectDataSourceEventArgs)
			If Session("CustomResourceDataSource") Is Nothing Then
				Session("CustomResourceDataSource") = New CustomResourceDataSource(GetCustomResources())
			End If
			e.ObjectInstance = Session("CustomResourceDataSource")
		End Sub

		Private Function GetCustomResources() As BindingList(Of CustomResource)
			Dim resources As New BindingList(Of CustomResource)()
			resources.Add(CreateCustomResource(1, -1, "Hong Kong"))
			resources.Add(CreateCustomResource(2, -1, "United States"))
			resources.Add(CreateCustomResource(3, 1, "Hotel A"))
			resources.Add(CreateCustomResource(4, 1, "Hotel B"))

			resources.Add(CreateCustomResource(6, 2, "Hotel 1"))
			resources.Add(CreateCustomResource(7, 2, "Hotel 2"))

			resources.Add(CreateCustomResource(8, 3, "A - Room 101"))
			resources.Add(CreateCustomResource(9, 3, "A - Room 102"))
			resources.Add(CreateCustomResource(10, 3, "A - Room 103"))
			resources.Add(CreateCustomResource(11, 4, "B - Room 101"))
			resources.Add(CreateCustomResource(12, 4, "B - Room 102"))
			resources.Add(CreateCustomResource(13, 4, "B - Room 103"))
			resources.Add(CreateCustomResource(14, 6, "1 - Room 101"))
			resources.Add(CreateCustomResource(15, 6, "1 - Room 102"))
			resources.Add(CreateCustomResource(16, 6, "1 - Room 103"))
			resources.Add(CreateCustomResource(17, 7, "2 - Room 101"))
			resources.Add(CreateCustomResource(18, 7, "2 - Room 102"))
			resources.Add(CreateCustomResource(19, 7, "2 - Room 103"))


			Return resources
		End Function

		Private Function CreateCustomResource(ByVal res_id As Integer, ByVal parent_id As Integer, ByVal caption As String) As CustomResource
			Dim cr As New CustomResource()
			cr.ResID = res_id
			cr.ParentResID = parent_id
			cr.Name = caption
			Return cr
		End Function

		Public RandomInstance As New Random()
		Private Function CreateCustomAppointment(ByVal subject As String, ByVal resourceId As Object, ByVal status As Integer, ByVal label As Integer) As CustomAppointment
			Dim apt As New CustomAppointment()
			apt.Subject = subject
			apt.OwnerId = resourceId
			Dim rnd As Random = RandomInstance
			Dim rangeInMinutes As Integer = 60 * 24
			apt.StartTime = DateTime.Today + TimeSpan.FromMinutes(rnd.Next(0, rangeInMinutes))
			apt.EndTime = apt.StartTime + TimeSpan.FromMinutes(rnd.Next(0, rangeInMinutes \ 4))
			apt.Status = status
			apt.Label = label
			Return apt
		End Function

		Protected Sub ObjectDataSourceAppointment_ObjectCreated(ByVal sender As Object, ByVal e As ObjectDataSourceEventArgs)
			If Session("CustomAppointmentDataSource") Is Nothing Then
				Session("CustomAppointmentDataSource") = New CustomAppointmentDataSource(GetCustomAppointments())
			End If
			e.ObjectInstance = Session("CustomAppointmentDataSource")
		End Sub

		Private Function GetCustomAppointments() As BindingList(Of CustomAppointment)
			Dim appointments As New BindingList(Of CustomAppointment)()

			Dim resources As CustomResourceDataSource = TryCast(Session("CustomResourceDataSource"), CustomResourceDataSource)
			If resources IsNot Nothing Then
				For Each item As CustomResource In resources.Resources
					If item.ResID > 7 Then
						Dim subjPrefix As String = item.Name & "'s "
						appointments.Add(CreateCustomAppointment(subjPrefix & "meeting", item.ResID, 2, 5))
						appointments.Add(CreateCustomAppointment(subjPrefix & "travel", item.ResID, 3, 6))
						appointments.Add(CreateCustomAppointment(subjPrefix & "phone call", item.ResID, 0, 10))
					End If
				Next item
			End If
			Return appointments
		End Function

		Protected Sub ASPxScheduler1_FilterResource(ByVal sender As Object, ByVal e As PersistentObjectCancelEventArgs)
			If (Not ASPxTreeList1.IsCallback) Then
				Dim resourcesFilter As New List(Of Integer)()
				Dim selectedNodes As List(Of TreeListNode) = ASPxTreeList1.GetSelectedNodes()
				For i As Integer = 0 To selectedNodes.Count - 1
					If (Not selectedNodes(i).HasChildren) Then
						resourcesFilter.Add(Convert.ToInt32(selectedNodes(i)("ResID")))
					End If
				Next i
				e.Cancel = Not resourcesFilter.Contains(Convert.ToInt32((TryCast(e.Object, Resource)).Id))
			End If
		End Sub
	End Class
End Namespace