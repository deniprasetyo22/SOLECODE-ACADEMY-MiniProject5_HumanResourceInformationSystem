using Microsoft.EntityFrameworkCore;
using MiniProject5.Application.DTOs;
using MiniProject5.Application.Interfaces.IRepositories;
using MiniProject5.Persistence.Context;
using MiniProject5.Persistence.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniProject5.Persistence.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly HrisContext _context;

        public DepartmentRepository(HrisContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Department>> GetAllDepartmentsAsync()
        {
            return await _context.Departments.ToListAsync();
        }

        public async Task<Department> GetDepartmentByIdAsync(int deptId)
        {
            return await _context.Departments
            .Include(d => d.Locations)
            .FirstOrDefaultAsync(d => d.Deptid == deptId);
        }

        public async Task<Department> AddDepartmentAsync(Department department)
        {
            _context.Departments.Add(department);
            await _context.SaveChangesAsync();
            return department;
        }

        public async Task UpdateDepartmentAsync(Department department)
        {
            //var existingDept = await _context.Departments
            //    .Include(d => d.Locations)
            //    .FirstOrDefaultAsync(d => d.Deptid == deptId);

            _context.Departments.Update(department);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteDepartmentAsync(int deptId)
        {
            var department = await _context.Departments.FindAsync(deptId);
            if (department != null)
            {
                _context.Departments.Remove(department);
                await _context.SaveChangesAsync();
            }
        }
    }
}
