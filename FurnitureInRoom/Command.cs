namespace FurnitureInRoom
{
    public class Command
    {
        private readonly string[] _parameters;

        public Command(string name,params string[] parameters)
        {
            _parameters = parameters;
            CommandName = name;
        }

        public string CommandName { get; private set; }
        
        public bool IsSuitableFor(string request)
        {
            //TODO: check required parameters
            return request.Trim().StartsWith(CommandName);
        }

        public string GetParametersString(string request)
        {
            return request.Replace(CommandName, string.Empty).Trim();
        }

        public string Help()
        {
            return string.Format("{0} {1}", CommandName, string.Join(" ", _parameters));
        }

    }
}
