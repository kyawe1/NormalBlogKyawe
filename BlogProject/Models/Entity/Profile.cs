using System.ComponentModel.DataAnnotations;
namespace BlogProject.Models.Entity
{
    public class Profile
    {
        [Key]
        public int Id { set; get; }
        [Required]
        public string UserId { set; get; }
        [Required]
        [StringLength(200)]
        public string DisplayName { set; get; }
        [Required]
        public string Address { set; get; }
        [Required]
        [DataType(DataType.Date)]
        public DateOnly Birthday { set; get; }
        public DateTime created_at { set; get; }
        public DateTime updated_at { set; get; } = DateTime.UtcNow;

        public Profile()
        {
            created_at= DateTime.UtcNow;
        }
    }
}
