namespace Career_Path.Contracts.Job
{
    public class ApplyJobRequestValidator: AbstractValidator<ApplyJobRequest>
    {
        public ApplyJobRequestValidator()
        {
            RuleFor(x => x.CV)
              .NotNull().WithMessage("CV file is required.")
              .Must(file => file != null && (file.ContentType == "application/pdf" || file.ContentType == "application/msword" || file.ContentType == "application/vnd.openxmlformats-officedocument.wordprocessingml.document"))
              .WithMessage("CV file must be a PDF or Word document.")
              .Must(file => file != null && file.Length <= 5 * 1024 * 1024) // 5 MB limit
              .WithMessage("CV file size must not exceed 5 MB.");

            RuleFor(x => x.Phone)
                .NotEmpty()
                .Matches(RegexPatterns.EgyptPhone);
            RuleFor(x => x.Notes)
               .MaximumLength(1000)
               .WithMessage("Notes cannot exceed 1000 characters.");
        }
    }
}
