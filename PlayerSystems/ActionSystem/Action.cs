using UnityEngine;

namespace VartraAbyss.Actions
{
	public abstract class Action : MonoBehaviour, IAction_Command
	{
		public enum ActionTypes
		{
			Unset,
			Idle,
			Move,
			UseItem,
			Interact,
			CastAbility,
			Cancel
		}

		public abstract void Execute(params object[] commandData);
	}
}