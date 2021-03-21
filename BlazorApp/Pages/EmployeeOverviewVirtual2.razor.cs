using BlazorApp.Services;
using BlazorApp.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web.Virtualization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp.Pages
{
    public partial class EmployeeOverviewVirtual2
    {
        public IList<Employee> Employees { get; set; } = new List<Employee>();

        [Inject]
        public IEmployeeDataService EmployeeDataService { get; set; }

        private float itemHeight = 50;
        private int totalNumberOfEmployees = 1000;

        protected async ValueTask<ItemsProviderResult<Employee>> LoadEmployees(ItemsProviderRequest request)
        {
            var numberOfEmployees = Math.Min(request.Count, totalNumberOfEmployees - request.StartIndex);
            var EmployeeListItems = await EmployeeDataService.GetTakeLongEmployeeList(request.StartIndex, numberOfEmployees);

            return new ItemsProviderResult<Employee>(EmployeeListItems, totalNumberOfEmployees);
        }
    }
}
