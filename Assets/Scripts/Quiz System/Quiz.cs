using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Hanzo.QuizSystem
{
    [System.Serializable]
    public class QuizData
    {
        public string question;
        public string[] options;
        public string correctAnswer;
    }


    public class Quiz : MonoBehaviour
    {
        public GameObject quizCamera;
        [SerializeField]
        private GameObject mainCamera;
        private Transform player;

        [SerializeField]
        private bool isQuiz = false;
        public bool quizEnabled = false;


        public string quizQuery;

        public bool isCorrrect = false;


        public GameObject QuizContainer;
        private QuizContainer quizContainer;

        public List<QuizData> quizQuestions;

        private void Start()
        {
            mainCamera = Camera.main.gameObject;
            quizCamera = GameObject.FindGameObjectWithTag("QuizCamera");
            QuizContainer = GameObject.Find("QuizContainer");

            GetQuiz();

          

        }

        internal void _init()
        {
            quizCamera.GetComponent<Camera>().enabled = true;
            quizCamera.GetComponent<AudioListener>().enabled = true;
            mainCamera.SetActive(false);
            player = GameObject.FindGameObjectWithTag("Player").transform;
            player.gameObject.GetComponent<Hanzo.PlayerScript>().DisableMovements();

            quizContainer = QuizContainer.GetComponent<QuizContainer>();

            isQuiz = true;

            EnableQuiz();

        }
        internal void _uninit()
        {
            quizCamera.GetComponent<Camera>().enabled = !true;
            quizCamera.GetComponent<AudioListener>().enabled = !true;
            mainCamera.SetActive(!false);
            player = GameObject.FindGameObjectWithTag("Player").transform;
            player.gameObject.GetComponent<Hanzo.PlayerScript>().EnableMovements();
            this.gameObject.SetActive(false);
            isQuiz = false;

        }

        void EnableQuiz()
        {
            // QuizContainer quizContainer = QuizContainer.GetComponent<QuizContainer>();
            quizContainer.EnableComponent();

            if (!quizEnabled)
            {
                if (quizQuestions.Count > 0)
                {
                    int randomIndex = Random.Range(0, quizQuestions.Count);
                    quizQuery = quizQuestions[randomIndex].question;

                    // Set the quiz question text in the quiz container
                    quizContainer.titleText.text = quizQuery;

                    // Retrieve the button objects from the QuizContainer
                    Button[] buttons = quizContainer.buttons;

                    // Check if the number of buttons matches the number of options
                    if (buttons.Length == quizQuestions[randomIndex].options.Length)
                    {
                        // Assign the answer options to the button texts
                        for (int i = 0; i < buttons.Length; i++)
                        {
                            buttons[i].GetComponentInChildren<TextMeshProUGUI>().text = quizQuestions[randomIndex].options[i];

                            // Check if this button represents the correct answer
                            if (quizQuestions[randomIndex].options[i] == quizQuestions[randomIndex].correctAnswer)
                            {
                                // Assign the correct answer behavior to this button
                                buttons[i].onClick.AddListener(() => CheckAnswer(quizQuestions[randomIndex].correctAnswer));
                            }
                            else
                            {
                                // Assign incorrect answer behavior to other buttons if needed
                                buttons[i].onClick.AddListener(() => IncorrectAnswerBehavior());
                            }
                        }

                        // Mark the quiz as enabled
                        quizEnabled = true;
                    }
                    else
                    {
                        Debug.LogError("The number of buttons in QuizContainer does not match the number of answer options.");
                    }
                }
                else
                {
                    Debug.Log("No quiz");
                }
            }
        }

        // Add a method to check the selected answer
        void CheckAnswer(string selectedAnswer)
        {
            isCorrrect = true;
            _uninit();
            // Handle correct answer (e.g., increase score, show feedback, etc.)
            Debug.Log("Correct Answer: " + selectedAnswer);

            quizContainer.DisableComponent();


        }

        // Add a method to handle incorrect answers
        void IncorrectAnswerBehavior()
        {
            // Handle incorrect answer (e.g., show feedback, deduct points, etc.)
            Debug.Log("Incorrect Answer!");
        }

        void GetQuiz()
        {

              quizQuestions.Add(new QuizData
            {
                question = "How many days did God take to create the world?",
                options = new string[] { "7 days", "5 days", "30 days", "50 days" },
                correctAnswer = "7 days"
            });

            quizQuestions.Add(new QuizData
            {
                question = "Who was the first man?",
                options = new string[] { "Tarzan", "Apollo", "Adam", "Jack" },
                correctAnswer = "Adam",
            });

            quizQuestions.Add(new QuizData
            {
                question = "Who was the first woman?",
                options = new string[] { "Jane", "Evelyn", "Eve", "Mary" },
                correctAnswer = "Eve"
            });

            quizQuestions.Add(new QuizData
            {
                question = "Who did God tell to build an ark?",
                options = new string[] { "Moses", "Noah", "David", "Jacob" },
                correctAnswer = "Noah",
            });

             quizQuestions.Add(new QuizData
            {
                question = "What was God’s sign to Noah that he would never destroy the earth again?",
                options = new string[] { "Coat of many colors", "A horse", "A Rainbow", "The Stars" },
                correctAnswer = "A Rainbow"
            });

            quizQuestions.Add(new QuizData
            {
                question = "How many brothers did Joseph have?",
                options = new string[] { "12", "20", "11", "9" },
                correctAnswer = "11",
            });


        }




    }

}