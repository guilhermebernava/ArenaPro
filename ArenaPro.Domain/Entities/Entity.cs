namespace ArenaPro.Domain.Entities;
public abstract class Entity
{
    public int Id { get; private set; }
    public DateTime? DeletedAt { get; private set; }

    public void Delete()
    {
        DeletedAt = DateTime.Now;
    }
}
