using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float movementRange;

    [SerializeField] private float moveTime;

    private void Start()
    {
        StartCoroutine(MoveToPoint(FindRandomPosition(), moveTime));
    }
    IEnumerator MoveToPoint(Vector2 newPos, float duration)
    {
        float time = 0;
        Vector2 startPos = transform.position;
        while (time < duration)
        {
            transform.position = Vector2.Lerp(startPos, newPos, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        transform.position = newPos;
        StartCoroutine(MoveToPoint(FindRandomPosition(), moveTime));
    }
    /// <summary>
    /// Return random position at top of the screen
    /// </summary>
    /// <returns></returns>
    Vector2 FindRandomPosition()
    {
        return new Vector2(Random.Range(-15, 15), Random.Range(5.5f, 7.5f));
    }
}
