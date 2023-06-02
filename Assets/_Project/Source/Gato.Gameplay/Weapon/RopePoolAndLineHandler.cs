using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gato.Gameplay
{
    public class RopePoolAndLineHandler : MonoBehaviour
    {
        private int RopeJoints;
        private LineRenderer line;
        public GameObject chainJointPrefab;
        public int ropeJointsSize;
        private Rigidbody2D _playerRigidBody;
        private Rigidbody2D _projectileRigidBody;
        public HingeJoint2D FirstHinge;
        private float currentRopeDist;
        private float _oldRopeDist;
        private List<Rigidbody2D> RopeJointsPool = new List<Rigidbody2D>();
        private bool _isActive = true;
        public bool IsMoving = true;

        //Variáveis pra CreatJoints()

        public void Setup(Rigidbody2D playerRigidBody, Rigidbody2D projectileRigidBody)
        {
            _playerRigidBody = playerRigidBody;
            FirstHinge = _playerRigidBody.GetComponent<HingeJoint2D>();
            _projectileRigidBody = projectileRigidBody;
            line = GetComponent<LineRenderer>();
            CreateJoints();
            RopeJoints = 0;
            currentRopeDist = 1;
        }

        public void ComeBack()
        {
            FirstHinge.connectedBody = null;

            foreach (Rigidbody2D rb in RopeJointsPool)
            {
                HingeJoint2D hinge = rb.GetComponent<HingeJoint2D>();
                hinge.connectedBody = null;     
                rb.isKinematic = true;

            }
        }

        private void Update()
        {
            if (!_isActive) return;
            currentRopeDist = Vector3.Distance(FirstHinge.transform.position, _projectileRigidBody.transform.position) * 6;
            
            if ((currentRopeDist - _oldRopeDist) > 0.01f)
            {
                ActivateJoints();
            }
            
            if(IsMoving)
            {
                ActivateJoints();
            }
            _oldRopeDist = currentRopeDist;
            ShowLine();
        }

        private void CreateJoints()
        {
            Rigidbody2D previousJoint = null;

            for (int i = 0; i < ropeJointsSize; i++)
            {
                GameObject instance = Instantiate(chainJointPrefab, Vector3.zero, Quaternion.identity, transform);
                HingeJoint2D hinge = instance.GetComponent<HingeJoint2D>();

                if (i != 0)
                {
                    instance.transform.SetParent(previousJoint.transform);
                    hinge.connectedBody = previousJoint;
                }
                

                previousJoint = instance.GetComponent<Rigidbody2D>();
                instance.SetActive(false);
                RopeJointsPool.Add(previousJoint);
            }
        }

        public void ActivateJoints()
        {
            ClearJoints();
            for (int i = 0; i <= currentRopeDist; i++)
            {
                if (i > RopeJointsPool.Count - 1)
                {
                    return;
                }


                RopeJointsPool[i].transform.position = Vector3.Lerp(transform.position, FirstHinge.transform.position, (float)i /currentRopeDist);
                RopeJointsPool[i].gameObject.SetActive(true);
                RopeJoints++;

                /*
                if (i == currentRopeDist - 1)
                {
                    HingeJoint2D hingeBody = RopeJointsPool[i].GetComponent<HingeJoint2D>();
                    // handJoint.connectedBody = hingeBody;
                    hingeBody.enabled = true;
                    FirstHinge.connectedBody = hingeBody.GetComponent<Rigidbody2D>();
                    FirstHinge.enabled = true;
                }
                */
                if (i >= currentRopeDist)
                {
                    RopeJointsPool[i].gameObject.SetActive(false);
                }
            }

        }

        public void ClearJoints()
        {
            if (RopeJoints <= 0)
            {
                return;
            }

            for (int i = 0; i < RopeJoints; i++)
            {
                RopeJointsPool[i].gameObject.SetActive(false);
                RopeJointsPool[i].transform.localPosition = Vector3.zero;
            }

            RopeJoints = 0;
        }

        private void ShowLine()
        {
            if (currentRopeDist <= 1f)
            {
                return;
            }
            line.positionCount = RopeJoints-1;

            RopeJointsPool[RopeJoints-1].MovePosition(FirstHinge.transform.position);
            for (int i = 0; i < line.positionCount-1; i++)
            {
                line.SetPosition(i, RopeJointsPool[i].transform.position);
            }

            line.SetPosition(line.positionCount-1, FirstHinge.transform.position);


        }

        public void Deactivate()
        {
            _isActive = false;
            ClearJoints();
            line.enabled = false;
            FirstHinge.connectedBody = null;
            FirstHinge.enabled = false;
        }
        private void OnDestroy()
        {
            if (RopeJointsPool[0].gameObject == null)
            {
                return;
            }

            RopeJointsPool.Clear();
            RopeJoints = 0;
        }
    }
}
