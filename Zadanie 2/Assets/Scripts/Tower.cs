using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.GridLayoutGroup;

public class Tower : MonoBehaviour
{
    public float Timer = 6;
    private int Shoot = 12;

    // Start is called before the first frame update
    void Start()
    {
        Main.Towers.Add(gameObject);
        Main.main.UpdateTowerCount();
    }

    // Update is called once per frame
    void Update()
    {
        if (Timer <= 0 && 0 < Shoot)
        {
            var angleRange = 45 - 15;
            var angle = (Random.value * angleRange) + 15;

            gameObject.transform.Rotate(
                0,
                0,
                gameObject.transform.rotation.z + angle
                );
            Fire();
            Shoot--;
            if (Shoot == 0)
                gameObject
                    .GetComponent<SpriteRenderer>()
                    .color = Color.white;
            Timer = 0.5f;
        }
        else
        {
            Timer -= Time.deltaTime;
        }
    }

    private void Fire()
    {
        var Bullet = Main.main.GetBullet();

        Bullet.transform.position = gameObject.transform.position;
        Bullet.transform.rotation = gameObject.transform.rotation;

        var Script  = Bullet.GetComponent<Bullet>();

        Script.Owner = gameObject;
        Script.Direction = gameObject.transform.rotation * Vector3.up;
    }

    public void Restart100() {


        gameObject
                    .GetComponent<SpriteRenderer>()
                    .color = Color.red;
        Shoot = 12;
    }

    private void OnDestroy()
    {
        Main.Towers.Remove(gameObject);
        Main.main.UpdateTowerCount();
    }
}
