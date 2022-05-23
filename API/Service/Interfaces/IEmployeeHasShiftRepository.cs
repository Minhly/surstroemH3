using surstroem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Service.Interfaces
{
    public interface IEmployeeHasShiftRepository : IGenericRepository<EmployeeHasShift>
    {
        Task<ICollection<EmployeeHasShift>> GetAllShiftsByEmployeeId(int employeeId);
        Task<ICollection<EmployeeHasShift>> GetAllShiftsByEmployees();
    }
}
