using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSCounter : MonoBehaviour
{
    private int frameCount = 0;
    private int updateCounter = 0;
    private double fpsSum = 0.0;
    private double dt = 0.0;
    private double fps = 0.0;
    private double updateRate = 4.0;  
    public bool isStarted = false;
    
    void Update()
    {
        if (!isStarted && Input.GetKeyDown(KeyCode.Return)) {
            isStarted = true;
            Invoke("stopCounter", 3);
        }
        if (isStarted) {
            frameCount++;
            dt += Time.deltaTime;
            if (dt > 1.0/updateRate)
            {
                fps = frameCount / dt;
                fpsSum += fps;
                updateCounter += 1;
                Debug.Log(fpsSum / updateCounter);
                frameCount = 0;
                dt -= 1.0/updateRate;
            }
        }
    }

    void stopCounter() {
        isStarted = false;
    }
}
