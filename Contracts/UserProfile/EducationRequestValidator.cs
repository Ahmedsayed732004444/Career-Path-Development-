namespace Career_Path.Contracts.UserProfile
{
    public class EducationRequestValidator: AbstractValidator<EducationRequest>
    {
        public EducationRequestValidator()
        {
            RuleFor(x => x.University)
                .Length(2, 100);
            RuleFor(x => x.Degree)
               .Length(2, 100);
            RuleFor(x => x.GraduationYear)
                .GreaterThan(1900);
        }
    }
}