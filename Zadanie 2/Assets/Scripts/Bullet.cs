using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Bullet : MonoBehaviour
{
    //static members and functions
    public static List<GameObject> Bullets = new List<GameObject>();

    public readonly GameObject BulletPrefab;
    private static GameObject BulletPrefabStaticReference;
    public static GameObject GetBullet()
    {
        if (Bullets.Count == 0)
        {
            var newBullet = Instantiate(BulletPrefabStaticReference);
            Bullets.Add(newBullet);
        }

        var giveBullet = Bullets[0];
        Bullets.RemoveAt(0);

        return giveBullet;
    }
    
    public readonly GameObject TowerPrefab;

    //instance code
    private GameObject Owner;
    private float Velocity = 4f;
    private float Timer;

    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update() {
        if (0 < Timer)
        {
            Timer -= Time.deltaTime;
        }
        else {
            Stop();
        }
    }

    private void Stop()
    {
        gameObject.SetActive(false);

        if (!Tower.Towers100)
        {
            var newTower = Instantiate(TowerPrefab);
            newTower.transform.position = gameObject.transform.position;
        }

        var position = gameObject.transform.position;
        position.z = -1000;
        gameObject.transform.position = position;

        //return bullet to pool of bullets
        Bullets.Add(gameObject);
    }

    public void Restart(GameObject Owner, Quaternion Rotation) {
        gameObject.SetActive(true);

        //position
        var position = gameObject.transform.position;
        position.z = 0;
        gameObject.transform.position = position;

        //rotation
        gameObject.transform.rotation = Rotation;

        rb.velocity = gameObject.transform.rotation * Vector2.up * Velocity;

        var distanceRange = 4 - 1;
        var distance = (Random.value * distanceRange) + 1;
        Timer = Velocity / distance;
    }

    private void OnTriggerEnter(Collider collision) {
        if(Owner != null)
            if (collision.gameObject == Owner)
                return;

        var Go = collision.gameObject;

        GameObject.Destroy(Go);
        Timer = 0;
        Stop();
    }
}
