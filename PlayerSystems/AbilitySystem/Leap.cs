namespace VartraAbyss.Abilities
{
	public class Leap : Ability, IAbility_Strategy
	{
		public void UseAbility()
		{
			UnityEngine.Debug.Log("Used Leap Ability");
		}
	}
}