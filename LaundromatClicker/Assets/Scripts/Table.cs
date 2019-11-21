using UnityEngine;

public class Table : MonoBehaviour
{
    public static Table instance;

    public GameObject box;

    public Vector2 randomItemIntervalRange;
    public bool timerRunning = true;

    private float thisRandomItemInterval = 10;
    private float currentRandomItemInterval;

    private void Start()
    {
        instance = this;
    }

    //A timer to ensure that the box is enabled after a random amount of time (within a range) from when it was disabled
    private void Update()
    {
        if (timerRunning)
        {
            currentRandomItemInterval += Time.deltaTime;

            if (currentRandomItemInterval >= thisRandomItemInterval)
            {
                box.SetActive(true);
                thisRandomItemInterval = Random.Range(randomItemIntervalRange[0], randomItemIntervalRange[1]);
                timerRunning = false;
                currentRandomItemInterval = 0;
            }
        }
    }
}
