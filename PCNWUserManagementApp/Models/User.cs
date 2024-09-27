namespace PCNWUserManagementApp.Models
{
	public class User
	{
        public int Userid { get; set; }
		public string Name { get; set; }
		public string UserName { get; set; }
		public string Password { get; set; }
		public string Email { get; set; }
		public string ContactNo { get; set; }
		public string Address { get; set; }
		public string Gender { get; set; }
		public string ImagePath { get; set; }
		
        public string  ProjectName { get; set; }
		public string projectFile { get; set; }
		public string UserType { get; set; }
		public string IsActive { get; set; }
		public DateTime LastUpdated { get; set; }


    }
}
