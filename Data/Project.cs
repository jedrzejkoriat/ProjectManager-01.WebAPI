namespace ProjectManager_01.WebAPI.Data
{
    internal sealed class Project
    {
        // ID
        public int Id { get; set; }
        // STRING
        public string Name { get; init; }
        // DATE
        public DateTime CreatedAt { get; init; }
    }
}
