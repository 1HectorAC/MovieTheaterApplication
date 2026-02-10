use MovieTheaterDb;

INSERT INTO Auditoriums (Title) VALUES 
('One'),
('Two');


INSERT INTO Movies (Title, Description) VALUES 
('The Great One', 'A movie about the greatest people of all time.'),
('Sharknado 27: This Time its foreal', 'The Greatest Movie about shark tornados.'),
('Moon Man and His Dog', 'A Heart warming movie about an astronaut and a dog');

INSERT INTO Seats (AuditoriumId, Row, [Column]) VALUES
(1, 'A', 1),
(1, 'A', 2),
(1, 'A', 3),
(1, 'B', 1),
(1, 'B', 2),
(1, 'B', 3),
(1, 'C', 1),
(1, 'C', 2),
(1, 'C', 3),
(2, 'A', 1),
(2, 'A', 2),
(2, 'A', 3),
(2, 'B', 1),
(2, 'B', 2),
(2, 'B', 3),
(2, 'C', 1),
(2, 'C', 2),
(2, 'C', 3);

INSERT INTO Showings (AuditoriumId, ShowingTime, MovieId) VALUES
(1, '2026-01-01T10:00:00', 1),
(1, '2026-01-01T12:00:00', 1),
(1, '2026-01-01T14:00:00', 1),
(2, '2026-01-01T10:00:00', 2);

INSERT INTO Tickets (ShowingId, SeatId) VALUES
(1,6),
(1, 7);
