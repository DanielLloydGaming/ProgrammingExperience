using UnityEngine;
using UnityEngine.InputSystem;
using VartraAbyss.Entity;
using VartraAbyss.Entity.Player;
using VartraAbyss.Managers;
using VartraAbyss.Utility;
using static VartraAbyss.Actions.Action;


namespace VartraAbyss.PlayerInputs
{
	public class PlayerInputController : MonoBehaviour
	{
		[SerializeField] private PlayerInput m_playerControl;
		[SerializeField] private PlayerBehaviour m_player;
		[SerializeField] private LayerMask m_ignorePlayerLayer;

		private Vector3 m_clickPoint;
		public LayerMask IgnorePlayerLayer => m_ignorePlayerLayer;
		public Vector3 ClickPoint { get => m_clickPoint; set => m_clickPoint = value; }
		public PlayerInput PlayerControl => m_playerControl;
		public PlayerBehaviour Player => m_player;

		public delegate Vector3 PlayerClickEvent();
		public static PlayerClickEvent OnPlayerClick;

		private void OnEnable()
		{
			m_playerControl.actions.FindAction("Primary").started += OnPrimaryInputCommand;
			m_playerControl.actions.FindAction("Primary").performed += OnPrimaryInputCommand;
			m_playerControl.actions.FindAction("Ability1").performed += OnAbilityOnePressed;
			m_playerControl.actions.FindAction("Primary").canceled += OnPrimaryInputCommand;
			OnPlayerClick += OnPrimaryInput;
		}

		private void OnDisable()
		{
			m_playerControl.actions.FindAction("Primary").started -= OnPrimaryInputCommand;
			m_playerControl.actions.FindAction("Primary").performed -= OnPrimaryInputCommand;
			m_playerControl.actions.FindAction("Ability1").performed -= OnAbilityOnePressed;
			m_playerControl.actions.FindAction("Primary").canceled -= OnPrimaryInputCommand;
			OnPlayerClick -= OnPrimaryInput;
		}

		private bool IsWithinAbilityRange(Actor actor1 , GameObject actor2)
		{
			return Utilities.GetDistanceBetweenTwoActors(actor1.gameObject , actor2) > actor1.CurrentAbility.Range;
		}

		private void OnPrimaryInputCommand(InputAction.CallbackContext context)
		{
			if( context.performed )
			{
				OnPrimaryInput();
			}
		}

		private void OnAbilityOnePressed(InputAction.CallbackContext context)
		{
			if( context.performed )
			{
				EventManager.OnQAbility?.Invoke();
				Debug.Log("Q Button has been pressed and Invoked.");
			}
		}

		private Vector3 OnPrimaryInput()
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			Actor player = Global.OnGetPlayerEvent?.Invoke();

			if( Physics.Raycast(ray , out RaycastHit hit , IgnorePlayerLayer) )
			{
				player.SetTarget(hit.point);

				if( hit.collider.GetComponent<Entity.Enemy.EnemyBehaviour>() != null )
				{
					player.SetTarget(hit.collider.GetComponent<Entity.Enemy.EnemyBehaviour>().transform.position);

					if( IsWithinAbilityRange(gameObject.GetComponent<Actor>() , hit.collider.gameObject) )
					{
						player.SetIsMoving(true);
						player.SetIsAttacking(true);
						player.SetCurrentAction(ActionTypes.Move);
						return player.Target;
					}
					else
					{
						player.SetIsMoving(false);
						player.SetCurrentAction(ActionTypes.CastAbility);
						return player.Target;
					}
				}
				else
				{
					player.SetIsMoving(true);
					player.SetCurrentAction(ActionTypes.Move);
					return player.Target;
				}
			}

			return player.Target;
		}
	}
}