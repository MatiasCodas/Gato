using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gato.Gameplay
{
    public class CurseProjectile : MonoBehaviour
    {
        public delegate void TargetHitHandler();

        public event TargetHitHandler OnCurseTriggered;

        public event TargetHitHandler OnObjectTriggered;


        [SerializeField]
        private float _movementSpeed = 3f;

        private bool _isMoving;
        public static bool _isCursed;
        private GameObject _collisionObject;
        private Vector2 _direction;
        public CurseRopeShooter _ropeShooter;
        private LineRenderer _line;
        private static bool isTarget = true;
        public List<GameObject> connectedToRope;
        private float timeActive;
        private int layerMask;

        public float timerToDestroy;
        public bool goBack = false;
        
        public void Setup(Vector2 direction)
        {
            _isMoving = true;
            _direction = direction;
            _collisionObject = null;
            _line = GetComponent<LineRenderer>();
            layerMask = LayerMask.GetMask("Default");
        }


        public void ActivateCurse(bool cursed)
        {
            
            
        }

        private void Update() // gambiarra, favor trocar os inputs pro inputmanager depois
        {
            if(Input.GetKeyDown(KeyCode.Q))
            {
                goBack = true;
            }
        }

        private void FixedUpdate()
        {
            LineCollisions();
            LineUpdate();
            if (!_isMoving && !goBack)
            {
                timeActive = 0;
                return;
            }
            timeActive += Time.deltaTime;
            Vector2 backForce = Vector2.ClampMagnitude(connectedToRope[connectedToRope.Count-2].transform.position - transform.position, 1) * timeActive;
            transform.Translate((_direction * _movementSpeed * Time.deltaTime) + backForce);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                Destroy(gameObject);
                return;
            }
            if(goBack)
            {
                if (collision.gameObject == connectedToRope[connectedToRope.Count - 2]) connectedToRope.RemoveAt(connectedToRope.Count - 2);
                lineIndex--;
                Debug.Log("removed one");
                return;
            }
            if (!_isMoving) return;
            transform.parent = collision.transform;
            _isMoving = false;
            _collisionObject = collision.gameObject;

            if (_isCursed) collision.SendMessage("Curse1", gameObject);
            if (collision.gameObject.name == "Curse")
            {
                _isCursed = true;
            }
        

            isTarget = !isTarget;
            ActivateCurse(_isCursed);
            /*
            if(isTarget)
            {
                OnCurseTriggered?.Invoke();
                return;
            }
            
            OnObjectTriggered?.Invoke();*/
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (!goBack) return;
            if (collision.gameObject.layer != 7) return;
            collision.gameObject.layer = 0;
        }

        //private Vector2 target;

        private int lineIndex = 0;
        private void LineCollisions()//this is almost working, I'm trying
        {
            Vector2 rayShooter = connectedToRope[lineIndex + 1].transform.position;
            Vector2 target = connectedToRope[lineIndex].transform.position;
            RaycastHit2D ray2D = Physics2D.Raycast(rayShooter, target - rayShooter, Vector2.Distance(target, rayShooter), layerMask);
            Debug.DrawRay(rayShooter, target - rayShooter, ray2D.collider != null ? Color.blue : Color.red);
            if (ray2D.collider == null)
            {
                if (lineIndex <= 0)
                {
                    lineIndex = connectedToRope.Count - 2;
                    return;
                }
                lineIndex--;
                LineCollisions();
                return;
            }
            
            ray2D.collider.gameObject.layer = 7;
            Debug.Log(ray2D.collider.name);
            connectedToRope.Insert(lineIndex+1, ray2D.collider.gameObject);

            if (ray2D.collider.name == "Curse") _isCursed = true;
            if (_isCursed) ray2D.collider.SendMessage("Curse1", gameObject);
        }
        

        private void OnDestroy()
        {
            for (int i = 0; i < connectedToRope.Count; i++)
            {
                connectedToRope[i].layer = 0;
            }
        }
        private void LineUpdate()
        {
            _line.positionCount = connectedToRope.Count;
            //_line.SetPosition(0, transform.position);
            for (int i = 0; i < connectedToRope.Count; i++)
            {
                _line.SetPosition(i, connectedToRope[i].transform.position);
            }
        }
        private IEnumerator SetTimer(bool both)
        {

            yield return new WaitForSeconds(timerToDestroy);
            if (!_isMoving && !both) yield break;
            if (both && _collisionObject != null)
            {
                Destroy(connectedToRope[0]);
            }
            Destroy(gameObject);
        }
    }
}
