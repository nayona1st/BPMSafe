using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Animator animator;
    CharacterController cc;
    

    public GameObject GroundColition;

    public LayerMask layerMask;
    RaycastHit hit;

    bool Grounded;

    float horizontalInput;
    float verticalInput;
    Vector3 forwardMove;
    Vector3 rightMove;
    Vector3 movement;

    public float normalSpeed = 2.5f;
    float bpmSpeed = 0;
    float moveSpeed = 0;

    Vector3 dir;
    public float jumpPower = 2.5f;
    bool jumping = false;

    bool Drop = false;
    bool landing = false;

    bool running = false;

    bool Crouch = false;

    public float BPM = 0f;
    float heartRate = 80f;
    float moveHeartRate = 0f;
    float Rate = 0f;
    public float deathTimer = 10f;

    public bool die = false;
    int revivePoint = 0;

    bool Ability = false;

    GreenAndRad GandR;
    bool GreenRad = false;

    public bool stepCourage = false;

    public bool Immortality = false;

    public bool Ending = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        cc = GetComponent<CharacterController>();
        GandR = GameObject.Find("GreenRad").GetComponent<GreenAndRad>();
        Ending = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!Ending)
        {
            GroundCheck();
            isDrop();
            Jump();
            CharacterMoveAnim();
            Heartbeat();
        }
        else
        {
            End();
        }
    }

    void Heartbeat()
    {
        BPM = moveHeartRate + heartRate; // 심박수 = 이동심박수 + 기본심박수
        bpmSpeed = (moveHeartRate / 50) + 1;
        if (BPM > 130 && !die && !Immortality)
            deathTimer -= Time.deltaTime;
        else if(BPM < 50 && !die && !Immortality)
            deathTimer -= Time.deltaTime;
        else if(!die)
            deathTimer = 10;
        if(BPM <= 0)
            BPM = 0;
        if(deathTimer <= 0)
        {
            deathTimer = 0;
            Dead();
        }
    }

    void Dead()
    {
        if (!die)
        {
            animator.SetTrigger("Dead");
            die = true;
        }
        if (Input.GetKeyDown(KeyCode.Space) && die)
        {
            revivePoint += 1;
        }
        if(revivePoint >= 15)
        {
            Revive();
        }
    }
    void Revive()
    {
        revivePoint = 0;
        moveHeartRate = 0;
        deathTimer = 5;
        animator.SetTrigger("Revive");
        die = false;
    }

    void GameOver()
    {
        Revive();
    }

    void GroundCheck()
    {
        Physics.Raycast(GroundColition.transform.position, Vector3.down, out hit, 1000f, layerMask);
    }


    void Jump()
    {
        if (Grounded && !landing && !die && !GreenRad)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                dir.y = jumpPower * 1.5f;
                animator.SetTrigger("isJump");
                jumping = true;
            }
        }
    }

    void isDrop()
    {
        if (Grounded)
        {
            Drop = false;
        }
        else if (!Grounded && hit.distance > 0.3)
        {
            Drop = true;
            GroundColition.SetActive(true);
            Rate += 1;
        }
        if (Drop && !die)
        {
            if (Grounded || hit.distance <= 0.2)
            {
                if (landing && die)
                    return;

                if (running)
                    running = false;
                landing = true;
                Drop = false;
                animator.SetTrigger("isLanding");
                dir.y = 0;
            }
            else
            {
                if(!jumping)
                    animator.SetTrigger("Drop");
                landing = false;
            }
        }
    }

    public void LandingStart()
    {
        landing = true;
        Crouch = false;
        running = false;
        jumping = false;
        normalSpeed = 4f;
    }
    public void LandingEnd()
    {
        animator.SetTrigger("LandingEnd");
        landing = false;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("GreenAndRad"))
        {
            GreenRad = true;
            if (!GandR.isGreen && animator.GetBool("isWalking"))
            {
                if (BPM > 0)
                    moveHeartRate -= 150f * Time.deltaTime;
            }
        }
        else
            GreenRad = false;
        if (hit.gameObject.CompareTag("Courage") && !stepCourage)
        {
            stepCourage = true;
            Ability = true;
        }
        if (hit.gameObject.CompareTag("Immortality"))
        {
            Immortality = true;
        }
        else
        {
            Immortality = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == 6) // Ground 태그가 있는 오브젝트를 바닥으로 인식
        {
            Grounded = true;
            if (Drop && !die)
            {
                if (landing)
                    return;
                if (running)
                {
                    running = false;
                }
                landing = true;
                Drop = false;
                animator.SetTrigger("isLanding");
                dir.y = 0;
            }
        }
        if(other.gameObject.layer == 7)
        {
            GameOver();
        }
        if (other.gameObject.CompareTag("Angry"))
        {
            moveHeartRate += 55f * Time.deltaTime;
            Ability = true;
        }
        else Ability = false;
        if (other.gameObject.CompareTag("Composure"))
        {
            moveHeartRate -= 55f * Time.deltaTime;
            Ability = true;
        }
        else Ability = false;
    }

    // 바닥에서 떨어졌을 때
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            Grounded = false;
            
        }
    }
    private void CharacterMoveAnim()
    {
        if (!landing && !die)
        {
            horizontalInput = Input.GetAxis("Horizontal");
            verticalInput = Input.GetAxis("Vertical");

            animator.SetFloat("WalkingY", verticalInput);
            animator.SetFloat("WalkingX", horizontalInput);
            if (Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput) <= 0.01f)
            {
                animator.SetBool("isWalking", false);
                Rate += 4;
                if (BPM > 0 && !Ability)
                {
                    moveHeartRate -= Rate * Time.deltaTime;
                }
                Rate = 0;
            }
            else if (Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput) >= 0)
            {
                animator.SetBool("isWalking", true);
                Rate += 4.5f;
                if(BPM < 200 && !Ability)
                {
                    moveHeartRate += Rate * Time.deltaTime;
                }
                Rate = 0;
            }

            //뛰기
            if (Input.GetKey(KeyCode.LeftShift))
            {
                if (!Crouch && !running && !jumping)
                {
                    running = true;
                    normalSpeed *= 1.5f;
                }
            }
            else if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                running = false;
                normalSpeed = 4f;
            }
            if (running && animator.GetBool("isWalking"))
            {
                animator.SetBool("Run", true);
                Rate += 6f;
            }
            else if (!running)
            {
                animator.SetBool("Run", false);
            }

            //앉기
            if (Input.GetKeyDown(KeyCode.C))
            {
                if (!Crouch)
                {
                    if (animator.GetBool("Run"))
                    {
                        animator.SetBool("Run", false);
                        normalSpeed = 4f;
                    }
                    animator.SetTrigger("Crouch");
                    Crouch = true;
                    normalSpeed *= 0.6f;
                }
            }
            else if (Input.GetKeyUp(KeyCode.C))
            {
                animator.SetTrigger("StandUp");
                Crouch = false;
                normalSpeed = 4f;
            }
            if(Crouch && BPM < 200)
            {
                Rate += 8;
            }
            if (bpmSpeed < 0)
                bpmSpeed = 0;
        }
        if (!landing && !die)
        {
            moveSpeed = normalSpeed * bpmSpeed;
            if (moveSpeed < 0.8)
                moveSpeed = 0.8f;
            forwardMove = transform.forward * verticalInput * moveSpeed;
            rightMove = transform.right * horizontalInput * moveSpeed;

            movement = (forwardMove + rightMove + dir) * Time.deltaTime;
            cc.Move(movement);
        }
    }

    void End()
    {
        cc.Move(Vector3.forward);
        animator.SetBool("isWalking", true);
        animator.SetFloat("WalkingY", 1);
    }
}