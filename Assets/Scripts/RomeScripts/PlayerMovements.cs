using UnityEngine;

public class PlayerMovements : MonoBehaviour
{
    private Animator anim;
    private float movementSpeed = 2f;
    private float runSpeedMultiplier = 2f;
    private bool isMoving;
    private bool isRunning;
    private Quaternion initialRotationOffset;

    private void Start()
    {
        anim = GetComponent<Animator>();
        initialRotationOffset = transform.rotation;
    }

    private void Update()
    {
        ControlPlayer player = GetComponent<ControlPlayer>();
        if (player != null && !player.IsDead)
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");

            isMoving = (Mathf.Abs(moveHorizontal) > 0.1f || Mathf.Abs(moveVertical) > 0.1f);

            isRunning = Input.GetKey(KeyCode.LeftShift);

            anim.SetBool("Walk", isMoving && !isRunning);
            anim.SetBool("Run", isMoving && isRunning);

            float currentMovementSpeed = movementSpeed;
            if (isRunning)
            {
                currentMovementSpeed *= runSpeedMultiplier;
            }

            if (Mathf.Abs(moveHorizontal) > 0.1f || Mathf.Abs(moveVertical) > 0.1f)
            {
                Vector3 movement = new Vector3(moveHorizontal, 0f, moveVertical);
                transform.position += movement * currentMovementSpeed * Time.deltaTime;

            if (movement != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(movement);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 0.1f);
            }
            }
        }
    }
}