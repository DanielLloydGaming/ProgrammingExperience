using UnityEngine;

namespace VartraAbyss.Abilities
{
	public class Bite : Ability, IAbility_Strategy
	{
		public void UseAbility()
		{
			Debug.Log("Bite has been used.");
		}
	}
}