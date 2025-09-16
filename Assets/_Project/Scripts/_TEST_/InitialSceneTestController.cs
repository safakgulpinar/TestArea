using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace _Project.Scripts._TEST_
{
    public class InitialSceneTestController : MonoBehaviour
    {
        [SerializeField] private Button goMainSceneButton;

        private void Awake()
        {
            AssignButtonAction();
        }

        private void AssignButtonAction()
        {
            goMainSceneButton.onClick.AddListener(() =>
            {
                SceneManager.LoadScene(1);
            });
        }
    }
}