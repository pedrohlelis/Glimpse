using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Glimpse.Models;

public class Card
{
    private int _CardId;
    private String? _CardName;
    private String? _CardDescription;

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int CardId {get{return _CardId;} set{_CardId = value;}}
    public String? CardName {get{return _CardName;} set{_CardName = value;}}
    public String? CardDescription {get{return _CardDescription;} set{_CardDescription = value;}}
    public Lane lane {get; set;}

}
