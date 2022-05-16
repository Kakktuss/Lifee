using System.Linq.Expressions;
using Lifee.RSS.Application.Models.User;
using Microsoft.EntityFrameworkCore;

namespace Lifee.RSS.Application.DataAccess.Repositories;

public class UserRepository : IUserRepository
{
	private readonly LifeeDbContext _context;

	private readonly DbSet<User> _users;
    
	public UserRepository(LifeeDbContext context)
	{
		_context = context;
        
		_users = context.Set<User>();
	}

	public Task<User?> GetUserAsync(Expression<Func<User, bool>> where, Func<IQueryable<User>, IQueryable<User>>? func = null)
	{
		if(_disposed)
			throw new ObjectDisposedException(nameof(UserRepository));
		
		if (func is not null)
			return func(_users).FirstOrDefaultAsync(where);

		return _users.FirstOrDefaultAsync(where);
	}
	
	private bool _disposed = false;

	protected void Dispose(bool disposing)
	{
		if (!_disposed && disposing)
		{
			_context.Dispose();
		}

		_disposed = true;
	}
    
	public void Dispose()
	{
		Dispose(true);
		GC.SuppressFinalize(this);
	}
}