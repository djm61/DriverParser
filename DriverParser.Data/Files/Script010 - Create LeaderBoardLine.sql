﻿CREATE TABLE LeaderBoardLine (
	Id BIGINT NOT NULL PRIMARY KEY,
	CarId BIGINT NOT NULL,
	CurrentDriverId BIGINT NOT NULL,
	CurrentDriverIndex BIGINT NOT NULL,
	TimingId BIGINT NOT NULL,
	FOREIGN KEY (CarId) REFERENCES Car(Id),
	FOREIGN KEY (CurrentDriverId) REFERENCES Driver(Id),
	FOREIGN KEY (TimingId) REFERENCES Timing(Id)
);
