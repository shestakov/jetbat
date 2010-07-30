using System;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using JetBat.Client.Entities;
using JetBat.Client.Metadata.Abstract;

namespace JetBat.Test
{
	public static class MembershipTestHelper
	{
		const string ApplicationName = "Swiftshot";

		public static void CreateApplication(SqlConnection sqlConnection)
		{
			const string command = @"INSERT INTO aspnet_Applications (ApplicationName, LoweredApplicationName, ApplicationId) VALUES (@ApplicationName, @LoweredApplicationName, @ApplicationId)";
			Guid applicationId = new Guid("48264B02-CD79-42ce-81DE-DF69FDE369EA");
			SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
			sqlCommand.Parameters.AddWithValue("@ApplicationName", ApplicationName);
			sqlCommand.Parameters.AddWithValue("@LoweredApplicationName", ApplicationName);
			sqlCommand.Parameters.AddWithValue("@ApplicationId", applicationId);
			sqlCommand.ExecuteNonQuery();
		}

		public static void CreateDefaultUsersAndRoles(IAccessAdapter sqlAccessAdapter, SqlConnection sqlConnection)
		{
			Console.WriteLine("Creating default roles and users...");
			
			int roleId = CreateRole(sqlAccessAdapter, "Admin");
			CreateUser(sqlAccessAdapter, "Admin", "theadminpass", "Администратор", roleId);
			addRolePermissions(sqlConnection, roleId, AddRolePermissionSqlForAdmin);

			roleId = CreateRole(sqlAccessAdapter, "Viewer");
			CreateUser(sqlAccessAdapter, "viewer", "viewer", "Наблюдатель", roleId);
			addRolePermissions(sqlConnection, roleId, AddRolePermissionSqlForViewer);

			roleId = CreateRole(sqlAccessAdapter, "ReferencedDataEditor");
			CreateUser(sqlAccessAdapter, "editor", "rdepss", "Редактор НСИ", roleId);
			addRolePermissions(sqlConnection, roleId, AddRolePermissionSqlForReferencedDataEditor);

			roleId = CreateRole(sqlAccessAdapter, "DocumentEditor");
			CreateUser(sqlAccessAdapter, "DocumentEditor", "docedpass", "Редактор документов", roleId);
			addRolePermissions(sqlConnection, roleId, AddRolePermissionSqlForDocumentEditor);
			
			Console.WriteLine("Default roles and users created.");
		}

		private static int CreateRole(IAccessAdapter sqlAccessAdapter, string roleName)
		{
			PlainObjectInstance role = (PlainObjectInstance)sqlAccessAdapter.ObjectFactory.New<PlainObjectDefinition>("АДМ", "Роль");
			role["Имя"] = roleName;
			var result = role.Insert();
			if (result.Count > 0)
				throw new Exception(result[0].Text);
			return (int)role["ID"];
		}

		private static void CreateUser(IAccessAdapter sqlAccessAdapter, string userName, string password, string userFullName, int roleId)
		{
			string passwordSalt = GenerateSalt();
			string userPassword = HashPassword(password, passwordSalt);
			PlainObjectInstance user = (PlainObjectInstance)sqlAccessAdapter.ObjectFactory.New<PlainObjectDefinition>("АДМ", "Пользователь");
			user["UserName"] = userName;
			user["PasswordSalt"] = passwordSalt;
			user["Password"] = userPassword;
			user["ФИО"] = userFullName;
			var result = user.Insert();
			if (result.Count > 0)
				throw new Exception(result[0].Text);
			int userId = (int)user["ID"];

			PlainObjectInstance userRole = (PlainObjectInstance)sqlAccessAdapter.ObjectFactory.New<PlainObjectDefinition>("АДМ", "РольПользователя");
			userRole["IDРоли"] = roleId;
			userRole["IDПользователя"] = userId;
			result = userRole.Insert();
			if (result.Count > 0)
				throw new Exception(result[0].Text);
			return;
		}

		private static void addRolePermissions(SqlConnection connection, int roleID, string fillRoleSql)
		{
			SqlCommand command = new SqlCommand(fillRoleSql, connection);
			command.Parameters.AddWithValue("@RoleID", roleID);
			command.ExecuteNonQuery();
		}

		#region Password hashing

