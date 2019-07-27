CREATE TABLE ResultsLeaderBoardLines (
	ResultId BIGINT NOT NULL,
	LeaderBoardLineId BIGINT NOT NULL,
	PRIMARY KEY (ResultId, LeaderBoardLineId),
	FOREIGN KEY (ResultId) REFERENCES Result(Id),
	FOREIGN KEY (LeaderBoardLineId) REFERENCES LeaderBoardLine(Id)
);
