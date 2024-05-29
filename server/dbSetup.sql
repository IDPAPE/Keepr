-- Active: 1715717093297@@127.0.0.1@3306
CREATE TABLE IF NOT EXISTS accounts (
    id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
    createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
    updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
    name varchar(255) COMMENT 'User Name',
    email varchar(255) COMMENT 'User Email',
    picture varchar(255) COMMENT 'User Picture',
    coverImg varchar(255)
) default charset utf8mb4 COMMENT '';

DROP TABLE keeps;

CREATE TABLE keeps (
    id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
    creatorId VARCHAR(255) NOT NULL,
    createdAt DATETIME DEFAULT CURRENT_TIMESTAMP,
    updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    name VARCHAR(255) NOT NULL,
    description VARCHAR(1000) NOT NULL,
    img VARCHAR(1000) NOT NULL,
    views INT NOT NULL DEFAULT 0,
    FOREIGN KEY (creatorId) REFERENCES accounts (id) ON DELETE CASCADE
);

DROP TABLE vaults;

CREATE TABLE vaults (
    id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
    creatorId VARCHAR(255) NOT NULL,
    createdAt DATETIME DEFAULT CURRENT_TIMESTAMP,
    updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    name VARCHAR(255) NOT NULL,
    description VARCHAR(1000) NOT NULL,
    img VARCHAR(1000) NOT NULL,
    isPrivate BOOLEAN NOT NULL,
    FOREIGN KEY (creatorId) REFERENCES accounts (id) ON DELETE CASCADE
);

DROP TABLE vaultKeeps;

CREATE TABLE vaultKeeps (
    id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
    createdAt DATETIME DEFAULT CURRENT_TIMESTAMP,
    updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    keepId INT NOT NULL,
    vaultId INT NOT NULL,
    creatorId VARCHAR(255) NOT NULL
);

UPDATE keeps SET views = @views WHERE keeps.id = 1;

SELECT keeps.*, COUNT(vaultKeeps.id) AS kept, accounts.*
FROM
    keeps
    JOIN accounts ON accounts.id = keeps.creatorId
    LEFT JOIN vaultKeeps ON vaultKeeps.keepId = keeps.id
WHERE
    keeps.id = 1
GROUP BY (keeps.id)
ORDER By keeps.createdAt;

UPDATE keeps SET views = 33 WHERE keeps.id = 1;