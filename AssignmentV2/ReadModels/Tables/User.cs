namespace AssignmentV2.ReadModels.Tables
{
    public class User
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Логин.
        /// </summary>
        public string? login { get; set; }

        /// <summary>
        /// Пароль.
        /// </summary>
        public string? password { get; set; }
    }
}
