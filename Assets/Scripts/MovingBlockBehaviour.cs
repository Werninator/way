using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBlockBehaviour : MonoBehaviour
{
	public float pixelPerSecond;

	public float timeMovingLeft;
	public float timeMovingRight;

	public bool moveLeftFirst;
	public bool startMovingImmediately= false;

	float timer = -1f;

	void Start()
	{
		if (startMovingImmediately)
			timer = (moveLeftFirst ? timeMovingLeft : timeMovingRight) / 2;
	}

	void Update()
	{
		handleTimer();
	}

	void FixedUpdate()
	{
		handleMovement();
	}

	void handleTimer()
	{
		if (timer != -1f) {
			timer -= Time.deltaTime;

			if (timer <= 0f) {
				timer = -1f;
				onTimer();
			}
		}
	}

	void onTimer()
	{
		moveLeftFirst = !moveLeftFirst;

		float timerToSet = moveLeftFirst ? timeMovingLeft : timeMovingRight;

		timer = timerToSet;
	}

	void handleMovement()
	{
		if (timer == -1f)
			return;

		int factor = moveLeftFirst ? -1 : 1;

		Vector3 movement = new Vector3(pixelPerSecond * Time.deltaTime * factor, 0, 0);

		transform.position += movement;
	}
}
