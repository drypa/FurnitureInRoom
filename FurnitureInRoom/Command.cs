namespace FurnitureInRoom
{
    public class Command
    {
        public Command(string name)
        {
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
    }
}
