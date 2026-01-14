using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace web_api_1771020152.Models // SỬA NAMESPACE NÀY
{
    [Table("Tables")]
    public class Table
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        public string TableNumber { get; set; } // VD: "T01", "T02"

        [Required]
        public int Capacity { get; set; } // Số ghế (sức chứa)

        public bool IsAvailable { get; set; } = true; // True: Bàn trống, False: Đang có khách

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}