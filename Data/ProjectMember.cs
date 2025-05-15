namespace ProjectManager_01.WebAPI.Data
{
    public sealed class ProjectMember
    {
        // ID
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ProjectId { get; set; }
        public int RoleId { get; set; }
        // DATE
        public DateTime JoinDate { get; set; }
    }
}
