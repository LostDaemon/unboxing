using UnityEngine;

public class CoinController : MonoBehaviour
{
    public Transform Target;
    public float Speed = 1;
    public float RotationSpeed = 1;
    private float lerpTime = 0f;
    private Vector3 _initialPoint;

    void Start()
    {
        _initialPoint = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        lerpTime += Time.deltaTime * Speed / Vector3.Distance(_initialPoint, Target.position);
        lerpTime = Mathf.Clamp01(lerpTime);
        transform.position = Vector3.Lerp(_initialPoint, Target.position, lerpTime);

        var rotation = RotationSpeed * Time.deltaTime;
        transform.Rotate(rotation, rotation, rotation);

        if (lerpTime >= 1f)
        {
            Destroy(this.gameObject);
        }
    }
}

