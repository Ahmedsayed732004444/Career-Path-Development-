using Career_Path.Contracts.Extraction;
using Career_Path.Contracts.UserProfile;
using Career_Path.Contracts.Roadmap;

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

        config.NewConfig<UserProfile, BasicInfoRequest>();

        config.NewConfig<EducationRequest, UserProfile>();

        config.NewConfig<ExtractionResponse, ModelExtration>()
                 .Map(dest => dest.FullName, src => src.Personal_Info.Full_Name)
                 .Map(dest => dest.Email, src => src.Personal_Info.Email)
                 .Map(dest => dest.Phone, src => src.Personal_Info.Phone)
                 .Map(dest => dest.Location, src => src.Personal_Info.Location)
                 .Map(dest => dest.Summary, src => src.Summary)
                 .Map(dest => dest.Skills, src => src.Skills)
                 .Map(dest => dest.Certifications, src => src.Certifications)
                 .Map(dest => dest.Languages, src => src.Languages);

        config.NewConfig<EducationDto, Education>()
            .Map(dest => dest.Degree, src => src.Degree)
            .Map(dest => dest.Field, src => src.Field)
            .Map(dest => dest.Institution, src => src.Institution)
            .Map(dest => dest.Year, src => src.Year);

        config.NewConfig<ExperienceDto, Experience>()
            .Map(dest => dest.JobTitle, src => src.Job_Title)
            .Map(dest => dest.Company, src => src.Company)
            .Map(dest => dest.StartDate, src => src.Start_Date)
            .Map(dest => dest.EndDate, src => src.End_Date)
            .Map(dest => dest.Description, src => src.Description);

        // Roadmap Mappings
        config.NewConfig<RoadmapResponse, Roadmap>()
            .Map(dest => dest.ApplicationUserId, src => src.UserId)
            .Map(dest => dest.CurrentDomain, src => src.CurrentDomain)
            .Map(dest => dest.CurrentLevel, src => src.CurrentLevel)
            .Map(dest => dest.TargetRole, src => src.TargetRole)
            .Map(dest => dest.DurationMonths, src => src.DurationMonths)
            .Map(dest => dest.TransitionDifficulty, src => src.TransitionDifficulty)
            .Map(dest => dest.IsValidTransition, src => src.IsValidTransition)
            .Map(dest => dest.ValidationMessage, src => src.ValidationMessage)
            .Map(dest => dest.MermaidDiagram, src => src.MermaidDiagram)
            .Map(dest => dest.Phases, src => src.Phases.Select(p => new RoadmapPhase
            {
                Month = p.Month,
                FocusArea = p.FocusArea,
                Skills = p.SkillsToLearn.Select(s => new PhaseSkill { Name = s }).ToList(),
                Resources = p.Resources.Select(r => new PhaseResource { Url = r }).ToList()
            }).ToList())
            .Map(dest => dest.ProjectImprovements, src => src.ProjectImprovements.Select(pi => new ProjectImprovement { Description = pi }).ToList());

        config.NewConfig<Roadmap, RoadmapResponse>()
            .MapWith(roadmap => new RoadmapResponse(
                roadmap.ApplicationUserId,
                roadmap.CurrentDomain,
                roadmap.CurrentLevel,
                roadmap.TargetRole,
                roadmap.DurationMonths,
                roadmap.TransitionDifficulty,
                roadmap.IsValidTransition,
                roadmap.ValidationMessage,
                roadmap.Phases
                    .OrderBy(p => p.Month)
                    .Select(p => new PhaseDto(
                        p.Month,
                        p.FocusArea,
                        p.Skills.Select(s => s.Name).ToList(),
                        p.Resources.Select(r => r.Url).ToList()
                    )).ToList(),
                roadmap.ProjectImprovements.Select(pi => pi.Description).ToList(),
                roadmap.MermaidDiagram
            ));
    }
}