using API.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using surstroem.Data;
using surstroem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Service.Repositories
{
    public class EmployeeRepository : GenericRepository<Employee, surstroemContext>, IEmployeeRepository
    {
        public EmployeeRepository(surstroemContext dbcontext)
            : base(dbcontext)
        {

        }

        public async Task<Employee> GetEmployeeWithAllInfo(int employeeId)
        {
            return await _dbcontext.Employees.Where(c => c.Id == employeeId)
                .Include(c => c.User)
                .Include(c => c.User.Address)
                .Include(s => s.User.Address.PostalCode)
                .Include(d => d.User.Address.PostalCode.Country)
                .Include(c => c.Warehouse)
                .Include(s => s.Warehouse.WarehouseType)
                .Include(p => p.Warehouse.Address)
                .Include(i => i.Warehouse.Address.PostalCode)
                .Include(o => o.Warehouse.Address.PostalCode.Country)
                .FirstOrDefaultAsync();
        }

        public async Task<ICollection<Employee>> GetEmployeesContactInfo()
        {
            return await _dbcontext.Set<Employee>()
                    .Include(q => q.User)
                    .Include(j => j.User.Address)
                    .Include(w => w.User.Address.PostalCode)
                    .Include(r => r.User.Address.PostalCode.Country)
                    .Include(c => c.Warehouse)
                    .Include(s => s.Warehouse.WarehouseType)
                    .Include(p => p.Warehouse.Address)
                    .Include(i => i.Warehouse.Address.PostalCode)
                    .Include(o => o.Warehouse.Address.PostalCode.Country)
                    .ToListAsync();
        }
    }
}
