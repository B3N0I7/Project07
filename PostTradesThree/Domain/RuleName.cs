using System.ComponentModel.DataAnnotations;

namespace PostTradesThree.Domain
{
    public class RuleName
    {
        public int RuleNameId { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(50)]
        public string Name { get; set; }
        [Required(ErrorMessage = "Description is required.")]
        [StringLength(50)]
        public string Description { get; set; }
        [Required(ErrorMessage = "Json is required.")]
        [StringLength(50)]
        public string Json { get; set; }
        [Required(ErrorMessage = "Template is required.")]
        [StringLength(50)]
        public string Template { get; set; }
        [Required(ErrorMessage = "SQL String is required.")]
        [StringLength(50)]
        public string SqlStr { get; set; }
        [Required(ErrorMessage = "SQL Partition is required.")]
        [StringLength(50)]
        public string SqlPart { get; set; }
    }
}
