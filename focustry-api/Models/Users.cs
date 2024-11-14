namespace focustry_api.Models
{
    public class Users
    {
        public int id { get; set; }
        public int role_id { get; set; }
        public string? firstname { get; set; }
        public string? lastname { get; set; }
        public string? username { get; set; }
        public string? password { get; set; }
    }
}
