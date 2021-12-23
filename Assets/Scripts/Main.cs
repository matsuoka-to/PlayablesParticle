
using UnityEngine;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
    [SerializeField]
    GameObject particleSystem;

    [SerializeField]
    Text btnText;

    [SerializeField]
    InputField speedText;

    [SerializeField]
    Slider timeSlider;

    [SerializeField]
    Text timeNow;

    [SerializeField]
    Text timeMax;
    
    [SerializeField]
    GameObject parent;

    PlayablesParticle playable;
    bool isPlay = false;

    enum Type
    {
        Load,
        Play,
    }
    
    /// <summary>
    /// ボタン処理
    /// </summary>
    public void OnButton(int id)
    {
        switch (id)
        {
            case (int)Type.Load:
            {
                if (playable == null)
                {
                    playable = new PlayablesParticle();
                    playable.Init(GameObject.Instantiate(particleSystem, parent.transform, false));

                    var time = playable.GetPlayMaxTime();
                    timeMax.text = string.Format("{0}", time);
                    timeSlider.maxValue = time;
                    
                    ButtonUpdate();
                }
            }
            break;


            case (int)Type.Play:
            {
                ButtonUpdate();
            }
            break;
        }
    }

    /// <summary>
    /// コマ送り処理
    /// </summary>
    public void OnSlider(float value)
    {
        if (playable != null)
        {
            // 停止中のみ更新
            if (isPlay == false)
            {
                var time = string.Format("{0:F2}", value);
                timeNow.text = time;
                playable.SetTime(float.Parse(time));
            }
        }
    }

    /// <summary>
    /// ボタンテキスト更新
    /// </summary>
    void ButtonUpdate()
    {
        if (playable != null)
        {
            if (isPlay == false)
            {
                particleSystem.SetActive(true);

                btnText.text = "Stop";
                        
                var time = float.Parse(speedText.text);
                playable.SetSpeed(time);
                playable.Play();
            }
            else
            {
                btnText.text = "Play";
                playable.Pause();
            }

            isPlay = !isPlay;
        }
    }
    
    void Update()
    {
        if (playable != null)
        {
            // 再生中のみ更新
            if (isPlay == true)
            {
                var time = string.Format("{0:F2}", playable.GetPlayTime());
                timeNow.text     = time;
                timeSlider.value = float.Parse(time);
            }
        }
    }
}
