using Microsoft.EntityFrameworkCore;
using MiniProject5.Application.DTOs;
using MiniProject5.Application.Interfaces.IRepositories;
using MiniProject5.Persistence.Context;
using MiniProject5.Persistence.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Numerics;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MiniProject5.Persistence.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly HrisContext _context;

        public EmployeeRepository(HrisContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Employee>> GetAllEmployeesAsync()
        {
            return await _context.Employees.ToListAsync();
        }

        public async Task<Employee> GetEmployeeByIdAsync(int empId)
        {
            return await _context.Employees.Include(e => e.Dependents).FirstOrDefaultAsync(e => e.Empid == empId);
        }

        public async Task<Employee> AddEmployeeAsync(Employee employee)
        {
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
            return employee;
        }

        public async Task UpdateEmployeeAsync(int empId, Employee employee)
        {
            var existingEmployee = await _context.Employees
                .Include(e => e.Dependents) // Include dependents if needed
                .FirstOrDefaultAsync(e => e.Empid == empId);

            // Update properties
            existingEmployee.Fname = employee.Fname;
            existingEmployee.Lname = employee.Lname;
            existingEmployee.Ssn = employee.Ssn;
            existingEmployee.Email = employee.Email;
            existingEmployee.Address = employee.Address;
            existingEmployee.Position = employee.Position;
            existingEmployee.Salary = employee.Salary;
            existingEmployee.Sex = employee.Sex;
            existingEmployee.Dob = employee.Dob;
            existingEmployee.Phoneno = employee.Phoneno;
            existingEmployee.Emptype = employee.Emptype;
            existingEmployee.Level = employee.Level;
            existingEmployee.Deptid = employee.Deptid;
            existingEmployee.Lastupdateddate = DateTime.Now;
            existingEmployee.SupervisorId = employee.SupervisorId;

            // Handle dependents
            if (employee.Dependents != null)
            {
                // Find existing dependents by their ID in the database
                var existingDependents = existingEmployee.Dependents.ToList();

                // Remove dependents that are not in the updated list
                var dependentsToRemove = existingDependents
                    .Where(d => !employee.Dependents.Any(ud => ud.Dependentid == d.Dependentid))
                    .ToList();

                foreach (var dependent in dependentsToRemove)
                {
                    _context.Dependents.Remove(dependent);
                }

                // Add or update dependents
                foreach (var updatedDependent in employee.Dependents)
                {
                    var existingDependent = existingDependents
                        .FirstOrDefault(d => d.Dependentid == updatedDependent.Dependentid);

                    if (existingDependent != null)
                    {
                        // Update existing dependent
                        existingDependent.fName = updatedDependent.fName;
                        existingDependent.lName = updatedDependent.lName;
                        existingDependent.Sex = updatedDependent.Sex;
                        existingDependent.Dob = updatedDependent.Dob;
                        existingDependent.Relationship = updatedDependent.Relationship;
                    }
                    else
                    {
                        // Add new dependent
                        existingEmployee.Dependents.Add(new Dependent
                        {
                            fName = updatedDependent.fName,
                            lName = updatedDependent.lName,
                            Sex = updatedDependent.Sex,
                            Dob = updatedDependent.Dob,
                            Relationship = updatedDependent.Relationship
                        });
                    }
                }
            }
            else
            {
                // Remove all dependents if none are provided
                _context.Dependents.RemoveRange(existingEmployee.Dependents);
            }

            // Save changes to the database
            await _context.SaveChangesAsync();
        }

        public async Task DeactivateEmployeeAsync(Employee employee)
        {
            _context.Employees.Update(employee);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteEmployeeAsync(int empId)
        {
            var employee = await _context.Employees.FindAsync(empId);
            if (employee != null)
            {
                _context.Employees.Remove(employee);
                await _context.SaveChangesAsync();
            }
        }
    }

}
