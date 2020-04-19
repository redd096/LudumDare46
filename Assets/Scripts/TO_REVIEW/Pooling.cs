using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LudumDare46
{
    public class Pooling
    {
        public int pooledAmount = 20;
        public bool canGrow = true;

        List<GameObject> pooledObjects = new List<GameObject>();

        public void Init(GameObject prefab)
        {
            //spawn amount and deactive
            for (int i = 0; i < pooledAmount; i++)
            {
                GameObject obj = Spawn(prefab);

                obj.SetActive(false);
            }
        }

        public GameObject Instantiate(GameObject prefab)
        {
            //get the first inactive and return
            foreach (GameObject obj in pooledObjects)
            {
                if (obj.activeInHierarchy == false)
                {
                    obj.SetActive(true);
                    return obj;
                }
            }

            //else if can grow, create new one and return it
            if (canGrow)
            {
                return Spawn(prefab);
            }

            return null;
        }

        public GameObject Instantiate(GameObject prefab, Vector3 position, Quaternion rotation)
        {
            //return obj but with position and rotation set
            GameObject obj = Instantiate(prefab);

            obj.transform.position = position;
            obj.transform.rotation = rotation;

            return obj;
        }

        GameObject Spawn(GameObject prefab)
        {
            //instantiate and add to list
            GameObject obj = Object.Instantiate(prefab);
            pooledObjects.Add(obj);

            return obj;
        }
    }

}