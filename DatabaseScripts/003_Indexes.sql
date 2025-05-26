USE ProjectManagerDB;

CREATE INDEX IX_Comments_TicketId ON Comments(TicketId);
CREATE INDEX IX_ProjectRoles_ProjectId ON ProjectRoles(ProjectId);
CREATE INDEX IX_ProjectUserRoles_ProjectId ON ProjectUserRoles(ProjectId);
CREATE INDEX IX_ProjectUserRoles_ProjectRoleId ON ProjectUserRoles(ProjectRoleId);
CREATE INDEX IX_ProjectUserRoles_UserId ON ProjectUserRoles(UserId);
CREATE INDEX IX_Tags_ProjectId ON Tags(ProjectId);
CREATE INDEX IX_Tickets_ProjectId ON Tickets(ProjectId);
CREATE INDEX IX_Tickets_PriorityId ON Tickets(PriorityId);
CREATE INDEX IX_Tickets_AssigneeId ON Tickets(AssigneeId);
CREATE INDEX IX_Tickets_ReporterId ON Tickets(ReporterId);
CREATE INDEX IX_Tickets_Status ON Tickets(Status);
CREATE INDEX IX_Tickets_Resolution ON Tickets(Resolution);
CREATE INDEX IX_Tickets_TicketType ON Tickets(TicketType);
CREATE INDEX IX_TicketRelations_SourceId ON TicketRelations(SourceId);
CREATE INDEX IX_TicketRelations_TargetId ON TicketRelations(TargetId);
CREATE INDEX IX_TicketTags_TagId ON TicketTags(TagId);
CREATE INDEX IX_TicketTags_TicketId ON TicketTags(TicketId);

CREATE UNIQUE INDEX IX_Priorities_Name ON Priorities(Name);
CREATE UNIQUE INDEX IX_Projects_Name ON Projects(Name);
CREATE UNIQUE INDEX IX_Projects_Key ON Projects([Key]);
CREATE UNIQUE INDEX IX_Users_UserName ON Users(UserName);
CREATE UNIQUE INDEX IX_Users_Email ON Users(Email);
CREATE UNIQUE INDEX IX_UserRoles_UserId ON UserRoles(UserId);
CREATE UNIQUE INDEX IX_Roles_Name ON Roles(Name);
CREATE UNIQUE INDEX IX_Permissions_Name ON Permissions(Name);

CREATE UNIQUE INDEX IX_ProjectRoles_ProjectId_Name ON ProjectRoles(ProjectId, Name);
CREATE UNIQUE INDEX IX_ProjectUserRoles_ProjectId_ProjectRoleId_UserId ON ProjectUserRoles(ProjectId, ProjectRoleId, UserId);
CREATE UNIQUE INDEX IX_Tags_ProjectId_Name ON Tags(ProjectId, Name);
CREATE UNIQUE INDEX IX_Tickets_ProjectId_TicketNumber ON Tickets(ProjectId, TicketNumber);
CREATE UNIQUE INDEX IX_TicketRelations_SourceId_TargetId_RelationType ON TicketRelations(SourceId, TargetId, RelationType);
CREATE UNIQUE INDEX IX_TicketTags_TagId_TicketId ON TicketTags(TagId, TicketId);
