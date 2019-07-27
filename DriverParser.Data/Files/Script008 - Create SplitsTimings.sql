CREATE TABLE SplitsTimings (
	SplitsId BIGINT NOT NULL,
	TimingId BIGINT NOT NULL,
	PRIMARY KEY (SplitsId, TimingId),
	FOREIGN KEY (SplitsId) REFERENCES Splits(Id),
	FOREIGN KEY (TimingId) REFERENCES Timing(Id)
);
