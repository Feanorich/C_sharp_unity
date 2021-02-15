using System;
using UnityEditor;
using UnityEngine;


namespace Geekbrains
{
    public class BonusesGeneratorWindow : EditorWindow
    {

        #region Fields

        private const string NOTICE = "First needs to fill required fields";
        private const string PARENT = "Bonuses";

        private GameObject _parent;
        private GameObject _road;
        private GameObject _goodBonus;
        private GameObject _badBonus;
        private GameObject _forbiddenField;
        private GUIStyle style = new GUIStyle();
        private int _goodBonuses;
        private int _badBonuses;
        private float _heigh = 0.7f;

        #endregion


        #region Window

        [MenuItem("Tools/Bonuses Generatorr")]
        public static void ShowWindow()
        {
            GetWindow(typeof(BonusesGeneratorWindow), false, "Bonuses Generator");
        }

        private void OnEnable()
        {
            style.richText = true;
            style.alignment = TextAnchor.MiddleCenter;
        }

        private void OnGUI()
        {
            GUILayout.Space(20);            
            _goodBonuses = EditorGUILayout.IntField("Количество GoodBonus", _goodBonuses);
            _badBonuses = EditorGUILayout.IntField("Количество BadBonus", _badBonuses);
            _heigh = EditorGUILayout.FloatField("Высота над платформой", _heigh);

            GUILayout.Space(10);
            GUILayout.Label("Дорога (Обязательно!)", EditorStyles.boldLabel);
            _road = EditorGUILayout.ObjectField(_road, typeof(GameObject), true) as GameObject;

            GUILayout.Space(10);
            GUILayout.Label("префаб GoodBonus (опционально)", EditorStyles.boldLabel);
            _goodBonus = EditorGUILayout.ObjectField(_goodBonus, typeof(GameObject), true) as GameObject;

            GUILayout.Space(10);
            GUILayout.Label("префаб BadBonus (опционально)", EditorStyles.boldLabel);
            _badBonus = EditorGUILayout.ObjectField(_badBonus, typeof(GameObject), true) as GameObject;

            GUILayout.Space(10);
            GUILayout.Label("Запретное поле (опционально)", EditorStyles.boldLabel);
            _forbiddenField = EditorGUILayout.ObjectField(_forbiddenField, typeof(GameObject), true) 
                as GameObject;

            GUILayout.Space(10);
            GUILayout.Label("Родительский обект на сцене (опционально, будет создан автоматически)",
                EditorStyles.boldLabel);
            _parent = EditorGUILayout.ObjectField(_parent, typeof(GameObject), true) as GameObject;

            GUILayout.Space(10);
            var notice = CheckFills() == false ? $"<color=red>{NOTICE}</color>" : String.Empty;
            GUILayout.Label(notice, style);

            GUILayout.Space(10);
            if (GUILayout.Button("Generate") && CheckFills())
                Generate();
        }

        #endregion


        #region Methods

        private void Generate()
        {
            if (_parent == null)
                _parent = new GameObject { name = PARENT };
            BonusesGenerator BG = new BonusesGenerator(_goodBonus, _badBonus, _parent,
                _road, _forbiddenField, _goodBonuses, _badBonuses, _heigh);
            BG.GenerateBonuses();
        }

        private bool CheckFills()
        {
            bool _ok = false;
            if (((_goodBonus != null) && (_goodBonuses > 0))
                || ((_badBonus != null) && (_badBonuses > 0)))
            {
                _ok = true;
            }
            return _ok;
        }

        #endregion


    }
}
