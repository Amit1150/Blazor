using BlazorApp.ComponentsLibrary.Map;
using BlazorApp.Services;
using BlazorApp.Shared;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorApp.Pages
{
    public partial class EmployeeDetail
    {
		[Parameter]
		public string EmployeeId { get; set; }
		public Employee Employee { get; set; } = new Employee();

		[Inject]
		public IEmployeeDataService EmployeeDataService { get; set; }

		public List<Marker> MapMarkers { get; set; } = new List<Marker>();

		protected override async Task OnInitializedAsync()
		{
			Employee = await this.EmployeeDataService.GetEmployeeDetails(int.Parse(EmployeeId));
			MapMarkers = new List<Marker>
			{
				new Marker {Description = $"{Employee.FirstName} {Employee.LastName}", ShowPopup= true, X = Employee.Longitude, Y= Employee.Latitude}
			};
		}
	}
}
