create procedure GetUsersNotEnrolled
	@cohortid int = 0
as
begin
	select u.* 
		from Users u
		join Roles r
			on r.Code = u.RoleCode
		where r.IsStudent = 1
			and id not in (
				select UserId from Enrollments where CohortId = @cohortid
			)
end
go
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
insert into Configs (KeyValue, DataValue, System, Active, Created)
	values ('notify.1', 'You are a good person!', 0, 1, GetDate());
insert into Configs (KeyValue, DataValue, System, Active, Created)
	values ('notify.2', 'Everything will be all right!', 0, 1, GetDate());
insert into Configs (KeyValue, DataValue, System, Active, Created)
	values ('notify.3', 'Things are looking up!', 0, 1, GetDate());
select * from Configs;

insert into Roles (Code, Name, IsRoot, IsAdmin, IsStaff, IsInstructor, IsStudent, Active, Created) 
	values ('root', 'Root', 1, 1, 1, 1, 1, 1, GetDate());
insert into Roles (Code, Name, IsRoot, IsAdmin, IsStaff, IsInstructor, IsStudent, Active, Created) 
	values ('admstf', 'Admin/Staff', 0, 1, 1, 0, 0, 1, GetDate());
insert into Roles (Code, Name, IsRoot, IsAdmin, IsStaff, IsInstructor, IsStudent, Active, Created) 
	values ('admins', 'Admin/Instructor', 0, 1, 0, 1, 0, 1, GetDate());
insert into Roles (Code, Name, IsRoot, IsAdmin, IsStaff, IsInstructor, IsStudent, Active, Created) 
	values ('stfins', 'Staff/Instructor', 0, 0, 1, 1, 0, 1, GetDate());
insert into Roles (Code, Name, IsRoot, IsAdmin, IsStaff, IsInstructor, IsStudent, Active, Created) 
	values ('adm', 'Admin', 0, 1, 0, 0, 0, 1, GetDate());
insert into Roles (Code, Name, IsRoot, IsAdmin, IsStaff, IsInstructor, IsStudent, Active, Created) 
	values ('stf', 'Staff', 0, 0, 1, 0, 0, 1, GetDate());
insert into Roles (Code, Name, IsRoot, IsAdmin, IsStaff, IsInstructor, IsStudent, Active, Created) 
	values ('ins', 'Instructor', 0, 0, 0, 1, 0, 1, GetDate());
insert into Roles (Code, Name, IsRoot, IsAdmin, IsStaff, IsInstructor, IsStudent, Active, Created) 
	values ('stu', 'Student', 0, 0, 0, 0, 0, 1, GetDate());
select * from Roles;

insert into Users (Username, Password, Firstname, Lastname, Email, CellPhone, WorkPhone, RoleCode, Active, Created)
	values ('gpdoud', 'MaxPass@8888', 'Greg', 'Doud', 'gdoud@maxtrain.com', '513-703-7315', '513-322-8888', 'root', 1, GetDate());
insert into Users (Username, Password, Firstname, Lastname, Email, CellPhone, WorkPhone, RoleCode, Active, Created)
	values ('dbartick', 'MaxPass@8888', 'Denise', 'Bartick', 'dbartick@maxtrain.com', null, '513-322-8888', 'admstf', 1, GetDate());
insert into Users (Username, Password, Firstname, Lastname, Email, CellPhone, WorkPhone, RoleCode, Active, Created)
	values ('pmiller', 'MaxPass@8888', 'Patricia', 'Miller', 'pmiller@maxtrain.com', null, '513-322-8888', 'admstf', 1, GetDate());
insert into Users (Username, Password, Firstname, Lastname, Email, CellPhone, WorkPhone, RoleCode, Active, Created)
	values ('kpeace', 'MaxPass@8888', 'Kim', 'Peace', 'kpeace@maxtrain.com', null, '513-322-8888', 'admstf', 1, GetDate());
insert into Users (Username, Password, Firstname, Lastname, Email, CellPhone, WorkPhone, RoleCode, Active, Created)
	values ('dmiller', 'MaxPass@8888', 'Dustin', 'Miller', 'dmiller@maxtrain.com', null, '513-322-8888', 'stf', 1, GetDate());
insert into Users (Username, Password, Firstname, Lastname, Email, CellPhone, WorkPhone, RoleCode, Active, Created)
	values ('sblessing', 'MaxPass@8888', 'Sean', 'Blessing', 'sblessing@maxtrain.com', null, '513-322-8888', 'ins', 1, GetDate());
select * from Users;

insert into Cohorts ([Name], [BeginDate], [EndDate], [Capacity], InstructorId, Active, Created)
	values ('.Net FT BC #9', '2020-01-28', '2020-04-10', 12, 1, 1, '2020-01-10');
insert into Cohorts ([Name], [BeginDate], [EndDate], [Capacity], InstructorId, Active, Created)
	values ('.Net FT BC #10', '2020-06-88', '2020-08-21', 12, 1, 1, '2020-01-10');
select * from Cohorts;
go