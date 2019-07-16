using System.ComponentModel.DataAnnotations;

namespace MonitorV.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}