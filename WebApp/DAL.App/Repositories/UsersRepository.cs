using DAL.Base;
using DAL.Repositories;
using PublicApi.DTO.v1;
using System;
using Entity = Domain.App.User;
using Dto = PublicApi.DTO.v1.User;
using Microsoft.EntityFrameworkCore;
using Mapping;
using DAL.App.EF;
using Microsoft.AspNetCore.Identity;

namespace DAL.App.Repositories
{
    public class UsersRepository : BaseRepository<int, Entity, Dto>, IUsersRepository
    {
        private readonly UserManager<Entity> _userManager;
        public UsersRepository(AppDbContext dbContext, IMapper<Entity, Dto> mapper, UserManager<Entity> userManager) : base(dbContext, mapper)
        {
            _userManager = userManager;
        }

        public async Task DeleteAsync(int id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            var logins = await _userManager.GetLoginsAsync(user);
            var rolesForUser = await _userManager.GetRolesAsync(user);

            using var transaction = DbContext.Database.BeginTransaction();
            IdentityResult result = IdentityResult.Success;
            foreach (var login in logins)
            {
                result = await _userManager.RemoveLoginAsync(user, login.LoginProvider, login.ProviderKey);
                if (result != IdentityResult.Success) break;
            }

            if (result == IdentityResult.Success)
            {
                foreach (var item in rolesForUser)
                {
                    result = await _userManager.RemoveFromRoleAsync(user, item);
                    if (result != IdentityResult.Success) break;
                }
            }

            if (result == IdentityResult.Success)
            {
                result = await _userManager.DeleteAsync(user);
                if (result == IdentityResult.Success) transaction.Commit();
            }
        }

        public async Task<IEnumerable<Dto>> GetAllBySearchPhraseAsync(string phrace, int userId)
        {
            return await DbContext.Set<Entity>()
                .Where(user => !user.Id.Equals(userId))
                .Where(user => user.UserName.Contains(phrace))
                .Include(user => user.FriendshipsWithUser)
                .Where(user => !user.FriendshipsWithUser!.Any(friendship => friendship.UserId.Equals(userId)))
                .Select(user => Mapper.Map(user))
                .ToListAsync();
        }

        public async Task<Dto?> GetOneDetailedAsync(int id)
        {
            DbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            var dto = await DbContext.Set<Entity>()
                .Include(user => user.PlayedGames)
                .Include(user => user.PersonalFriendships)
                .Include(user => user.FriendshipsWithUser)
                .FirstOrDefaultAsync(user => user.Id.Equals(id));
            DbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.TrackAll;
            if (dto != null) return Mapper.Map(dto); 
            return null;
        }
    }
}
