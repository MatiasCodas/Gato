using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopePoolTongue : MonoBehaviour
{
	public int RopeJoints;
	public List<GameObject> RopeJointsPool = new List<GameObject>();
	private List<ChainJointManager> chainJointManagers = new List<ChainJointManager>();
	public float totalDistance;
	private LineRenderer line;
	public GameObject chainJointPrefab;
	public int poolSize;
	public Rigidbody2D targetRB;
	public Transform hand;

	//Variáveis pra CreatJoints()
	private GameObject theJoint;
	private SpringJoint2D hinge;
	private Rigidbody2D jointRB;

	private void Awake()
	{
		line = GetComponent<LineRenderer>();
		hand = transform.GetChild(0);
		jointRB = null;
		CreateJoints();
		RopeJoints = 0;
	}


	private void Update()
    {
		ShowLine();
    }

	private void FixedUpdate()
	{
		Vector3 previousPosition = RopeJointsPool[0].transform.position;
		foreach (GameObject joint in RopeJointsPool)
		{
			previousPosition = joint.transform.position;
			totalDistance += Vector3.Distance(joint.transform.position, previousPosition);
		}
		
	}

	private void ShowLine()
	{
		if (RopeJoints <= 0)
		{
			line.positionCount = 0;
			return;
		}
		line.positionCount = RopeJoints;
		for (int i = 0; i < RopeJoints; i++)
		{
			line.SetPosition(i, RopeJointsPool[i].transform.position);
		}
	}

	private void CreateJoints()//CriaBufas()
	{
		for (int i = 0; i < poolSize; i++)
		{
			theJoint = Instantiate(chainJointPrefab, Vector3.zero, Quaternion.identity, transform);
			chainJointManagers.Add(theJoint.GetComponent<ChainJointManager>());
            chainJointManagers[i].parentPoolTongue = this;
            RopeJoints++;
			hinge = chainJointManagers[i].sj2d;
			hinge.connectedBody = jointRB;
			if (i == 0)
			{
				hinge.connectedBody = targetRB;
				hinge.autoConfigureDistance = false;
				hinge.distance = 0.005f;
			}
			jointRB = chainJointManagers[i].rb2d;
			theJoint.SetActive(false);
			RopeJointsPool.Add(theJoint);
		}
	}

	public void ClearJoints()
	{
		
		if (RopeJoints <= 0) return;
		for (int i = 0; i < RopeJoints; i++)
		{
			RopeJointsPool[i].gameObject.SendMessage("DestroyNow");

		}
		RopeJoints = 0;
	}

	private void OnDestroy()
	{
		RopeJointsPool.Clear();
		RopeJoints = 0;
	}
}
