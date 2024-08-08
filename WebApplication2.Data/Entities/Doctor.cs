using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication2.Data.Entities;

public class Doctor : BaseEntity
{
    public string Name { get; set; }
    public int Experience { get; set; }
    [ForeignKey("Department")]
    public int DepartmentId { get; set; }
    public Department Department { get; set; }
}
