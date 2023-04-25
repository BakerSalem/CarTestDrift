using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowD.Combat
{

    public class PlayerControlCombat : MonoBehaviour
    {
        [SerializeField] private bool combatEnable;
        [SerializeField] private float InputTimer, attackRadius, attackDamage;
        [SerializeField] private Transform attackHitBoxPos;
        [SerializeField] private LayerMask WhatIsDamageable;
        private Animator anim;
        private bool gotInput, isAttacking;
        private float lastInputTime = Mathf.NegativeInfinity;


        private void Start()
        {
            anim = GetComponent<Animator>();
            anim.SetBool("canAttack", combatEnable);

        }
        public void Update()
        {
            CheckCombatInput();
            CanAttacks();
        }

        private void CheckCombatInput()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (combatEnable)
                {
                    gotInput = true;
                    lastInputTime = Time.deltaTime;
                }
            }
        }
        private void CanAttacks()
        {
            if (gotInput)
            {//preform attack
                gotInput = false;
                isAttacking = true;
                anim.SetBool("Attack", true);
                anim.SetBool("isAttacking", isAttacking);
            }
            if (Time.time >= lastInputTime + InputTimer)
            {
                //Wit for new input 
                gotInput = false;
            }
        }
        private void CheckAttackHitBox()
        {
            Collider2D[] delectedObject = Physics2D.OverlapCircleAll(attackHitBoxPos.position, attackRadius, WhatIsDamageable);
            foreach (Collider2D collider in delectedObject)
            {
                collider.transform.parent.SendMessage("Damage", attackDamage);
            }
        }
        private void FinishAttack()
        {
            isAttacking = false;
            anim.SetBool("isAttacking", isAttacking);
            anim.SetBool("Attack", false);
        }
        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(attackHitBoxPos.position, attackRadius);
        }
    }
}