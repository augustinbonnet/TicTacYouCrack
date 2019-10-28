using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform PlayerPosition;

    private void Update()
    {
        transform.position = PlayerPosition.position;
    }
}
