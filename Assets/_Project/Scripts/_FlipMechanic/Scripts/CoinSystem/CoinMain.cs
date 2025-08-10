using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Project.Scripts._FlipMechanic.Scripts.CoinSystem
{
    [RequireComponent(typeof(Rigidbody))]
    public class CoinMain : MonoBehaviour
    {
        #region Configuration
        
        [Serializable]
        public class FlipSettings
        {
            [Header("Fırlatma Ayarları")]
            [Tooltip("Flip için uygulanacak minimum dikey güç.")]
            [Range(1f, 20f)] public float MinFlipForce = 8f;

            [Tooltip("Flip için uygulanacak maksimum dikey güç.")]
            [Range(1f, 20f)] public float MaxFlipForce = 12f;

            [Tooltip("Uygulanan güce oranla rotasyon hızını belirleyen çarpan.")]
            [Range(1f, 20f)] public float RotationSpeedMultiplier = 10f;
        }
        
        #endregion

        #region State
        
        private enum CoinState
        {
            Ready,
            Flipping
        }
        
        #endregion

        #region Events

        public event Action OnCoinFlipped;
        public event Action OnCoinLanded;

        #endregion

        #region Serialized Fields
        
        [SerializeField] private FlipSettings settings;

        [SerializeField] private LayerMask groundLayer;

        #endregion

        #region Private Fields

        private Rigidbody _rb;
        private Transform _transform;
        private CoinState _currentState = CoinState.Ready;
        private bool _wobbleAppliedThisFlip = false;
        private Vector3 _initialPosition;
        private Quaternion _initialRotation;
        
        private const float GROUND_CHECK_DISTANCE = 1f;

        #endregion

        #region Unity Lifecycle Methods

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            _transform = transform;
        }

        private void Start()
        {
            _initialPosition = _transform.position;
            _initialRotation = _transform.rotation;
            
            _rb.isKinematic = true;
        }

        private void Update()
        {
            if (_currentState == CoinState.Ready && Input.GetKeyDown(KeyCode.Space))
            {
                FlipCoin();
            }
        }

        private void FixedUpdate()
        {
            // FixedUpdate, fizik ile ilgili tüm kontroller için en doğru yerdir.
            // Sadece coin havadayken çalışarak işlemciden tasarruf eder.
            if (_currentState == CoinState.Flipping)
            {
                CheckForLanding();
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Coini başlangıç pozisyonuna sıfırlamak için kullanılabilecek genel bir metot.
        /// </summary>
        public void ResetCoin()
        {
            _currentState = CoinState.Ready;
            _rb.isKinematic = true;
            _transform.SetPositionAndRotation(_initialPosition, _initialRotation);
            _rb.velocity = Vector3.zero;
            _rb.angularVelocity = Vector3.zero;
        }

        #endregion

        #region Internal Logic

        /// <summary>
        /// Coin fırlatma işlemini başlatır ve ilgili event'i tetikler.
        /// </summary>
        private void FlipCoin()
        {
            _currentState = CoinState.Flipping;
            _wobbleAppliedThisFlip = false;
            _rb.isKinematic = false;
            
            float randomFlipForce = Random.Range(settings.MinFlipForce, settings.MaxFlipForce);
            _rb.AddForce(Vector3.up * randomFlipForce, ForceMode.Impulse);

            Vector3 randomTorqueDirection = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized;
            float torqueAmount = randomFlipForce * settings.RotationSpeedMultiplier;
            _rb.AddTorque(randomTorqueDirection * torqueAmount, ForceMode.Impulse);

            OnCoinFlipped?.Invoke();
        }

        /// <summary>
        /// Coinin yere düşüp durduğunu, Rigidbody'nin uyku durumuna bakarak kontrol eder.
        /// </summary>
        private void CheckForLanding()
        {
            if (_rb.IsSleeping())
            {
                // Ekstra kontrol: Uyuduğu yer gerçekten zemin mi?
                if (Physics.Raycast(_transform.position, Vector3.down, GROUND_CHECK_DISTANCE, groundLayer))
                {
                    StabilizeCoin();
                }
            }
        }

        private void StabilizeCoin()
        {
            _rb.isKinematic = true;
            _currentState = CoinState.Ready;
            Debug.Log("Coin Yere Düştü ve Stabilize Oldu! Yeniden fırlatılabilir.");

            // Event'i tetikle.
            OnCoinLanded?.Invoke();
        }

        #endregion
    }
}