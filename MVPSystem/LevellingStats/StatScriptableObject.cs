using UnityEngine;

namespace VartraAbyss.Stats
{
	[CreateAssetMenu(fileName = "NewStat" , menuName = "ScriptableObjects/Stats/Stat")]
	public class StatScriptableObject : ScriptableObject
    {
		[SerializeField] public int vitality;
		[SerializeField] public int mind;
		[SerializeField] public int strength;
		[SerializeField] public int dexterity;

		[SerializeField] public int health;
		[SerializeField] public int maximumHealth;
		[SerializeField] public int blood;
		[SerializeField] public int maximumBlood;
		[SerializeField] public float moveSpeed;
		[SerializeField] public float maximumMoveSpeed;

		[SerializeField] public int currentStatPointPool;

		public StatScriptableObject(int vitality, int mind, int strength, int dexterity, int health,
								int maximumHealth, int blood, int maximumBlood, float moveSpeed,
								float maximumMoveSpeed, int currentStatPoints)
		{
			this.vitality = vitality;
			this.mind = mind;
			this.strength = strength;
			this.dexterity = dexterity;
			this.health = health;
			this.maximumHealth = maximumHealth;
			this.blood = blood;
			this.maximumBlood = maximumBlood;
			this.moveSpeed = moveSpeed;
			this.maximumMoveSpeed = maximumMoveSpeed;
			currentStatPointPool = currentStatPoints;
		}
	}
}