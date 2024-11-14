using System;
using System.Collections.Generic;
using System.IO;
using Game_2048.Helpers;
using Game_2048.Models;

namespace Game_2048.Database;

public class UsersDB : IUsersDB
{
	public List<UserModel> Users { get; private set; }

	public void Add(UserModel user)
	{
		Users.Add(user);
	}

	public void Read()
	{
		try
		{
			Users = XmlSerializerHelper.Deserializing<List<UserModel>>(File.ReadAllText(Constants.UsersFileName));
		}
		catch (Exception e)
		{
			Users = [new UserModel()];
		}
	}

	public void Write()
	{
		File.WriteAllText(Constants.UsersFileName, XmlSerializerHelper.Serializing(Users));
	}
}