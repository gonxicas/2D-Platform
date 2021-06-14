using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using HeroesGames.Arcade.Physics;
using HeroesGames.Arcade.SO;
using HeroesGames.Arcade.SO.Platform;
using HeroesGames.Arcade.Utils;
using UnityEngine;
using Random = UnityEngine.Random;

namespace HeroesGames.Arcade.Platforms
{
    public class PlatformController : MovementPhysics
    {
        [SerializeField] private PlatformSO platform;
        /// <summary>
        /// Tiempo que espera la plataforma después de llegar a cada destino.
        /// </summary>
        [Tooltip("Tiempo que espera la plataforma después de llegar a cada destino.")]
        [SerializeField] private float waitTime;
        [SerializeField] private int speed;
        [SerializeField] private bool playOnAwake = true;
        [SerializeField] private bool stopPlatform;
        [SerializeField] private bool randomDelayToStart;
        [SerializeField] private float delayToStart = 1f;
        [SerializeField] private float maxRandomDelay = 2f;

        [SerializeField] private StringVariableSO playerTag;
        
        private CoroutineInfo _moveCoroutine;
        private CoroutineInfo _platformSequence;

        protected override void Awake()
        {
            base.Awake();
            platform.CreateFinalList();
            if(playOnAwake)
                StartMovimgPlatform();

        }

        public void StartMovimgPlatform()
        {
            _platformSequence.currentCoroutine = PlatformSequence();
            if(!_platformSequence.isRunning)
                StartCoroutine(_platformSequence.currentCoroutine);
        }
        private IEnumerator Move(Vector2 direction, int distance, int speed )
        {
            if (_moveCoroutine.isRunning) yield break;
            _moveCoroutine.isRunning = true;
            var pixelsMoved = 0;
            while (pixelsMoved<distance)
            {
                MoveByPixels(transform,direction,speed);
                yield return new WaitForSeconds(CoroutineWaitSeconds);
                pixelsMoved += speed;
            }
            
            _moveCoroutine.isRunning = false;
        }
        private IEnumerator PlatformSequence()
        {
            _platformSequence.isRunning = true;
            var delay = randomDelayToStart ? Random.Range(0f, maxRandomDelay) :delayToStart;
            yield return new WaitForSeconds(delay);
            
            while (!stopPlatform)
            {
                for (int i = 0; i < platform.DirectionList.Count; i++)
                {
                    _moveCoroutine.currentCoroutine = Move(platform.DirectionList[i].ToVector2(),
                        platform.DirectionList[i].Distance, speed);
                    StartCoroutine(_moveCoroutine.currentCoroutine);
                    while (_moveCoroutine.isRunning)
                    {
                        yield return 1;
                    }
                    if(waitTime!=0)
                        yield return new WaitForSeconds(waitTime);
                }
            }

            _platformSequence.isRunning = false;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (!other.collider.CompareTag(playerTag.RuntimeValue)) return;
            
            other.collider.attachedRigidbody.gameObject.transform.SetParent(transform);
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            if (!other.collider.CompareTag(playerTag.RuntimeValue)) return;
            
            transform.DetachChildren();
            
        }
    }
}