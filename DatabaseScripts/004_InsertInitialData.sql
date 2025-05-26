USE ProjectManagerDB;

INSERT INTO Permissions (Id, Name) VALUES
('d2f6a7ce-15e3-45df-9a2a-7b1eec0042a3', 'Comment'),
('e5cc3f04-a0cf-4d95-ae7c-9d3b76247be6', 'AddProjectMember'),
('f4a25e30-6d9e-4e3a-8a4c-0b1de8dfad5a', 'EditProjectMember'),
('a8b3e297-bfb2-464f-8fa6-ffba0bcaa2b4', 'RemoveProjectMember'),
('39f59bc3-2be4-4e45-bfb0-c80f4cb1bfe3', 'AddTag'),
('1d1b1a99-f875-47fa-a6dc-786db14ed4da', 'EditTag'),
('a9d231b0-6f35-46c3-b8d5-15a576a31371', 'RemoveTag'),
('cc50c1e2-3b89-4aa9-b139-83d5024579f1', 'AddTicket'),
('f35149b0-b84c-4d80-bc7b-bf8ef71a3aef', 'EditTicket'),
('2c2ef38a-b3aa-4d1c-b23f-5c89e432ac1f', 'RemoveTicket'),
('7038f5cd-faf3-4ed5-ae2c-378b0a3940ec', 'AddTicketRelation'),
('fdc35c17-bf02-47a1-a046-2a913de7d70a', 'EditTicketRelation'),
('23d3449a-dcd0-4aeb-9331-9828475ab5b2', 'RemoveTicketRelation');

INSERT INTO Priorities (Id, Name, Level) VALUES
('b7d8f22e-bc89-456a-9f90-48c211f60a3e', 'Lowest', 0),
('8b9d2e8a-bb27-40be-a12f-f63d91aa2aef', 'Low', 1),
('d2fc7c7c-c509-4d50-93d4-7dfbf262e06f', 'Medium', 2),
('c5d8d5e2-d5fd-4e67-ae56-d77b1efcf174', 'High', 3),
('69cf2f1a-ea70-4ce0-802d-fc9a498d2d5a', 'Highest', 4);

INSERT INTO Roles (Id, Name) VALUES
('bdc9fd77-60f0-4c4a-97c2-2647e11831b7', 'Admin'),
('48cc34f2-5d53-4e9b-83a8-457e5ec80c7a', 'User');
