namespace ProjectManager_01.WebAPI.Data
{
    internal sealed class ProjectMember
    {
        // ID
        public int Id { get; set; }
        public int UserId { get; init; }
        public int ProjectId { get; init; }
        public int RoleId { get; init; }
        // DATE
        public DateTime JoinDate { get; init; }
    }
}
