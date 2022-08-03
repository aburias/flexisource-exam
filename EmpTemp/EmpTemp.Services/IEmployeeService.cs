using EmpTemp.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpTemp.Services
{
    public interface IEmployeeService
    {
        Task<List<EmployeeDto>> GetAllEmployeesAsync();
        Task<EmployeeDto> GetEmployeeByIdAsync(int id);
        Task<int> CreateEmployeeAsync(EmployeeDto emp);
        Task UpdateEmployeeAsync(int id, EmployeeDto emp);
        Task DeleteEmployeeAsync(int id);
        Task<List<EmployeeDto>> SearchEmployeeAsync(string name, string firstName, string lastName, string temp, string recordDate);
    }
}
