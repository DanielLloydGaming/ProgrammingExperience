using UnityEngine;

namespace VartraAbyss.Abilities
{
	public class Claw : Ability, IAbility_Strategy
	{
		public void UseAbility()
		{
			Debug.Log("Claw has been used.");
		}
	}
}