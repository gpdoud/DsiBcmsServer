UPDATE Configs Set
	DataValue = 'DSI'
		Where KeyValue = 'author';
go
INSERT into Configs
	(KeyValue, DataValue, System, Active, Created) VALUES
	('late.hour', 9, 0, 1, GETDATE());
go
INSERT into Configs
	(KeyValue, DataValue, System, Active, Created) VALUES
	('late.minute', 15, 0, 1, GETDATE());
go
UPDATE Configs Set
	DataValue = 'BCMS'
		Where KeyValue = 'name';
go
UPDATE Configs Set
	DataValue = 'BCMS v1.0 is live! Click to rotate.'
		Where KeyValue = 'notify.1';
go
UPDATE Configs Set
	DataValue = 'A new red on blue logo. Click to rotate.'
		Where KeyValue = 'notify.2';
go
UPDATE Configs Set
	DataValue = 'Dev team: ABOUT > DEV TEAM! Click to rotate.'
		Where KeyValue = 'notify.3';
go
INSERT into Configs
	(KeyValue, DataValue, System, Active, Created) VALUES
	('notify.4', 'Revision list: ABOUT > REVISION HISTORY! Click to rotate.', 0, 1, GETDATE());
go
UPDATE Configs Set
	DataValue = 'reserved'
		Where KeyValue = 'rights';
go
UPDATE Configs Set
	DataValue = 'PROD'
		Where KeyValue = 'status';
go
UPDATE Configs Set
	DataValue = '1.0 (gd-log-db-#40)'
		Where KeyValue = 'version';
go
UPDATE Configs Set
	DataValue = '(c) 2020'
		Where KeyValue = 'copyright';
go
INSERT into Configs
	(KeyValue, DataValue, System, Active, Created) VALUES
	('version.date', 'April 26, 2020', 0, 1, GETDATE());
go
