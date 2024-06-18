using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VartraAbyss.Managers;

namespace VartraAbyss.Stats
{
	public class LevellingSystem : MonoBehaviour
	{
		[SerializeField] private int m_currentLevel;
		[SerializeField] private int m_currentXp;
		[SerializeField] private int m_currentXPThreshold;
		[SerializeField] private float m_xpThresholdMultiplier;

		public int CurrentLevel => m_currentLevel;
		public int CurrentXP => m_currentXp;
		public int CurrentXPThreshold => m_currentXPThreshold;
		public float XPThresholdMultiplier => m_xpThresholdMultiplier;

		private void OnEnable()
		{
			EventManager.OnGainXPEvent += AddXP;
		}

		private void OnDisable()
		{
			EventManager.OnGainXPEvent -= AddXP;
		}

		public void AddXP(int xpGained)
		{
			m_currentXp += xpGained;

			if( m_currentXp >= m_currentXPThreshold )
			{
				LevelUp();
			}

		}

		private void LevelUp()
		{
			m_currentLevel++;
			m_currentXp -= m_currentXPThreshold;
			m_currentXPThreshold = (int)( m_currentXPThreshold * m_xpThresholdMultiplier );

			if( m_currentXp >= m_currentXPThreshold )
			{
				LevelUp();
			}

			EventManager.OnRefreshStatsEvent?.Invoke(Global.OnGetPlayerEvent?.Invoke());
		}
	}
}