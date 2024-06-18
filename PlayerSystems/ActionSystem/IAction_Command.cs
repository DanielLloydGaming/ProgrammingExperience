namespace VartraAbyss.Actions
{
	public interface IAction_Command
	{
		public void Execute(params object[] commandData);
	}
}