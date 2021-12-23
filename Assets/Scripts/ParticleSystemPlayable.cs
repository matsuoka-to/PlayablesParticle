using UnityEngine;
using UnityEngine.Playables;

using System.Collections.Generic;
using System.Linq;

public class ParticleSystemPlayable : PlayableBehaviour
{
    List<ParticleSystem> particleSystem;
    float playTime;
    float playTimeMax;

    /// <summary>
    /// 初期化する
    /// </summary>
    public void Initialize(GameObject obj)
    {
        playTime    = 0;
        playTimeMax = 0;
        
        var data = obj.GetComponentsInChildren<ParticleSystem>(true);
        particleSystem = data.ToList();
        for (int i = 0; i < particleSystem.Count; i++)
        {
            var time = particleSystem[i].main.duration;
            if (playTimeMax < time)
            {
                playTimeMax = time;
            }
        }

        Stop();
    }

    /// <summary>
    /// 再生Max時間取得
    /// </summary>
    public float GetPlayTime()
    {
        return playTime;
    }

    /// <summary>
    /// 再生Max時間取得
    /// </summary>
    public float GetPlayMaxTime()
    {
        return playTimeMax;
    }

    /// <summary>
    /// Playableが再生されたときの処理
    /// </summary>
    public override void OnBehaviourPlay(Playable playable, FrameData info)
    {
        Play();
    }

    /// <summary>
    /// Playableの再生が止まった時の処理
    /// </summary>
    public override void OnBehaviourPause(Playable playable, FrameData info)
    {
        Pause();
    }

    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        for (int i = 0; i < particleSystem.Count; i++)
        {
            var time = particleSystem[i].time;
            if (playTime < time)
            {
                playTime = time;
            }

            if (time < playTime)
            {
                playTime = 0;
            }
        }
    }

    /// <summary>
    /// 再生
    /// </summary>
    public void Play()
    {
        for (int i = 0; i < particleSystem.Count; i++)
        {
            particleSystem[i].Play();
        }
    }
    
    /// <summary>
    /// 停止
    /// </summary>
    public void Pause()
    {
        for (int i = 0; i < particleSystem.Count; i++)
        {
            particleSystem[i].Pause();
        }
    }
    
    /// <summary>
    /// 止める
    /// </summary>
    public void Stop()
    {
        for (int i = 0; i < particleSystem.Count; i++)
        {
            particleSystem[i].Stop();
        }
    }

    /// <summary>
    /// スピード設定
    /// </summary>
    public void SetSpeed(float value)
    {
        for (int i = 0; i < particleSystem.Count; i++)
        {
            var ps = particleSystem[i].main;
            ps.simulationSpeed = value;
        }
    }

    /// <summary>
    /// 時間設定
    /// </summary>
    public void SetTime(float value)
    {
        playTime = value;
        
        for (int i = 0; i < particleSystem.Count; i++)
        {
            particleSystem[i].Simulate(value);
        }
    }

}
