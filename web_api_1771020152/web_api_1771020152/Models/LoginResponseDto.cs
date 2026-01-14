namespace web_api_1771020152.Models
{
    public class LoginResponseDto
    {
        public bool Success { get; set; }
        public string Token { get; set; }
        public object User { get; set; } // Trả về thông tin user (Id, Name)
        public string StudentId { get; set; } // BẮT BUỘC
    }
}