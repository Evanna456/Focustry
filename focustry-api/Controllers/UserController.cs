using Dapper;
using focustry_api.Models;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System.Web;
using BC = BCrypt.Net.BCrypt;

namespace focustry_api.Controllers
{
    [ApiController]
    [Controller]
    public class UserController : Controller
    {
        [Route("v1/user/register")]
        [HttpPost]
        public async Task<IActionResult> register(Users user)
        {
            string firstname = HttpUtility.HtmlAttributeEncode(user.firstname)!;
            string lastname = HttpUtility.HtmlAttributeEncode(user.lastname)!;
            string username = HttpUtility.HtmlAttributeEncode(user.username)!;
            string password = HttpUtility.HtmlAttributeEncode(user.password)!;
            string hashed_password = BC.HashPassword(password);

            var connectionString = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["Default"];
            using (var connection = new MySqlConnection(connectionString))
            {
                var user_v = new { Firstname = firstname, Lastname = lastname, Username = username, Password = hashed_password };
                var sql = "INSERT INTO users (id, role_id, firstname, lastname, username, password, remember_token) VALUES ('', 1,@Firstname, @Lastname, @Username, @Password, '')";
                await connection.QueryAsync<Users>(sql, user_v);
            }
            return StatusCode(200);
        }
        [Route("v1/user/login")]
        [HttpPost]
        public async Task<IActionResult> login(Users user)
        {
            string username = HttpUtility.HtmlAttributeEncode(user.username)!;
            string password = HttpUtility.HtmlAttributeEncode(user.password)!;

            var connectionString = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["Default"];
            using (var connection = new MySqlConnection(connectionString))
            {
                var user_v = new { Username = username, Password = password };
                var sql = "SELECT password FROM users WHERE username = @Username";
                var data = connection.ExecuteScalarAsync(sql, user_v);

                bool result = BC.Verify(password, data.Result?.ToString());
                if (result == true)
                {
                    var token = Guid.NewGuid().ToString();
                    var session_v = new { Username = username, Token = token };
                    sql = "INSERT INTO sessions (username, token) VALUES (@Username, @Token)";
                    await connection.QueryAsync<Sessions>(sql, session_v);
                    return StatusCode(200);
                }
                else
                {
                    return StatusCode(500);
                }

            }

        }
    }
}
