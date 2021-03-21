using BlazorApp.Services;
using BlazorApp.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace BlazorApp.Pages
{
    public partial class EmployeeEdit
    {
        [Inject]
        public IEmployeeDataService EmployeeDataService { get; set; }

        [Inject]
        public ICountryDataService CountryDataService { get; set; }

        [Inject]
        public IJobCategoryDataService JobCategoryDataService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Parameter]
        public string EmployeeId { get; set; }

        public IEnumerable<Country> Countries { get; set; } = new List<Country>();
        public IEnumerable<JobCategory> JobCategories { get; set; } = new List<JobCategory>();
        public Employee Employee { get; set; } = new Employee();

        protected string CountryId = string.Empty;
        protected string JobCategoryId = string.Empty;
        private IReadOnlyList<IBrowserFile> selectedFiles;
        private ElementReference LastNameInput;

        protected string Message = string.Empty;
        protected string StatusClass = string.Empty;
        protected bool IsSaved;

        protected override async Task OnInitializedAsync()
        {
            IsSaved = false;
            Countries = await this.CountryDataService.GetAllCountries();
            JobCategories = await this.JobCategoryDataService.GetAllJobCategories();

            int employeeId;
            if(int.TryParse(EmployeeId, out employeeId) && employeeId > 0)
            {
                Employee = await this.EmployeeDataService.GetEmployeeDetails(int.Parse(EmployeeId));
            }
            else
            {
                Employee = new Employee { CountryId = 1, JobCategoryId = 1, BirthDate = DateTime.Now, JoinedDate = DateTime.Now };
            }

            CountryId = Employee.CountryId.ToString();
            JobCategoryId = Employee.JobCategoryId.ToString();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await LastNameInput.FocusAsync();
        }

        protected async Task HandleValidSubmit()
        {
            IsSaved = false;
            Employee.CountryId = int.Parse(CountryId);
            Employee.JobCategoryId = int.Parse(JobCategoryId);
            if (Employee.EmployeeId < 1)
            {
                if(selectedFiles != null)
                {
                    var file = selectedFiles[0];
                    Stream stream = file.OpenReadStream();
                    MemoryStream ms = new MemoryStream();
                    await stream.CopyToAsync(ms);
                    stream.Close();
                    Employee.ImageName = file.Name;
                    Employee.ImageContent = ms.ToArray();
                }

                var addEmployee = await EmployeeDataService.AddEmployee(Employee);
                if(addEmployee != null)
                {
                    StatusClass = "alert-success";
                    Message = "New employee added successfully.";
                    IsSaved = true;
                }
                else
                {
                    StatusClass = "alert-danger";
                    Message = "Something went wrong adding the new employee. Please try again.";
                    IsSaved = false;
                }
            }
            else
            {
                await EmployeeDataService.UpdateEmployee(Employee);
                StatusClass = "alert-success";
                Message = "Employee updated successfully.";
                IsSaved = true;
            }
        }

        protected void HandleInvalidSubmit()
        {
            StatusClass = "alert-danger";
            Message = "There are some validation error. Please try again later.";
        }

        protected async Task DeleteEmployee()
        {
            await EmployeeDataService.DeleteEmployee(Employee.EmployeeId);
            StatusClass = "alert-success";
            Message = "Deleted successfully.";
            IsSaved = true;
        }

        protected void NavigateToOverview()
        {
            NavigationManager.NavigateTo("/employeeoverview");
        }

        protected void OnInputFileChanged(InputFileChangeEventArgs e)
        {
            selectedFiles = e.GetMultipleFiles();
            Message = $"{selectedFiles.Count} file(s) selected";
            StateHasChanged();
        }
    }
}
