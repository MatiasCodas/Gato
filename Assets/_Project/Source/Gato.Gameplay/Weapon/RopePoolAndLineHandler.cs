using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gato.Gameplay
{
    public class RopePoolAndLineHandler : MonoBehaviour
    {
        public int RopeJoints;
        public List<GameObject> RopeJointsPool = new List<GameObject>();
        public float totalDistance;
        private LineRenderer line;
        public GameObject chainJointPrefab;
        public int poolSize;
        public Rigidbody2D targetRB;
        public Transform hand;
        private RopeTip handScript;
        private SpringJoint2D handJoint;
        private int currentRopeDist;

        //Variáveis pra CreatJoints()
        private GameObject theJoint;
        private SpringJoint2D hinge;
        private Rigidbody2D jointRB;

        private void Awake()
        {
            line = GetComponent<LineRenderer>();
            jointRB = null;
            CreateJoints();
            RopeJoints = 0;
            //handScript = hand.GetComponent<RopeTip>();
           // handJoint = hand.GetComponent<SpringJoint2D>();
        }


        private void Update()
        {

            currentRopeDist = (int)Vector3.Distance(hand.position, transform.position);
            //ShowLine();
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
            if (currentRopeDist <= 1f) return;
            if (RopeJoints <= 0)
            {
                
                line.positionCount = currentRopeDist+1;
                
                for (int i = 0; i < line.positionCount; i++)
                {
                    line.SetPosition(i, Vector3.Lerp(transform.position, hand.position, (float)i/(line.positionCount-1)));
                }
                
                return;
            }
            line.positionCount = RopeJoints+1;
            line.SetPosition(0, transform.position);
            for (int i = 1; i < RopeJoints; i++)
            {
                    line.SetPosition(i, RopeJointsPool[i].transform.position);
            }
            line.SetPosition(RopeJoints, hand.position);
        }

        private void CreateJoints()//CriaBufas()
        {
            for (int i = 0; i < poolSize; i++)
            {
                theJoint = Instantiate(chainJointPrefab, Vector3.zero, Quaternion.identity, transform);
                hinge = theJoint.GetComponent<SpringJoint2D>();
                hinge.connectedBody = jointRB;
                if (i == 0)
                {
                    hinge.connectedBody = targetRB;
                    hinge.autoConfigureDistance = false;
                    hinge.distance = 0.005f;
                }
                jointRB = theJoint.GetComponent<Rigidbody2D>();
                theJoint.SetActive(false);
                RopeJointsPool.Add(theJoint);
            }
        }

        public void ActivateJoints(Transform targetHand)
        {
            for (int i = 0; i < currentRopeDist; i++)
            {
                
                RopeJointsPool[i].transform.position = Vector3.Lerp(transform.position, hand.position, (float)i/currentRopeDist);
                RopeJointsPool[i].SetActive(true);
                RopeJoints++;
                continue;
                if (i == currentRopeDist - 1)
                {
                    handJoint.connectedBody = RopeJointsPool[i].GetComponent<Rigidbody2D>();
                    handJoint.enabled = true;
                    handJoint.enableCollision = true;
                }
            }

        }

        public void ClearJoints()
        {

            if (RopeJoints <= 0) return;
            for (int i = 0; i < RopeJoints; i++)
            {
                RopeJointsPool[i].SetActive(false);
                RopeJointsPool[i].transform.localPosition = Vector3.zero;

            }
            RopeJoints = 0;
        }

        private void OnDestroy()
        {
            if (RopeJointsPool[0].gameObject == null) return;
            RopeJointsPool.Clear();
            RopeJoints = 0;
        }
    }
}
