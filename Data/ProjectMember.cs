namespace ProjectManager_01.WebAPI.Data
{
    public sealed class ProjectMember
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ProjectId { get; set; }
        public int RoleId { get; set; }
        public DateTime JoinDate { get; set; }
    }
}
