namespace ArenaPro.Domain.Entities;
public abstract class Entity
{
    public int Id { get; private set; }
    public DateTime DeletedAt { get; private set; }

    public void ChangeDate(DateTime date)
    {
        DeletedAt = date;
    }
}
