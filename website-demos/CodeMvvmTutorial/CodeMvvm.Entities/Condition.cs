using System.ComponentModel.DataAnnotations;

namespace CodeMvvm.Entities;

public class Condition
{
    public int? Id { get; set; }
    [MaxLength(100)]
    public string Name { get; set; }
    [MaxLength(10000)]
    public string Description { get; set; }
    [DataType(DataType.Url)]
    public string URL { get; set; }

    [Range(1,9, ErrorMessage = "Spell Level must be between 1 and 9")]
    public int SpellLevel { get; set; }
}