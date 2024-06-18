using UnityEngine;

namespace VartraAbyss.Abilities
{
	public class BloodProjectile : Ability, IAbility_Strategy
	{
		public void UseAbility()
		{
			Debug.Log("Blood Projectile has been used.");
		}
	}
}