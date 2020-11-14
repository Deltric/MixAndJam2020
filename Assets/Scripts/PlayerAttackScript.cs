using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackScript : MonoBehaviour {

    private bool attacking = false;
    private bool attacked = false;
    private float attackTimer = 0f;

    [SerializeField]
    private float attackSpeed = 2f;

    [SerializeField]
    private float attackRange; 

    [SerializeField]
    private LayerMask enemyMask;

    [SerializeField]
    private Transform claw;

    [SerializeField]
    private Transform startingPosition;

    [SerializeField]
    private Transform endPosition;

    [SerializeField]
    private Transform attackPosition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && !attacking) {
            attacking = true;
        }
    }

    void FixedUpdate() {
        if(attacking) {
            attackTimer += Time.fixedDeltaTime * attackSpeed;

            // Reset attacking to default
            if(attackTimer > 1f) {
                attacking = false;
                attacked = false;
                attackTimer = 0f;
                claw.rotation = startingPosition.rotation;
                claw.position = startingPosition.position;
                return;
            }

            // Rotation
            Vector3 clawPos = claw.position;
            claw.rotation = new Quaternion(claw.rotation.x - (endPosition.rotation.x * attackTimer), claw.rotation.y - (endPosition.rotation.y * attackTimer), claw.rotation.z - (endPosition.rotation.z * attackTimer), 1);
            claw.transform.position = new Vector3(clawPos.x + (-0.03f * Mathf.Clamp(attackTimer, 0, 1f)), clawPos.y + (-0.03f * Mathf.Clamp(attackTimer, 0, 1f)), clawPos.z);

            // Execute attack
            if(attackTimer >= 0.8f && !attacked) {
                attacked = true;
                Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPosition.position, attackRange, enemyMask);
                for(int i = 0; i < enemies.Length; i++) {
                    Destroy(enemies[i].gameObject);
                    Debug.Log("hi");
                }
            }
        }
    }

    void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPosition.position, attackRange);
    }

}