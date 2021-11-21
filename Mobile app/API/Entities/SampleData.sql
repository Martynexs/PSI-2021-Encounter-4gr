-- Should be reviewed and modified every time new migration happens to match tables and columns
-- Never runs automatically, only manually

-- Deleting from all tables in specific order to avoid triggering FK constraints:
DELETE FROM UserAnswers;
DELETE FROM Ratings;
DELETE FROM UserAnswers;
DELETE FROM QuestionChoices;
DELETE FROM Questions;
DELETE FROM Quizes;
DELETE FROM Waypoints;
DELETE FROM Routes;
DELETE FROM Users;

INSERT INTO Users (Id, Name, Password, Username) VALUES (1,'Justina',  'R9oaw0bHVXmyyQ3Ik2A/IWMDbFh/eMWTaCDC/xTduLVBPlSL', 'JustinaG'); --password: jus
INSERT INTO Users (Id, Name, Password, Username) VALUES (2,'Martynas', 'g6zxGYGhszT4RIju8kO9LF3cfF8LruqMfbjWuzDI1CSCUFFp', 'MartynasV'); -- password: mar
INSERT INTO Users (Id, Name, Password, Username) VALUES (3,'Eligijus', 'ydND6SIqvF3NYDlQ+Me5WerZd5TVA3dgkIqPBOa4vfiJz/nT', 'EligijusS'); -- password: eli

INSERT INTO Routes ("Id", "CreatorID", "Description", "Location", "Name", "RateSum", "Raters", "Rating")
			VALUES (1, 1, 'Aplankyk visus Vilniaus McDonalds', 'Vilnius', 'Vilniaus McDonalds', 0, '', 0);
INSERT INTO Routes ("Id", "CreatorID", "Description", "Location", "Name", "RateSum", "Raters", "Rating")
			VALUES (2, 1, 'Aplankyk svarbiausius Vilniaus parkus', 'Vilnius', 'Vilniaus Parkai', 0, '', 0);
INSERT INTO Routes ("Id", "CreatorID", "Description", "Location", "Name", "RateSum", "Raters", "Rating")
			VALUES (3, 1, 'Keletas Vilniaus rajonu', 'Vilnius, Lietuva', 'Vilniaus Rajonai', 0, '', 0);
			
-- Route 1 Waypoints:
INSERT INTO Waypoints
("Id", "Description", "Latitude", "Longitude", "Name", "Position", "RouteId",
	"ClosingTime", "OpeningHours", "PhoneNumber", "Price", "Type")
	VALUES (1, 'MCD prie Vilniaus autobusų stoties', 54.67164078, 25.28532675, 'Stoties MCDonalds', 1, 1,
	'0001-01-01 23:00:00', '0001-01-01 07:00:00', '+37061122222', '0', 2);
INSERT INTO Waypoints
("Id", "Description", "Latitude", "Longitude", "Name", "Position", "RouteId",
	"ClosingTime", "OpeningHours", "PhoneNumber", "Price", "Type")
	VALUES (2, 'MCD Kedrų g.', 54.6767048, 25.2565732, 'Naujamiesčio MCDonalds', 2, 1,
	'0001-01-01 23:00:00', '0001-01-01 07:00:00', '+37061122223', '0', 2);
INSERT INTO Waypoints
("Id", "Description", "Latitude", "Longitude", "Name", "Position", "RouteId",
	"ClosingTime", "OpeningHours", "PhoneNumber", "Price", "Type")
	VALUES (3, 'MCD Ukmergės g.', 54.7204533, 25.246647, 'Fabijoniškių MCDonalds', 3, 1,
	'0001-01-01 23:00:00', '0001-01-01 07:00:00', '+37061122224', '0', 2);
INSERT INTO Waypoints
("Id", "Description", "Latitude", "Longitude", "Name", "Position", "RouteId",
	"ClosingTime", "OpeningHours", "PhoneNumber", "Price", "Type")
	VALUES (4, 'MCD Kareivių g.', 54.7192713, 25.3017, 'Žirmūnų MCDonalds', 4, 1,
	'0001-01-01 23:00:00', '0001-01-01 07:00:00', '+37061122225', '0', 2);
	
-- Route2 Waypoints:
INSERT INTO Waypoints
("Id", "Description", "Latitude", "Longitude", "Name", "Position", "RouteId",
	"ClosingTime", "OpeningHours", "PhoneNumber", "Price", "Type")
	VALUES (5, 'Katedros parkas', 54.6868349, 25.28676346, 'Arkikatedros parkas', 1, 2,
	'0001-01-01 00:00:00', '0001-01-01 00:00:00', null, '0', 6);
