using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gato.Rendering;

namespace Gato.Gameplay
{
    public class Elevator : Teleport
    {

        private bool _isMoving;
        private bool _teleported;
        public GameObject PlayerCam;
        public Vector3 DistanceFromTarget;
        public float LevitateSpeed;
        private PostProcessingManager _ppManager;

        private void Start()
        {
            _ppManager = Camera.main.GetComponent<PostProcessingManager>();
        }

        public override void Update()
        {
            float h, s, v;
            Color.RGBToHSV(SpriteRenderer.color, out h, out s, out v);
            SpriteRenderer.color = Color.HSVToRGB(h + ColorSpeed, s, v);

            if (!_isMoving) return;
            Time.timeScale = Mathf.Lerp(Time.timeScale, 0, 0.5f);
            Player.transform.position += new Vector3(0, LevitateSpeed * Time.fixedDeltaTime);

            
            if (_teleported)
            {
                MovingToFinish();
                
                return;
            }
            MovingFromStart();
            

        }

        private void MovingFromStart()
        {
           // Camera.main.SendMessage("FadeOut", Vector3.Distance(Player.transform.position, transform.position) / DistanceFromTarget.y);
            _ppManager.FadeOut(Vector3.Distance(Player.transform.position, transform.position) / DistanceFromTarget.y);
            if (Player.transform.position.y - transform.position.y < -DistanceFromTarget.y) return;
            TeleportNow(DistanceFromTarget);
            _teleported = true;
            Time.timeScale = 1;
            Camera.main.transform.position = Player.transform.position + DistanceFromTarget;
            PlayerCam.transform.position = Player.transform.position + DistanceFromTarget;
        }

        private void MovingToFinish()
        {

            //Camera.main.SendMessage("FadeIn", Vector3.Distance(Player.transform.position, TeleportTo.position) / DistanceFromTarget.y);
            _ppManager.FadeIn(Vector3.Distance(Player.transform.position, TeleportTo.position) / DistanceFromTarget.y);
            if (Player.transform.position.y < TeleportTo.position.y) return;
            _isMoving = false;
            _teleported = false;
            Player = null;
            Time.timeScale = 1;

        }
        public override void OnTriggerEnter2D(Collider2D collision)
        {
            Player = collision.gameObject;
            _isMoving = true;
            
        }

    }
}
