namespace FurnitureInRoom
{
    public class Command
    {
        private readonly string _exapmle;

        public Command(string name,string exapmle=null)
        {
            _exapmle = exapmle;
            CommandName = name;
        }

        public string CommandName { get; private set; }
        
        public bool IsSuitableFor(string request)
        {
            return request.Trim().StartsWith(CommandName);
        }

        public string GetParametersString(string request)
        {
            return request.Replace(CommandName, string.Empty).Trim();
        }

        public string Help()
        {
            return _exapmle ?? CommandName;
        }

    }
}
