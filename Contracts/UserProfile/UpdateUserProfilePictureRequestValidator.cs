namespace Career_Path.Contracts.UserProfile
{
    public class UpdateUserProfilePictureRequestValidator: AbstractValidator<UpdateUserProfilePictureRequest>
    {
        public UpdateUserProfilePictureRequestValidator()
        {
            RuleFor(x => x.ProfilePicture)
                .NotNull().WithMessage("Profile picture is required.")

                .Must(file => file != null && (file.ContentType == "image/jpeg" || file.ContentType == "image/png"))
                .WithMessage("Profile picture must be a JPEG or PNG image.")

                .Must(file => file != null && file.Length <= 2 * 1024 * 1024) // 2 MB limit
                .WithMessage("Profile picture size must not exceed 2 MB.");
        }
    }
}
