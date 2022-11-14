using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainJointManager : MonoBehaviour
{
	public Rigidbody2D rb2d;
	public SpringJoint2D sj2d;
	public SpriteRenderer spriteRenderer;
    public RopePoolTongue parentPoolTongue;

	public ChainJointStats stats;
	public ObjectBoolean elongate;

	public GameObject jointPrefab;
	private bool tip;

	private Rigidbody2D previousJoint;//useless for now but have plans on using it
	public float startUnitDistance;

	
	private void OnEnable()
	{
		rb2d.constraints = RigidbodyConstraints2D.None;
		StartCoroutine(ShouldCreateJoint());
		
		tip = true;
		/*
		if (previousJoint != null)
		{
			startUnitDistance = Vector2.Distance(transform.position, previousJoint.transform.position);
		}*/
	}

	private void Start()
	{
        parentPoolTongue.RopeJoints++;
        //can't make this on Awake due to needing other codes to do stuff on start for this to work
        /*
		previousJoint = gameObject.GetComponent<SpringJoint2D>().connectedBody;
		startUnitDistance = Vector2.Distance(transform.position, previousJoint.transform.position);
		*/
    }


	private void Update()
	{
		if (tip)
		{
			parentPoolTongue.hand.SendMessage("IAmTip", transform);
		}
	}

	private void FixedUpdate()
	{
		/*
		if (Vector2.Distance(transform.position, previousJoint.transform.position) > startUnitDistance)
		{
			rb2d.AddForce(previousJoint.transform.position - transform.position);

		}
		*/
	}

	private IEnumerator ShouldCreateJoint()
	{
		yield return new WaitForSeconds(stats.shotSpeed);
		if (!elongate.value) yield return StartCoroutine(ShouldCreateJoint());
		if(parentPoolTongue.RopeJointsPool.Count > parentPoolTongue.RopeJoints)
		{
			GameObject nextJoint = parentPoolTongue.RopeJointsPool[parentPoolTongue.RopeJoints];
			nextJoint.transform.position = transform.position + transform.forward * spriteRenderer.bounds.size.x * stats.distanceMultiplier;
			nextJoint.transform.rotation = transform.rotation;
			nextJoint.SetActive(true);
			yield return new WaitForSeconds(stats.shotSpeed / 2);
			tip = false;
		}
		yield return null;
	}

	private void DestroyNow()
	{
		gameObject.SetActive(false);
		startUnitDistance = 0;
		transform.localPosition = Vector3.zero;
		if (tip)
		{
			parentPoolTongue.hand.SendMessage("IAmNotTip");
		}


	}


	private void OnCollisionStay2D(Collision2D collision)
	{
		if (tip)
		{
			elongate.value = false;
			//rb2d.constraints = RigidbodyConstraints2D.FreezeAll;
            collision.gameObject.AddComponent<SpringJoint2D>().connectedBody = rb2d;
			
		}
	}

}
