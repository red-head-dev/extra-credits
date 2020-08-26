using UnityEngine;
using System.Collections;

namespace Pathfinding {
	/// <summary>
	/// Simple patrol behavior.
	/// This will set the destination on the agent so that it moves through the sequence of objects in the <see cref="targets"/> array.
	/// Upon reaching a target it will wait for <see cref="delay"/> seconds.
	///
	/// See: <see cref="Pathfinding.AIDestinationSetter"/>
	/// See: <see cref="Pathfinding.AIPath"/>
	/// See: <see cref="Pathfinding.RichAI"/>
	/// See: <see cref="Pathfinding.AILerp"/>
	/// </summary>
	[UniqueComponent(tag = "ai.destination")]
	public class MonsterBehavior: MonoBehaviour {
		/// <summary>Target points to move to in order</summary>
		public Transform[] targets;

		/// <summary>Time in seconds to wait at each target</summary>
		public float delay = 0;
		public float visionRange = 1;

		/// <summary>Current target index</summary>
		int index;

		IAstarAI agent;
		float switchTime = float.PositiveInfinity;

		private Transform ajksldfjsd;

		void Start() {
			agent = GetComponent < IAstarAI > ();
		}

		/// <summary>Update is called once per frame</summary>
		void Update() {

			Transform player = GameObject.FindWithTag("Player").transform;

			if (Vector2.Distance(player.position, transform.position) < visionRange) {
				Debug.Log("Saw Player");
				agent.destination = player.position;
			} else {

				if (targets.Length == 0) return;

				bool search = false;

				// Note: using reachedEndOfPath and pathPending instead of reachedDestination here because
				// if the destination cannot be reached by the agent, we don't want it to get stuck, we just want it to get as close as possible and then move on.
				if (agent.reachedEndOfPath && !agent.pathPending && float.IsPositiveInfinity(switchTime)) {
					switchTime = Time.time + delay;
				}

				if (Time.time >= switchTime) {
					index = index + 1;
					search = true;
					switchTime = float.PositiveInfinity;
				}

				index = index % targets.Length;
				agent.destination = targets[index].position;

				if (search) agent.SearchPath();
			}
		}
	}
}
