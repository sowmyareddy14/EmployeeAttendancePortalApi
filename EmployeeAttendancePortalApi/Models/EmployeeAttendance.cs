using System.ComponentModel.DataAnnotations;

namespace EmployeeAttendancePortalApi.Models
{
    public enum AttendanceStatus : byte
    {
        Present,
        Absent,
        Leave,
        Remote
    }

    public class EmployeeAttendance
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public string EmployeeId { get; set; } = string.Empty;

        [Required]
        public string EmployeeName { get; set; } = string.Empty;

        [Required]
        public DateOnly Date { get; set; }

        [Required]
        public AttendanceStatus Status { get; set; }

        public string? Notes { get; set; }
    }
}
