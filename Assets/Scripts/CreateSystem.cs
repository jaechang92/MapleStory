using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateSystem : MonoBehaviour
{
    static public CreateSystem instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }


    public List<Sprite> enemySprites;

    public GameObject pointParent;
    public List<Transform> pointsList;

    public float createTime;
    private WaitForSeconds waitTime;

    public List<GameObject> objectPool;
    public GameObject createOriginObject;

    // Test
    public List<GameObject> ativeList;
    public int maxAtiveCount = 10;
    public bool isCreate;

    
    void Start()
    {
        init();


        StartCoroutine(OnCreateForDurationOfTime());


    }
    
    void Update()
    {
        
    }

    private void init()
    {
        waitTime = new WaitForSeconds(createTime);

        if (pointParent.GetComponentsInChildren<Transform>() != null)
        {
            foreach (var item in pointParent.GetComponentsInChildren<Transform>())
            {
                if (item == pointParent.transform)
                {
                    continue;
                }
                pointsList.Add(item);
            }

        }
    }

    IEnumerator OnCreateForDurationOfTime()
    {
        while (true)
        {
            if (isCreate && maxAtiveCount > ativeList.Count)
            {

                Debug.Log("inCoroutine");
                if (objectPool.Count == 0)
                {
                    GameObject obj = Instantiate(createOriginObject, this.gameObject.transform);
                    objectPool.Add(obj);
                }

                objectPool[0].SetActive(true);
                
                yield return waitTime;
            }
            else
            {
                yield return null;
            }
        }
    }


    public void DisableObject(GameObject obj)
    {
        objectPool.Add(obj);
        ativeList.Remove(obj);
    }

    public void OnEnableObject(GameObject obj)
    {
        ativeList.Add(obj);
        objectPool.Remove(obj);
    }

}
