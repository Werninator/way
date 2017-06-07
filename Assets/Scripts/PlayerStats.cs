using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
	public static PlayerStats Instance;

	public int health;

	bool lost = false;

	void Awake()   
	{
		if (Instance == null)
		{
			DontDestroyOnLoad(gameObject);
			Instance = this;
		}
		else if (Instance != this)
			Destroy(gameObject);
	}

	public void hurtPlayer(int amount)
	{
		health -= amount;
	}

	void Update()
	{
		if (health <= 0 && !lost) {
			SceneManager.LoadScene("Lost");
			lost = true;
		}
	}
}
