using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Bullet : MonoBehaviour
{
    public GameObject Tower;

    public GameObject Owner;
    public Vector3 Direction;

    private float Speed = 4f;
    private float Timer;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.up * Speed;

        SetTimer();

    }

    void Update() {
        if (0 < Timer)
        {
            Timer -= Time.deltaTime;

            var position = gameObject.transform.position;
            position += Direction * Speed * Time.deltaTime;
            gameObject.transform.position = position;
        }
        else {
            gameObject.SetActive(false);

            if (!Main.Towers100)
            {
                var newTower = Instantiate(Tower);
                newTower.transform.position = gameObject.transform.position;
            }

            var position = gameObject.transform.position;
            position.z = -1000;
            gameObject.transform.position = position;

            Main.Bullets.Add(gameObject);
        }
    }

    public void Restart() {

        gameObject.SetActive(true);

        var position = gameObject.transform.position;
        position.z = 0;
        gameObject.transform.position = position;

        SetTimer();
    }

    private void SetTimer() {
        var distanceRange = 4 - 1;
        var distance = (Random.value * distanceRange) + 1;
        Timer = Speed / distance;
    }

    private void OnTriggerEnter(Collider collision) {
        if(Owner != null)
            if (collision.gameObject == Owner)
                return;
        if (collision.gameObject.tag.Contains("Bullet"))
            return;

        GameObject.Destroy(Tower);
        Timer = 0;
    }
}
