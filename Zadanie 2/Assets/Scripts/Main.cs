using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    public static List<GameObject> Towers = new List<GameObject>();
    public static List<GameObject> Bullets = new List<GameObject>();
    static public bool Towers100 = false;
    static public Main main;
    

    public GameObject Text;
    public GameObject BulletPrefab;

    // Start is called before the first frame update
    void Start()
    {
        main = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject GetBullet() {

        if (Bullets.Count == 0) {
            var newBullet = Instantiate(BulletPrefab);
            Bullets.Add(newBullet);
        }

        var giveBullet = Bullets[0];
        Bullets.RemoveAt(0);

        giveBullet.GetComponent<Bullet>().Restart();

        return giveBullet;
    }

    public void UpdateTowerCount() {
        //update 
        if (Towers.Count == 100 && !Towers100) {
            Towers100 = true;
            Towers.ForEach(x => x.GetComponent<Tower>().Restart100());
        }
        
    }
}
