﻿CREATE TABLE SchemaVersions (
	Id BIGINT PRIMARY KEY,
	ScriptName TEXT NOT NULL,
	Added DATETIME NOT NULL
);
