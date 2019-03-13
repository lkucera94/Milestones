using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Milestones.Data
{
    public class Page
    {
        [Key]
        public int PageId { get; set; }
        [Required]
        public string PageName { get; set; }
        [Required]
        public Guid OwnerID { get; set; }
        [Required]
        public int UserID { get; set; }
    }
}
