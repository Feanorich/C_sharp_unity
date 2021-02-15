using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Geekbrains
{
    public class BonusesGenerator : MonoBehaviour
    {
        #region Fields

        private string _parentGoodName = "GoodBonuses";
        private string _parentBadName = "BadBonuses";

        private GameObject _goodPrephub;
        private GameObject _badPrephub;
        private GameObject _parent;
        private GameObject _forbiddenField;  
        
        private GameObject _road;

        private int _goodBonuses;
        private int _badBonuses;
        private float _heigh;
        private List<GameObject> _places;

        #endregion

        public BonusesGenerator(GameObject goodPrephub, GameObject badPrephub, GameObject parent,
            GameObject road, GameObject forbiddenField, int goodBonuses, int badBonuses, float heigh)
        {
            _goodPrephub = goodPrephub;
            _badPrephub = badPrephub;            
            _parent = parent;
            _forbiddenField = forbiddenField;
            _road = road;

            _goodBonuses = goodBonuses;
            _badBonuses = badBonuses;
            _heigh = heigh;
        }

        #region Methods 
        public void GenerateBonuses()
        {
            int count = _road.transform.childCount;
            _places = new List<GameObject>(count);

            for (int i = 0; i < count; i++)
            {
                var place = _road.transform.GetChild(i).gameObject;
                if (place != _forbiddenField)
                    _places.Add(place);
            }

            Debug.Log(count);
            Debug.Log(_places.Count);                    

            PlaceBonuses(_goodPrephub, _goodBonuses, _parentGoodName);

            PlaceBonuses(_badPrephub, _badBonuses, _parentBadName);

        }

        private void PlaceBonuses(GameObject prephub, int numBonuses, string _parentName)
        {
            if ((prephub != null) && (numBonuses > 0) && (_places.Count > 0))
            {
                GameObject gbParent = new GameObject { name = _parentName };
                Transform ParentTransform = gbParent.transform;
                ParentTransform.SetParent(_parent.transform);

                for (int i = 0; i < numBonuses; i++)
                {
                    int number = Random.Range(1, _places.Count);
                    Transform placeTransform = _places[number].transform;
                    Instantiate(prephub, placeTransform.position.Shift(0, _heigh),
                        prephub.transform.rotation, ParentTransform);

                    _places.RemoveAt(number);
                }

            }
        }

        #endregion

    }
}
