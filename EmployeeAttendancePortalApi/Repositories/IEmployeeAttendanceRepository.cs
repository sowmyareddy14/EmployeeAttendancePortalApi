using EmployeeAttendancePortalApi.Models;

namespace EmployeeAttendancePortalApi.Repositories
{
    public interface IEmployeeAttendanceRepository
    {
        Task<IEnumerable<EmployeeAttendance>> GetAllAsync();
        Task<EmployeeAttendance?> GetByIdAsync(Guid id);
        Task<EmployeeAttendance> AddAsync(EmployeeAttendance attendance);
        Task<bool> UpdateAsync(EmployeeAttendance attendance);
        Task<bool> DeleteAsync(Guid id);
    }
}
