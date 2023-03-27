CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" TEXT NOT NULL CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY,
    "ProductVersion" TEXT NOT NULL
);

BEGIN TRANSACTION;

CREATE TABLE "EventsInfo" (
    "EventName" TEXT NOT NULL,
    "EventId" TEXT NOT NULL,
    "EventType" TEXT NOT NULL,
    "EventNumber" INTEGER NOT NULL,
    "EventDescription" TEXT NOT NULL,
    "EventCategory" TEXT NOT NULL,
    "UserPings" TEXT NOT NULL
);

CREATE TABLE "Users" (
    "UserName" TEXT NOT NULL,
    "DiscordUser" TEXT NOT NULL,
    "Email" TEXT NOT NULL,
    "EmailConfirmed" INTEGER NOT NULL,
    "Password" TEXT NOT NULL,
    "UserRole" TEXT NOT NULL
);

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20230227165559_InitialCreate', '7.0.2');

COMMIT;

BEGIN TRANSACTION;

ALTER TABLE "EventsInfo" ADD "QuestCompatable" INTEGER NOT NULL DEFAULT 0;

CREATE TABLE "ef_temp_EventsInfo" (
    "EventNumber" INTEGER NOT NULL CONSTRAINT "PK_EventsInfo" PRIMARY KEY AUTOINCREMENT,
    "EventDescription" TEXT NOT NULL,
    "EventName" TEXT NOT NULL,
    "EventType" TEXT NOT NULL,
    "QuestCompatable" INTEGER NOT NULL,
    "UserPings" TEXT NOT NULL
);

INSERT INTO "ef_temp_EventsInfo" ("EventNumber", "EventDescription", "EventName", "EventType", "QuestCompatable", "UserPings")
SELECT "EventNumber", "EventDescription", "EventName", "EventType", "QuestCompatable", "UserPings"
FROM "EventsInfo";

CREATE TABLE "ef_temp_Users" (
    "UserName" TEXT NOT NULL CONSTRAINT "PK_Users" PRIMARY KEY,
    "DiscordUser" TEXT NOT NULL,
    "Email" TEXT NOT NULL,
    "EmailConfirmed" INTEGER NOT NULL,
    "Password" TEXT NOT NULL,
    "UserRole" TEXT NOT NULL
);

INSERT INTO "ef_temp_Users" ("UserName", "DiscordUser", "Email", "EmailConfirmed", "Password", "UserRole")
SELECT "UserName", "DiscordUser", "Email", "EmailConfirmed", "Password", "UserRole"
FROM "Users";

COMMIT;

PRAGMA foreign_keys = 0;

BEGIN TRANSACTION;

DROP TABLE "EventsInfo";

ALTER TABLE "ef_temp_EventsInfo" RENAME TO "EventsInfo";

DROP TABLE "Users";

ALTER TABLE "ef_temp_Users" RENAME TO "Users";

COMMIT;

PRAGMA foreign_keys = 1;

BEGIN TRANSACTION;

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20230228044718_EventInfo_Update', '7.0.2');

COMMIT;

BEGIN TRANSACTION;

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20230228180418_TECData_publish', '7.0.2');

COMMIT;

BEGIN TRANSACTION;

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20230228185640_database_move', '7.0.2');

COMMIT;

BEGIN TRANSACTION;

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20230228190009_database_move2', '7.0.2');

COMMIT;

BEGIN TRANSACTION;

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20230301180232_releaseDataTest', '7.0.2');

COMMIT;

