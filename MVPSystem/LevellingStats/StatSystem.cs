using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VartraAbyss.Entity;
using VartraAbyss.Managers;

namespace VartraAbyss.Stats
{
	public class StatSystem : MonoBehaviour
	{
		[SerializeField] private StatScriptableObject m_playerStats;
		[SerializeField] private int m_vitality;
		[SerializeField] private int m_baseVitality;
		[SerializeField] private int m_mind;
		[SerializeField] private int m_baseMind;
		[SerializeField] private int m_strength;
		[SerializeField] private int m_baseStrength;
		[SerializeField] private int m_dexterity;
		[SerializeField] private int m_baseDexterity;

		[SerializeField] private float m_vitalityMultiplier;
		[SerializeField] private float m_mindMultiplier;
		[SerializeField] private float m_strengthMultiplier;
		[SerializeField] private float m_dexterityMultiplier;

		[SerializeField] private int m_initialStatPointPool;
		[SerializeField] private int m_currentStatPointPool;
		[SerializeField] private int m_statPointPoolOnLevelUp;

		public int Vitality => m_playerStats.vitality;
		public int BaseVitality => m_baseVitality;
		public int Mind => m_playerStats.mind;
		public int BaseMind => m_baseMind;
		public int Strength => m_playerStats.strength;
		public int BaseStrength => m_baseStrength;
		public int Dexterity => m_playerStats.dexterity;
		public int BaseDexterity => m_baseDexterity;

		private void OnEnable()
		{
			EventManager.OnLevelUpEvent += CalculateStats;
			EventManager.OnRefreshStatsEvent += CalculateStats;
		}

		private void OnDisable()
		{
			EventManager.OnLevelUpEvent -= CalculateStats;
			EventManager.OnRefreshStatsEvent -= CalculateStats;
		}

		public void AssignRandomStats()
		{
			m_currentStatPointPool = m_initialStatPointPool;

			m_vitality = Random.Range(5 , m_currentStatPointPool / 2);
			m_currentStatPointPool -= m_vitality;

			m_mind = Random.Range(5 , m_currentStatPointPool / 2);
			m_currentStatPointPool -= m_mind;

			m_strength = Random.Range(5 , m_currentStatPointPool / 2);
			m_currentStatPointPool -= m_strength;

			m_dexterity = m_currentStatPointPool;
			m_currentStatPointPool -= m_dexterity;
			EventManager.OnRefreshStatsEvent?.Invoke(Global.OnGetPlayerEvent?.Invoke());
			Logger();
		}

		public void CalculateStats(Actor self)
		{
			self.Stat.MaximumHealth = (int)( m_baseVitality * m_vitalityMultiplier );
			self.Stat.MaximumBlood = (int)( m_baseMind * m_mindMultiplier );
			self.Stat.MaximumMoveSpeed = (int)( m_baseDexterity * m_dexterityMultiplier );

			self.Stat.Health = self.Stat.MaximumHealth;
			self.Stat.Blood = self.Stat.MaximumBlood;
			self.Stat.MoveSpeed = self.Stat.MaximumMoveSpeed;
		}

		private void Logger()
		{
			Debug.Log($"Player Vitality is: {m_vitality}. \n Player Mind is: {m_mind}. \n Player Strength is: {m_strength}. \n Player Dexterity is: {m_dexterity}.");
		}
	}
}