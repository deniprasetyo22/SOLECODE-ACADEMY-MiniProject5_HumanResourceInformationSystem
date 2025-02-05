﻿using MiniProject5.Application.DTOs;
using MiniProject5.Persistence.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniProject5.Application.Interfaces.IServices
{
    public interface IEmployeeService
    {
        Task<IEnumerable<Employee>> GetAllEmployeesNoPagesAsync();
        Task<object> GetAllEmployeesAsync(QueryObject query);
        Task<Employee> GetEmployeeByIdAsync(int empId);
        Task<Employee> AddEmployeeAsync(Employee employee);
        Task UpdateEmployeeAsync(int empId, Employee employee);
        Task DeactivateEmployeeAsync(int empId, string reason);
        Task ActivateEmployeeAsync(int empId);
        Task DeleteEmployeeAsync(int empId);
        Task<IEnumerable<Employee>> SearchEmployee(searchDto search, paginationDto pagination);
    }

}
