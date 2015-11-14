#region

using Assets.Scripts.Pooling;
using Assets.Scripts.Pooling.Prefab_Holders;
using UnityEngine;

#endregion

namespace Assets.Scripts
{
    [RequireComponent(typeof (CharacterController))]
    public class HeroBehaviour : MonoBehaviour
    {
        /* Movement variables */
        private bool _canDoubleJump;
        private CharacterController _character;
        private float _damage;
        private bool _exploded;
        private Vector3 _gravity;
        /* Health */
        private float _health;
        private Vector3 _horizontalDirection;
        private bool _isGrounded;
        private bool _isJumping;
        /* Debugging Variables*/
        private float _timeScale;
        private Vector3 _verticalDirection;
        /* Score */
        public int AccumulatedScore;
        /* Audio */
        public new Audio audio;
        public static HeroBehaviour Ctrl;
        public Vector3 InitialPosition;
        public bool IsAlive;
        public Vector3 JumpForce = new Vector3(0, 3f, 0);
        public float MovementRate;
        public int Score;

        private bool IsDead
        {
            get { return (_health <= _damage); }
        }

        public void Awake()
        {
            if (Ctrl == null)
                Ctrl = this;
            else if (Ctrl != this)
                Destroy(gameObject);
        }

        public void Start()
        {
            _character = GetComponent<CharacterController>();
            _timeScale = 1f;
            /* Health */
            _health = 1;
            _damage = 0;
            _exploded = false;
            IsAlive = true;
            /* Movement */
            MovementRate = 0.1f;
            _gravity = new Vector3(0, -0.25f, 0);
            _verticalDirection = _gravity*MovementRate;
            _horizontalDirection = new Vector3(1.5f, 0, 0)*MovementRate;
            /* Score */
            Score = 0;
            InitialPosition = transform.position;
            //MovementRate = Mathf.Max(0.1f, Config.Ctrl.Difficulty/10.0f);
            MovementRate = 0.5f;
        }

        public void FixedUpdate()
        {
            if (_character.isGrounded)
            {
                _canDoubleJump = true;
            }
            _isGrounded = _character.isGrounded;
            if (!IsDead)
            {
                Move();
                UpdateScore();
            }
            else
            {
                GetComponent<Renderer>().enabled = false;
                if (!_exploded)
                {
                    Instantiate(AssetHolder.Ctrl.BodyExplosionParticleSystem, transform.position, Quaternion.identity);
                    audio.playDeath();
                    _exploded = true;
                }
                Invoke("Die", 0.5f);
            }
        }

        private void Die()
        {
            IsAlive = false;
            GameController.Ctrl.GameOver();
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.touchCount > 0)
            {
                _isJumping = true;
            }
        }

        public void Move()
        {
            if (GameController.Ctrl.IsPaused)
            {
                return;
            }
            var direction = Vector3.zero;
            if (_isJumping)
            {
                if (_character.isGrounded)
                {
                    Jump();
                }
                else if (_canDoubleJump)
                {
                    Jump();
                    _canDoubleJump = false;
                }
                _isJumping = false;
            }
            else if (!_character.isGrounded)
            {
                var gravityDecrease = _gravity*MovementRate;
                _verticalDirection += gravityDecrease;
            }
            if ((_character.collisionFlags & CollisionFlags.Sides) == 0)
            {
                direction += _horizontalDirection;
            }


            direction += _verticalDirection;
            _character.Move(direction);
        }

        private void Jump()
        {
            if (_character.isGrounded)
            {
                var spawnPosition = transform.position + new Vector3(-0.5f, -0.5f, 0);
                Instantiate(AssetHolder.Ctrl.JumpParticleSystem, spawnPosition, Quaternion.identity);
            }
            _verticalDirection = JumpForce*MovementRate;
            audio.playJump();
        }

        private void UpdateScore()
        {
            Score = AccumulatedScore + (int) (transform.position.x - InitialPosition.x);
        }

        public void DamageCharacter(float damage = 1)
        {
            _damage = damage;
        }
        
    }
}