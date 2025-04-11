    using TMPro;
    using UnityEngine;

    public class Points : MonoBehaviour
    {
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        [SerializeField] private TextMeshProUGUI text;
        public int points;



        void Start()
        {
            points = 0;
        }

        public void addPoints()
        {
            points++;
        }

        // Update is called once per frame
        void Update()
        {
        text.text = string.Format("Points: {0}", points);
        }
    }
