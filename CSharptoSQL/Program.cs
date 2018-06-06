using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharptoSQL {
	class Program {

		static List<User> users = new List<User>();

		void Run() {
			User user = new User();
			user.Id = 6;
			user.Username = "YYY";
			user.Password = "PAss";
			user.FirstName = "USer";
			user.LastName = "Name";
			user.Phone = "nani";
			user.Email = "nano";
			user.IsReviewer = true;
			user.IsAdmin = true;
			Update(user);
		}

		static void Main(string[] args) {
			(new Program()).Run();
		}

		void Update(User user) {

			string connstr = @"server=STUDENT12\SQLEXPRESS;database=PRSDatabase;Trusted_connection=true";
			SqlConnection conn = new SqlConnection(connstr);
			conn.Open();
			if (conn.State != ConnectionState.Open) {
				throw new ApplicationException("Connection did not open properly");
			}
			string sql = "update [user] "
				+ "set Username = @Username, "
				+ "Password = @Password, "
				+ "Firstname = @Firstname, "
				+ "Lastname = @Lastname, "
				+ "Phone = @Phone, "
				+ "Email = @Email, "
				+ "IsReviewer = @IsReviewer, "
				+ "IsAdmin = @IsAdmin "
				+ "where Id = @Id ";

			SqlCommand cmd = new SqlCommand(sql, conn);
			cmd.Parameters.Add(new SqlParameter("@Id", user.Id));
			cmd.Parameters.Add(new SqlParameter("@Username", user.Username));
			cmd.Parameters.Add(new SqlParameter("@Password", user.Password));
			cmd.Parameters.Add(new SqlParameter("@Firstname", user.FirstName));
			cmd.Parameters.Add(new SqlParameter("@Lastname", user.LastName));
			cmd.Parameters.Add(new SqlParameter("@Phone", user.Phone));
			cmd.Parameters.Add(new SqlParameter("@Email", user.Email));
			cmd.Parameters.Add(new SqlParameter("@IsReviewer", user.IsReviewer));
			cmd.Parameters.Add(new SqlParameter("@IsAdmin", user.IsAdmin));
			int RecsAffected = cmd.ExecuteNonQuery();
			if (RecsAffected != 1) {
				Debug.WriteLine("Records not updated");
			}

			conn.Close();
		}

		void Insert(User user) {

			string connstr = @"server=STUDENT12\SQLEXPRESS;database=PRSDatabase;Trusted_connection=true";
			SqlConnection conn = new SqlConnection(connstr);
			conn.Open();
			if (conn.State != ConnectionState.Open) {
				throw new ApplicationException("Connection did not open properly");
			}
			string sql = "insert into [user] (Username, Password, Firstname, Lastname, Phone, Email, IsReviewer, IsAdmin)" 
				+ "values (@Username, @Password, @Firstname, @Lastname, @Phone, @Email, @IsReviewer, @IsAdmin)";
			SqlCommand cmd = new SqlCommand(sql, conn);
			cmd.Parameters.Add(new SqlParameter("@Username", user.Username));
			cmd.Parameters.Add(new SqlParameter("@Password", user.Password));
			cmd.Parameters.Add(new SqlParameter("@Firstname", user.FirstName));
			cmd.Parameters.Add(new SqlParameter("@Lastname", user.LastName));
			cmd.Parameters.Add(new SqlParameter("@Phone", user.Phone));
			cmd.Parameters.Add(new SqlParameter("@Email", user.Email));
			cmd.Parameters.Add(new SqlParameter("@IsReviewer", user.IsReviewer));
			cmd.Parameters.Add(new SqlParameter("@IsAdmin", user.IsAdmin));
			int RecsAffected = cmd.ExecuteNonQuery();
			if(RecsAffected !=1) {
				Debug.WriteLine("Records not updated");
			}

			conn.Close();
		}

		void Select() {

			string connstr = @"server=STUDENT12\SQLEXPRESS;database=PRSDatabase;Trusted_connection=true";
			SqlConnection conn = new SqlConnection(connstr);
			conn.Open();
			if(conn.State != ConnectionState.Open) {
				throw new ApplicationException("Connection did not open properly");
			}
			string sql = "select * from [user]";
			SqlCommand cmd = new SqlCommand(sql, conn);
			SqlDataReader reader = cmd.ExecuteReader();
			while (reader.Read()) {
				int id = reader.GetInt32(reader.GetOrdinal("Id"));
				string username = reader.GetString(reader.GetOrdinal("Username"));
				string password = reader.GetString(reader.GetOrdinal("Password"));
				string firstname = reader.GetString(reader.GetOrdinal("Firstname"));
				string lastname = reader.GetString(reader.GetOrdinal("Lastname"));
				string phone = reader.GetString(reader.GetOrdinal("Phone"));
				string email = reader.GetString(reader.GetOrdinal("Email"));
				bool isreviewer = reader.GetBoolean(reader.GetOrdinal("IsReviewer"));
				bool isadmin = reader.GetBoolean(reader.GetOrdinal("IsAdmin"));
				bool active = reader.GetBoolean(reader.GetOrdinal("Active"));
				System.Diagnostics.Debug.WriteLine($"{id}, {username}, {password}, {firstname}, {lastname}, {phone}, {email}, {isreviewer}, {isadmin}, {active}");

				User user = new User();
				user.Id = id;
				user.Username = username;
				user.Password = password;
				user.FirstName = firstname;
				user.LastName = lastname;
				user.Phone = phone;
				user.Email = email;
				user.IsReviewer = isreviewer;
				user.IsAdmin = isadmin;
				user.Active = active;

				users.Add(user);

				
			}









			conn.Close();
		}
	}
}
