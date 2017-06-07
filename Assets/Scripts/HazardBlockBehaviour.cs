using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HazardBlockMove
{
	public enum Direction { None, Up, Right, Down, Left }

	public float time;
	public Direction dir;
}

public class HazardBlockBehaviour : MonoBehaviour
{
	public float speed;
	public float delay = 0f;
	public List<HazardBlockMove> moves;

	int moveIndex = 0;

	float timer = -1f;

	bool isDelay = false;

	void Start()
	{
		isDelay = delay != 0f;

		if (!isDelay)
			startMove();
		else
			timer = delay;
	}

	void Update()
	{
		handleTimer();
	}

	void FixedUpdate()
	{
		if (!isDelay)
			handleMovement();
	}

	void handleTimer()
	{
		if (timer == -1f)
			return;

		timer -= Time.deltaTime;

		if (timer <= 0) {
			timer = -1f;
			onTimer();
		}
	}

	void onTimer()
	{
		if (isDelay) {
			isDelay = false;
			startMove();
			return;
		}

		if (moveIndex == moves.Capacity - 1) {
			moveIndex = 0;
		} else {
			moveIndex++;
		}

		startMove();
	}

	void handleMovement()
	{
		HazardBlockMove currentMove = getCurrentMove();

		Vector3 addPos = new Vector3();

		switch (currentMove.dir) {
			case HazardBlockMove.Direction.Up:
				addPos.y += speed * Time.deltaTime;
				break;
			case HazardBlockMove.Direction.Right:
				addPos.x += speed * Time.deltaTime;
				break;
			case HazardBlockMove.Direction.Down:
				addPos.y -= speed * Time.deltaTime;
				break;
			case HazardBlockMove.Direction.Left:
				addPos.x -= speed * Time.deltaTime;
				break;
		}

		transform.position += addPos;
	}

	void startMove()
	{
		HazardBlockMove currentMove = getCurrentMove();

		timer = currentMove.time;
	}

	HazardBlockMove getCurrentMove()
	{
		return moves[moveIndex];
	}
}
