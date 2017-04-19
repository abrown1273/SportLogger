using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SportLogger.Models
{
    public class ResortReference
    {
        [Key]
        [Display(Name = "Resort Name")]
        public string ResortName { get; set; }
    }

    public class SkiDay
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime SkiDate { get; set; }

        [Required]
        public string Resort { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:n0}")]
        [Range(0, 100000)]
        public int Vertical { get; set; }

        public string Partners { get; set; }

        [Display(Name = "Snow In 24")]
        [Range(0, 100)]
        public int NewSnow24 { get; set; }

        [Display(Name = "Snow In 72")]
        [Range(0, 100)]
        public int NewSnow72 { get; set; }

        [DisplayFormat(DataFormatString = "{0:n0}"+" °F")]
        [Range(-30,100)]
        public int Temperature { get; set; }

        [StringLength(100, ErrorMessage = "The comments value cannot exceed 100 characters.")]
        public string Comments { get; set; }
    }
}
