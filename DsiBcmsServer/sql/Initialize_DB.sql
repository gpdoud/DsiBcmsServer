insert into Configs (KeyValue, DataValue, System, Active, Created)
	values ('name', 'Boot Camp Management System (BCMS)', 1, 1, GetDate());
insert into Configs (KeyValue, DataValue, System, Active, Created)
	values ('version', '0.0', 1, 1, GetDate());
insert into Configs (KeyValue, DataValue, System, Active, Created)
	values ('status', 'Development', 1, 1, GetDate());
insert into Configs (KeyValue, DataValue, System, Active, Created)
	values ('author', 'Doud Systems, Inc.', 1, 1, GetDate());
insert into Configs (KeyValue, DataValue, System, Active, Created)
	values ('copyright', 'Copyright (c) 2020', 1, 1, GetDate());
insert into Configs (KeyValue, DataValue, System, Active, Created)
	values ('rights', 'All rights reserved.', 1, 1, GetDate());
select * from Configs;

insert into Roles (Code, Name, IsAdmin, IsStaff, IsInstructor, IsStudent, Active, Created) 
	values ('AALL', 'Super Admin', 1, 1, 1, 1, 1, GetDate());
insert into Roles (Code, Name, IsAdmin, IsStaff, IsInstructor, IsStudent, Active, Created) 
	values ('ADMSTF', 'Admin/Staff', 1, 1, 0, 0, 1, GetDate());
insert into Roles (Code, Name, IsAdmin, IsStaff, IsInstructor, IsStudent, Active, Created) 
	values ('ADMINS', 'Admin/Instructor', 1, 0, 1, 0, 1, GetDate());
insert into Roles (Code, Name, IsAdmin, IsStaff, IsInstructor, IsStudent, Active, Created) 
	values ('STFINS', 'Staff/Instructor', 0, 1, 1, 0, 1, GetDate());
insert into Roles (Code, Name, IsAdmin, IsStaff, IsInstructor, IsStudent, Active, Created) 
	values ('ADM', 'Admin', 1, 0, 0, 0, 1, GetDate());
insert into Roles (Code, Name, IsAdmin, IsStaff, IsInstructor, IsStudent, Active, Created) 
	values ('STF', 'Staff', 0, 1, 0, 0, 1, GetDate());
insert into Roles (Code, Name, IsAdmin, IsStaff, IsInstructor, IsStudent, Active, Created) 
	values ('INS', 'Instructor', 0, 0, 1, 0, 1, GetDate());
insert into Roles (Code, Name, IsAdmin, IsStaff, IsInstructor, IsStudent, Active, Created) 
	values ('STU', 'Student', 0, 0, 0, 0, 1, GetDate());
select * from Roles;

insert into Users (Username, Password, Firstname, Lastname, Email, CellPhone, WorkPhone, RoleCode, Active, Created)
	values ('gpdoud', 'MaxPass@8888', 'Greg', 'Doud', 'gdoud@maxtrain.com', '513-703-7315', '513-322-8888', 'AALL', 1, GetDate());
insert into Users (Username, Password, Firstname, Lastname, Email, CellPhone, WorkPhone, RoleCode, Active, Created)
	values ('dbartick', 'MaxPass@8888', 'Denise', 'Bartick', 'dbartick@maxtrain.com', null, '513-322-8888', 'ADMSTF', 1, GetDate());
insert into Users (Username, Password, Firstname, Lastname, Email, CellPhone, WorkPhone, RoleCode, Active, Created)
	values ('pmiller', 'MaxPass@8888', 'Patricia', 'Miller', 'pmiller@maxtrain.com', null, '513-322-8888', 'ADMSTF', 1, GetDate());
insert into Users (Username, Password, Firstname, Lastname, Email, CellPhone, WorkPhone, RoleCode, Active, Created)
	values ('kpeace', 'MaxPass@8888', 'Kim', 'Peace', 'kpeace@maxtrain.com', null, '513-322-8888', 'ADMSTF', 1, GetDate());
insert into Users (Username, Password, Firstname, Lastname, Email, CellPhone, WorkPhone, RoleCode, Active, Created)
	values ('dmiller', 'MaxPass@8888', 'Dustin', 'Miller', 'dmiller@maxtrain.com', null, '513-322-8888', 'STF', 1, GetDate());
insert into Users (Username, Password, Firstname, Lastname, Email, CellPhone, WorkPhone, RoleCode, Active, Created)
	values ('sblessing', 'MaxPass@8888', 'Sean', 'Blessing', 'sblessing@maxtrain.com', null, '513-322-8888', 'INS', 1, GetDate());
select * from Users;

insert into Cohorts ([Name], [BeginDate], [EndDate], [Capacity], InstructorId, Active, Created)
	values ('C#BC/9 Jan28-Apr10 2020', '2020-01-28', '2020-04-10', 12, 1, 1, '2020-01-10');
select * from Cohorts;