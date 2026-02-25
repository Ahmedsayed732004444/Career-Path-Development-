using Career_Path.Contracts.Common;
using Career_Path.Contracts.Users;
namespace Career_Path.Services
{
    public interface IMembershipUpgradeService
    {
        Task<Result> RequestMembershipUpgradeAsync(string userId, MembershipUpgradeRequest request, CancellationToken cancellationToken);

        Task<Result<PaginatedList<MembershipUpgradeResponse>>> GetMembershipUpgradeStatusAsync(RequestFilters filters, CancellationToken cancellationToken);

        Task<Result> ApprovedRequestAsync(string Id, CancellationToken cancellationToken);

        Task<Result> RejectedRequestAsync(string Id, CancellationToken cancellationToken);
        
    }
}
