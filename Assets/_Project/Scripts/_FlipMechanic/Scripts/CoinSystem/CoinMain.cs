using System;
using System.Collections;
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
            
            [Tooltip("Dönüş hızı çarpanı. Dönüş kuvvetini artırmak için kullanılır.")]
            [Range(1f, 20f)] public float RotationForceMultiplier = 1f;
        }
        
        #endregion

        #region State
        
        private enum CoinState
        {
            Ready,
            Flipping,
            Preparing
        }
        
        #endregion

        #region Events

        /// <summary>
        /// Coin başarılı bir şekilde fırlatıldığında tetiklenir.
        /// </summary>
        public event Action OnCoinFlipped;
        
        /// <summary>
        /// Coin yere düşüp stabil hale geldiğinde tetiklenir.
        /// </summary>
        public event Action OnCoinLanded;

        /// <summary>
        /// Coin yere düştükten 1 saniye sonra, hazırlanma durumuna geçtiğinde tetiklenir.
        /// </summary>
        public event Action OnCoinPrepare;

        #endregion

        #region Serialized Fields
        
        [SerializeField] private FlipSettings settings;
        [SerializeField] private LayerMask groundLayer;

        #endregion

        #region Private Fields

        private Rigidbody _rb;
        private Transform _transform;
        private CoinState _currentState = CoinState.Ready;
        private Vector3 _initialPosition;
        private Quaternion _initialRotation;
        
        // Coroutine içinde GC (çöp toplama) oluşturmamak için bekleme objesini cache'liyoruz.
        private WaitForSeconds _oneSecondWait;
        private const float GROUND_CHECK_DISTANCE = 1f;

        #endregion

        #region Unity Lifecycle Methods

        private void Awake()
        {
            _rb = GetComponent(typeof(Rigidbody)) as Rigidbody;
            _transform = transform;
            
            // OPTIMIZASYON: Varsayılan dönüş hızı limitini artırarak
            // RotationSpeedMultiplier'ın etkili olmasını sağlıyoruz.
            _rb.maxAngularVelocity = 10;
            
            // OPTIMIZASYON: WaitForSeconds objesini bir kere oluşturuyoruz.
            _oneSecondWait = new WaitForSeconds(1f);
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
            if (_currentState == CoinState.Flipping)
            {
                CheckForLanding();
            }
        }

        #endregion

        #region Public Methods

        public void ResetCoin()
        {
            StopAllCoroutines();
            _currentState = CoinState.Ready;
            _rb.isKinematic = true;
            _transform.SetPositionAndRotation(_initialPosition, _initialRotation);
            _rb.velocity = Vector3.zero;
            _rb.angularVelocity = Vector3.zero;
        }

        #endregion

        #region Internal Logic

        private void FlipCoin()
        {
            _currentState = CoinState.Flipping;
            _rb.isKinematic = false;
            
            float randomFlipForce = Random.Range(settings.MinFlipForce, settings.MaxFlipForce);
            _rb.AddForce(Vector3.up * randomFlipForce, ForceMode.Impulse);

            Vector3 randomTorqueDirection = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized;
            _rb.AddTorque(randomTorqueDirection * (randomFlipForce * settings.RotationForceMultiplier), ForceMode.Impulse);

            OnCoinFlipped?.Invoke();
        }

        private void CheckForLanding()
        {
            if (_rb.IsSleeping())
            {
                if (Physics.Raycast(_transform.position, Vector3.down, GROUND_CHECK_DISTANCE, groundLayer))
                {
                    // Aynı frame içinde birden çok kez çağrılmasını önlemek için state kontrolü
                    if (_currentState == CoinState.Flipping)
                    {
                        StabilizeCoin();
                    }
                }
            }
        }

        private void StabilizeCoin()
        {
            // Coroutine başlamadan önce durumu değiştirerek tekrar tetiklenmesini engelliyoruz.
            _currentState = CoinState.Preparing;
            _rb.isKinematic = true;
            StartCoroutine(PostLandingSequence());
        }

        private IEnumerator PostLandingSequence()
        {
            OnCoinLanded?.Invoke();
            
            // Cache'lenmiş bekleme objesini kullanıyoruz.
            yield return _oneSecondWait;
            
            // Durum zaten Preparing olarak ayarlandığı için tekrar ayarlamaya gerek yok.
            OnCoinPrepare?.Invoke();
            Debug.Log("Coin hazırlanıyor...");
            
            yield return _oneSecondWait;
            
            _currentState = CoinState.Ready;
            Debug.Log("Coin yeniden fırlatılmaya hazır.");
        }

        #endregion
    }
}