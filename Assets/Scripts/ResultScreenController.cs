using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


namespace DeleteAfterReading
{

    public class ResultScreenController : MonoBehaviour
    {
        public Image successImage;
        public Image failImage;

        public TextMeshProUGUI headlineText;
        public TextMeshProUGUI bodyText;

        private string successBody = "With the aid of a new specially trained agent, the latest CIA mission was a resounding success!  America is safe once more.  When asked how he accomplished the a task with such limited resources the agent only responded \"I dug through a lot of";
        private string failBody = "It is with great dismay that we report to you on a failed operation.  A new agent has brough shame not only to the CIA, but to the entire country!  Our security has never been more at risk then it is today.  When asked for comment the agent only said ";

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
            successImage.gameObject.SetActive(true);
            failImage.gameObject.SetActive(false);

            headlineText.text = headline;
            bodyText.text = successBody;
        }

        public void ShowNewspaperFailure(string failedBlob)
        {
            gameObject.SetActive(true);
            successImage.gameObject.SetActive(false);
            failImage.gameObject.SetActive(true);

            headlineText.text = failedBlob + "?  Agent bungles mission.";
            bodyText.text = failBody;
        }

        public void HandleNext()
        {
            gameObject.SetActive(false);
            //TODO do we do anything else here?  or is the rest handled by the solver?
        }
    }
}