using BlazorApp.Components;
using BlazorApp.Services;
using BlazorApp.Shared;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorApp.Pages
{
    public partial class EmployeeOverview
    {
        public IEnumerable<Employee> Employees { get; set; }

        private List<Country> Countries { get; set; }

        private List<JobCategory> JobCategories { get; set; }

        [Inject]
        public IEmployeeDataService EmployeeDataService { get; set; }

        public AddEmployeeDialog AddEmployeeDialog { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Employees = await this.EmployeeDataService.GetAllEmployees();
        }

        protected void QuickAddEmployee()
        {
            AddEmployeeDialog.Show();
        }

        public async void AddEmployeeDialog_OnDialogClose()
        {
            Employees = await this.EmployeeDataService.GetAllEmployees();
            StateHasChanged();
        }
    }
}
