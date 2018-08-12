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

        private Puzzle puzzle;

        // Use this for initialization
        void Start()
        {
            Puzzle p = new Puzzle();
            p.solutionPrompt = "[1] something something [2] something something [3] something something [4] something something [5] something something.";
            p.keywords = new List<string>() { "test", "test", "test", "test", "test"};
            SetupSolver(p, new List<string>() { "test", "test2", "test", "test2" , "test", "test2" , "test", "test2" , "test", "test2" , "test", "test2" , "test", "test2" });
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void SetupSolver(Puzzle p, List<string> keywords)
        {
            puzzle = p;
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
                keywordButton.clickHandler += onKeywordClick;
            }
            ColorNextPrompt();
        }

        public void onKeywordClick(string s)
        {
            Debug.Log(s);
            currentAnswer.Add(s);
            messageToSolve.text = messageToSolve.text.Replace("<color=#77ff77>[" + currentAnswer.Count + "]</color>", s);
            if (currentAnswer.Count == puzzle.keywords.Count)
            {
                //We're done, check if its the right answer.
                bool correct = CheckAnswer();
                Debug.Log("YOUR ANSWER IS : " + correct);
            }
            else
            {
                ColorNextPrompt();
            }
        }

        private void ColorNextPrompt()
        {
            messageToSolve.text = messageToSolve.text.Replace("[" + (currentAnswer.Count + 1) + "]", "<color=#77ff77>[" + (currentAnswer.Count + 1) + "]</color>");
        }

        private bool CheckAnswer()
        {
            if (currentAnswer.Count != puzzle.keywords.Count)
                return false;
            for(int i = 0; i < currentAnswer.Count; i++)
            {
                if (currentAnswer[i] != puzzle.keywords[i])
                    return false;
            }
            return true;
        }
    }
}
