using UnityEngine;

namespace _TestGame.Scripts.Core.UI.Views
{
    public class BaseScreenView : MonoBehaviour
    {
        public virtual void Show()
        {
            gameObject.SetActive(true);
        }

        public virtual void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}