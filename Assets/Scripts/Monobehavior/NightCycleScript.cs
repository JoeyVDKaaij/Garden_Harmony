using UnityEngine;
using UnityEngine.Playables;

[RequireComponent(typeof(PlayableDirector))]
public class NightCycleScript : MonoBehaviour
{
    private PlayableDirector _pd;

    private bool _cycleStartedPlaying;
    
    // Start is called before the first frame update
    void Start()
    {
        _pd = GetComponent<PlayableDirector>();
        GameManager.ActivatedAction += ActivateNightCycle;
    }

    private void OnDestroy() { GameManager.ActivatedAction -= ActivateNightCycle; }

    // Update is called once per frame
    void Update()
    {
        if (_cycleStartedPlaying && _pd.state != PlayState.Playing)
        {
            _cycleStartedPlaying = false;
            GameManager._instance.ResetActions();
        }
    }

    private void ActivateNightCycle(int pActionsLeft)
    {
        if (pActionsLeft <= 0)
        {
            _pd.Play();
            _cycleStartedPlaying = true;
        }
    }
}
