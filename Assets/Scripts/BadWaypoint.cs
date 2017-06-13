using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadWaypoint : MonoBehaviour {

	public GameObject explosion;

	void OnTriggerEnter(Collider other)
	{
		if (!other.name.Equals("Player"))
			return;

		PlayerMovement p = other.gameObject.GetComponent<PlayerMovement>();

		if (!p.rdy())
			return;

		spawn();
	}

	void spawn()
	{
		Instantiate(explosion, transform.position, Quaternion.identity);
	}
}
