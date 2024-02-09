namespace Database
{
    internal readonly struct Constraint
    {
        internal readonly string Name;
        internal readonly string Check;

        internal Constraint(string name, string check)
        {
            Name = name;
            Check = check;
        }
    }
}
