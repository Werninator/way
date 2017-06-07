using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
	public Transform waypointList;
	Transform nextWaypoint;

	public KeyCode up;
	public KeyCode right;
	public KeyCode down;
	public KeyCode left;

	public Material falseMovementMaterial;

	public float speed;

	float hspeed;
	float vspeed;
	bool onMyWay;

	float errorTimer = .2f;
	float currentErrorTimer = -1f;

	Renderer rend;
	Material defaultMaterial;

	Vector3 posBefore;

	KeyCode bufferedInput = KeyCode.None;

	PlayerStats playerStats;

	void Start()
	{
		rend = GetComponent<Renderer>();
		defaultMaterial = rend.material;

		playerStats = (PlayerStats) FindObjectOfType(typeof(PlayerStats));
	}
	
	void Update()
	{
		if (currentErrorTimer != -1f) {
			shake();
		}

		handleTimers();
		checkInputs();

		if (nextWaypoint == null && waypointList.childCount > 0)
			nextWaypoint = waypointList.GetChild(0);

		if (nextWaypoint == null && waypointList.childCount == 0) {
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
		}

		if (nextWaypoint != null && !onMyWay)
			handleInputs();
	}

	void FixedUpdate()
	{
		if (onMyWay) {
			transform.position = Vector3.MoveTowards(transform.position, nextWaypoint.position, Time.deltaTime * speed);
			checkIfAtEnd();
		}
	}

	void checkInputs()
	{
		if (Input.GetKeyDown(up))
			bufferedInput = up;

		if (Input.GetKeyDown(right))
			bufferedInput = right;

		if (Input.GetKeyDown(down))
			bufferedInput = down;

		if (Input.GetKeyDown(left))
			bufferedInput = left;
	}

	void handleTimers()
	{
		if (currentErrorTimer != -1f) {
			currentErrorTimer -= Time.deltaTime;

			if (currentErrorTimer <= 0f) {
				currentErrorTimer = -1f;
				onErrorTimer();
			}
		}
	}

	void shake()
	{
		float shakeAmount = 1f;

		transform.position = posBefore;

		transform.position += new Vector3(
			Random.Range(-shakeAmount, shakeAmount),
			Random.Range(-shakeAmount, shakeAmount),
			0
		);
	}

	void onErrorTimer()
	{
		rend.material = defaultMaterial;
		transform.position = posBefore;
	}

	void handleInputs() {
		Vector3 direction = getDirectionToWaypoint();

		if (currentErrorTimer != -1f || bufferedInput == KeyCode.None)
			return;

		if (bufferedInput == up) {
			if (direction.y == 1f) {
				onMyWay = true;
			} else
				errorInMovement();
		}

		if (bufferedInput == right) {
			if (direction.x == 1f) {
				onMyWay = true;
			} else
				errorInMovement();
		}

		if (bufferedInput == down) {
			if (direction.y == -1f) {
				onMyWay = true;
			} else
				errorInMovement();
		}

		if (bufferedInput == left) {
			if (direction.x == -1f) {
				onMyWay = true;
			} else
				errorInMovement();
		}

		bufferedInput = KeyCode.None;
	}

	void errorInMovement()
	{
		currentErrorTimer = errorTimer;
		rend.material = falseMovementMaterial;

		posBefore = transform.position;

		playerStats.hurtPlayer(1);
	}

	Vector3 getDirectionToWaypoint()
	{
		Vector3 direction = nextWaypoint.position - transform.position;
		float distance = direction.magnitude;

		direction = direction / distance;

		return direction;
	}

	void checkIfAtEnd()
	{
		if (Vector3.Distance(transform.position, nextWaypoint.position) != 0f)
			return;

		onMyWay = false;
		Destroy(nextWaypoint.gameObject);
	}

	void OnTriggerEnter(Collider other)
	{
		if (currentErrorTimer != -1)
			return;

		if (other.gameObject.name.Contains("Pathpoint")) {
			Destroy(other.gameObject);
			return;
		}

		if (other.gameObject.name.Contains("MovingBlock")) {
			errorInMovement();

			// Reload the level
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}
	}
}