namespace Career_Path.Contracts.UserProfile
{
    public class BasicInfoRequestValidator : AbstractValidator<BasicInfoRequest>
    {
        public BasicInfoRequestValidator()
        {
            RuleFor(x => x.FirstName)
               .Length(3, 100);

            RuleFor(x => x.LastName)
                .Length(3, 100);

            RuleFor(x => x.Gender)
                .IsInEnum();

            RuleFor(x => x.Country)
              .Length(3, 100);

            RuleFor(x => x.City)
             .Length(3, 100);

            RuleFor(x => x.JobTitle)
              .Length(3, 200);

            RuleFor(x => x.CurrentCompany)
              .Length(3, 200);
        }
    }
}