using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class ScreenMovement : MonoBehaviour
{
    [SerializeField]
    private Camera gameCamera;
    [SerializeField]
    private BackgroundMovement speedData;
    [SerializeField]
    private SpriteRenderer firstSpriteRenderer;
    [SerializeField]
    private SpriteRenderer secondSpriteRenderer;
    
    private void FixedUpdate()
    {
        firstSpriteRenderer.transform.position = CalculateNextPositionForFirstBackground();
        secondSpriteRenderer.transform.position = RepositionSecondBackgroundAcordingToFirst();
    }

    private Vector3 RepositionSecondBackgroundAcordingToFirst()
    {
        if (firstSpriteRenderer.transform.position.x>0)
        {
            return new Vector2(firstSpriteRenderer.transform.position.x-firstSpriteRenderer.bounds.size.x, firstSpriteRenderer.transform.position.y);
        }
        else
        {
            return new Vector2(firstSpriteRenderer.transform.position.x+firstSpriteRenderer.bounds.size.x, firstSpriteRenderer.transform.position.y);
        }
    }
    private Vector2 CalculateNextPositionForFirstBackground()
    {
        
        Vector2 pos = firstSpriteRenderer.gameObject.transform.position;
        Vector2 movement = new Vector2(-1 * speedData.speed * Time.fixedDeltaTime, 0);
        Vector2 newPosition = pos+movement;
        double positionXlimit = (firstSpriteRenderer.bounds.size.x/2);
        if (newPosition.x <= -18)
        {
            newPosition = new Vector2(17, newPosition.y);
        }
        return newPosition;
    }
}
