using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardSprite : MonoBehaviour
{

	private void Start()
	{
		//transform.forward = Camera.main.transform.forward;
		transform.up = Vector3.up;
		
	}
	private void Update()
	{
		transform.forward = Camera.main.transform.forward;
		transform.up = transform.parent.forward;

	}


}
