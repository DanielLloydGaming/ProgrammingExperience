using AYellowpaper.SerializedCollections;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AI;
using VartraAbyss.Abilities;
using VartraAbyss.Actions;
using VartraAbyss.Managers;
using VartraAbyss.PlayerInputs;
using VartraAbyss.Stats;

namespace VartraAbyss.Entity.Player
{
	[RequireComponent(typeof(PlayerInputController))]
	[RequireComponent(typeof(Stat))]
	[RequireComponent(typeof(NavMeshAgent))]
	public class PlayerBehaviour : Actor
	{
		[TabGroup("Actor" , "Actions" , SdfIconType.Activity , TextColor = "white")]
		private bool m_isSkillsMenuOpen;
		private GameObject m_skillToAbsorb;
		[SerializeField] private AbilitySO m_abilityData;

		private void OnEnable()
		{
			Global.OnGetPlayerEvent += StoreActor;
		}

		private void OnDisable()
		{
			Global.OnGetPlayerEvent -= StoreActor;
		}

		private void Awake()
		{
			m_isSkillsMenuOpen = false;
			m_skillToAbsorb = null;
			SetNavMeshAgent(GetComponent<NavMeshAgent>());
			SetCurrentAction(Action.ActionTypes.Idle);
			EventManager.OnLevelUpEvent?.Invoke(this);
		}

		private void Start()
		{
			SetupActions();

			if( CurrentAbility is IAbility_Strategy strategy )
			{
				strategy.UseAbility();
			}
		}

		protected override Actor StoreActor()
		{
			return this;
		}

		//TO DO: Probably should put the responsibility of the ability pick up on the ability itself.

		private void OnTriggerEnter(Collider other)
		{
			if( other.CompareTag("AbilityToAbsorb") )
			{
				EventManager.OnCanAbsorbAbility?.Invoke();
				m_skillToAbsorb = other.gameObject;
			}
		}

		private void OnTriggerExit(Collider other)
		{
			if( other.CompareTag("AbilityToAbsorb") )
			{
				EventManager.OnCannotAbsorbAbility?.Invoke();
				m_skillToAbsorb = null;
			}
		}

		private void FixedUpdate()
		{
			if( ListOfActions.TryGetValue(CurrentAction , out Action action) )
			{
				// 1st param is self, then a Vector, 
				action.Execute(this , Target);
			}
		}

		private void SetupActions()
		{
			GameObject actions = new("PlayerActions");
			actions.transform.SetParent(this.transform);
			ListOfActions = new SerializedDictionary<Action.ActionTypes , Action>
			{
				{ Action.ActionTypes.Idle , actions.AddComponent<Idle>() } ,
				{ Action.ActionTypes.Move , actions.AddComponent<Move>() } ,
				{ Action.ActionTypes.CastAbility , actions.AddComponent<CastAbility>() } ,
				{ Action.ActionTypes.UseItem , actions.AddComponent<UseItem>() } ,
				{ Action.ActionTypes.Interact , actions.AddComponent<Interact>() } ,
				{ Action.ActionTypes.Cancel , actions.AddComponent<Cancel>() }
			};

			SetCurrentAction(Action.ActionTypes.Idle);
		}

		public override void SetTarget(Vector3 newTarget)
		{
			base.SetTarget(newTarget);
		}

		public override void SetCurrentAbility(Ability ability)
		{
			base.SetCurrentAbility(ability);
		}
	}
}