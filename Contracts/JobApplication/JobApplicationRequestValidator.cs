namespace Career_Path.Contracts.JobApplication;

public class JobApplicationRequestValidator: AbstractValidator<JobApplicationRequest>
{
    public JobApplicationRequestValidator()
    {
        RuleFor(x => x.JobTitle)
            .NotEmpty()
            .WithMessage("Job title is required.")
            .MaximumLength(100)
            .WithMessage("Job title must not exceed 100 characters.");
        RuleFor(x => x.CompanyName)
            .NotEmpty()
            .WithMessage("Company name is required.")
            .MaximumLength(100)
            .WithMessage("Company name must not exceed 100 characters.");

        RuleFor(x => x.ApplicationDate)
            .NotEmpty()
            .WithMessage("Application date is required.")
            .LessThanOrEqualTo(DateTime.Now)
            .WithMessage("Application date cannot be in the future.");
    }
}
