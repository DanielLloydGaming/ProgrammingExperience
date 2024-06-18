using System.Collections;
using UnityEngine;

namespace VartraAbyss.Utility
{
	public class Timer : MonoBehaviour
	{
		public float CurrentTime { get; private set; }
		public float TargetTime { get; private set; }
		public bool IsProcessing { get; private set; }

		public Timer(float targetTime)
		{
			TargetTime = targetTime;
			CurrentTime = TargetTime;
		}

		private IEnumerator ProcessTime()
		{
			yield return new WaitForSeconds(CurrentTime);
			Execute();
		}

		private void FixedUpdate()
		{
			if( CurrentTime <= 0 )
			{
				return;
			}
			else
			{
				CurrentTime -= Time.fixedDeltaTime;
			}
		}

		public void StartTimer()
		{
			if( IsProcessing )
			{
				StopCoroutine(ProcessTime());
			}

			StartCoroutine(ProcessTime());
		}

		public void StopTimer()
		{
			StopCoroutine(ProcessTime());
		}

		public void ResetTimer()
		{
			CurrentTime = TargetTime;
		}

		public void SetTimer(float targetTime)
		{
			TargetTime = targetTime;
		}

		public void Execute()
		{
			// Execute a command.
		}
	}
}