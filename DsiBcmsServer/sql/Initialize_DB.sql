insert into SysCtrls ([Key], Value, Category, Active, Created)
	values ('name', 'Boot Camp Management System (BCMS)', 'system', 1, GetDate());
insert into SysCtrls ([Key], Value, Category, Active, Created)
	values ('version', '0.0', 'system', 1, GetDate());
insert into SysCtrls ([Key], Value, Category, Active, Created)
	values ('status', 'Development', 'system', 1, GetDate());
insert into SysCtrls ([Key], Value, Category, Active, Created)
	values ('author', 'Doud Systems, Inc.', 'system', 1, GetDate());
insert into SysCtrls ([Key], Value, Category, Active, Created)
	values ('copyright', 'Copyright (c) 2020', 'system', 1, GetDate());
insert into SysCtrls ([Key], Value, Category, Active, Created)
	values ('rights', 'All rights reserved.', 'system', 1, GetDate());
select * from SysCtrls;

insert into roles (Code, Name, IsStaff, IsInstructor, IsAdmin, Active, Created) 
	values ('Admin', 'Administrator', 1, 1, 1, 1, GetDate());
insert into roles (Code, Name, IsStaff, IsInstructor, IsAdmin, Active, Created) 
	values ('Staff-Instructor', 'Staff', 1, 0, 0, 1, GetDate());
insert into roles (Code, Name, IsStaff, IsInstructor, IsAdmin, Active, Created) 
	values ('Staff', 'Staff', 1, 0, 0, 1, GetDate());
insert into roles (Code, Name, IsStaff, IsInstructor, IsAdmin, Active, Created) 
	values ('Instructor', 'Instructor', 0, 1, 0, 1, GetDate());
select * from roles;

insert into users (Username, Password, Firstname, Lastname, Email, CellPhone, WorkPhone, RoleCode, Active, Created)
	values ('gpdoud', 'MaxPass@8888', 'Greg', 'Doud', 'gdoud@maxtrain.com', '513-703-7315', '513-322-8888', 'Admin', 1, GetDate());
insert into users (Username, Password, Firstname, Lastname, Email, CellPhone, WorkPhone, RoleCode, Active, Created)
	values ('pmiller', 'MaxPass@8888', 'Patricia', 'Miller', 'pmiller@maxtrain.com', null, '513-322-8888', 'Admin', 1, GetDate());
insert into users (Username, Password, Firstname, Lastname, Email, CellPhone, WorkPhone, RoleCode, Active, Created)
	values ('kpeace', 'MaxPass@8888', 'Kim', 'Peace', 'kpeace@maxtrain.com', null, '513-322-8888', 'Admin', 1, GetDate());
insert into users (Username, Password, Firstname, Lastname, Email, CellPhone, WorkPhone, RoleCode, Active, Created)
	values ('dmiller', 'MaxPass@8888', 'Dustin', 'Miller', 'dmiller@maxtrain.com', null, '513-322-8888', 'Staff', 1, GetDate());
insert into users (Username, Password, Firstname, Lastname, Email, CellPhone, WorkPhone, RoleCode, Active, Created)
	values ('sblessing', 'MaxPass@8888', 'Sean', 'Blessing', 'sblessing@maxtrain.com', null, '513-322-8888', 'Instructor', 1, GetDate());
select * from users;
