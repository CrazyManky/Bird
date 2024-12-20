using Project.Screpts.Interfaces;
using UnityEngine;

namespace Project.Screpts.Services
{
    public class PositionScreenService : IPositionScreenService
    {
        private Camera _camera;

        public PositionScreenService()
        {
            _camera = Camera.main;
        }

        public Vector3 GetScreenPosition(Vector2 screenAnchor)
        {
            Vector3 screenPosition = new Vector3(screenAnchor.x * UnityEngine.Screen.width,
                screenAnchor.y * UnityEngine.Screen.height, 0);
            Vector3 worldPosition = _camera.ScreenToWorldPoint(screenPosition);
            worldPosition.z = 0;
            return worldPosition;
        }

        public Quaternion GetRotationTowardsCenter(Vector3 objectPosition)
        {
            Vector3 screenCenter =
                _camera.ScreenToWorldPoint(
                    new Vector3(UnityEngine.Screen.width / 2f, UnityEngine.Screen.height / 2f, 0));
            Vector3 directionToCenter = (screenCenter - objectPosition).normalized;

            float angle = Mathf.Atan2(directionToCenter.y, directionToCenter.x) * Mathf.Rad2Deg;
            return Quaternion.Euler(0, 0, angle - 90);
        }
    }


    public class MoveCharacter
    {
        private float _horizontalSpeed = 2f;
        private Vector2 _screenBounds;
        private BirdController _birdController;
        private bool isMovingRight = true;

        public MoveCharacter(BirdController birdController, Camera camera)
        {
            _screenBounds =
                camera.ScreenToWorldPoint(new Vector3(UnityEngine.Screen.width, UnityEngine.Screen.height, 0));
            _birdController = birdController;
        }

        public void MoveHorizontally()
        {
            float horizontalDirection = isMovingRight ? 1 : -1;
            _birdController.RB.velocity =
                new Vector2(horizontalDirection * _horizontalSpeed, _birdController.RB.velocity.y);

            _birdController.FlipSprite(horizontalDirection);
        }

        public void CheckHorizontalBounds()
        {
            if (_birdController.transform.position.x >= _screenBounds.x && isMovingRight)
            {
                isMovingRight = false;
            }


            if (_birdController.transform.position.x <= -_screenBounds.x && !isMovingRight)
            {
                isMovingRight = true;
            }
        }
    }
}