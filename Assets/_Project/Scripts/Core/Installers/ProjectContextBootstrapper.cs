using UnityEngine;
using Zenject;

namespace _Project.Scripts.Core.Installers
{
    public class ProjectContextBootstrapper : MonoBehaviour
    {
        private void Awake()
        {
            // ProjectContext'i ilk sahnede garanti ayağa kaldırır
            var _ = ProjectContext.Instance;
        }
    }
}