INSERT INTO Waypoints
("Id", "Description", "Latitude", "Longitude", "Name", "Position", "RouteId",
	"ClosingTime", "OpeningHours", "PhoneNumber", "Price", "Type")
	VALUES (6, 'Lukiškių aikštė', 54.6888525, 25.2700657, 'Lukiškių a.', 2, 2,
	'0001-01-01 00:00:00', '0001-01-01 00:00:00', null, '0', 6);
	
-- Route3 Waypoints:
INSERT INTO Waypoints
("Id", "Description", "Latitude", "Longitude", "Name", "Position", "RouteId",
	"ClosingTime", "OpeningHours", "PhoneNumber", "Price", "Type")
	VALUES (7, 'Lazdynai', 54.6735259, 25.2143533, 'Lazdynai', 1, 3,
	'0001-01-01 00:00:00', '0001-01-01 00:00:00', null, '0', 0);
INSERT INTO Waypoints
("Id", "Description", "Latitude", "Longitude", "Name", "Position", "RouteId",
	"ClosingTime", "OpeningHours", "PhoneNumber", "Price", "Type")
	VALUES (8, 'Karoliniškės', 54.6886139, 25.2148983, 'Karoliniškės', 2, 3,
	'0001-01-01 00:00:00', '0001-01-01 00:00:00', null, '0', 0);
INSERT INTO Waypoints
("Id", "Description", "Latitude", "Longitude", "Name", "Position", "RouteId",
	"ClosingTime", "OpeningHours", "PhoneNumber", "Price", "Type")
	VALUES (9, 'Žvėrynas', 54.6932506, 25.2510236, 'Žvėrynas', 3, 3,
	'0001-01-01 00:00:00', '0001-01-01 00:00:00', null, '0', 0);

-- Inserting some Ratings
INSERT INTO Ratings ("RouteId", "UserId", "Value") VALUES (1, 1, 5);
INSERT INTO Ratings ("RouteId", "UserId", "Value") VALUES (3, 1, 1);

-- Inserting Quizes and other quiz related data:
INSERT INTO Quizes ("Id", "WaypointId") VALUES (1, 1);

INSERT INTO Questions
	("Id", "Position", "Text", "Type", "SecondsToAnswer", "QuizId")
	VALUES (1, 1, 'Which vehicles can do you see in front of McDonalds?', 'Single', 30, 1);
INSERT INTO Questions
	("Id", "Position", "Text", "Type", "SecondsToAnswer", "QuizId")
	VALUES (2, 2, 'Which ports or stations do you see?', 'Multiple', 25, 1);

-- Answers to Question with ID 1
INSERT INTO QuestionChoices
	("Id", "Position", "Letter", "Text", "IsRight", "SelectedScore", "UnselectedScore", "QuestionId")
	VALUES (1, 1, 'A', 'Bicycles', 0, 0.0, -0.5, 1);
INSERT INTO QuestionChoices
	("Id", "Position", "Letter", "Text", "IsRight", "SelectedScore", "UnselectedScore", "QuestionId")
	VALUES (2, 2, 'B', 'Trains', 1, 5.0, 0.0, 1);
INSERT INTO QuestionChoices
	("Id", "Position", "Letter", "Text", "IsRight", "SelectedScore", "UnselectedScore", "QuestionId")
	VALUES (3, 3, 'C', 'Cars', 0, 0.0, -0.5, 1);
INSERT INTO QuestionChoices
	("Id", "Position", "Letter", "Text", "IsRight", "SelectedScore", "UnselectedScore", "QuestionId")
	VALUES (4, 4, 'D', 'Buses', 0, 0.0, -0.5, 1);

-- Answers to Question with ID 2
INSERT INTO QuestionChoices
	("Id", "Position", "Letter", "Text", "IsRight", "SelectedScore", "UnselectedScore", "QuestionId")
	VALUES (5, 1, 'A', 'Air port', 0, 0.0, -3.0, 2);
INSERT INTO QuestionChoices
	("Id", "Position", "Letter", "Text", "IsRight", "SelectedScore", "UnselectedScore", "QuestionId")
	VALUES (6, 2, 'B', 'Train station', 1, 2.5, 0.0, 2);
INSERT INTO QuestionChoices
	("Id", "Position", "Letter", "Text", "IsRight", "SelectedScore", "UnselectedScore", "QuestionId")
	VALUES (7, 3, 'C', 'Bus station', 1, 2.5, 0.5, 2);
INSERT INTO QuestionChoices
	("Id", "Position", "Letter", "Text", "IsRight", "SelectedScore", "UnselectedScore", "QuestionId")
	VALUES (8, 4, 'D', 'Ferry', 0, 0.0, -3.0, 2);