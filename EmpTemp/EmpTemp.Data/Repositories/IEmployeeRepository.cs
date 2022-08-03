using EmpTemp.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpTemp.Data.Repositories
{
    public interface IEmployeeRepository
    {
        Task<Employee> GetAsync(int id);
        Task<List<Employee>> GetAllAsync();
        Task<Employee> UpdateAsync(Employee entity);
        Task<bool> DeleteAsync(int id);
        Task<Employee> CreateAsync(Employee entity);
        Task<List<Employee>> SearchAsync(List<KeyValuePair<string, string>> query);
    }
}
