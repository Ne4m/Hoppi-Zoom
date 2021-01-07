using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Platforms
{
    /*TODO
    - Write the platform pack #4's movement code
    - Get camera at the scene instead of using Camera.Main
    - *
    */
    public class PlatformMovement : MonoBehaviour
    {
        private Transform[] _children;

        private bool _isClosingGap;
        private bool _isMovingRight;
    
        private Vector3 _left;
        private Vector3 _right;
        private Vector3 _middle;

        #region Platform #4
        private GameObject _middleGameObject;
        private SpriteRenderer _middleSpriteRenderer;
        private BoxCollider2D _middleCollider2D;
        private bool _isDecreasing;
        private bool _isOn;
        private float _alpha;
        #endregion
        
        
        private int _difficulty, _platformNumber;
        
        
        public void GetChildren(float stayTime)
        {
            _children = new Transform[this.transform.childCount];
            for (int i = 0; i < this.transform.childCount; i++)
            {
                if (_platformNumber == 4)
                {
                    _middleGameObject = this.transform.GetChild(i).gameObject;
                    _middleSpriteRenderer = _middleGameObject.GetComponent<SpriteRenderer>();
                    _middleCollider2D = _middleGameObject.GetComponent<BoxCollider2D>();
                    StartCoroutine(StayCurrentState(stayTime));
                }
                
                _children[i] = this.transform.GetChild(i).transform;
            }
        }
        public void GetPlatformInfo()
        {
            if (this.gameObject.tag.Contains("1"))
            {
                _platformNumber = 1;
            }
            else if (this.gameObject.tag.Contains("2"))
            {
                _platformNumber = 2;
            }
            else if (this.gameObject.tag.Contains("3"))
            {
                _platformNumber = 3;
            }else if (this.gameObject.tag.Contains("4"))
            {
                _platformNumber = 4;
            }
        }
        public void MoveChildren(float platformSpeed, float moveDistance, float pFourTransition)
        {
            if(_platformNumber != 4) 
            {  
                _left = _children[0].position;
                _right = _children[1].position;
                if(_platformNumber == 3) _middle = _children[2].position;
            }
            else if(_platformNumber == 4)
            {
                _middle = _children[0].position;
            }
        
            if (Camera.main is null) return;
            Vector3 leftScreenPos = Camera.main.WorldToScreenPoint(_left);
            Vector3 rightScreenPos = Camera.main.WorldToScreenPoint(_right);

            if (_platformNumber == 1)
            {
                float distance = Vector2.Distance(_left, _right);

                if (leftScreenPos.x < 100 && Mathf.Abs(rightScreenPos.x - Screen.width) < 100)
                {
                    _isClosingGap = true;
                }
                else if (distance < moveDistance)
                {
                    _isClosingGap = false;
                }

                if (_isClosingGap)
                {
                    _left += new Vector3(platformSpeed * Time.fixedDeltaTime, 0, 0);
                    _right += new Vector3(platformSpeed * Time.fixedDeltaTime * -1, 0, 0);
                }
                else
                {
                    _left += new Vector3(platformSpeed * Time.fixedDeltaTime * -1, 0, 0);
                    _right += new Vector3(platformSpeed * Time.fixedDeltaTime, 0, 0);
                }
            }

            if (_platformNumber== 2)
            {
                if (Mathf.Abs(rightScreenPos.x - Screen.width) < 100)
                {
                    _isMovingRight = false;
                }
                else if (leftScreenPos.x < 100)
                {
                    _isMovingRight = true;
                }

                if (_isMovingRight)
                {
                    _left += new Vector3(platformSpeed * Time.fixedDeltaTime, 0, 0);
                    _right += new Vector3(platformSpeed * Time.fixedDeltaTime, 0, 0);
                }
                else
                {
                    _left += new Vector3(platformSpeed * Time.fixedDeltaTime * -1, 0, 0);
                    _right += new Vector3(platformSpeed * Time.fixedDeltaTime * -1, 0, 0);
                }
            }

            if(_platformNumber == 3)
            {
                float leftDistance = Vector3.Distance(_left, _middle);
                float rightDistance = Vector3.Distance(_right, _middle);

                if(leftDistance < moveDistance)
                {
                    _isMovingRight = true;
                }
                else if(rightDistance < moveDistance)
                {
                    _isMovingRight = false;
                }

                if (_isMovingRight)
                {
                    _middle += new Vector3(platformSpeed * Time.fixedDeltaTime, 0, 0);
                }
                else
                {
                    _middle += new Vector3(platformSpeed * Time.fixedDeltaTime * -1, 0, 0);
                }
            }

            if (_platformNumber == 4)
            {
                _alpha = _middleSpriteRenderer.color.a;
                
                if (_alpha>=1)
                {
                    _isDecreasing = true;
                }else if (_alpha <= 0)
                {
                    _isDecreasing = false;
                }
                if (_alpha < 0.5)
                {
                    _middleCollider2D.enabled = false;
                }
                else if (_alpha > 0.5)
                {
                    _middleCollider2D.enabled = true;
                }

                if (_isOn)
                {
                    if(_alpha < 1)
                        _alpha += Time.fixedDeltaTime * pFourTransition;
                }
                else
                {
                    if(_alpha >0)
                        _alpha -= Time.fixedDeltaTime * pFourTransition;
                }
                
                // if (_isDecreasing)
                // {
                //     _alpha -= Time.fixedDeltaTime * pFourTransition;
                // }
                // else
                // {
                //     _alpha += Time.fixedDeltaTime * pFourTransition;
                // }
                
                _middleSpriteRenderer.color =  new Color(255,255,255,_alpha);
                // //StartCoroutine(PlatformFourHideAndShow(5f));
                
            }

            if (_platformNumber !=4)
            {
                _children[0].position = _left;
                _children[1].position = _right;
            }
            else if (_platformNumber == 4)
            {
                _children[0].position = _middle;
            }
            if (_platformNumber == 3)
            {
                _children[2].position = _middle;
            }
        }

        private IEnumerator PlatformOnOff(float interval)
        {
            while (true)
            {
                if (_isOn)
                {
                    yield return new WaitForSeconds(interval);
                    _middleSpriteRenderer.color = new Color(255, 255, 255, 1);
                }
                else
                {
                    yield return new WaitForSeconds(interval);
                    _middleSpriteRenderer.color = new Color(255, 255, 255, 0);
                }
            }
        }

        private IEnumerator StayCurrentState(float time)
        {
            while (true)
            {
                yield return new WaitForSeconds(time);
                if (_isDecreasing)
                {
                    _isOn = false;
                }
                yield return new WaitForSeconds(time);
                if(!_isDecreasing)
                {
                    _isOn = true;
                }
            }
        }
        
        // private IEnumerator PlatformFourHideAndShow(float interval)
        // {
        //     while (true)
        //     {
        //         yield return new WaitForSeconds(interval);
        //         _middleSpriteRenderer.color = new Color(255, 255, 255, 0);
        //         Debug.Log("First One");
        //         yield return new WaitForSeconds(interval);
        //         _middleSpriteRenderer.color = new Color(255, 255, 255, 1);
        //         Debug.Log("Second One");
        //         yield return new WaitForSeconds(interval/2);
        //         _middleSpriteRenderer.color = new Color(255, 255, 255, 0);
        //         Debug.Log("Third One");
        //         StopCoroutine(PlatformFourHideAndShow(interval));
        //         StopAllCoroutines();
        //         Debug.Log("dickhead");
        //     }
        // }
        public void ArrangeDistanceAtStart(float moveDistance)
        {
            if (_platformNumber == 1 || _platformNumber == 4) return;
            
            float manualDistance = Mathf.Abs(_children[0].position.x - _children[1].position.x);
            
            if (manualDistance < moveDistance)
            {
                _children[0].position = new Vector3((moveDistance / 2) * -1, _children[1].position.y, 0);
                _children[1].position = new Vector3(moveDistance / 2, _children[1].position.y, 0);
            }
            else if (manualDistance > moveDistance)
            {
                _children[0].position = new Vector3(moveDistance / 2, _children[1].position.y, 0);
                _children[1].position = new Vector3((moveDistance / 2) * -1, _children[1].position.y, 0);
            }
        }
    }
}
