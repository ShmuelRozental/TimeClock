CREATE TABLE Users (
    UserId INT PRIMARY KEY IDENTITY(1,1),
    UserName NVARCHAR(50) NOT NULL,
    Password NVARCHAR(255) NOT NULL,
	PasswordExpiry  DATETIME NOT NULL DEFAULT GETDATE(),
    CreatedAt DATETIME NOT NULL DEFAULT GETDATE()
	)

CREATE TABLE PasswordHistory (
    HistoryId INT PRIMARY KEY IDENTITY(1,1),
    UserId INT NOT NULL,
    OldPassword NVARCHAR(255) NOT NULL,
    ChangedAt DATETIME NOT NULL DEFAULT GETDATE(),
    FOREIGN KEY (UserId) REFERENCES Users(UserId)
);


	CREATE TABLE TimeEntries (
    EntryId INT PRIMARY KEY IDENTITY(1,1),
    UserId INT NOT NULL,
    EntryTime DATETIME NOT NULL,
    ExitTime DATETIME NULL,  -- Allow NULL for cases where user hasn't exited yet
    Date DATE NOT NULL, 
    CONSTRAINT UQ_UserEntryTime UNIQUE (UserId, EntryTime), -- Ensure each entry is unique for a user
    FOREIGN KEY (UserId) REFERENCES Users(UserId)
);


INSERT INTO Users (UserName, Password, PasswordExpiry, CreatedAt)
VALUES 
('shmuel', '1234', DATEADD(day, 30, GETDATE()), GETDATE()),
('david', '2345', DATEADD(day, 30, GETDATE()), GETDATE()),
('aaron', '3456', DATEADD(day, 30, GETDATE()), GETDATE());

select * from TimeEntries

INSERT INTO Users (UserName, Password, PasswordExpiry, CreatedAt)
VALUES 
('pinchas', '4567', DATEADD(day, 30, GETDATE()), GETDATE())




