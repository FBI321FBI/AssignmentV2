namespace AssignmentV2.DataBase.Tables
{
    /// <summary>
    /// Класс предоставляещий стандартыне возможности управления таблицей.
    /// </summary>
    /// <typeparam name="T">Модель.</typeparam>
    public class BaseTable<T>
    {
        public string ConnectionString = "Server=localhost;Database=assignment_v2;Trusted_Connection=True;";

        /// <summary>
        /// Выборка все записей.
        /// </summary>
        /// <returns>Возвращает записи таблицы.</returns>
        /// <exception cref="NotImplementedException"></exception>
        public virtual Task<IEnumerable<T>?> Select() => throw new NotImplementedException("This method is not yet implemented.");

        /// <summary>
        /// Добавляет запись в таблицу.
        /// </summary>
        /// <param name="model">Модель.</param>
        /// <exception cref="NotImplementedException"></exception>
        public virtual Task Insert(T model) => throw new NotImplementedException("This method is not yet implemented.");

        /// <summary>
        /// Обновляет запись таблицы.
        /// </summary>
        /// <param name="model">Модель.</param>
        /// <exception cref="NotImplementedException"></exception>
        public virtual Task Update(T model) => throw new NotImplementedException("This method is not yet implemented.");

        /// <summary>
        /// Удаляет запись из таблицы.
        /// </summary>
        /// <param name="model">Модель.</param>
        /// <exception cref="NotImplementedException"></exception>
        public virtual Task Delete(T model) => throw new NotImplementedException("This method is not yet implemented.");
    }
}
