using FluentValidation;
using FluentValidation.Attributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace SportLogger.Models
{
    public class ResortReference
    {
        [Key]
        [Display(Name = "Resort Name")]
        public string ResortName { get; set; }
    }

    [Validator(typeof(SkiDayValidator))]
    public class SkiDay
    {
        [Key]
        public int Id { get; set; }

        [DataType(DataType.Date,ErrorMessage="Invalid ski date.")]
        public DateTime SkiDate { get; set; }

        public string Resort { get; set; }

        [DisplayFormat(DataFormatString = "{0:n0}")]
        public int Vertical { get; set; }

        public string Partners { get; set; }

        [Display(Name = "Snow In 24")]
        public int NewSnow24 { get; set; }

        [Display(Name = "Snow In 72")]
        public int NewSnow72 { get; set; }

        [DisplayFormat(DataFormatString = "{0:n0}"+" °F")]
        public int Temperature { get; set; }

        public string Comments { get; set; }
    }

    public class SkiDayValidator : AbstractValidator<SkiDay>
    {
        public SkiDayValidator()
        {
            RuleFor(x => x.SkiDate.ToString())
                .NotEmpty().WithMessage("Ski date is required.");
                //.Must(IsValidDate).WithMessage("Ski date is not valid.");

            RuleFor(x => x.Resort).NotEmpty().WithMessage("Please select a resort.");

            RuleFor(x => x.Vertical).InclusiveBetween(0,100000).WithMessage("Vertical feet should be between 0 and 100K.");

            RuleFor(x => x.NewSnow24).InclusiveBetween(0, 100).WithMessage("Snow in last 24 hours should be between 0 and 100 inches.");

            RuleFor(x => x.NewSnow72).InclusiveBetween(0, 100).WithMessage("Snow in last 72 hours should be between 0 and 100 inches.");

            RuleFor(x => x.Temperature).InclusiveBetween(-30,100).WithMessage("Temperature range should be -30 to 100.");

            RuleFor(x => x.Comments).Length(0, 100).WithMessage("The comments value cannot exceed 100 characters.");
        }

        public bool IsValidDate(string val)
        {
            DateTime date;
            return DateTime.TryParse(val, out date);
        }

    }


}
