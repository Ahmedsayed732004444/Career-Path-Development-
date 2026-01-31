namespace Career_Path.Contracts.UserProfile
{
    public class SummaryRequestValidator: AbstractValidator<SummaryRequest>
    {
        public SummaryRequestValidator()
        {
            RuleFor(x => x.Summary)
                .MaximumLength(500);
        }
    }
}
