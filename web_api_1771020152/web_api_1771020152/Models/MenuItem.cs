using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace web_api_1771020152.Models // Đảm bảo đúng namespace của bạn
{
    public class MenuItem
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; } // Thêm dòng này

        public string Category { get; set; }

        public decimal Price { get; set; }

        public string ImageUrl { get; set; }

        public int PreparationTime { get; set; } // Thêm dòng này (Thời gian chế biến)

        public bool IsVegetarian { get; set; } = false;
        public bool IsSpicy { get; set; } = false;
        public bool IsAvailable { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}