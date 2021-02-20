using UnityEngine;
using DG.Tweening;

/// <summary>
/// A manager for fading audio, creating new instances, etc
/// </summary>
public class AudioManager : TG.Core.Audio.AudioManagerBase
{
    [Header("Settings")]
    [SerializeField] float fadeInTime = 1f;
    [SerializeField] float fadeOutTime = 1f;

    protected override void OnSceneIsGoingToLoad(int activeSceneBuildIndex, int newSceneBuildIndex) {
        base.OnSceneIsGoingToLoad(activeSceneBuildIndex, newSceneBuildIndex);

        if(bgmAudioSource != null) {
            bgmAudioSource.DOFade(0, fadeOutTime).SetUpdate(true);
        }
    }

    protected override void OnSceneLoaded(int activeSceneBuildIndex, int newSceneBuildIndex)
    {
        base.OnSceneLoaded(activeSceneBuildIndex, newSceneBuildIndex);

        if (bgmAudioSource != null)
        {
            bgmAudioSource.DOFade(1, fadeInTime).SetUpdate(true);
        }
    }
}
