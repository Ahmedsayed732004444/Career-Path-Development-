using Career_Path.Contracts.Common;
using Career_Path.Contracts.Users;
using Microsoft.AspNetCore.Identity.UI.Services;
using System.Data;
using System.Linq.Dynamic.Core;
namespace Career_Path.Services
{
    public class MembershipUpgradeService(ApplicationDbContext context, IEmailSender emailSender) : IMembershipUpgradeService
    {
        private readonly ApplicationDbContext _context = context;
        private readonly IEmailSender _emailSender = emailSender;
        public async Task<Result> RequestMembershipUpgradeAsync(string userId, MembershipUpgradeRequest request, CancellationToken cancellationToken)
        {
            var Existuser = await _context.MembershipUpgrades
                .AnyAsync(m => m.UserId == userId && (m.Status == RequestStatus.Pending|| m.Status == RequestStatus.Approved), cancellationToken);
            if (Existuser)
                return Result.Failure(UserErrors.DuplicatedRequest);

            var upgradeRequest = request.Adapt<MembershipUpgrade>();
            upgradeRequest.UserId = userId;
              await _context.MembershipUpgrades.AddAsync(upgradeRequest);
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }

        public async Task<Result<PaginatedList<MembershipUpgradeResponse>>> GetMembershipUpgradeStatusAsync(RequestFilters filters, CancellationToken cancellationToken)
        {
            var query = _context.MembershipUpgrades.AsQueryable();

            if (!string.IsNullOrEmpty(filters.SearchValue))
            {
                query = query.Where(x => x.Note != null && x.Note.Contains(filters.SearchValue));
            }

            if (!string.IsNullOrEmpty(filters.SortColumn))
            {
                query = query.OrderBy($"{filters.SortColumn} {filters.SortDirection}");
            }

            var source = query
              .ProjectToType<MembershipUpgradeResponse>()
              .AsNoTracking();

            var submissions = await PaginatedList<MembershipUpgradeResponse>.CreateAsync(source, filters.PageNumber, filters.PageSize, cancellationToken);
            return Result.Success(submissions);
        }
        public async Task<Result> ApprovedRequestAsync(string Id, CancellationToken cancellationToken)
        {
            var request = await _context.MembershipUpgrades
                .Include(r => r.User)
                .FirstOrDefaultAsync(r => r.Id == Id, cancellationToken);
            if (request is null)
                return Result.Failure(UserErrors.RequestNotFound);
            if (request.Status != RequestStatus.Pending)
                return Result.Failure(UserErrors.CannotApproveRequest);
            request.Status = RequestStatus.Approved;
            await _context.SaveChangesAsync(cancellationToken);
            await _emailSender.SendEmailAsync(request.User.Email!, "✅ Career Path: MembershipUpgrade", ApprovalEmailBody(request));
            return Result.Success();
        }
        public async Task<Result> RejectedRequestAsync(string Id, CancellationToken cancellationToken)
        {
            var request = await _context.MembershipUpgrades
                .Include(r => r.User)
                .FirstOrDefaultAsync(r=>r.Id==Id, cancellationToken);
            if (request is null)
                return Result.Failure(UserErrors.RequestNotFound);
            if (request.Status != RequestStatus.Pending)
                return Result.Failure(UserErrors.CannotRejectRequest);
            request.Status = RequestStatus.Rejected;
            await _context.SaveChangesAsync(cancellationToken);
            await _emailSender.SendEmailAsync(request.User.Email!, "✅ Career Path: MembershipUpgrade", RejectionEmailBody(request));
            return Result.Success();
        }

        private string ApprovalEmailBody(MembershipUpgrade request)
        {
            return $@"
        <h2>Membership Upgrade Approved 🎉</h2>
        <p>Dear {request.User.FirstName},</p>

        <p>We are pleased to inform you that your request to upgrade your membership 
        has been <strong>approved</strong>.</p>

        <p>You can now access company features and start posting jobs.</p>

        <p>Thank you for being part of Career Path.</p>
         ";
        }

        private string RejectionEmailBody(MembershipUpgrade request)
        {
            return $@"
        <h2>Membership Upgrade Update</h2>
        <p>Dear {request.User.FirstName},</p>

        <p>We regret to inform you that your membership upgrade request has been 
        <strong>rejected</strong>.</p>

        <p>If you believe this was a mistake, please contact support.</p>

        <p>Thank you for your understanding.</p>
         ";
        }
    }
}
