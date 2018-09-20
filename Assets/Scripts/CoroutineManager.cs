using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// CoroutineManager and singleton approach
/// Used in scriptable objects that require delay actions. 
/// </summary>
public class CoroutineManager : MonoBehaviour {
    public static CoroutineManager Instance;

    private void Start()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    
    public IEnumerator MoveAfter(GameObject go, Vector3 move, float time,  float delay)
    {
        yield return new WaitForSeconds(delay);
        StartCoroutine(Move(go, move, time));
    }

    public IEnumerator Move(GameObject go, Vector3 move, float time)
    {
        float timer = 0;
        Vector3 initPos = go.transform.position;
        while (timer < time)
        {
            go.transform.position = Vector3.Lerp(initPos, initPos + move, timer / time);
            timer += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }

}
