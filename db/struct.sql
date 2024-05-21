-- СОЗДАНИЕ СЛОЯ УЧЕТНЫХ ЗАПИСЕЙ --
CREATE TABLE [groups]
(
    [id] int not null
        PRIMARY KEY, 
    [title] NVARCHAR(MAX),
    [content] NVARCHAR(MAX)
)

CREATE TABLE [users]
(
    [id] INT NOT NULL
        PRIMARY KEY,

    
    [name] NVARCHAR(MAX),
    [sname] NVARCHAR(MAX),
    [phone] NVARCHAR(MAX),
    [gid] INT NOT NULL 
        FOREIGN KEY REFERENCES [groups](id)
)

-- СОЗДАНИЕ СЛОЯ СООБЩЕНИЙ --
CREATE TABLE [types]
(
    [id] INT NOT NULL
        PRIMARY KEY,
    [title] NVARCHAR(MAX),
    [content] NVARCHAR(MAX)
)

CREATE TABLE [messages]
(
    [id] INT NOT NULL,
    [uid] INT NOT NULL
        FOREIGN KEY REFERENCES [users](id),
    [tid] INT NOT NULL
        FOREIGN KEY REFERENCES [types](id),
    [title] NVARCHAR(MAX),
    [content] NVARCHAR(MAX),
    [time] NVARCHAR(MAX)
)