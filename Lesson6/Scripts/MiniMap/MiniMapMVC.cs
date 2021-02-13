using UnityEngine;

namespace Geekbrains
{
    public sealed class MiniMapMVC : IExecute
    {        
        private Transform _player;
        private Transform _mapCamera;
        private Camera _camera;
        private Vector3 _offset;
        public MiniMapMVC(Transform player, Camera mapCamera)
        {
            _player = player;
            _mapCamera = mapCamera.transform;
            _camera = mapCamera;

            _mapCamera.parent = null;
            _mapCamera.rotation = Quaternion.Euler(90.0f, 0, 0);
            _mapCamera.position = _player.position + new Vector3(0, 5.0f, 0);

            var rt = Resources.Load<RenderTexture>("MiniMap/MiniMapTexture");

            _camera.targetTexture = rt;
        }

        public void Execute()
        {
            var newPosition = _player.position;
            newPosition.y = _mapCamera.position.y;
            _mapCamera.position = newPosition;
            //_mapCamera.rotation = Quaternion.Euler(90, _player.eulerAngles.y, 0);
        }
    }

}
