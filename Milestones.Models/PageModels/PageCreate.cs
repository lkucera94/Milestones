using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Milestones.Models.PageModels
{
    public class PageCreate
    {
        [Key]
        public int PageId { get; set; }

        [Required]
        [MinLength(2, ErrorMessage = "Please enter atleast 2 characters")]
        [MaxLength(100, ErrorMessage = "There are too many characters in this field.")]
        public string PageName { get; set; }
    }
}
