SET QUOTED_IDENTIFIER ON;
SET ANSI_NULLS ON;
SET ANSI_PADDING ON;
SET ANSI_WARNINGS ON;
SET CONCAT_NULL_YIELDS_NULL ON;
SET ARITHABORT ON;
SET NUMERIC_ROUNDABORT OFF;
DELETE FROM Grades;
DELETE FROM SubjectGroups;
DELETE FROM AspNetUserRoles;
DELETE FROM Subjects;
DELETE FROM AspNetUsers;
DELETE FROM AspNetRoles;
DELETE FROM Groups;

SET IDENTITY_INSERT Groups ON;
INSERT INTO Groups (Id, Name) VALUES
(1, N'IO'),
(2, N'PAI'),
(3, N'AIP Erasmus');
SET IDENTITY_INSERT Groups OFF;

SET IDENTITY_INSERT AspNetRoles ON;
INSERT INTO AspNetRoles (Id, RoleValue, Name, NormalizedName, ConcurrencyStamp) VALUES
(1, 1, N'Student', N'STUDENT', NEWID()),
(2, 2, N'Parent', N'PARENT', NEWID()),
(3, 3, N'Teacher', N'TEACHER', NEWID()),
(4, 4, N'Admin', N'ADMIN', NEWID());
SET IDENTITY_INSERT AspNetRoles OFF;

SET IDENTITY_INSERT AspNetUsers ON;
INSERT INTO AspNetUsers
(Id, FirstName, LastName, RegistrationDate, UserType, GroupId, ParentId, Title, UserName, NormalizedUserName, Email, NormalizedEmail, EmailConfirmed, PasswordHash, SecurityStamp, ConcurrencyStamp, PhoneNumberConfirmed, TwoFactorEnabled, LockoutEnabled, AccessFailedCount)
VALUES
(1, N'Adam', N'Bednarski', '2010-01-01', 3, NULL, NULL, N'mgr in¿.', N't1@eg.eg', N'T1@EG.EG', N'real_email@eg.eg', N'REAL_EMAIL@EG.EG', 1, NULL, NEWID(), NEWID(), 0, 0, 1, 0),
(2, N'Jan', N'Nowak', '2010-11-12', 3, NULL, NULL, N'mgr', N't2@eg.eg', N'T2@EG.EG', N't2@eg.eg', N'T2@EG.EG', 1, NULL, NEWID(), NEWID(), 0, 0, 1, 0),
(12, N'Stanis³aw', N'Nowakowski', '2010-11-12', 3, NULL, NULL, N'mgr in¿.', N't11@eg.eg', N'T11@EG.EG', N't11@eg.eg', N'T11@EG.EG', 1, NULL, NEWID(), NEWID(), 0, 0, 1, 0),

(3, N'Zbigniew', N'Kowalski', '2014-03-20', 2, NULL, NULL, NULL, N'p1@eg.eg', N'P1@EG.EG', N'real_email@eg.eg', N'REAL_EMAIL@EG.EG', 1, NULL, NEWID(), NEWID(), 0, 0, 1, 0),
(4, N'Anna', N'Nowakowska', '2014-06-21', 2, NULL, NULL, NULL, N'p2@eg.eg', N'P2@EG.EG', N'p2@eg.eg', N'P2@EG.EG', 1, NULL, NEWID(), NEWID(), 0, 0, 1, 0),

(5, N'Tomasz', N'Kowalski', '2016-05-11', 1, 1, 3, NULL, N's1@eg.eg', N'S1@EG.EG', N's1@eg.eg', N'S1@EG.EG', 1, NULL, NEWID(), NEWID(), 0, 0, 1, 0),
(6, N'Krzysztof', N'Kowalski', '2015-09-18', 1, 1, 3, NULL, N's2@eg.eg', N'S2@EG.EG', N's2@eg.eg', N'S2@EG.EG', 1, NULL, NEWID(), NEWID(), 0, 0, 1, 0),
(7, N'Natalia', N'Kowalska', '2017-07-16', 1, 2, 3, NULL, N's3@eg.eg', N'S3@EG.EG', N's3@eg.eg', N'S3@EG.EG', 1, NULL, NEWID(), NEWID(), 0, 0, 1, 0),
(8, N'Magdalena', N'Wiœniewska', '2018-05-14', 1, 2, 4, NULL, N's4@eg.eg', N'S4@EG.EG', N's4@eg.eg', N'S4@EG.EG', 1, NULL, NEWID(), NEWID(), 0, 0, 1, 0),
(9, N'Jan', N'Wiœniewski', '2019-02-19', 1, 3, 4, NULL, N's5@eg.eg', N'S5@EG.EG', N's5@eg.eg', N'S5@EG.EG', 1, NULL, NEWID(), NEWID(), 0, 0, 1, 0),
(10, N'Krystian', N'Wiœniewski', '2019-05-01', 1, 3, 4, NULL, N's6@eg.eg', N'S6@EG.EG', N's6@eg.eg', N'S6@EG.EG', 1, NULL, NEWID(), NEWID(), 0, 0, 1, 0),

(11, N'Jacek', N'Kowalczyk', '2009-01-01', 4, NULL, NULL, NULL, N'a1@eg.eg', N'A1@EG.EG', N'a1@eg.eg', N'A1@EG.EG', 1, NULL, NEWID(), NEWID(), 0, 0, 1, 0);
SET IDENTITY_INSERT AspNetUsers OFF;

INSERT INTO AspNetUserRoles (UserId, RoleId) VALUES
(1, 3), (2, 3), (12, 3),
(3, 2), (4, 2),
(5, 1), (6, 1), (7, 1), (8, 1), (9, 1), (10, 1),
(11, 4);

SET IDENTITY_INSERT Subjects ON;
INSERT INTO Subjects (Id, Name, Description, TeacherId) VALUES
(1, N'Aplikacje WWW', N'Aplikacje webowe', 1),
(2, N'Programowanie obiektowe', N'Programowanie obiektowe jest przedmiotem realizuj¹cym przyk³ady programowanie obiektowego', 1),
(3, N'Advanced Internet Programming', N'Advanced Internet Programming is a course for ERASMUS+ students', 2),
(4, N'Administracja Intenetowymi Systemami Baz Danych', N'Administracja Intenetowymi Systemami Baz Danych jest kontynuacj¹ przedmiotu Bazy danych na studiach stacjonarnych I-go stopnia spec. PAI', 2),
(5, N'Programowanie interaktywnej grafiki dla stron WWW', NULL, 12);
SET IDENTITY_INSERT Subjects OFF;

INSERT INTO SubjectGroups (SubjectId, GroupId) VALUES
(1, 1),
(1, 2),
(2, 1),
(2, 2),
(2, 3),
(3, 3),
(4, 2),
(4, 3);

INSERT INTO Grades (DateOfIssue, GradeValue, SubjectId, StudentId) VALUES
('2019-03-21T17:46:38', 4, 1, 5);

