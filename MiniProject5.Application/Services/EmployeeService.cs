using Microsoft.EntityFrameworkCore;
using MiniProject5.Application.DTOs;
using MiniProject5.Application.Interfaces.IRepositories;
using MiniProject5.Application.Interfaces.IServices;
using MiniProject5.Persistence.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace MiniProject5.Application.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<IEnumerable<Employee>> GetAllEmployeesNoPagesAsync()
        {
            return await _employeeRepository.GetAllEmployeesAsync();
        }

        public async Task<object> GetAllEmployeesAsync(QueryObject query)
        {
            var employees = await _employeeRepository.GetAllEmployeesAsync();
            var temp = employees.AsQueryable();

            if (!string.IsNullOrEmpty(query.Keyword))
            {
                string keywordLower = query.Keyword.ToLower();
                temp = temp.Where(b => b.Fname.ToLower().Contains(keywordLower) ||
                                       b.Lname.ToLower().Contains(keywordLower) ||
                                       b.Position.ToLower().Contains(keywordLower) ||
                                       b.Emptype.ToLower().Contains(keywordLower) ||
                                       b.Level.ToString().Contains(keywordLower) ||
                                       b.Status.ToString().Contains(keywordLower) ||
                                       b.Lastupdateddate.ToString().Contains(keywordLower));
            }

            if (!string.IsNullOrEmpty(query.FirstName))
                temp = temp.Where(b => b.Fname.ToLower().Contains(query.FirstName.ToLower()));

            if (!string.IsNullOrEmpty(query.LastName))
                temp = temp.Where(b => b.Lname.ToLower().Contains(query.LastName.ToLower()));

            if (!string.IsNullOrEmpty(query.FullName))
            {
                temp = temp.Where(b => (b.Fname + " " + b.Lname).ToLower().Contains(query.FullName.ToLower()));
            }

            if (query.Department.HasValue)
                temp = temp.Where(b => b.Deptid == query.Department.Value);

            if (!string.IsNullOrEmpty(query.Position))
                temp = temp.Where(b => b.Position.ToLower().Contains(query.Position.ToLower()));

            if (query.Level.HasValue)
                temp = temp.Where(b => b.Level == query.Level.Value);

            if (!string.IsNullOrEmpty(query.EmpType))
                temp = temp.Where(b => b.Emptype.ToLower().Contains(query.EmpType.ToLower()));

            if (!string.IsNullOrEmpty(query.Status))
                temp = temp.Where(b => b.Status.ToLower().Contains(query.Status.ToLower()));

            if (!string.IsNullOrEmpty(query.LastUpdated))
            {
                if (DateTime.TryParse(query.LastUpdated, out DateTime lastUpdatedDate))
                {
                    // Compare the date part only
                    temp = temp.Where(e => e.Lastupdateddate.HasValue && e.Lastupdateddate.Value.Date == lastUpdatedDate.Date);
                }
            }

            var total = temp.Count();

            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                switch (query.SortBy.ToLower())
                {
                    case "fname":
                        temp = query.SortOrder.Equals("asc", StringComparison.OrdinalIgnoreCase)
                            ? temp.OrderBy(s => s.Fname)
                            : temp.OrderByDescending(s => s.Fname);
                        break;
                    case "lname":
                        temp = query.SortOrder.Equals("asc", StringComparison.OrdinalIgnoreCase)
                            ? temp.OrderBy(s => s.Lname)
                            : temp.OrderByDescending(s => s.Lname);
                        break;
                    case "dept":
                        temp = query.SortOrder.Equals("asc", StringComparison.OrdinalIgnoreCase)
                            ? temp.OrderBy(s => s.Deptid)
                            : temp.OrderByDescending(s => s.Deptid);
                        break;
                    case "position":
                        temp = query.SortOrder.Equals("asc", StringComparison.OrdinalIgnoreCase)
                            ? temp.OrderBy(s => s.Position)
                            : temp.OrderByDescending(s => s.Position);
                        break;
                    case "level":
                        temp = query.SortOrder.Equals("asc", StringComparison.OrdinalIgnoreCase)
                            ? temp.OrderBy(s => s.Level)
                            : temp.OrderByDescending(s => s.Level);
                        break;
                    case "emptype":
                        temp = query.SortOrder.Equals("asc", StringComparison.OrdinalIgnoreCase)
                            ? temp.OrderBy(s => s.Emptype)
                            : temp.OrderByDescending(s => s.Emptype);
                        break;
                    case "status":
                        temp = query.SortOrder.Equals("asc", StringComparison.OrdinalIgnoreCase)
                            ? temp.OrderBy(s => s.Status)
                            : temp.OrderByDescending(s => s.Status);
                        break;
                    case "lastupdated":
                        temp = query.SortOrder.Equals("asc", StringComparison.OrdinalIgnoreCase)
                            ? temp.OrderBy(s => s.Lastupdateddate)
                            : temp.OrderByDescending(s => s.Lastupdateddate);
                        break;
                    default:
                        temp = query.SortOrder.Equals("asc")
                            ? temp.OrderBy(s => s.Empid)
                            : temp.OrderByDescending(s => s.Empid);
                        break;
                }
            }

            var skipNumber = (query.PageNumber - 1) * query.PageSize;

            var empList = temp.Skip(skipNumber).Take(query.PageSize).ToList();

            return new { total = total, data = empList };
        }


        public async Task<Employee> GetEmployeeByIdAsync(int empId)
        {
            var employee = await _employeeRepository.GetEmployeeByIdAsync(empId);
            if (employee == null)
            {
                throw new KeyNotFoundException($"Employee with ID {empId} not found.");
            }
            return employee;
        }

        public async Task<Employee> AddEmployeeAsync(Employee employee)
        {
            if (employee == null)
            {
                throw new ArgumentNullException(nameof(employee), "Employee cannot be null.");
            }

            var existingEmp = await _employeeRepository.GetAllEmployeesAsync();

            if (existingEmp.Any(e => e.Ssn == employee.Ssn))
            {
                throw new InvalidOperationException("An employee with the same SSN already exists.");
            }

            if (existingEmp.Any(e => e.Email.Equals(employee.Email, StringComparison.OrdinalIgnoreCase)))
            {
                throw new InvalidOperationException("An employee with the same email already exists.");
            }

            employee.Status = "Active"; // Set default status
            return await _employeeRepository.AddEmployeeAsync(employee);
        }

        public async Task UpdateEmployeeAsync(int empId, Employee employee)
        {
            await _employeeRepository.UpdateEmployeeAsync(empId, employee);
        }

        //private void HandleDependents(Employee existingEmployee, List<Dependent> updatedDependents)
        //{
        //    if (updatedDependents != null)
        //    {
        //        // Logic to add, update, or remove dependents
        //        var existingDependents = existingEmployee.Dependents.ToList();

        //        // Remove dependents that are not in the updated list
        //        var dependentsToRemove = existingDependents
        //            .Where(d => !updatedDependents.Any(ud => ud.Dependentid == d.Dependentid))
        //            .ToList();

        //        foreach (var dependent in dependentsToRemove)
        //        {
        //            _employeeRepository.DeleteDependent(dependent.Dependentid); // Assuming you have a method to delete dependents
        //        }

        //        // Add or update dependents
        //        foreach (var updatedDependent in updatedDependents)
        //        {
        //            var existingDependent = existingDependents
        //                .FirstOrDefault(d => d.Dependentid == updatedDependent.Dependentid);

        //            if (existingDependent != null)
        //            {
        //                // Update existing dependent
        //                existingDependent.fName = updatedDependent.fName;
        //                existingDependent.lName = updatedDependent.lName;
        //                existingDependent.Sex = updatedDependent.Sex;
        //                existingDependent.Dob = updatedDependent.Dob;
        //                existingDependent.Relationship = updatedDependent.Relationship;
        //            }
        //            else
        //            {
        //                // Add new dependent
        //                existingEmployee.Dependents.Add(new Dependent
        //                {
        //                    fName = updatedDependent.fName,
        //                    lName = updatedDependent.lName,
        //                    Sex = updatedDependent.Sex,
        //                    Dob = updatedDependent.Dob,
        //                    Relationship = updatedDependent.Relationship
        //                });
        //            }
        //        }
        //    }
        //    else
        //    {
        //        // Remove all dependents if none are provided
        //        foreach (var dependent in existingEmployee.Dependents.ToList())
        //        {
        //            _employeeRepository.DeleteDependent(dependent.Dependentid); // Assuming you have a method to delete dependents
        //        }
        //    }
        //}

        public async Task DeactivateEmployeeAsync(int empId, string reason)
        {
            if (string.IsNullOrWhiteSpace(reason))
            {
                throw new ArgumentException("Reason for deactivation cannot be empty.");
            }

            var employee = await _employeeRepository.GetEmployeeByIdAsync(empId);
            if (employee == null)
            {
                throw new KeyNotFoundException($"Employee with ID {empId} not found.");
            }

            employee.Status = "Not Active";
            employee.Reason = reason;
            await _employeeRepository.DeactivateEmployeeAsync(employee);
        }

        public async Task ActivateEmployeeAsync(int empId)
        {
            var employee = await _employeeRepository.GetEmployeeByIdAsync(empId);
            if (employee == null)
            {
                throw new KeyNotFoundException($"Employee with ID {empId} not found.");
            }

            if(employee.Status == "Active")
            {
                throw new ArgumentException($"Employee with ID {empId} was active.");
            }

            employee.Status = "Active";
            employee.Reason = null;
            await _employeeRepository.UpdateEmployeeAsync(empId, employee);
        }


        public async Task DeleteEmployeeAsync(int empId)
        {
            var employee = await _employeeRepository.GetEmployeeByIdAsync(empId);
            if (employee == null)
            {
                throw new KeyNotFoundException($"Employee with ID {empId} not found.");
            }

            await _employeeRepository.DeleteEmployeeAsync(empId);
        }

        public async Task<IEnumerable<Employee>> SearchEmployee(searchDto search, paginationDto pagination)
        {
            var employees = await _employeeRepository.GetAllEmployeesAsync();

            // Apply search filters
            if (search.empId.HasValue)
            {
                employees = employees.Where(e => e.Empid == search.empId.Value);
            }

            if (!string.IsNullOrEmpty(search.fName))
            {
                employees = employees.Where(e => e.Fname.ToLower().Contains(search.fName.ToLower()));
            }

            if (!string.IsNullOrEmpty(search.lName))
            {
                employees = employees.Where(e => e.Lname.ToLower().Contains(search.lName.ToLower()));
            }

            if (!string.IsNullOrEmpty(search.ssn))
            {
                employees = employees.Where(e => e.Ssn.ToLower().Contains(search.ssn.ToLower()));
            }

            if (!string.IsNullOrEmpty(search.address))
            {
                employees = employees.Where(e => e.Address.ToLower().Contains(search.address.ToLower()));
            }

            if (!string.IsNullOrEmpty(search.position))
            {
                employees = employees.Where(e => e.Position.ToLower().Contains(search.position.ToLower()));
            }

            if (!string.IsNullOrEmpty(search.sex))
            {
                employees = employees.Where(e => e.Sex.ToLower().Contains(search.sex.ToLower()));
            }

            if (!string.IsNullOrEmpty(search.empType))
            {
                employees = employees.Where(e => e.Emptype.ToLower().Contains(search.empType.ToLower()));
            }

            if (search.level.HasValue)
            {
                employees = employees.Where(e => e.Level == search.level.Value);
            }

            if (search.deptId.HasValue)
            {
                employees = employees.Where(e => e.Deptid == search.deptId.Value);
            }

            if (!string.IsNullOrEmpty(search.status))
            {
                employees = employees.Where(e => e.Status.ToLower().Contains(search.status.ToLower()));
            }

            // Apply pagination
            var skipNumber = (pagination.pageNumber - 1) * pagination.pageSize;
            return employees.Skip(skipNumber).Take(pagination.pageSize).ToList();
        }
    }

}
