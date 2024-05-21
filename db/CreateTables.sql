-- { Справка }
-- Внешние ключи называют ограничениями, потому что
-- невозможно добавить в таблицу с внешним ключом (ссылкой на первичный ключ)
-- значение, которого не существует в таблице с первичным ключом.
--
-- Если в [groups] нет группы 3, внести пользователя с этой группой в [users] невозможно
-- 
-- Создание связей написано в запросе несколько раз. Связи комментариями еще раз указаны ниже
/*
 *   В запросе существуют следуюшие связи:
 *       [groups].ID (PK) -> [users].GID (FK)
 *       [users].ID (PK) -> [messages].UID (FK)
 *       [types].ID (PK) -> [messages].TID (FK)
 */


-- СОЗДАНИЕ СЛОЯ УЧЕТНЫХ ЗАПИСЕЙ --
-- Слой пользователей включает в себя таблицу с группами пользователей, и таблицей неуправляемых
-- данных пользователя.
-- Таблица groups похожа на таблицу types, но предназначена для хранения совершенно других значений
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
-- Слоц сообщений создан для организации доставки и хранения заявок ИС
-- Таблица types необходима для быстрого анализа типов заявок
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
