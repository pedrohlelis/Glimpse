using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Glimpse.Models;

public class Tag
{
    private int _TagId;
    private String? _TagName;
    private String? _TagColor;

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int TagId {get{return _TagId;} set{_TagId = value;}}
    public String? TagName {get{return _TagName;} set{_TagName = value;}}
    public String? TagColor {get{return _TagColor;} set{_TagColor = value;}}

}
