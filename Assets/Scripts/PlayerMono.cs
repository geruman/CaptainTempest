using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerMono : MonoBehaviour
{
    GunController[] guns;
    private const float TOP_LIMIT = 4f;
    private const float BOTTOM_LIMIT = -4.5f;
    private const float LEFT_LIMIT = -8f;
    private const float RIGHT_LIMIT = 7.6f;
    [SerializeField] private GameObject spriteGameObject;
    private bool up;
    private bool down;
    private bool left;
    private bool right;
    Animator animationAnimator;
    float speed = 3;
    private bool shoot;

    // Start is called before the first frame update
    void Start()
    {
        animationAnimator = spriteGameObject.GetComponent<Animator>();
        guns = transform.GetComponentsInChildren<GunController>();
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckPlayerInput();

    }

    private void FixedUpdate()
    {
        Vector2 positionMove = Vector2.zero;
        positionMove=CalculateMovement(positionMove);
        float maximunMoveMagnitud = speed * Time.fixedDeltaTime;
        float positionMoveMagnitude = Mathf.Sqrt(positionMove.x*positionMove.x +positionMove.y * positionMove.y);
        float moveScale = maximunMoveMagnitud/positionMoveMagnitude;
        positionMove *= moveScale;
        if (positionMoveMagnitude>0)
        {
            Vector2 newPosition = new Vector2(transform.position.x+positionMove.x, transform.position.y+positionMove.y);
            newPosition = CheckLimits(newPosition);
            transform.position = newPosition;
        }
        AnimatePlayer(positionMove);

    }
    private void CheckPlayerInput()
    {
        CheckPlayerMovement();
        CheckShooting();
    }
    private void CheckShooting()
    {
        shoot = Input.GetKeyDown(KeyCode.LeftControl)||Input.GetKeyDown(KeyCode.RightControl);
        if (shoot)
        {
            shoot = false;
            foreach(GunController gun in guns)
            {
                gun.Shoot();
            }
        }
    }

    private void CheckPlayerMovement()
    {
        up = Input.GetKey(KeyCode.UpArrow);
        down = Input.GetKey(KeyCode.DownArrow);
        left = Input.GetKey(KeyCode.LeftArrow);
        right = Input.GetKey(KeyCode.RightArrow);
    }

    private Vector2 CalculateMovement(Vector2 positionMove)
    {
        if (up)
        {
            positionMove.y += speed * Time.fixedDeltaTime;
        }
        if (down)
        {
            positionMove.y -= speed * Time.fixedDeltaTime;
        }
        if (left)
        {
            positionMove.x -= speed * Time.fixedDeltaTime;
        }
        if (right)
        {
            positionMove.x += speed * Time.fixedDeltaTime;
        }

        return positionMove;
    }
    private void AnimatePlayer(Vector2 positionMove)
    {
        if (positionMove.x<0)
        {
            animationAnimator.SetBool("Back", true);
        }
        else
        {
            animationAnimator.SetBool("Back", false);
        }

        if (positionMove.y>0)
        {
            animationAnimator.SetBool("Up", true);
        }
        else if (positionMove.y<0)
        {
            animationAnimator.SetBool("Down", true);
        }
        else
        {
            animationAnimator.SetBool("Up", false);
            animationAnimator.SetBool("Down", false);
        }
    }

    private Vector2 CheckLimits(Vector2 newPosition)
    {
        if (newPosition.y>TOP_LIMIT)
        {
            newPosition.y = TOP_LIMIT;
        }
        if (newPosition.y<BOTTOM_LIMIT)
        {
            newPosition.y = BOTTOM_LIMIT;
        }
        if (newPosition.x< LEFT_LIMIT)
        {
            newPosition.x = LEFT_LIMIT;
        }
        if (newPosition.x>RIGHT_LIMIT)
        {
            newPosition.x = RIGHT_LIMIT;
        }

        return newPosition;
    }
}
