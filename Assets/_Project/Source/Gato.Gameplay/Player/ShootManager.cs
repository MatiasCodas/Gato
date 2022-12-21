using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootManager : MonoBehaviour
{
	public GameObject projectile;
	public ObjectBoolean activate;
	public bool shot;
	private HingeJoint2D hinge;
	public GameObject ropePoolGameObject;
	private Rigidbody2D rb2d;
	GameObject firstJoint;


    private void Start()
    {
		hinge = GetComponent<HingeJoint2D>();
		rb2d = GetComponent<Rigidbody2D>();
	//	firstJoint = RopePoolTongue.RopeJointsPool[0];
		shot = false;
	}
	
    private void Update()
    {
		if (Input.GetKeyDown(KeyCode.Space))
		{
			 ShootTongue();
		}
		
		if (firstJoint.activeSelf)
		{
			transform.rotation = firstJoint.transform.rotation;
		}
		
	}

	private void ShootTongue()
	{

		if (shot)
		{
			shot = false;
			activate.value = false;
		//	RopePoolTongue.ClearJoints();
			return;
		}
		shot = true;
		transform.rotation = Quaternion.LookRotation(new Vector3(0,0, transform.position.z) - transform.position); //here to change the direction to shot the rope
		firstJoint.transform.position = transform.localPosition;
		firstJoint.transform.rotation = transform.localRotation;
		firstJoint.SetActive(true);
		activate.value = true;
	}

	private void OnDisable()
	{
	//	RopePoolTongue.ClearJoints();
		activate.value = false;
	}
}
