namespace ArenaPro.Application.Models.MatchValidations;
public class PlayerModel
{
    public string Nick { get; private set; }
    public string? Name { get; set; }
    public int? Age { get; private set; }
    public string? Genre { get; set; }
    public string? Email { get; private set; }
    public int TeamId { get; private set; }
}
