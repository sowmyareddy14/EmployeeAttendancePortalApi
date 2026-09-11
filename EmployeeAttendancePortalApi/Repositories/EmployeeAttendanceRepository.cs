using EmployeeAttendancePortalApi.Data;
using EmployeeAttendancePortalApi.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeAttendancePortalApi.Repositories
{
    public class EmployeeAttendanceRepository : IEmployeeAttendanceRepository
    {
        private readonly AttendanceDbContext _db;

        public EmployeeAttendanceRepository(AttendanceDbContext db)
        {
            _db = db;
        }

        public async Task<EmployeeAttendance> AddAsync(EmployeeAttendance attendance)
        {
            if (attendance.Id == Guid.Empty)
                attendance.Id = Guid.NewGuid();

            _db.EmployeeAttendances.Add(attendance);
            await _db.SaveChangesAsync();
            return attendance;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var entity = await _db.EmployeeAttendances.FindAsync(id);
            if (entity == null)
                return false;

            _db.EmployeeAttendances.Remove(entity);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<EmployeeAttendance>> GetAllAsync()
        {
            return await _db.EmployeeAttendances.AsNoTracking().ToListAsync();
        }

        public async Task<EmployeeAttendance?> GetByIdAsync(Guid id)
        {
            return await _db.EmployeeAttendances.FindAsync(id);
        }

        public async Task<bool> UpdateAsync(EmployeeAttendance attendance)
        {
            var exists = await _db.EmployeeAttendances.AnyAsync(e => e.Id == attendance.Id);
            if (!exists)
                return false;

            _db.EmployeeAttendances.Update(attendance);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
