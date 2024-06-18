using Sirenix.OdinInspector;
using UnityEngine;

namespace VartraAbyss.Stats
{
	[System.Serializable]
	public class Stat
	{
		[ProgressBar(0 ,  100, ColorGetter = "GetHealthBarColor")]
		[SerializeField] private int m_health;
		[SerializeField] private int m_maximumHealth;
		[ProgressBar(0 , 10 , 0.7f , 0 , 0.7f , Segmented = true)]
		[SerializeField] private int m_blood;
		[SerializeField] private int m_maximumBlood;
		[Range(1 , 10)]
		[SerializeField] private float m_moveSpeed;
		[SerializeField] private float m_maximumMoveSpeed;

		public int Health { get => m_health; set => m_health = value; }
		public int MaximumHealth { get => m_maximumHealth; set => m_maximumHealth = value; }
		public int Blood { get => m_blood; set => m_blood = value; }
		public int MaximumBlood { get => m_maximumBlood; set => m_maximumBlood = value; }
		public float MoveSpeed { get => m_moveSpeed; set => m_moveSpeed = value; }
		public float MaximumMoveSpeed { get => m_maximumMoveSpeed; set => m_maximumMoveSpeed = value; }

		private Color GetHealthBarColor(float value)
		{
			return Color.Lerp(Color.red , Color.green , Mathf.Pow(value / 100f , 2));
		}


	}
}