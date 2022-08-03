using EmpTemp.Data.DbContexts;
using EmpTemp.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpTemp.Data.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        EmployeeDbContext _context;

        public EmployeeRepository(EmployeeDbContext context)
        {
            _context = context;
        }

        public async Task<Employee> CreateAsync(Employee entity)
        {
            if(await _context.Employees.AnyAsync(e => e.EmployeeNumber.ToLower() == entity.EmployeeNumber))
                throw new Exception("Duplicate Employee Number!");

            _context.Employees.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var emp = await _context.Employees.SingleOrDefaultAsync(e => e.Id == id);
            if(emp == null)
                return false;

            _context.Employees.Remove(emp);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<List<Employee>> GetAllAsync()
        {
            return await _context.Employees.ToListAsync();
        }

        public async Task<Employee> GetAsync(int id)
        {
            return await _context.Employees.SingleOrDefaultAsync(e => e.Id == id);
        }

        public async Task<List<Employee>> SearchAsync(List<KeyValuePair<string, string>> query)
        {
            // for simplicity we'll load all and filter
            // Might be better to have paging and maybe proper indexing
            var employees = await _context.Employees.ToListAsync();
            if (query.Any(k => k.Key.ToLower() == "name") && !string.IsNullOrEmpty(query.SingleOrDefault(k => k.Key.ToLower() == "name").Value))
            {
                var nameQuery = query.SingleOrDefault(q => q.Key.ToLower() == "name").Value;
                employees = employees.Where(e => 
                e.EmployeeNumber.ToLower().Contains(nameQuery.ToLower())
                || e.FirstName.ToLower().Contains(nameQuery.ToLower())
                || e.LastName.ToLower().Contains(nameQuery.ToLower())
                ).ToList();
            }

            if(query.Any(k => k.Key.ToLower() == "firstname") && !string.IsNullOrEmpty(query.SingleOrDefault(k => k.Key.ToLower() == "firstname").Value))
            {
                var firstNameQuery = query.SingleOrDefault(q => q.Key.ToLower() == "firstname").Value;
                employees = employees.Where(e => e.FirstName.ToLower() == firstNameQuery.ToLower()).ToList();
            }

            if (query.Any(k => k.Key.ToLower() == "lastname") && !string.IsNullOrEmpty(query.SingleOrDefault(k => k.Key.ToLower() == "lastname").Value))
            {
                var lastNameQuery = query.SingleOrDefault(q => q.Key.ToLower() == "lastname").Value;
                employees = employees.Where(e => e.LastName.ToLower() == lastNameQuery.ToLower()).ToList();
            }

            if (query.Any(k => k.Key.ToLower() == "temp") && !string.IsNullOrEmpty(query.SingleOrDefault(k => k.Key.ToLower() == "temp").Value))
            {
                var tempQuery = query.SingleOrDefault(q => q.Key.ToLower() == "temp").Value;
                var tempFrom = tempQuery.Split('-')[0];
                var tempTo = tempQuery.Split('-')[1];

                if(!float.TryParse(tempFrom, out var _) || !float.TryParse(tempTo, out var _))
                    throw new Exception("Invalid Temp Filter.");

                var tempFromFloat = float.Parse(tempFrom);
                var tempToFloat = float.Parse(tempTo);

                employees = employees.Where(e => e.Temperature >= tempFromFloat && e.Temperature <= tempToFloat).ToList();
            }

            if (query.Any(k => k.Key.ToLower() == "recorddate") && !string.IsNullOrEmpty(query.SingleOrDefault(k => k.Key.ToLower() == "recorddate").Value))
            {
                var recordQuery = query.SingleOrDefault(q => q.Key.ToLower() == "recorddate").Value;
                var recordFrom = recordQuery.Split('-')[0];
                var recordTo = recordQuery.Split('-')[1];

                if (!DateTime.TryParse(recordFrom, out var _) || !DateTime.TryParse(recordTo, out var _))
                    throw new Exception("Invalid Record Filter.");

                var recordDateFrom = DateTime.Parse(recordFrom);
                var recordDateTo = DateTime.Parse(recordTo);

                employees = employees.Where(e => e.RecordDate.Date >= recordDateFrom.Date && e.RecordDate.Date <= recordDateTo.Date).ToList();
            }

            return employees;
        }

        public async Task<Employee> UpdateAsync(Employee entity)
        {
            if(await _context.Employees.AnyAsync(e => e.Id == entity.Id))
            {
                _context.Update(entity);
                await _context.SaveChangesAsync();
            }
            return entity;
        }
    }
}
