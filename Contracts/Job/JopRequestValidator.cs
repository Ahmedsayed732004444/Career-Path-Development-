using System.Text.Json;

namespace Career_Path.Contracts.Job
{
    public class JopRequestValidator: AbstractValidator<JopRequest>
    {
        public JopRequestValidator()
        {
            RuleFor(x => x.JobTitle)
                .NotEmpty()
                .MaximumLength(200);

            RuleFor(x => x.JobDescription)
                .NotEmpty()
                .MaximumLength(2000);
            RuleFor(x => x.JobType)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.Location)
                .MaximumLength(200);

            RuleFor(x => x.JobRequirements)
                .NotEmpty()
                .Must(x => x.All(r => !string.IsNullOrEmpty(r)));

            RuleFor(x => x.ExperienceLevel)
                .IsInEnum();
            RuleFor(x => x.SalaryMin)
                .GreaterThanOrEqualTo(0)
                .LessThanOrEqualTo(x => x.SalaryMax ?? decimal.MaxValue);
            RuleFor(x => x.SalaryMax)
                .GreaterThanOrEqualTo(x => x.SalaryMin ?? 0);
            RuleFor(x => x.DeadlineDate)
                .GreaterThan(DateTime.UtcNow);
        }
    }
  }

