using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

public class Player : Singleton<Player>
{
    public List<Transform> collectibles;

    [SerializeField] private float speed;
    
    private bool isInTrigger = false;
    
    void Update()
    {
        float horizontalAxis = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        float verticalAxis = Input.GetAxis("Vertical") * Time.deltaTime * speed;

        Vector3 moveDirection = new Vector3(horizontalAxis, 0, verticalAxis);

        transform.position += moveDirection;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Fill") && !isInTrigger)
        {
            var area = other.gameObject.GetComponent<Area>();
            area.Transfer();
            isInTrigger = true;
        }

        if (other.gameObject.CompareTag("Empty") && !isInTrigger)
        {
            StartCoroutine(RemoveObjCoroutine(other.transform));
            isInTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        isInTrigger = false;
        StopAllCoroutines();
    }

    public void CollectObj(Transform obj)
    {
        collectibles.Add(obj);
        obj.SetParent(gameObject.transform);
        obj.DOLocalMove(new Vector3(0, collectibles.IndexOf(obj) / 5f + 2f, 0), 1f);
    }

    private IEnumerator RemoveObjCoroutine(Transform area)
    {
        int count = collectibles.Count;
        for (int i = 0; i < count; i++)
        {
            var obj = collectibles.Last();

            obj.SetParent(null);
            collectibles.Remove(obj);
            
            area.GetComponent<Area>().Collect(obj);
            yield return new WaitForSeconds(0.15f);
        }
    }
}