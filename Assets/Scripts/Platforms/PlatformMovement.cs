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
        
        private int _difficulty, _platformNumber;

        public void GetChildren()
        {
            _children = new Transform[this.transform.childCount];
            for (int i = 0; i < this.transform.childCount; i++)
            {
                _children[i] = this.transform.GetChild(i);
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
            }
        }
        public void MoveChildren(float platformSpeed, float moveDistance)
        {
            if(_platformNumber != 4) 
            {  
                _left = _children[0].position;
                _right = _children[1].position;
                if(_platformNumber == 3) _middle = _children[2].position;
            }
            else
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
            _children[0].position = _left;
            _children[1].position = _right;
            if (_platformNumber == 3)
            {
                _children[2].position = _middle;
            }
        }
        public void ArrangeDistanceAtStart(float moveDistance)
        {
            if (_platformNumber == 1) return;
            
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
