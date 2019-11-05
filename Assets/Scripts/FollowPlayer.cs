using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform PlayerPosition;
    public bool IsLower = false;
    public float LowerLength = 0.5f;

    private void Update()
    {
        if (IsLower)
            transform.position = new Vector3(PlayerPosition.position.x, PlayerPosition.position.y - LowerLength, PlayerPosition.position.z);
        else
            transform.position = PlayerPosition.position;
    }
}
