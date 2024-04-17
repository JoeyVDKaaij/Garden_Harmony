using UnityEngine;

public class RestartValuesScript : MonoBehaviour
{
    public void RestartValues()
    {
        GameManager._instance.ResetValues();
        SceneSwitch._instance.scene_changer(2);
    }
}
