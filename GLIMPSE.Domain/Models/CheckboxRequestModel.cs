namespace GLIMPSE.Domain.Models;
public class CheckboxRequestModel
{
    public int CardId { get; set; }
    public string? Name { get; set; }
    public bool Finished { get; set;}
}