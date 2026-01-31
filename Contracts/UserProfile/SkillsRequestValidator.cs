namespace Career_Path.Contracts.UserProfile
{
    public class SkillsRequestValidator : AbstractValidator<SkillsRequest>
    {
        public SkillsRequestValidator()
        {
            RuleFor(x => x.Skills)
                .NotNull()
                .Must(skills => skills != null && skills.All(skill => !string.IsNullOrWhiteSpace(skill)))
                .WithMessage("Skills list cannot contain null or empty skills.");
        }
    }
}
