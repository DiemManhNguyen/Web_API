using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema; // Thêm dòng này

namespace web_api_1771020152.Models
{
    public class Reservation
    {
        [Key]
        public int Id { get; set; }

        public int CustomerId { get; set; }

        [Required]
        [StringLength(50)] // QUAN TRỌNG: Phải có dòng này mới tạo được Index
        [Column(TypeName = "varchar")] // Tùy chọn: Dùng varchar cho nhẹ hơn nvarchar
        public string ReservationNumber { get; set; }

        public DateTime ReservationDate { get; set; }
        public int NumberOfGuests { get; set; }
        public string Status { get; set; } = "pending";
        public decimal Total { get; set; } = 0;
        public string PaymentStatus { get; set; } = "pending";

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public virtual Customer Customer { get; set; }
        public virtual ICollection<ReservationItem> ReservationItems { get; set; }
    }
}