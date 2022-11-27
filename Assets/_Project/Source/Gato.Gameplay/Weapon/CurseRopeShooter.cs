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
        public TongueTip ropeTip;


        private void Start()
        {
            hinge = GetComponent<HingeJoint2D>();
            rb2d = GetComponent<Rigidbody2D>();
            ropePoolScript = ropePoolGameObject.GetComponent<RopePoolTongue>();
            shot = false;
        }


        private void ShootTongue(Transform target)
        {
            shot = true;
            ropeTip.currentAttachedBody = hinge.connectedBody.gameObject;
            ropeTip.gameObject.SetActive(true);
            ropeTip.transform.position = transform.localPosition;
            ropeTip.isGettingLonger = true;
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
            TongueTip.globalTarget = ActiveCurseTransform;
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
