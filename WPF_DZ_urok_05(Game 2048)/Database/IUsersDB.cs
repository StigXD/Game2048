using System.Collections.Generic;
using Game_2048.Entities;
using Game_2048.Models;

namespace Game_2048.Database;

public interface IUsersDB
{
	List<UserModel> Users { get; }

	void Add(UserModel user);
	void Read();
	void Write();
}