using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Ithas
{
    //base class for all enemy scripts to inherit
    public class EnemyScript : MonoBehaviour
    {
        private GameController gameController;
        private bool isAttacking = false;
        private float attackStartTime = 0f;
        private float nextAttackTime = 0f; //tracker

        public Transform player;
        public Slider hpBar;
        public int enemyNo;

        [Header("Stats")]
        public int enemyId;
        public string enemyName;
        public float hp;
        public float damage;
        public float moveSpeed;
        public float exp;

        [Header("Chase Stats")]
        public float chaseRadius;
        public float chaseEndRadius; //must be lesser than attackRadius
        public float attackRadius;

        [Header("Attack Stats")]
        public float attackRange;
        public float attackRate;
        public float attackDelay;

        [Header("Others")]
        public string enemyPrefabName;
        public Vector2 homePosition;
        public LayerMask playerLayer;

        [Header("State Machine")]
        public EnemyState currentState;

        public void ChangeState(EnemyState newState)
        {
            if (currentState != newState)
            {
                currentState = newState;
            }
        }

        public void ReadSpawnEnemyPrefab()
        {
            gameController = FindObjectOfType<GameController>();
            CsvReader csvReader = FindObjectOfType<CsvReader>();
            StartLevel startLevel = FindObjectOfType<StartLevel>();
            enemyNo = gameController.currentEnemyNo;
            if (csvReader != null && csvReader.levelDataList.levelData.Length > 0)
            {
                foreach (var levelData in csvReader.levelDataList.levelData)
                {
                    if (levelData.enemyNo == enemyNo)
                    {
                        enemyPrefabName = gameController.GetEnemyPrefabName(startLevel);
                        homePosition = gameController.GetEnemyHomePosition(startLevel);
                        InstantiateEnemyPrefabs();
                    }
                }
            }
        }

        public void InstantiateEnemyPrefabs()
        {
            GameObject enemyPrefab = Resources.Load<GameObject>("Prefabs/Enemy/" + enemyPrefabName);
            if (enemyPrefab != null)
            {
                GameObject enemyInstance = Instantiate(enemyPrefab, homePosition, Quaternion.identity); //instantiate
                EnemyScript enemy = enemyInstance.GetComponent<EnemyScript>(); //get component of instantiated enemy
                if (enemy != null)
                {
                    if (enemy is EnemyTypeGuard guardScript)
                    {
                        guardScript.player = player;
                        guardScript.enemyId = 1;
                        guardScript.ReadEnemyData();
                        guardScript.SetEnemyHpBar();
                        guardScript.enemyPrefabName = enemyPrefabName;
                        guardScript.homePosition = homePosition;
                        guardScript.enemyNo = enemyNo;
                    }
                    else if (enemy is EnemyTypePolice policeScript)
                    {
                        policeScript.player = player;
                        policeScript.enemyId = 2;
                        policeScript.ReadEnemyData();
                        policeScript.SetEnemyHpBar();
                        policeScript.enemyPrefabName = enemyPrefabName;
                        policeScript.homePosition = homePosition;
                        policeScript.enemyNo = enemyNo;
                    }
                    else if (enemy is EnemyTypeObjectWeak weakObjectScript)
                    {
                        weakObjectScript.player = player;
                        weakObjectScript.enemyId = 99;
                        weakObjectScript.ReadEnemyData();
                        weakObjectScript.SetEnemyHpBar();
                        weakObjectScript.enemyPrefabName = enemyPrefabName;
                        weakObjectScript.homePosition = homePosition;
                        weakObjectScript.enemyNo = enemyNo;
                    }
                    else if (enemy is EnemyTypeObjectStrong strongObjectScript)
                    {
                        strongObjectScript.player = player;
                        strongObjectScript.enemyId = 98;
                        strongObjectScript.ReadEnemyData();
                        strongObjectScript.SetEnemyHpBar();
                        strongObjectScript.enemyPrefabName = enemyPrefabName;
                        strongObjectScript.homePosition = homePosition;
                        strongObjectScript.enemyNo = enemyNo;
                    }
                    enemyNo++;
                    gameController.currentEnemyNo++;
                }
            }
        }

        public void ReadEnemyData()
        {
            gameController = FindObjectOfType<GameController>();
            CsvReader csvReader = FindObjectOfType<CsvReader>();
            if (csvReader != null && csvReader.enemyTypeDataList.enemyTypeData.Length > 0)
            {
                enemyName = gameController.GetEnemyName(this);
                hp = gameController.GetEnemyHp(this);
                damage = gameController.GetEnemyDamage(this);
                moveSpeed = gameController.GetEnemyMoveSpeed(this);
                exp = gameController.GetEnemyExp(this);
                chaseRadius = gameController.GetEnemyChaseRadius(this);
                chaseEndRadius = gameController.GetEnemyChaseEndRadius(this);
                attackRadius = gameController.GetEnemyAttackRadius(this);
                attackRange = gameController.GetEnemyAttackRange(this);
                attackRate = gameController.GetEnemyAttackRate(this);
                attackDelay = gameController.GetEnemyAttackDelay(this);
            }
        }

        public void SetEnemyHpBar() //only for starting
        {
            hpBar.gameObject.SetActive(false);
            hpBar.maxValue = hp;
            hpBar.value = hp;
        }

        public void UpdateEnemyHpBar(float damage) //everytime when damaged
        {
            hpBar.gameObject.SetActive(true);
            hpBar.value -= damage;
        }

        public void AttackPlayer()
        {
            if (!isAttacking && Time.time >= nextAttackTime) //if not already attacking and current time >= nextAttackTime
            {
                isAttacking = true;
                attackStartTime = Time.time; //set attackStartTime to current time
            }

            if (isAttacking && Time.time >= (attackStartTime + attackDelay)) //isAttacking and current time >= (current time + attackDelay)
            {
                Collider2D hitPlayer = Physics2D.OverlapCircle(transform.position, attackRange, playerLayer); //detect player in range

                if (hitPlayer != null) //damage player
                {
                    GameController gameController = FindObjectOfType<GameController>();
                    if (gameController != null)
                    {
                        gameController.DamagePlayer(damage);
                    }
                }

                ChangeState(EnemyState.attack);

                nextAttackTime = Time.time + 1f / attackRate; //for controlling attack speed, higher attackRate = faster attack speed
                isAttacking = false;
            }
        }
    }

    public enum EnemyState
    {
        idle,
        move,
        attack
    }
}