		private static string GenerateSalt()
		{
			byte[] buffer = new byte[16];
			(new RNGCryptoServiceProvider()).GetBytes(buffer);
			return Convert.ToBase64String(buffer);
		}

		private static string HashPassword(string password, string salt)
		{
			byte[] passwordBinary = Encoding.Unicode.GetBytes(password);
			byte[] saltBinary = Convert.FromBase64String(salt);
			byte[] passwordSaltCombined = new byte[saltBinary.Length + passwordBinary.Length];
			Buffer.BlockCopy(saltBinary, 0, passwordSaltCombined, 0, saltBinary.Length);
			Buffer.BlockCopy(passwordBinary, 0, passwordSaltCombined, saltBinary.Length, passwordBinary.Length);
			SHA1Managed sha1Managed = new SHA1Managed();
			return Convert.ToBase64String(sha1Managed.ComputeHash(passwordSaltCombined));
		}

		#endregion

		#region Sql scripts

		private const string AddRolePermissionSqlForAdmin =
			@"
INSERT АДМ_РазрешениеРоли(IDРоли, ИмяРоли, ПространствоИмен, ИмяБО, ИмяМетода)
SELECT 
	@RoleID,
	(SELECT Имя FROM АДМ_Роль WHERE ID = @RoleID),
	Metadata_Namespace.Name,
	Metadata_Object.Name,
	Metadata_ObjectMethod.Name
FROM Metadata_Object
	INNER JOIN Metadata_Namespace ON Metadata_Object.NamespaceID = Metadata_Namespace.ID
	INNER JOIN Metadata_ObjectMethod ON Metadata_ObjectMethod.ObjectID = Metadata_Object.ID
			";

		private const string AddRolePermissionSqlForViewer =
			@"
INSERT АДМ_РазрешениеРоли(IDРоли, ИмяРоли, ПространствоИмен, ИмяБО, ИмяМетода)
SELECT 
	@RoleID,
	(SELECT Имя FROM АДМ_Роль WHERE ID = @RoleID),
	Metadata_Namespace.Name,
	Metadata_Object.Name,
	Metadata_ObjectMethod.Name
FROM Metadata_Object
	INNER JOIN Metadata_Namespace ON Metadata_Object.NamespaceID = Metadata_Namespace.ID
	INNER JOIN Metadata_ObjectMethod ON Metadata_ObjectMethod.ObjectID = Metadata_Object.ID
WHERE Metadata_ObjectMethod.Name IN ('Load', 'LoadList')
			";

		private const string AddRolePermissionSqlForReferencedDataEditor =
			@"
INSERT АДМ_РазрешениеРоли(IDРоли, ИмяРоли, ПространствоИмен, ИмяБО, ИмяМетода)
SELECT 
	@RoleID,
	(SELECT Имя FROM АДМ_Роль WHERE ID = @RoleID),
	Metadata_Namespace.Name,
	Metadata_Object.Name,
	Metadata_ObjectMethod.Name
FROM Metadata_Object
	INNER JOIN Metadata_Namespace ON Metadata_Object.NamespaceID = Metadata_Namespace.ID
	INNER JOIN Metadata_ObjectMethod ON Metadata_ObjectMethod.ObjectID = Metadata_Object.ID
WHERE Metadata_Namespace.Name = 'НСИ' AND Metadata_ObjectMethod.Name IN ('Insert', 'Update', 'Delete','Restore', 'Load', 'LoadList')
			";

		private const string AddRolePermissionSqlForDocumentEditor =
			@"
INSERT АДМ_РазрешениеРоли(IDРоли, ИмяРоли, ПространствоИмен, ИмяБО, ИмяМетода)
SELECT 
	@RoleID,
	(SELECT Имя FROM АДМ_Роль WHERE ID = @RoleID),
	Metadata_Namespace.Name,
	Metadata_Object.Name,
	Metadata_ObjectMethod.Name
FROM Metadata_Object
	INNER JOIN Metadata_Namespace ON Metadata_Object.NamespaceID = Metadata_Namespace.ID
	INNER JOIN Metadata_ObjectMethod ON Metadata_ObjectMethod.ObjectID = Metadata_Object.ID
WHERE Metadata_Namespace.Name = 'ДО' AND Metadata_ObjectMethod.Name IN ('Update', 'Insert')
			";

		#endregion
	}
}