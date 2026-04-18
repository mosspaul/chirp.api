using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace data.Models;

public class Expense {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public Guid AppUserId { get; set; }
    public User User { get; set; }
    public DateTime Date { get; set; }
    public float Amount { get; set; }
}