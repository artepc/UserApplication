-------------------------------------------------------------------------
if not exists (select 1 from dbo.[User])
begin
	insert into dbo.[User] (Username, Email, Fullname, Password)
	values ('kvetinka', 'kvetinka@gmail.com', 'Hana Nova', '12345'),
	('kvetinka2', 'kvetinka2@gmail.com', 'Pepa Novak', '98765');
end
