using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    private void Update()
    {
        this.transform.position = Input.mousePosition;
    }
}
