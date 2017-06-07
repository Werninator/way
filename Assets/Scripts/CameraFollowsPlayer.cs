using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowsPlayer : MonoBehaviour
{
	public Transform player;

	Camera cam;

	float height;
	float width;

	void Start()
	{
		cam = GetComponent<Camera>();

		height = 2f * cam.orthographicSize;
		width = height * cam.aspect;
	}

	void Update() {
		if (!playerInSight())
			adjustPosition();
	}

	bool playerInSight()
	{
		float left = transform.position.x - width / 2;
		float right = transform.position.x + width / 2;
		float top = transform.position.y + height / 2;
		float bottom = transform.position.y - height / 2;

		return player.position.x > left && player.position.x < right && player.position.y < top && player.position.y > bottom;
	}

	void adjustPosition()
	{
		Vector3 newVec = new Vector3(
			Mathf.Floor((player.position.x + width / 2) / width) * width,
			Mathf.Floor((player.position.y + height / 2) / height) * height,
			transform.position.z
		);

		transform.position = newVec;
	}
}
