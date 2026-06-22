

using System.ComponentModel.DataAnnotations;

namespace Flowboard.Application.DTOs
{
    public class CvRequestDto
    {
        [Required]
        public string FileUrl { get; set; }
    }
}