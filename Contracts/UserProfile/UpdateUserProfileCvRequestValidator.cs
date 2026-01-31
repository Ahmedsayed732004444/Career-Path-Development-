namespace Career_Path.Contracts.UserProfile
{
    public class UpdateUserProfileCvRequestValidator : AbstractValidator<UpdateUserProfileCvRequest>
    {
        public UpdateUserProfileCvRequestValidator()
        {
            RuleFor(x => x.CvFile)
                .NotNull().WithMessage("CV file is required.")
                .Must(file => file != null && (file.ContentType == "application/pdf" || file.ContentType == "application/msword" || file.ContentType == "application/vnd.openxmlformats-officedocument.wordprocessingml.document"))
                .WithMessage("CV file must be a PDF or Word document.")
                .Must(file => file != null && file.Length <= 5 * 1024 * 1024) // 5 MB limit
                .WithMessage("CV file size must not exceed 5 MB.");
        }
    }
}
