using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


namespace DeleteAfterReading
{

    public class ResultScreenController : MonoBehaviour
    {
        
        public TextMeshProUGUI headlineText;

        // Use this for initialization
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void ShowNewspaperSuccess(string headline)
        {
            gameObject.SetActive(true);
            headlineText.text = headline;
        }

        public void ShowNewspaperFailure(string failedBlob)
        {
            gameObject.SetActive(true);
            headlineText.text = failedBlob + "?  Agent bungles mission.";
        }

        public void HandleNext()
        {
            gameObject.SetActive(false);
            //TODO do we do anything else here?  or is the rest handled by the solver?
        }
    }
}