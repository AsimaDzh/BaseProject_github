using UnityEngine;

public class PlayerXPTest : MonoBehaviour
{
    public PlayerProgression progression;

    private void Awake()
    {
        if (progression == null)
            progression = FindFirstObjectByType<PlayerProgression>();
    }

    private void Update()
    {
        if (progression == null) return;

        if (Input.GetKeyDown(KeyCode.X))
            progression.AddXP(50f);
    }
}
