using AYellowpaper.SerializedCollections;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AI;
using VartraAbyss.Abilities;
using VartraAbyss.Actions;
using VartraAbyss.Stats;

namespace VartraAbyss.Entity
{
	public abstract class Actor : SerializedMonoBehaviour
	{
		[TabGroup("Actor" , "Stats" , SdfIconType.GraphUp , TextColor = "blue")]
		[SerializeField] protected Stat stat;

		[TabGroup("Actor" , "Actions" , SdfIconType.Activity , TextColor = "white")]
		[DictionaryDrawerSettings(DisplayMode = DictionaryDisplayOptions.OneLine)]
		[SerializeField] protected SerializedDictionary<Action.ActionTypes , Action> listOfActions = new();

		[TabGroup("Actor" , "Abilities" , SdfIconType.Magic , TextColor = "purple")]
		[SerializeField] protected SerializedDictionary<string , Ability> listOfAbilities = new();

		public bool IsMoving { get; private set; }
		public bool IsAttacking { get; private set; }
		public Stat Stat { get; private set; }

		public Vector3 Target { get; private set; }
		public Ability CurrentAbility { get; private set; }
		public ItemBase CurrentItem { get; private set; }
		public NavMeshAgent Agent { get; private set; }
		public Action.ActionTypes CurrentAction { get; private set; }
		public SerializedDictionary<Action.ActionTypes , Action> ListOfActions { get => listOfActions; set => listOfActions = value; }
		public SerializedDictionary<string , Ability> ListOfAbilities { get; private set; }

		protected virtual Actor StoreActor() { return this; }
		public virtual void SetTarget(Vector3 newTarget) { Target = newTarget; }
		public virtual void SetCurrentAbility(Ability ability) { CurrentAbility = ability; }
		public virtual void SetCurrentAction(Action.ActionTypes action) { CurrentAction = action; }
		public virtual void SetCurrentItem(ItemBase item) { CurrentItem = item; }
		public virtual void SetIsMoving(bool enabled) { IsMoving = enabled; }
		public virtual void SetIsAttacking(bool enabled) { IsAttacking = enabled; }
		public virtual void SetNavMeshAgent(NavMeshAgent agent) { Agent = agent; }
		public virtual void AddToActionList(Action.ActionTypes type , Action actionToAdd) { listOfActions.TryAdd(type , actionToAdd); }
		public virtual void RemoveFromActionList(Action.ActionTypes type , Action actionToAdd) { listOfActions.Remove(type); }
		public virtual void ClearActionList() { listOfActions.Clear(); }
		public virtual void AddToAbilityList(Ability abilityToAdd) { listOfAbilities.TryAdd(abilityToAdd.Name , abilityToAdd); }
		public virtual void RemoveFromAbilityList(Ability abilityToRemove) { listOfAbilities.Remove(abilityToRemove.Name); }
		public virtual void ClearAbilityList() { listOfAbilities.Clear(); }
		public virtual void SpawnActor(params object[] spawnData) { }
		public virtual void Die(params object[] deathData) { }
		public virtual void DespawnActor(params object[] despawnData) { }
	}
}