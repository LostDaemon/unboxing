using UnityEngine;

public class CoinsEffectController : MonoBehaviour
{

    public GameObject coinPrefab;
    public Transform target;


    public void SpawnCoins(Vector3 position, int count)
    {
        const float Noise = 0.1f;
        for (var i = 0; i < count; i++)
        {
            var dx = position.x - Noise + Random.Range(0f, Noise * 2);
            var dy = position.y - Noise + Random.Range(0f, Noise * 2);
            var dz = position.z - Noise + Random.Range(0f, Noise * 2);

            var rx = Random.Range(0f, 90f);
            var ry = Random.Range(0f, 90f);
            var rz = Random.Range(0f, 90f);

            var newpos = new Vector3(dx, dy, dz);
            var initialRotation = new Vector3(rx, ry, rz);

            var coin = Instantiate(coinPrefab, newpos, Quaternion.Euler(initialRotation))
            .GetComponent<CoinEffect>();
            coin.Target = target.position;
        }
    }
}
