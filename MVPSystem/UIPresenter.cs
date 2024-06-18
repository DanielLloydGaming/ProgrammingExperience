using System;
using System.Collections.Generic;
using UnityEngine;
using VartraAbyss.Stats;

namespace VartraAbyss.UI
{
	public class UIPresenter : MonoBehaviour
	{
		[SerializeField] private StatScriptableObject m_statsToDisplay;
		[SerializeField] private UIViewer m_uiViewer;

		private Dictionary<string , Action<int>> m_statModifiers;
		private Dictionary<string , Action<bool>> m_increaseButtonToggles;
		private Dictionary<string , Action<bool>> m_decreaseButtonToggles;

		private void Start()
		{
			m_statModifiers = new Dictionary<string , Action<int>>()
		{
			{ "Vitality", delta => m_statsToDisplay.vitality += delta },
			{ "Mind", delta => m_statsToDisplay.mind += delta },
			{ "Strength", delta => m_statsToDisplay.strength += delta },
			{ "Dexterity", delta => m_statsToDisplay.dexterity += delta }
		};

			m_increaseButtonToggles = new Dictionary<string , Action<bool>>()
		{
			{ "Vitality", state => m_uiViewer.ToggleVitalityIncreaseButton(state) },
			{ "Mind", state => m_uiViewer.ToggleMindIncreaseButton(state) },
			{ "Strength", state => m_uiViewer.ToggleStrengthIncreaseButton(state) },
			{ "Dexterity", state => m_uiViewer.ToggleDexterityIncreaseButton(state) }
		};

			m_decreaseButtonToggles = new Dictionary<string , Action<bool>>()
		{
			{ "Vitality", state => m_uiViewer.ToggleVitalityDecreaseButton(state) },
			{ "Mind", state => m_uiViewer.ToggleMindDecreaseButton(state) },
			{ "Strength", state => m_uiViewer.ToggleStrengthDecreaseButton(state) },
			{ "Dexterity", state => m_uiViewer.ToggleDexterityDecreaseButton(state) }
		};

			RefreshUI();
		}

		public void ModifyStat(string statName , int delta)
		{
			if( m_statModifiers.ContainsKey(statName) )
			{
				m_statModifiers[statName](delta);
				m_statsToDisplay.currentStatPointPool -= delta;
				RefreshUI();
			}
		}

		public void IncreaseVitality() => ModifyStat("Vitality" , 1);
		public void DecreaseVitality() => ModifyStat("Vitality" , -1);
		public void IncreaseMind() => ModifyStat("Mind" , 1);
		public void DecreaseMind() => ModifyStat("Mind" , -1);
		public void IncreaseStrength() => ModifyStat("Strength" , 1);
		public void DecreaseStrength() => ModifyStat("Strength" , -1);
		public void IncreaseDexterity() => ModifyStat("Dexterity" , 1);
		public void DecreaseDexterity() => ModifyStat("Dexterity" , -1);

		private void RefreshUI()
		{
			m_uiViewer.ToggleAllDecreaseButtons(true);
			m_uiViewer.ToggleAllIncreaseButtons(true);

			m_uiViewer.UpdateUI(
				m_statsToDisplay.vitality ,
				m_statsToDisplay.mind ,
				m_statsToDisplay.strength ,
				m_statsToDisplay.dexterity ,
				m_statsToDisplay.currentStatPointPool
			);

			if( m_statsToDisplay.currentStatPointPool <= 0 )
			{
				m_uiViewer.ToggleAllIncreaseButtons(false);
			}

			ToggleStatButtons(m_statsToDisplay.vitality , "Vitality");
			ToggleStatButtons(m_statsToDisplay.mind , "Mind");
			ToggleStatButtons(m_statsToDisplay.strength , "Strength");
			ToggleStatButtons(m_statsToDisplay.dexterity , "Dexterity");
		}

		private void ToggleStatButtons(int statValue , string statName)
		{
			if( m_increaseButtonToggles.ContainsKey(statName) && m_decreaseButtonToggles.ContainsKey(statName) )
			{
				m_increaseButtonToggles[statName](statValue < 20);
				m_decreaseButtonToggles[statName](statValue > 0);
			}
		}
	}
}