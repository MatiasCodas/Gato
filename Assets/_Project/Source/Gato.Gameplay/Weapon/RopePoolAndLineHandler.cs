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
        private HingeJoint2D _playerHinge;
        private int currentRopeDist;
        private int _oldRopeDist;
        private List<Rigidbody2D> RopeJointsPool = new List<Rigidbody2D>();

        //Variáveis pra CreatJoints()

        public void Setup(Rigidbody2D playerRigidBody, Rigidbody2D projectileRigidBody)
        {
            _playerRigidBody = playerRigidBody;
            _playerHinge = _playerRigidBody.GetComponent<HingeJoint2D>();
            _projectileRigidBody = projectileRigidBody;
            line = GetComponent<LineRenderer>();
            CreateJoints();
            RopeJoints = 0;
        }

        public void ComeBack()
        {
            _playerHinge.connectedBody = null;

            foreach (Rigidbody2D rb in RopeJointsPool)
            {
                HingeJoint2D hinge = rb.GetComponent<HingeJoint2D>();
                hinge.connectedBody = null;     
                rb.isKinematic = true;

            }
        }

        private void Update()
        {
            currentRopeDist = (int)Vector3.Distance(_playerRigidBody.transform.position, _projectileRigidBody.transform.position) * 6;
            if ((currentRopeDist - _oldRopeDist) > 1)
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

                if (i == 0)
                {
                    hinge.connectedBody = _projectileRigidBody;
                }
                else
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


                RopeJointsPool[i].transform.position = Vector3.Lerp(transform.position, _playerHinge.transform.position, (float)i /currentRopeDist);
                RopeJointsPool[i].gameObject.SetActive(true);
                RopeJoints++;


                if (i == currentRopeDist - 1)
                {
                    HingeJoint2D hingeBody = RopeJointsPool[i].GetComponent<HingeJoint2D>();
                    // handJoint.connectedBody = hingeBody;
                    hingeBody.enabled = true;
                    _playerHinge.connectedBody = hingeBody.GetComponent<Rigidbody2D>();
                    _playerHinge.enabled = true;
                }

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
            /*
            if (RopeJoints <= 0)
            {

               // line.positionCount = currentRopeDist + 1;

                for (int i = 0; i < line.positionCount; i++)
                {
                    line.SetPosition(i, Vector3.Lerp(transform.position, _projectileRigidBody.position, (float)i / (line.positionCount - 1)));
                }

                return;
            }
            */
            line.positionCount = RopeJoints+1;
            line.SetPosition(0, transform.position);

            for (int i = 1; i < RopeJoints; i++)
            {
                line.SetPosition(i, RopeJointsPool[i].transform.position);
            }

            line.SetPosition(RopeJoints, _projectileRigidBody.position);



            //debug counting
            int activeRJP = 0;
            for (int i = 0; i < RopeJointsPool.Count -1; i++)
            {
                if (RopeJointsPool[i].gameObject.activeSelf) activeRJP++;
            }
            Debug.Log("There's " + RopeJoints + " Ropejoints and " + activeRJP + "RopeJointsPool");
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
