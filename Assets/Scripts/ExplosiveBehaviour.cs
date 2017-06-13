using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBehaviour : MonoBehaviour {
	public Material explodeMaterial;

	float timer = .5f;

	SphereCollider sphereCollider;
	Renderer rend;

	void Start()
	{
		sphereCollider = GetComponent<SphereCollider>();
		rend = GetComponent<Renderer>();
	}
	
	void Update()
	{
		timer -= Time.deltaTime;

		if (timer <= .2f)
			show();

		if (timer <= 0f)
			Destroy(gameObject);			
	}

	void show()
	{
		rend.material = explodeMaterial;
		sphereCollider.enabled = true;
	}

	void OnTriggerEnter(Collider other)
	{
		if (!other.name.Equals("Player"))
			return;

		PlayerMovement p = other.gameObject.GetComponent<PlayerMovement>();

		p.errorInMovement();

		Destroy(gameObject);
	}
}
