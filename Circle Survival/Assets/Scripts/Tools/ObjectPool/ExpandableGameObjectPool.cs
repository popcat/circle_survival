using UnityEngine;
using System.Collections.Generic;

namespace CircleSurvival
{
    public class ExpandableGameObjectPool : IObjectPool<GameObject>
    {
        private GameObject prefab;
        private Transform root;
        public int size;

        private Stack<GameObject> stack;

        public ExpandableGameObjectPool(
            GameObject prefab, Transform root, int size)
        {
            this.prefab = prefab;
            this.root = root;
            this.size = size;
            stack = new Stack<GameObject>();
            InitStack();
        }

        private void InitStack()
        {
            for (int i = 0; i < size; i++)
            {
                stack.Push(InitObject());
            }
        }

        private GameObject InitObject()
        {
            GameObject obj = GameObject.Instantiate(prefab);
            obj.transform.SetParent(root);
            obj.SetActive(false);
            return obj;
        }

        public GameObject TakeFromPool()
        {
            if (stack.Count > 0)
            {
                return stack.Pop();
            }

            return InitObject();
        }

        public void AddToPool(GameObject obj)
        {
            obj.SetActive(false);
            stack.Push(obj);
        }
    }
}
