namespace ArenaPro.Application.Models.MatchModels;
public class MatchGetModel<T>
{
    public T Data { get; set; }
    public bool? Ended { get; set; }
}
