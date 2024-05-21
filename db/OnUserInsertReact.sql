-- Триггеры в базе данных необходимы 
--      для реакции на DML инструкцию (INSERT, DELETE или UPDATE)
-- Задача "On Insert - React" -> сообщить о вставке нового столбца в таблицу
CREATE TRIGGER [OnUserInsertReact]
    ON dbo.[users]
    AFTER INSERT AS
BEGIN
    SELECT 'Добавлена новая запись в [users]'
END
