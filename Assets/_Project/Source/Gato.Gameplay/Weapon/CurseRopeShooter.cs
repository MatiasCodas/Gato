using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gato.Gameplay
{
    public class CurseRopeShooter : MonoBehaviour
    {

        public static CurseRopeShooter ActiveCurse;
        public static Transform ActiveCurseTransform;
        private bool alreadyActive;

        public GameObject projectile;
        public ObjectBoolean activate;
        public bool shot;
        private HingeJoint2D hinge;
        public GameObject ropePoolGameObject;
        private RopePoolTongue ropePoolScript;
        private Rigidbody2D rb2d;
        GameObject firstJoint;


        private void Start()
        {
            hinge = GetComponent<HingeJoint2D>();
            rb2d = GetComponent<Rigidbody2D>();
            ropePoolScript = ropePoolGameObject.GetComponent<RopePoolTongue>();
            firstJoint = ropePoolScript.RopeJointsPool[0];
            shot = false;
        }

        private void Update()
        {/*
            if (Input.GetKeyDown(KeyCode.Space))
            {
                ShootTongue();
            }

            if (firstJoint.activeSelf)
            {
                transform.rotation = firstJoint.transform.rotation;
            }
            */
        }

        private void ShootTongue(Transform target)
        {
            if (shot)
            {
                shot = false;
                activate.value = false;
                ropePoolScript.ClearJoints();
                return;
            }
            shot = true;
            transform.rotation = Quaternion.LookRotation(new Vector3(target.position.x, target.position.y, transform.position.z) - transform.position); //here to change the direction to shot the rope
            firstJoint.transform.position = transform.localPosition;
            firstJoint.transform.rotation = transform.localRotation;
            firstJoint.SetActive(true);
            activate.value = true;
        }

        private void OnDisable()
        {
            ropePoolScript.ClearJoints();
            activate.value = false;
        }

        public void TargetHit(GameObject affectedByCurse)
        {
            if (alreadyActive) return;
            hinge.enabled = true;
            hinge.connectedBody = affectedByCurse.GetComponent<Rigidbody2D>();
            if (ActiveCurse == null)
            {
                ActiveCurse = this;
                ActiveCurseTransform = transform;
                return;
            }
            ChainJointManager.target = ActiveCurseTransform.position;
            ShootTongue(ActiveCurseTransform);
            ActiveCurse = null;
            ActiveCurseTransform = null;
            alreadyActive = true;
        }

        private void OnDestroy()
        {
            if (ActiveCurse == this)
            {
                ActiveCurse = null;
                ActiveCurseTransform = null;
            }
        }

    }
}
