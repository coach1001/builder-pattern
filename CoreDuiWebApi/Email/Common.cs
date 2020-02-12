using System;
using System.ComponentModel.DataAnnotations;

namespace CoreDuiWebApi.Email
{
    public class Common
    {
        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        [Required]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
