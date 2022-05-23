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
    public class EmployeeHasShiftRepository : GenericRepository<EmployeeHasShift, surstroemContext>, IEmployeeHasShiftRepository
    {
        public EmployeeHasShiftRepository(surstroemContext dbcontext)
            : base(dbcontext)
        {

        }

        public async Task<ICollection<EmployeeHasShift>> GetAllShiftsByEmployeeId(int employeeId)
        {
            return await _dbcontext.EmployeeHasShifts.Where(c => c.EmployeeId == employeeId)
                        .Include(e => e.Employee)
                        .Include(q => q.Employee.User)
                        .Include(w => w.Employee.User.Address)
                        .Include(x => x.Employee.User.Address.PostalCode)
                        .Include(a => a.Employee.User.Address.PostalCode.Country)
                        .Include(k => k.Employee.Warehouse)
                        .Include(k => k.Employee.Warehouse.Address)
                        .Include(k => k.Employee.Warehouse.Address.PostalCode)
                        .Include(k => k.Employee.Warehouse.Address.PostalCode.Country)
                        .Include(k => k.Employee.Warehouse.WarehouseType)
                        .Include(s => s.Shifts)
                        .ToListAsync();
        }

        public async Task<ICollection<EmployeeHasShift>> GetAllShiftsByEmployees()
        {
            return await _dbcontext.Set<EmployeeHasShift>()
                        .Include(e => e.Employee)
                        .Include(q => q.Employee.User)
                        .Include(w => w.Employee.User.Address)
                        .Include(x => x.Employee.User.Address.PostalCode)
                        .Include(a => a.Employee.User.Address.PostalCode.Country)
                        .Include(k => k.Employee.Warehouse)
                        .Include(k => k.Employee.Warehouse.Address)
                        .Include(k => k.Employee.Warehouse.Address.PostalCode)
                        .Include(k => k.Employee.Warehouse.Address.PostalCode.Country)
                        .Include(k => k.Employee.Warehouse.WarehouseType)
                        .Include(s => s.Shifts)
                        .ToListAsync();
        }
    }
}
