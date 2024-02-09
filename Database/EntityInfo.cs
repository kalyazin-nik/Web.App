namespace Database
{
    internal readonly struct EntityInfo
    {
        internal readonly string PropertyName;
        internal readonly string ColumnName;
        internal readonly string? ColumnType;
        internal readonly string? ColumnDefaultValue;

        internal EntityInfo(string propertyName, string columnName, string? columnType = null, string? columnDefaultValue = null)
        {
            PropertyName = propertyName;
            ColumnName = columnName;
            ColumnType = columnType;
            ColumnDefaultValue = columnDefaultValue;
        }
    }
}
