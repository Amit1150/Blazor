using BlazorApp.Services;
using BlazorApp.Shared;
using Microsoft.AspNetCore.Components;
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

		protected override async Task OnInitializedAsync()
		{
			Employee = await this.EmployeeDataService.GetEmployeeDetails(int.Parse(EmployeeId));
		}
	}
}
