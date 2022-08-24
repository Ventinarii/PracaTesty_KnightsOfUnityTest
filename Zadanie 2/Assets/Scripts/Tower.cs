using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.GridLayoutGroup;

public class Tower : MonoBehaviour
{
    public static List<GameObject> Towers = new List<GameObject>();
    static public bool Towers100 = false;

    public static void UpdateTowerCount()
    {
        //update 
        if (Towers.Count == 100 && !Towers100)
        {
            Towers100 = true;
            Towers.ForEach(x => x.GetComponent<Tower>().Restart100());
        }

    }


    public float Timer = 6;
    private int Shoot = 12;

    // Start is called before the first frame update
    void Start()
    {
        Towers.Add(gameObject);
        UpdateTowerCount();
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
        var BulletInstance = Bullet.GetBullet();

        BulletInstance.transform.position = gameObject.transform.position;
        BulletInstance.transform.rotation = gameObject.transform.rotation;

        var Script  = BulletInstance.GetComponent<Bullet>();

        //Script.Owner = gameObject;
        //Script.Direction = gameObject.transform.rotation * Vector3.up;
    }

    public void Restart100() {


        gameObject
                    .GetComponent<SpriteRenderer>()
                    .color = Color.red;
        Shoot = 12;
    }

    private void OnDestroy()
    {
        Towers.Remove(gameObject);
        UpdateTowerCount();
    }
}
