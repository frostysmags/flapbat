using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background_Generation : MonoBehaviour
{
    public scr_backgrounds bg;
    private GameObject[] obstacles;
    private SpriteRenderer[] sr;
    private GameObject[] bounds;
    private SpriteRenderer[] bsr;
    private GameObject[] background;
    private SpriteRenderer[] bgsr;
    private PolygonCollider2D[] obstacleColliders;

    void Start()
    {
        makeObjects();
    }

    void Update()
    {
        if (gameControllerVariables.gameEnd == false)
        {
            manageObjects();
        } else
        {
            for (int i = 0; i < obstacleColliders.Length; i++)
            {
                obstacleColliders[i].enabled = false;
            }
        }
    }

    void makeObjects()
    {
        background = new GameObject[2];
        bounds = new GameObject[2];
        obstacles = new GameObject[2];
        sr = new SpriteRenderer[2];
        bsr = new SpriteRenderer[2];
        bgsr = new SpriteRenderer[2];
        obstacleColliders = new PolygonCollider2D[4];
        for (int i = 0; i < obstacles.Length; i++)
        {
            background[i] = new GameObject("Background_"+i);
            bgsr[i] = background[i].AddComponent<SpriteRenderer>();
            bgsr[i].sprite = getSprite(1);
            changeLocalScale(bgsr[i], 5f);
            bgsr[i].sortingOrder = 0;
            background[i].transform.position = new Vector3(0 + (i * bgsr[i].bounds.size.x), 0);

            bounds[i] = new GameObject("Boundary_"+i);
            bsr[i] = bounds[i].AddComponent<SpriteRenderer>();
            bsr[i].sprite = getSprite(2);
            changeLocalScale(bsr[i], 5f);
            bsr[i].sortingOrder = 10;
            bounds[i].transform.position = new Vector3(0 + (i * bsr[i].bounds.size.x), 0);
            obstacleColliders[i] = bounds[i].AddComponent<PolygonCollider2D>();
            bounds[i].tag = "Boundary";

            obstacles[i] = new GameObject("Obstacle_"+i);
            sr[i] = obstacles[i].AddComponent<SpriteRenderer>();
            sr[i].sprite = getSprite(0);
            changeLocalScale(sr[i], 5f);
            sr[i].sortingOrder = 5;
            obstacles[i].transform.position = new Vector3(20 + (20 * i), 0);
            obstacleColliders[i + 2] = obstacles[i].AddComponent<PolygonCollider2D>();
            obstacles[i].tag = "Boundary";
        }
    }

    void manageObjects()
    {
        for (int i = 0; i < obstacles.Length; i++)
        {
            background[i].transform.position = new Vector3(background[i].transform.position.x - 2 * Time.deltaTime,
                background[i].transform.position.y);

            bounds[i].transform.position = new Vector3(bounds[i].transform.position.x - 5 * Time.deltaTime,
                bounds[i].transform.position.y);

            obstacles[i].transform.position = new Vector3(obstacles[i].transform.position.x - 5 * Time.deltaTime,
                obstacles[i].transform.position.y);


            if (background[i].transform.position.x <= 0 - bgsr[i].bounds.size.x)
            {
                background[i].transform.position = new Vector3(background[i].transform.position.x + (bgsr[i].bounds.size.x * 2), 0);
            }

            
            if (bounds[i].transform.position.x <= 0 - bsr[i].bounds.size.x)
            {
                bounds[i].transform.position = new Vector3(bounds[i].transform.position.x + (bsr[i].bounds.size.x * 2), 0);
            }
            if (obstacles[i].transform.position.x <= gameObject.transform.position.x - sr[i].bounds.size.x/2)
            {
                Destroy(obstacles[i].GetComponent<PolygonCollider2D>());
                Sprite preloadSprite = getSprite(0);
                obstacles[i].transform.position = new Vector3(obstacles[i == 0 ? 1 : 0].transform.position.x + sr[i == 0 ? 1 : 0].bounds.size.x/2 + 20 + preloadSprite.bounds.size.x/2, 0);
                sr[i].sprite = preloadSprite;
                obstacleColliders[i + 2] = obstacles[i].AddComponent<PolygonCollider2D>();

            }
        }
    }

    Sprite getSprite(int num)
    {
        switch (num)
        {
            case 0: return bg.obstacles[Random.Range(0, bg.obstacles.Length)];
            case 1: return bg.backgrounds[Random.Range(0, bg.backgrounds.Length)];
            case 2: return bg.bounds[Random.Range(0, bg.bounds.Length)];
            default: return null;
        }
    }

    private void changeLocalScale(SpriteRenderer tsr, float scale)
    {
        tsr.transform.localScale = new Vector2(scale, scale);
    }
}
