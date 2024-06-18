using UnityEngine;
using VartraAbyss.Abilities;
using VartraAbyss.Entity;
using VartraAbyss.Utility;

namespace VartraAbyss.Actions
{
	public class CastAbility : Action
	{
		private Timer m_abilityTimer;

		public override void Execute(params object[] commandData)
		{
			if( commandData[0] is Actor self && commandData[1] is Vector3 target )
			{
				Debug.Log("===Current State is Casting an Ability.===");
				if( m_abilityTimer == null )
				{
					m_abilityTimer = gameObject.AddComponent<Timer>();
					m_abilityTimer.SetTimer(self.CurrentAbility.CoolDownTime);
				}

				if( m_abilityTimer.CurrentTime <= 0 )
				{
					if( self.CurrentAbility is IAbility_Strategy strategy )
					{
						strategy.UseAbility();
					}

					m_abilityTimer.ResetTimer();
					self.SetCurrentAction(ActionTypes.Idle);
				}

				self.SetCurrentAction(ActionTypes.Idle);
				//target.Stat.Health -= self.CurrentAbility.Damage;
			}
		}
	}
}