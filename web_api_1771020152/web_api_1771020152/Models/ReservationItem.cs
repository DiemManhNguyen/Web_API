using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace web_api_1771020152.Models // SỬA NAMESPACE NÀY
{
    [Table("ReservationItems")]
    public class ReservationItem
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ReservationId { get; set; }

        [Required]
        public int MenuItemId { get; set; }

        [Required]
        public int Quantity { get; set; }

        // Định dạng decimal để lưu tiền tệ chính xác, tránh lỗi làm tròn
        [Required]
        [Column(TypeName = "money")]
        public decimal Price { get; set; } // Giá tại thời điểm đặt

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // --- Navigation Properties (Liên kết khóa ngoại) ---

        [ForeignKey("ReservationId")]
        public virtual Reservation Reservation { get; set; }

        [ForeignKey("MenuItemId")]
        public virtual MenuItem MenuItem { get; set; }
    }
}