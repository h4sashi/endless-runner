using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hanzo.Utils
{
    public class ObjectPool : MonoBehaviour
    {
        public static ObjectPool SharedObjectPool;
        public List<GameObject> PooledObjects;
        public GameObject ObjectToPool;
        public int AmountToPool;

        void Awake()
        {
            SharedObjectPool = this;
        }
        // Start is called before the first frame update
        void Start()
        {
            PooledObjects = new List<GameObject>();
            GameObject tmp;

            for (int i = 0; i < AmountToPool; i++)
            {
                tmp = Instantiate(ObjectToPool);
                tmp.SetActive(false);
                PooledObjects.Add(tmp);
            }

        }

      public GameObject GetPooledObject()
{
    for (int i = 0; i < PooledObjects.Count; i++) // Use PooledObjects.Count instead of AmountToPool
    {
        if (!PooledObjects[i].activeInHierarchy)
        {
            return PooledObjects[i];
        }
    }
    return null;
}


    }

}
