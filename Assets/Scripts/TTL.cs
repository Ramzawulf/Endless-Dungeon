#region

using UnityEngine;

#endregion

public class TTL : MonoBehaviour
{
    public Vector3 InitialRotation;
    public float TimeToLive = 1f;

    public void Start()
    {
        Destroy(gameObject, TimeToLive);
        transform.Rotate(InitialRotation);
    }
}