    using UnityEngine;
    using UnityEngine.SceneManagement;
    using TMPro;

    public class PlayerMovement : MonoBehaviour
    {
        public float moveSpeed;
        public float sprintSpeed;
        public float maxStamina;
        public float staminaRegenRate;
        public float sprintStaminaCost;
        public Animator anim;
        private bool isSprinting;
        public float currentStamina;

        public TMP_Text staminaText;

        private void Start()
        {
            currentStamina = maxStamina;
        }

        private void Update()
        {
            PlayerMove();
            UpdateStamina();
            UpdateStaminaText();
        }

        public void PlayerMove()
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            Vector3 move = new Vector3(horizontal, 0f, vertical);
            anim.SetFloat("Vertical", vertical);
            anim.SetFloat("Horizontal", horizontal);
            move = Vector3.ClampMagnitude(move, 1);

            anim.SetBool("MovingLeft", horizontal < 0);
            anim.SetBool("MovingDown", vertical < 0);

            if (move == Vector3.zero)
            {
                anim.SetBool("MovingLeft", false);
                anim.SetBool("MovingDown", false);
            }

            if (move.magnitude > 0)
            {
                // Calculate rotation based on movement direction
                Quaternion targetRotation = Quaternion.LookRotation(move);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10);
            }

            // Sprinting input
            if (Input.GetKeyDown(KeyCode.LeftShift) && currentStamina >= sprintStaminaCost)
            {
                isSprinting = true;
            }
            else if (Input.GetKeyUp(KeyCode.LeftShift) || currentStamina <= 0)
            {
                isSprinting = false;
            }

            // Move player in direction of input
            if (isSprinting && currentStamina > 0)
            {
                float sprintCost = sprintStaminaCost * Time.deltaTime;
                if (currentStamina >= sprintCost)
                {
                    transform.Translate(move * Time.deltaTime * sprintSpeed, Space.World);
                    anim.speed = sprintSpeed / moveSpeed; // Adjust the animation speed based on sprinting speed
                    currentStamina -= sprintCost;
                }
                else
                {
                    // Not enough stamina to sprint, move at normal speed
                    transform.Translate(move * Time.deltaTime * moveSpeed, Space.World);
                    anim.speed = 1f; // Reset the animation speed to default
                    isSprinting = false; // Automatically disengage sprint
                }
            }
            else
            {
                // Not sprinting, move at normal speed
                transform.Translate(move * Time.deltaTime * moveSpeed, Space.World);
                anim.speed = 1f; // Reset the animation speed to default
            }
        }

        private void UpdateStamina()
        {
            if (!isSprinting && currentStamina < maxStamina)
            {
                currentStamina += staminaRegenRate * Time.deltaTime;
                currentStamina = Mathf.Clamp(currentStamina, 0f, maxStamina);
            }
        }

        private void UpdateStaminaText()
        {
            staminaText.text = "Stamina: " + Mathf.RoundToInt(currentStamina).ToString();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {
                //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                SceneManager.LoadScene("GameOver");
            }
        }
    }
