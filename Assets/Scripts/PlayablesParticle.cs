using UnityEngine;
using UnityEngine.Playables;

public class PlayablesParticle
{
	PlayableGraph playableGraph;
	ScriptPlayable<ParticleSystemPlayable> particleSystemPlayable;

	/// <summary>
	/// 初期化
	/// </summary>
	public void Init(GameObject particleSystem)
	{
		playableGraph = PlayableGraph.Create();

		// ScriptPlayableを作る
		particleSystemPlayable = ScriptPlayable<ParticleSystemPlayable>.Create(playableGraph, 0);

		// Playable Behaviourを取得して初期化
		particleSystemPlayable.GetBehaviour().Initialize(particleSystem);

		// Playable Outputを作ってPlayableを登録
		var output = ScriptPlayableOutput.Create(playableGraph, "ParticleSystem");
		output.SetSourcePlayable(particleSystemPlayable);
		
		playableGraph.Play();
	}

	/// <summary>
	/// 削除
	/// </summary>
	public void Release()
	{
		if (playableGraph.IsValid())
		{
			playableGraph.Destroy();
		}
	}

	/// <summary>
	/// 再生
	/// </summary>
	public void Play()
	{
		if (particleSystemPlayable.IsValid())
		{
			particleSystemPlayable.Play();
		}
	}

	/// <summary>
	/// 停止
	/// </summary>
	public void Pause()
	{
		if (particleSystemPlayable.IsValid())
		{
			particleSystemPlayable.Pause();
		}
	}

	/// <summary>
	/// 止める
	/// </summary>
	public void Stop()
	{
		if (particleSystemPlayable.IsValid())
		{
			particleSystemPlayable.GetBehaviour().Stop();
		}
	}

	/// <summary>
	/// スピード設定
	/// </summary>
	public void SetSpeed(float value)
	{
		if (particleSystemPlayable.IsValid())
		{
			particleSystemPlayable.GetBehaviour().SetSpeed(value);
		}
	}

	/// <summary>
	/// 時間設定
	/// </summary>
	public void SetTime(float value)
	{
		if (particleSystemPlayable.IsValid())
		{
			particleSystemPlayable.GetBehaviour().SetTime(value);
		}
	}

	/// <summary>
	/// 再生時間取得
	/// </summary>
	public float GetPlayTime()
	{
		if (particleSystemPlayable.IsValid())
		{
			return particleSystemPlayable.GetBehaviour().GetPlayTime();
		}

		return 0;
	}
	
	/// <summary>
	/// 再生Max時間取得
	/// </summary>
	public float GetPlayMaxTime()
	{
		if (particleSystemPlayable.IsValid())
		{
			return particleSystemPlayable.GetBehaviour().GetPlayMaxTime();
		}

		return 0;
	}
}
