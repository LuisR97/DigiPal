using UnityEngine;

public class SpinCube : MonoBehaviour
{
    public float multiple;
    void Update()
    {
        transform.Rotate(Vector3.up* Time.deltaTime * multiple);
    }
}
