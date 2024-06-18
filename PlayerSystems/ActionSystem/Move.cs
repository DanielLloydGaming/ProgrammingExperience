using UnityEngine;
using VartraAbyss.Entity;

namespace VartraAbyss.Actions
{
	public class Move : Action, IAction_Command
	{
		public override void Execute(params object[] commandData)
		{
			if( commandData[0] is Actor self )
			{
				Debug.Log("===Current State is Moving.===");
				self.Agent.SetDestination(self.Target);

				if( !self.Agent.pathPending )
				{
					if( self.Agent.remainingDistance <= self.Agent.stoppingDistance )
					{
						if( !self.Agent.hasPath || self.Agent.velocity.sqrMagnitude == 0f )
						{
							self.SetIsMoving(false);
							self.SetCurrentAction(ActionTypes.Idle);
						}
					}
					else if( self.IsAttacking && self.Agent.remainingDistance <= self.CurrentAbility.Range )
					{
						self.SetIsMoving(false);
						self.Agent.ResetPath();
						self.SetCurrentAction(ActionTypes.CastAbility);
						self.SetIsAttacking(false);
					}
				}
			}
		}
	}
}