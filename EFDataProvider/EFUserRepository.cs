using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace EFDataProvider
{
	/// <summary>
	/// This class represent repository of users and provide
	/// all necessary operations with Entity Framework.
	/// Any exception handling in potentially hazardous operations.
	/// </summary>
	class EFUserRepository : IUserRepository
	{
		private EFUserRepository() { }
		
		private readonly PhotoAlbumsContext _dbContext = null;

		public EFUserRepository(string connectionString)
		{
			if (String.IsNullOrEmpty(connectionString))
			{
				throw new ArgumentNullException("Connection string can't be null.");
			}
			_dbContext = new PhotoAlbumsContext(connectionString);
		}

		public bool AddPhoneToUser(int userId, Phone phone)
		{
			try
			{
				User user = _dbContext.Users.Find(userId);

				if (user != null && user.Phone == null)
				{
					user.Phone = phone;
					_dbContext.Entry(user).State = EntityState.Modified;
					_dbContext.SaveChanges();

					return true;
				}
				return false;
			}
			catch (Exception e)
			{
				throw new Exception("Method of phone adding is failed.", e);
			}
		}

		public bool AddRole(Role role)
		{
			try
			{
				_dbContext.Roles.Add(role);
				_dbContext.SaveChanges();

				return true;
			}
			catch (Exception e)
			{
				throw new Exception("Method of role adding is failed.", e);
			}
		}

		public bool AddRoleToUser(int userId, Role role)
		{
			try
			{
				User user = _dbContext.Users.Find(userId);

				if (user != null)
				{
					user.UserRoles.Add(role);
					_dbContext.Entry(user).State = EntityState.Modified;
					_dbContext.SaveChanges();

					return true;
				}
				return false;
			}
			catch (Exception e)
			{
				throw new Exception("Method of adding role to user is failed.", e);
			}
		}

		public bool AddUser(User user)
		{
			try
			{
				_dbContext.Users.Add(user);
				_dbContext.SaveChanges();

				return true;
			}
			catch (Exception e)
			{
				throw new Exception("Method of user adding is failed.", e);
			}
		}

		public User GetUser(string login)
		{
			return _dbContext.Users.Where(u => u.Login == login).FirstOrDefault();
		}

		public User GetUser(int userId)
		{
			return _dbContext.Users.Find(userId);
		}

		public IEnumerable<User> GetUsers(string job)
		{
			return _dbContext.Users.Where(u => u.Job.Contains(job));
		}

		public IEnumerable<User> GetUsers(string firstName, string lastName)
		{
			return _dbContext.Users.Where(u => 
				String.Equals(u.FirstName, firstName, StringComparison.OrdinalIgnoreCase) &&
				String.Equals(u.LastName, lastName, StringComparison.OrdinalIgnoreCase)
			);
		}

		public IEnumerable<User> GetUsers(Role role)
		{
			Role result = _dbContext.Roles.Where(r => r.Name == role.Name).FirstOrDefault();

			return result == null ? null : result.UsersInRole;
		}

		public bool RemoveRoleFromUser(int userId, Role role)
		{
			User user = _dbContext.Users.Find(userId);

			if (user != null && user.UserRoles != null)
			{
				Role roleFound = user.UserRoles.Where(r => r.Name == role.Name).FirstOrDefault();
						
				if (roleFound != null)
				{
					user.UserRoles.Remove(role);
					_dbContext.Entry(user).State = EntityState.Modified;
					_dbContext.SaveChanges();

					return true;
				}
			}
			return false;
		}

		public bool RemoveUser(int userId)
		{
			User user = _dbContext.Users.Find(userId);

			if (user != null)
			{
				user.IsRemoved = true;
				_dbContext.Entry(user).State = EntityState.Modified;
				_dbContext.SaveChanges();

				return true;
			}
			return true;
		}

		public bool RestoreUser(string userLogin)
		{
			User user = _dbContext.Users.Where(u => u.Login == userLogin).FirstOrDefault();

			if (user != null)
			{
				user.IsRemoved = false;
				_dbContext.Entry(user).State = EntityState.Modified;
				_dbContext.SaveChanges();

				return true;
			}
			return false;
		}

		public bool UpdatePhoneOfUser(int userId, Phone phone)
		{
			try
			{
				User user = _dbContext.Users.Find(userId);

				if (user != null && user.Phone != null)
				{
					user.Phone.PhoneNumber = phone.PhoneNumber ?? user.Phone.PhoneNumber;
					user.Phone.IsHidden = phone.IsHidden;
					_dbContext.Entry(user).State = EntityState.Modified;
					_dbContext.SaveChanges();

					return true;
				}
				return false;
			}
			catch (Exception e)
			{
				throw new Exception("Method of user phone updating is failed.", e);
			}
		}

		public bool UpdateUser(User user)
		{
			User result = _dbContext.Users.Find(user.Id);

			if (user != null)
			{
				result.PhotoUrl = user.PhotoUrl ?? result.PhotoUrl;
				result.SmallPhotoUrl = user.SmallPhotoUrl ?? result.SmallPhotoUrl;
				result.Password = user.Password ?? result.Password;
				result.Email = user.Email ?? result.Email;
				_dbContext.Entry(result).State = EntityState.Modified;
				_dbContext.SaveChanges();

				return true;
			}
			return false;
		}

		public void Dispose()
		{
			_dbContext.Dispose();
		}
	}
}
