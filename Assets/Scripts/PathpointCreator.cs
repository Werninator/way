using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathpointCreator : MonoBehaviour {

	public GameObject pathpoint;

	public Transform pathpointList;
	public Transform waypointList;
	public Transform player;

	void Start () {
		Transform pathFrom;
		Transform pathTo;

		pathFrom = player;

		foreach (Transform waypoint in waypointList) {
			pathTo = waypoint;

			createPathpoints(pathFrom, pathTo);

			pathFrom = pathTo;
		}
	}

	void createPathpoints(Transform pathFrom, Transform pathTo)
	{
		Vector3 distance = pathFrom.position - pathTo.position;
		Vector3 direction = distance / distance.magnitude;

		float step = 1f;

		if (direction.x != 0f) {
			if (Mathf.Sign(direction.x) == -1f) {
				for (float i = pathFrom.position.x; i < pathTo.position.x; i += step)
					createWaypoint(new Vector3(i, pathFrom.position.y, pathFrom.position.z));
			} else {
				for (float i = pathFrom.position.x; i > pathTo.position.x; i -= step)
					createWaypoint(new Vector3(i, pathFrom.position.y, pathFrom.position.z));
			}
		} else if (direction.y != 0f) {
			if (Mathf.Sign(direction.y) == -1f) {
				for (float i = pathFrom.position.y; i < pathTo.position.y; i += step) {
					GameObject p = createWaypoint(new Vector3(pathFrom.position.x, i, pathFrom.position.z));

					p.transform.eulerAngles = new Vector3(
						p.transform.eulerAngles.x,
						p.transform.eulerAngles.y,
						p.transform.eulerAngles.z + 90
					);
				}
			} else {
				for (float i = pathFrom.position.y; i > pathTo.position.y; i -= step) {
					GameObject p = createWaypoint(new Vector3(pathFrom.position.x, i, pathFrom.position.z));

					p.transform.eulerAngles = new Vector3(
						p.transform.eulerAngles.x,
						p.transform.eulerAngles.y,
						p.transform.eulerAngles.z + 90
					);
				}
			}
		}	
	}

	GameObject createWaypoint(Vector3 pos)
	{
		GameObject p = Instantiate(pathpoint, pos, Quaternion.identity);

		p.transform.parent = pathpointList;

		return p;
	}
}
