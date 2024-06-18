using UnityEngine;
using UnityEngine.UI;
using VartraAbyss.Utility;

namespace VartraAbyss.Abilities
{
	public class Ability : MonoBehaviour
	{
		public AbilitySO AbilityData { get; private set; }
		public string Name { get; private set; }
		public Image Icon { get; private set; }
		public AudioClip SoundEffect { get; private set; }
		public ParticleSystem VisualEffect { get; private set; }
		public int Damage { get; private set; }
		public int BloodCost { get; private set; }
		public float CoolDownTime { get; private set; }
		public float Range { get; private set; }
		public Vector3 SpawnPosition { get; private set; }
		public Timer Timer { get; private set; }

		#region Ability Builder
		public class Builder<T> where T : Ability, new()
		{
			AbilitySO m_data;
			string m_name = "Default";
			Image m_icon;
			AudioClip m_audioClip;
			ParticleSystem m_particles;
			int m_damage = 1;
			int m_bloodCost = 1;
			float m_coolDownTime = 1;
			float m_range = 1;
			Vector3 m_spawnPosition = Vector3.zero;
			Timer m_timer;

			public Builder<T> SetAbilityData(AbilitySO data)
			{
				m_data = data;
				return this;
			}

			public Builder<T> SetName(string name)
			{
				m_name = name;
				return this;
			}

			public Builder<T> SetIcon(Image icon)
			{
				m_icon = icon;
				return this;
			}

			public Builder<T> SetAudio(AudioClip audioClip)
			{
				m_audioClip = audioClip;
				return this;
			}

			public Builder<T> SetVFX(ParticleSystem particle)
			{
				m_particles = particle;
				return this;
			}

			public Builder<T> SetDamage(int damage)
			{
				m_damage = damage;
				return this;
			}

			public Builder<T> SetBloodCost(int cost)
			{
				m_bloodCost = cost;
				return this;
			}

			public Builder<T> SetCoolDownTime(float time)
			{
				m_coolDownTime = time;
				return this;
			}

			public Builder<T> SetRange(float range)
			{
				m_range = range;
				return this;
			}

			public Builder<T> SetSpawnPosition(Vector3 position)
			{
				m_spawnPosition = position;
				return this;
			}

			public Builder<T> SetTimer(float time)
			{
				m_timer = new Timer(time);
				return this;
			}

			public T Build()
			{
				T ability = new GameObject(m_name).AddComponent<T>();
				ability.AbilityData = m_data;
				ability.Name = m_name;
				ability.Icon = m_icon;
				ability.SoundEffect = m_audioClip;
				ability.VisualEffect = m_particles;
				ability.Damage = m_damage;
				ability.BloodCost = m_bloodCost;
				ability.CoolDownTime = m_coolDownTime;
				ability.Range = m_range;
				ability.SpawnPosition = m_spawnPosition;
				ability.Timer = m_timer;
				return ability;
			}
		}

		#endregion
	}
}