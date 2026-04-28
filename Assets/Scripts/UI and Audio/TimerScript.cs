using UnityEngine;

public class TimerScript : MonoBehaviour
{
    public float time;
    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
    }
}
