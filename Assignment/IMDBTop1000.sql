CREATE DATABASE IMDBTop1000;
GO

USE IMDBTop1000;
GO

IF OBJECT_ID('dbo.Movies', 'U') IS NOT NULL
    DROP TABLE dbo.Movies;
IF OBJECT_ID('dbo.Actors', 'U') IS NOT NULL
    DROP TABLE dbo.Actors;
IF OBJECT_ID('dbo.Directors', 'U') IS NOT NULL
    DROP TABLE dbo.Directors;

CREATE TABLE Directors (
    DirectorID INT PRIMARY KEY IDENTITY(1,1),
    DirectorName NVARCHAR(255) NOT NULL
);
GO

CREATE TABLE Actors (
    ActorID INT PRIMARY KEY IDENTITY(1,1),
    ActorName NVARCHAR(255) NOT NULL
);

CREATE TABLE Movies (
    MovieID INT PRIMARY KEY IDENTITY(1,1),
    PosterLink NVARCHAR(MAX),
    SeriesTitle NVARCHAR(255),
    ReleasedYear INT,
    Certificate NVARCHAR(10),
    Runtime NVARCHAR(50),
    Genre NVARCHAR(255),
    IMDBRating FLOAT,
    Overview NVARCHAR(MAX),
    MetaScore INT,
    DirectorID INT,
    Star1ID INT,
    Star2ID INT,
    Star3ID INT,
    Star4ID INT,
    NoOfVotes INT,
    FOREIGN KEY (DirectorID) REFERENCES Directors(DirectorID),
    FOREIGN KEY (Star1ID) REFERENCES Actors(ActorID),
    FOREIGN KEY (Star2ID) REFERENCES Actors(ActorID),
    FOREIGN KEY (Star3ID) REFERENCES Actors(ActorID),
    FOREIGN KEY (Star4ID) REFERENCES Actors(ActorID)
);

-- Sample insert
INSERT INTO Directors (DirectorName) VALUES ('Frank Darabont');
INSERT INTO Actors (ActorName) VALUES ('Tim Robbins'), ('Morgan Freeman'), ('Bob Gunton'), ('William Sadler');
INSERT INTO Movies (PosterLink, SeriesTitle, ReleasedYear, Certificate, Runtime, Genre, IMDBRating, Overview, MetaScore, DirectorID, Star1ID, Star2ID, Star3ID, Star4ID, NoOfVotes)
VALUES ('https://m.media-amazon.com/images/M/MV5BMDFkYTc0MGEtZmNhMC00ZDIzLWFmNTEtODM1ZmRlYWMwMWFmXkEyXkFqcGdeQXVyMTMxODk2OTU@._V1_UX67_CR0,0,67,98_AL_.jpg', 'The Shawshank Redemption', 1994, 'A', '142 min', 'Drama', 9.3, 'Two imprisoned men bond over a number of years, finding solace and eventual redemption through acts of common decency', 80, 1, 1, 2, 3, 4, 2343110);
