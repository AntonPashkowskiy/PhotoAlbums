using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
	public interface IUserRepository
	{
		bool AddUser(User user);
		bool UpdateUser(User user);
		bool RemoveUser(int userId);
		bool RestoreUser(string userLogin);
		
		// Work with roles of users
		bool AddRole(Role role);
		bool AddRoleToUser(int userId, Role role);
		bool RemoveRoleFromUser(int userId, Role role);
		
		// Work with phones of users
		bool AddPhoneToUser(int userId, Phone phone);
		bool UpdatePhoneOfUser(int userId, Phone phone);
		
		// Get-methods by different params
		User GetUser(int userId);
		User GetUser(string login);
		IEnumerable<User> GetUsers(Role role);
		IEnumerable<User> GetUsers(string firstName, string lastName);
		IEnumerable<User> GetUsers(string job);
	}
}
