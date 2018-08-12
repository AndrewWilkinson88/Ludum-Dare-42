using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DeleteAfterReading.Model;

namespace DeleteAfterReading
{

    public class Solver : MonoBehaviour
    {

        public TextButton textButtonPrefab;
        public TextMeshPro messageToSolve;
        public GameObject keywordPositioner;

        private List<string> currentAnswer = new List<string>();

        private List<TextButton> keywordButtons = new List<TextButton>();
        private int keywordColumns = 3;
        private float xSpacing = 3.5f;
        private float ySpacing = .75f;


        // Use this for initialization
        void Start()
        {
            Puzzle p = new Puzzle();
            p.solutionPrompt = "[1] something something [2] something something [3] something something [4] something something [5] something something.";
            SetupSolver(p, new List<string>() { "test", "test2", "test", "test2" , "test", "test2" , "test", "test2" , "test", "test2" , "test", "test2" , "test", "test2" });
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void SetupSolver(Puzzle p, List<string> keywords)
        {

            messageToSolve.text = p.solutionPrompt;

            for(int i = 0; i < keywords.Count; i++)
            {
                string s = keywords[i];
                TextButton keywordButton = GameObject.Instantiate<TextButton>(textButtonPrefab);
                keywordButton.text.text = s;
                keywordButton.transform.parent = keywordPositioner.transform;

                int x = i % keywordColumns;
                int y = (int)(i / keywordColumns);
                keywordButton.transform.localPosition = new Vector3(x * xSpacing, -y * ySpacing, 0);
            }
        }
    }
}
