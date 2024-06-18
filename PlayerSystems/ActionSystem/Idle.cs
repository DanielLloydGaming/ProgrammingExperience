using UnityEngine;

namespace VartraAbyss.Actions
{
	public class Idle : Action
	{
		public override void Execute(params object[] commandData)
		{
			Debug.Log("===Current State is Idle.===");
			return;
		}
	}
}