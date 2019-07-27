CREATE TABLE ResultsSplits (
	ResultId BIGINT NOT NULL,
	SplitsId BIGINT NOT NULL,
	PRIMARY KEY (ResultId, SplitsId),
	FOREIGN KEY (ResultId) REFERENCES Result(Id),
	FOREIGN KEY (SplitsId) REFERENCES Splits(Id)
);
