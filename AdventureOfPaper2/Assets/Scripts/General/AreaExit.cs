using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class AreaExit : MonoBehaviour
{
    public string areaToLoad = "";
    public string areaTransitionName;
    public StartPoint starpoint = StartPoint.Apoint;
    AreaEntrance entrance;
    public float waitToLoad = 0.1f;
    private bool shouldLoadAfter;
    // Start is called before the first frame update

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            GameManager.instance.LoadWorldMap();
        }
    }
}
