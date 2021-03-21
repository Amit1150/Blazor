using BlazorApp.Services;
using BlazorApp.Shared;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp.Pages
{
    public partial class EmployeeOverviewVirtual
    {
        public IList<Employee> Employees { get; set; } = new List<Employee>();

        [Inject]
        public IEmployeeDataService EmployeeDataService { get; set; }

        private float itemHeight = 50;

        protected override async Task OnInitializedAsync()
        {
            Employees = (await this.EmployeeDataService.GetLongEmployeeList()).ToList();
        }
    }
}
