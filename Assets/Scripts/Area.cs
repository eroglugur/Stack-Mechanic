using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DG.Tweening;

public class Area : MonoBehaviour
{
    public List<Transform> collectibles;

    public void Collect(Transform obj)
    {
        collectibles.Add(obj);
        obj.SetParent(gameObject.transform);
        obj.DOLocalMove(new Vector3(0,collectibles.IndexOf(obj) / 5f + 2f,0), 1f);
    }

    public void Transfer()
    {
        StartCoroutine(RemoveCoroutine());
    }

    private IEnumerator RemoveCoroutine()
    {
        int count = collectibles.Count;
        for (int i = 0; i < count; i++)
        {
            var obj = collectibles.Last();

            obj.SetParent(null);
            collectibles.Remove(obj);

            Player.Instance.CollectObj(obj);
            yield return new WaitForSeconds(0.15f);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        StopAllCoroutines();
    }
}