using System.Linq.Expressions;
using Lifee.RSS.Application.Models.User;

namespace Lifee.RSS.Application.DataAccess.Repositories;

public interface IUserRepository : IDisposable
{
	public Task<User?> GetUserAsync(Expression<Func<User, bool>> where,
		Func<IQueryable<User>, IQueryable<User>>? func = null);
}