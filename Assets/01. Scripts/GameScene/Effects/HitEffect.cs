using System.Collections;
using UnityEngine;

public class HitEffect : MonoBehaviour
{
    
    void Start()
    {
        gameObject.SetActive(true);
        StartCoroutine(DestroyEffect());
    }
    IEnumerator DestroyEffect()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
