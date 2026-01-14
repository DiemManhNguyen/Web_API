using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using web_api_1771020152.Models;

namespace web_api_1771020152.Models // Nhớ đổi namespace theo tên project
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; } // Trong thực tế cần Hash, bài thi đơn giản có thể lưu text

        [Required]
        public string FullName { get; set; }

        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public int LoyaltyPoints { get; set; } = 0;
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation Property cho Entity Framework
        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}