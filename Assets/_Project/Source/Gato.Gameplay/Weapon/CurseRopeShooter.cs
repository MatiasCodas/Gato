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
        public HingeJoint2D hinge;
        public GameObject ropePoolGameObject;
        private RopePoolAndLineHandler ropePoolScript;
        private Rigidbody2D rb2d;
        GameObject firstJoint;
        public RopeTip ropeTip;
        private static bool isNotRope = false;

        private void Start()
        {
            //hinge = GetComponent<HingeJoint2D>();
            rb2d = GetComponent<Rigidbody2D>();
            ropePoolScript = ropePoolGameObject.GetComponent<RopePoolAndLineHandler>();
            shot = false;
        }


        private void ShootRope()
        {
            shot = true;
            RopeTip.globalTarget = null;
            ropeTip.currentAttachedBody = hinge.connectedBody.gameObject;
            ropeTip.gameObject.SetActive(true);
            ropeTip.transform.position = transform.localPosition;
            ropeTip.isGettingLonger = true;
            ActiveCurse = null;
            ActiveCurseTransform = null;
            alreadyActive = true;
        }

        private void OnDisable()
        {
            ropePoolScript.ClearJoints();
            activate.value = false;
        }

        public void TargetHit(GameObject affectedByCurse, bool isCursed)
        {
            if (alreadyActive) return;
            hinge.enabled = true;
            hinge.connectedBody = affectedByCurse.GetComponent<Rigidbody2D>();
            isNotRope = !isNotRope;
            if (ActiveCurse == null && !isNotRope)
            {
                RopeTip.globalTarget = transform;
                ActiveCurse = this;
                ActiveCurseTransform = transform;
                return;
            }
            ropeTip.cursed = isCursed;
            ShootRope();
            
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
