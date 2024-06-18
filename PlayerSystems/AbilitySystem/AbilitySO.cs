using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace VartraAbyss
{
	[CreateAssetMenu(fileName = "NewAbility" , menuName = "ScriptableObjects/Abilities/Ability")]
	public class AbilitySO : SerializedScriptableObject
	{
		public string abilityName;
		public Image icon;
		public AudioClip soundEffect;
		public ParticleSystem visualEffect;
		public int damage;
		public int bloodCost;
		public float coolDownTime;
		public float range;
		public Vector3 spawnPosition;
	}
}