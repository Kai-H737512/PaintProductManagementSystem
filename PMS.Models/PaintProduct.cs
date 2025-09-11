using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PMS.Models;

public class PaintProduct
//我们现在要在数据库里创建 PaintProduct table 
{
    //id, name, description, guid(duluxId), CTOR
    //System.Data.Annotations // 约束
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [MaxLength(50)]
    public string PaintProductName { get; set; }
    public string Description { get; set; }
    public Guid DuluxId { get; set; }

    //EF 
    //Navigation Property
    [JsonIgnore]
    public List<Order> Orders { get; set; }

    public PaintProduct(int id, string paintProductName, string description, Guid duluxId)
    {
        Id = id;
        PaintProductName = paintProductName;
        Description = description;
        DuluxId = duluxId;
        Orders = new List<Order>();
    }
}
