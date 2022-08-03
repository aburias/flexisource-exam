using EmpTemp.Data.Entities;
using EmpTemp.Data.Repositories;
using EmpTemp.Dtos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpTemp.Services.Implementations
{
    public class EmployeeService : IEmployeeService
    {
        IEmployeeRepository _repo;
        public EmployeeService(IEmployeeRepository repo)
        {
            _repo = repo;
        }

        public async Task<int> CreateEmployeeAsync(EmployeeDto emp)
        {
            var newEmp = new Employee()
            {
                EmployeeNumber = emp.EmployeeNumber,
                FirstName = emp.FirstName,
                LastName = emp.LastName,
                RecordDate = emp.RecordDate,
                Temperature = emp.Temperature
            };

            var createdEmp = await _repo.CreateAsync(newEmp);
            return createdEmp.Id;
        }

        public async Task DeleteEmployeeAsync(int id)
        {
            await _repo.DeleteAsync(id);
        }

        public async Task<List<EmployeeDto>> GetAllEmployeesAsync()
        {
            var emps = await _repo.GetAllAsync();
            return JsonConvert.DeserializeObject<List<EmployeeDto>>(JsonConvert.SerializeObject(emps));
        }

        public async Task<EmployeeDto> GetEmployeeByIdAsync(int id)
        {
            var emp = await _repo.GetAsync(id);
            return JsonConvert.DeserializeObject<EmployeeDto>(JsonConvert.SerializeObject(emp));
        }

        public async Task<List<EmployeeDto>> SearchEmployeeAsync(string name, string firstName, string lastName, string temp, string recordDate)
        {
            var query = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("name", name),
                new KeyValuePair<string, string>("firstname", firstName),
                new KeyValuePair<string, string>("lastname", lastName),
                new KeyValuePair<string, string>("temp", temp),
                new KeyValuePair<string, string>("recorddate", recordDate)
            };

            var emps = await _repo.SearchAsync(query);
            return JsonConvert.DeserializeObject<List<EmployeeDto>>(JsonConvert.SerializeObject(emps));
        }

        public async Task UpdateEmployeeAsync(int id, EmployeeDto emp)
        {
            emp.Id = id;
            await _repo.UpdateAsync(JsonConvert.DeserializeObject<Employee>(JsonConvert.SerializeObject(emp)));
        }
    }
}
