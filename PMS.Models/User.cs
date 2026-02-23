using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PMS.Models;

public class User
{
  [Key]
  [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
  public int Id { get; set; }

  [Required]
  [MaxLength(20)]
  public string PhoneNumber { get; set; } = "";
  
  [MaxLength(100)]
  public string? Email { get; set; }

  [MaxLength(100)]
  public string? Address { get; set; }
  public string? DuluxAccountInfo { get; set; }
  public DateTime CreatedAt { get; set; }

  public List<Order> Orders { get; set; } = new List<Order>();

}
