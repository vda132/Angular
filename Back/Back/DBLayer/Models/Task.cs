using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBLayer.Models;

[Table("tblTask")]
public class Task : ModelBase
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
}
