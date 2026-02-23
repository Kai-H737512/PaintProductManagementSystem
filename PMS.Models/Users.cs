using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PMS.Models;

public class Users
{
  [Key]
  [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
  public int Id { get; set; }

  [Required]
  [MaxLength(20)]
  public string PhoneNumber { get; set; } = "";
  
  // [MaxLength(100)]
  // public string PhoneNumber { get; set; } = "";
  // public string PhoneNumber { get; set; } = "";
  

}
