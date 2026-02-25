namespace Career_Path.Contracts.Users
{
    public class MembershipUpgradeRequestValidator: AbstractValidator<MembershipUpgradeRequest>
    {
        public MembershipUpgradeRequestValidator()
        {
            RuleFor(x => x.Note)
                .MaximumLength(1000)
                .WithMessage("Note cannot exceed 1000 characters.");
        }
    }
}
