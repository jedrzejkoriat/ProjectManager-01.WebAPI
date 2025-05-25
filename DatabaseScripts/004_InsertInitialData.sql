USE ProjectManagerDB;
GO

INSERT INTO Permissions (Id, Name) VALUES
('f47ac10b-58cc-4372-a567-0e02b2c3d479', 'Comment'),
('f47ac10b-58cc-4372-a567-0e02b2c3d480', 'AddProjectMember'),
('f47ac10b-58cc-4372-a567-0e02b2c3d481', 'EditProjectMember'),
('f47ac10b-58cc-4372-a567-0e02b2c3d482', 'RemoveProjectMember'),
('f47ac10b-58cc-4372-a567-0e02b2c3d483', 'AddTag'),
('f47ac10b-58cc-4372-a567-0e02b2c3d484', 'EditTag'),
('f47ac10b-58cc-4372-a567-0e02b2c3d485', 'RemoveTag'),
('f47ac10b-58cc-4372-a567-0e02b2c3d486', 'AddTicket'),
('f47ac10b-58cc-4372-a567-0e02b2c3d487', 'EditTicket'),
('f47ac10b-58cc-4372-a567-0e02b2c3d488', 'RemoveTicket'),
('f47ac10b-58cc-4372-a567-0e02b2c3d489', 'AddTicketRelation'),
('f47ac10b-58cc-4372-a567-0e02b2c3d48a', 'EditTicketRelation'),
('f47ac10b-58cc-4372-a567-0e02b2c3d48b', 'RemoveTicketRelation');

INSERT INTO Priorities (Id, Name, Level) VALUES
('a0f5c90e-c8d3-4a9b-b7a5-77c9bde1b123', 'Lowest', 0),
('a0f5c90e-c8d3-4a9b-b7a5-77c9bde1b124', 'Low', 1),
('a0f5c90e-c8d3-4a9b-b7a5-77c9bde1b125', 'Medium', 2),
('a0f5c90e-c8d3-4a9b-b7a5-77c9bde1b126', 'High', 3),
('a0f5c90e-c8d3-4a9b-b7a5-77c9bde1b127', 'Highest', 4);

INSERT INTO Roles (Id, Name) VALUES
('d4f56a29-3e5b-4c2f-9a27-4f2e7c8a8901', 'Admin'),
('d4f56a29-3e5b-4c2f-9a27-4f2e7c8a8902', 'User');
