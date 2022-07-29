using System.ComponentModel.DataAnnotations;

namespace MoeBinAPI.Models
{
    public class Paste
    {
        [Key]
        [Required]
        public int ID { get; set; }        
        [Required]
        [MaxLength(25)]
        public string PasteId { get; set; } = null!;
        [Required]
        public byte[] Iv { get; set; } = null!;
        [Required]
        public string CreatedBy { get; set; } = null!;
    }
}