using BlazorApp.Services;
using BlazorApp.Shared;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;

namespace BlazorApp.Components
{
    public partial class AddEmployeeDialog
    {
        public Employee Employee { get; set; } = new Employee
        {
            CountryId = 1,
            JobCategoryId = 1,
            BirthDate = DateTime.Now,
            JoinedDate = DateTime.Now
        };

        [Inject]
        public IEmployeeDataService EmployeeDataService { get; set; }

        [Parameter]
        public EventCallback<bool> CloseEventCallBack { get; set; }

        public bool ShowDialog { get; set; }

        protected async Task HandleValidSubmit()
        {
            await EmployeeDataService.AddEmployee(Employee);
            ShowDialog = false;
            await CloseEventCallBack.InvokeAsync(true);
            StateHasChanged();
        }

        public void Show()
        {
            ResetDialog();
            ShowDialog = true;
            StateHasChanged();
        }

        public void Close()
        {
            ShowDialog = false;
            StateHasChanged();
        }

        private void ResetDialog()
        {
            Employee = new Employee
            {
                CountryId = 1,
                JobCategoryId = 1,
                BirthDate = DateTime.Now,
                JoinedDate = DateTime.Now
            };
        }
    }
}
