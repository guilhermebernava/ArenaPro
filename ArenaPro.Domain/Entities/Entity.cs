namespace ArenaPro.Domain.Entities;
public abstract class Entity
{
    public int Id { get; set; }
    public DateTime DeletedAt { get; set; }
}
