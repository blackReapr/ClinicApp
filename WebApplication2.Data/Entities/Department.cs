namespace WebApplication2.Data.Entities;

public class Department : BaseEntity
{
    public string Name { get; set; }
    public int Limit { get; set; }
    public string Image { get; set; }

    public List<Doctor> Doctors { get; set; }
}
