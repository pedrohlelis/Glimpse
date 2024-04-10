using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Glimpse.Models;

public class Tag
{
    private int _TagId;
    private string? _TagName;
    private string? _TagColor;

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int TagId {get{return _TagId;} set{_TagId = value;}}
    public string? TagName {get{return _TagName;} set{_TagName = value;}}
    public string? TagColor {get{return _TagColor;} set{_TagColor = value;}}

}
