

using Career_Path.Contracts.UserProfile;

namespace Career_Path.Mapping;

public class MappingConfigurations : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
       

        config.NewConfig<RegisterRequest, ApplicationUser>()
            .Map(dest => dest.UserName, src => src.Email);

        config.NewConfig<UserProfile, UserProfileResponse>()
             .Map(dest => dest.FullName, src => $"{src.ApplicationUser.FirstName} {src.ApplicationUser.LastName}")
             .Map(dest => dest.Email, src => src.ApplicationUser.Email)
             .Map(dest => dest.Skills, src => src.Skills.Select(s => s.Name));

        // ✅ Mapping من UserProfile إلى BasicInfoRequest (للقراءة)
        config.NewConfig<UserProfile, BasicInfoRequest>();
        config.NewConfig<EducationRequest, UserProfile>();
    }


}


