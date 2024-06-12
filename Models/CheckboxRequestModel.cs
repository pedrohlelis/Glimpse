namespace Glimpse.Models;
public class CheckboxRequestModel
{
    public int? CardId { get; set; }
    public string? Name { get; set; }
    public bool Finished { get; set; }
    public int? CheckboxId { get; set; }
    public int? BoardId { get; set; }
}