using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinMe : MonoBehaviour
{
	void FixedUpdate()
	{
		transform.eulerAngles += new Vector3(0, 2, 2);

		transform.eulerAngles = new Vector3(
			transform.eulerAngles.x % 90,
			transform.eulerAngles.y % 90,
			transform.eulerAngles.z % 90
		);
	}

}
