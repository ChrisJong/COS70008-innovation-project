namespace Manager
{
    using System.Collections.Generic;

    using UnityEngine;

    using Extension;

    public class DrawingManager : SingletonMono<DrawingManager>
    {
        [SerializeField] private BoxCollider2D _boxCollider;
        [SerializeField] private GameObject _drawingPrefab;
        [SerializeField] private GameObject _currentDrawPrefab;

        [SerializeField] private List<DrawPoints> drawCollection;

        private void Update()
        {
            
        }
    }
}