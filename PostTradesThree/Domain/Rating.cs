using System.ComponentModel.DataAnnotations;

namespace PostTradesThree.Domain
{
    public class Rating
    {
        public int RatingId { get; set; }

        [Required(ErrorMessage = "Moody's Rating is required.")]
        [StringLength(50)]
        public string MoodysRating { get; set; }
        [Required(ErrorMessage = "Standard and Poor's is required.")]
        [StringLength(50)]
        public string SandPRating { get; set; }
        [Required(ErrorMessage = "Fitch Rating is required.")]
        [StringLength(50)]
        public string FitchRating { get; set; }
        public byte? OrderNumber { get; set; }
    }
}
