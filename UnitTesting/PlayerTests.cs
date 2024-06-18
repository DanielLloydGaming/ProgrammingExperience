using AYellowpaper.SerializedCollections;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using UnityEngine.TestTools;
using VartraAbyss.Actions;
using VartraAbyss.Entity.Player;
using VartraAbyss.PlayerInputs;
using VartraAbyss.Stats;

namespace VartraAbyss.UnitTest
{
	public class PlayerTests
	{
		private const string TEST_ITEM = "Player";
		private const string TESTER = "Tester";
		private GameObject m_player;
		private GameObject m_tester;
		private bool m_isSceneLoaded;

		[UnitySetUp]
		public IEnumerator SetUp()
		{
			if( !m_isSceneLoaded )
			{
				EditorSceneManager.OpenScene("Assets/_VartraAbyss/Scenes/EditorTestScenes/PlayerSetUpTestScene.unity");

				m_player = GameObject.Find(TEST_ITEM);
				m_tester = GameObject.Find(TESTER);
				m_player.GetComponent<PlayerBehaviour>().SetCurrentAction(Action.ActionTypes.Idle);
				m_player.GetComponent<PlayerBehaviour>().SetNavMeshAgent(m_player.GetComponent<NavMeshAgent>());
				m_isSceneLoaded = true;
				yield return null;
			}
		}

		[Test]
		public void PlayerHasInputComponent()
		{
			bool result = m_player.TryGetComponent(out PlayerInput component);
			Assert.IsTrue(result);
		}

		[Test]
		public void PlayerHasInputControllerComponent()
		{
			bool result = m_player.TryGetComponent(out PlayerInputController component);
			Assert.IsTrue(result);
		}

		[Test]
		public void PlayerInputControllerInputAssigned()
		{
			bool result = m_player.GetComponent<PlayerInputController>().PlayerControl;
			Assert.IsTrue(result);
		}

		[Test]
		public void PlayerInputControllerPlayerBehaviourAssigned()
		{
			bool result = m_player.GetComponent<PlayerInputController>().PlayerControl;
			Assert.IsTrue(result);
		}

		[Test]
		public void PlayerHasInputActionsAssigned()
		{
			bool result = m_player.GetComponent<PlayerInput>().actions;
			Assert.IsTrue(result);
		}

		[Test]
		public void PlayerHasInputNotificationBehaviourAssigned()
		{
			bool result = m_player.GetComponent<PlayerInput>().notificationBehavior == PlayerNotifications.InvokeCSharpEvents;
			Assert.IsTrue(result);
		}

		[Test]
		public void PlayerHasNavMeshAgentComponent()
		{
			bool result = m_player.TryGetComponent(out NavMeshAgent component);
			Assert.IsTrue(result);
		}

		[Test]
		public void PlayerNavMeshAgentAssigned()
		{
			bool result = m_player.GetComponent<PlayerBehaviour>().Agent;
			Assert.IsTrue(result);
		}

		[Test]
		public void PlayerHasCharacterControllerComponent()
		{
			bool result = m_player.TryGetComponent(out CharacterController component);
			Assert.IsTrue(result);
		}

		[Test]
		public void PlayerHasCorrectTag()
		{
			bool result = m_player.CompareTag("Player");
			Assert.IsTrue(result);
		}

		[Test]
		public void PlayerHasPlayerBehaviourComponent()
		{
			bool result = m_player.TryGetComponent(out PlayerBehaviour component);
			Assert.IsTrue(result);
		}

		[Test]
		public void PlayerHasStatsGreaterThanZero()
		{
			Stat results = m_player.GetComponent<PlayerBehaviour>().Stat;

			bool result = AreAllStatsGreaterThanZero(results);

			bool AreAllStatsGreaterThanZero(Stat stats) => new[]
			{
				stats.Health,
				stats.MaximumHealth,
				stats.Blood,
				stats.MaximumBlood,
				stats.MoveSpeed,
				stats.MaximumMoveSpeed
			}
			.All(stat => stat > 0);

			Assert.IsTrue(result);
		}

		[Test]
		public void PlayerHasDefaultActionSetToIdle()
		{
			bool result = m_player.GetComponent<PlayerBehaviour>().CurrentAction == Action.ActionTypes.Idle;

			Assert.IsTrue(result);
		}

		[Test]
		public void PlayerHasRequiredActions()
		{
			var listToCheck = m_player.GetComponent<PlayerBehaviour>().ListOfActions;
			List<Action> actionsToTest = m_tester.GetComponent<Tester>().GetAllActions();

			bool result = DictionaryContainsActions(listToCheck , actionsToTest);

			bool DictionaryContainsActions(SerializedDictionary<Action.ActionTypes , Action> dictionary , List<Action> actions)
			{
				foreach( var action in actions )
				{
					if( !dictionary.Values.Any(val => val.GetType() == action.GetType()) )
					{
						return false;
					}
				}
				return true;
			}

			Assert.IsTrue(result);
		}

		[Test]
		public void PlayerInputControllerHasCorrectlySetLayerMask()
		{
			LayerMask layerMask = m_player.GetComponent<PlayerInputController>().IgnorePlayerLayer;

			bool result = LayerMaskContainsLayers(layerMask , "Default" , "TransparentFX" , "Ignore Raycast" , "Water" , "UI" , "Item");

			bool LayerMaskContainsLayers(LayerMask layerMask , params string[] layers)
			{
				int maskValue = layerMask.value;

				foreach( string layer in layers )
				{
					int layerIndex = LayerMask.NameToLayer(layer);
					int layerMaskValue = 1 << layerIndex;

					if( ( maskValue & layerMaskValue ) == 0 )
					{
						Debug.Log($"Missing Layer: {layer}");
						return false;
					}
				}

				return true;
			}

			Assert.IsTrue(result);
		}
	}
}