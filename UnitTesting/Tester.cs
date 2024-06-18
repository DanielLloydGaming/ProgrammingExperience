using System.Collections.Generic;
using UnityEngine;
using VartraAbyss.Actions;

namespace VartraAbyss.UnitTest
{
	public class Tester : MonoBehaviour
	{
		public static List<Action> ActionsToCheck = new()
		{
			new Idle() ,
			new Move() ,
			new CastAbility()
		};

		public List<Action> GetAllActions()
		{
			return ActionsToCheck;
		}
	}
}