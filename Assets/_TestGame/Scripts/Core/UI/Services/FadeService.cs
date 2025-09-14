using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _TestGame.Scripts.Core.UI.Services
{
    public interface IFadeService
    {
        UniTask FadeOut(FadeOptions opt, CancellationToken ct);
        UniTask FadeIn(FadeOptions opt, CancellationToken ct);
    }

    public class FadeService : IFadeService
    {
        readonly CanvasGroup _cg;

        public FadeService(CanvasGroup cg)
        {
            _cg = cg;
        }

        public async UniTask FadeOut(FadeOptions opt, CancellationToken ct)
        {
            _cg.gameObject.SetActive(true);
            _cg.blocksRaycasts = true;
            
            var t = 0f;
            while (t < opt.Duration)
            {
                ct.ThrowIfCancellationRequested();
                t += Time.unscaledDeltaTime;
                _cg.alpha = Mathf.Clamp01(t / opt.Duration);
                await UniTask.Yield(PlayerLoopTiming.Update, ct);
            }
            
            _cg.alpha = 1f;
        }

        public async UniTask FadeIn(FadeOptions opt, CancellationToken ct)
        {
            var t = opt.Duration;
            while (t > 0f)
            {
                ct.ThrowIfCancellationRequested();
                t -= Time.unscaledDeltaTime;
                _cg.alpha = Mathf.Clamp01(t / opt.Duration);
                await UniTask.Yield(PlayerLoopTiming.Update, ct);
            }
            
            _cg.alpha = 0f;
            _cg.blocksRaycasts = false;
            _cg.gameObject.SetActive(false);
        }
    }
}