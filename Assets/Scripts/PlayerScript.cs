using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Hanzo.AudioSystem;

namespace Hanzo
{
    [System.Serializable]
    public class CameraSwitch
    {
        public Camera mainCamera;
        public Camera canvasCamera;


        public void Transition()
        {
            canvasCamera.gameObject.SetActive(true);
            mainCamera.gameObject.SetActive(false);

        }
        public void InverseTransition()
        {
            canvasCamera.gameObject.SetActive(false);
            mainCamera.gameObject.SetActive(true);

        }
    }

    enum MotionParameter
    {
        Run,
        Slide,
        Jump,
        Right,
        Left,
        Death,
    }
    enum ScoreParam
    {
        TOP_DISTANCE,
        TOP_COINSCORE,
    }
    enum SelectedPlayer
    {
        SELECTED_PLAYER
    }


    public class PlayerScript : MonoBehaviour
    {
       

        private ScreenShake screenShake;
        public CameraSwitch cameraSwitch;

        [HideInInspector] public Animator animator;
        private Rigidbody rigidbody;
        public SkinnedMeshRenderer skinnedMeshRenderer;

        [SerializeReference]
        float VerticalLeap = 1f;
        public float playerSpeed = 3f;

        private bool isJumpDown = false;
        private int New_X_Pos;

        private bool Left, Right;
        public bool isDead = false;
        public static int CurrentTile = 0;
        public float timeScale = 1.2f;
        public int sideSpeed = 6;

        public GameObject particleFX;
        public GameObject GameOverUI;

        public int coinScore = 1;
        private string storedCoinScore;

        public IAbilities headstartAbility;
        private ScoreSystem scoreSystem;
        private GameObject canvasCamera;


        [Header("Shield Ability Functionality")]

        public GameObject shieldFX;
        public GameObject Air;
        private List<Transform> shieldTransforms;

        public Slider ShieldSlider;
        public GameObject ShieldUI;

        public bool isShieldActive = false;
        public float shieldTimer = 25f;
        private float shieldElapsedTime = 0f;
        private int shieldCollisionCount = 0;

        [Header("Multiplier Ability Functionality")]

        public GameObject MultiplierUI;
        public Slider multiplierSlider;

        int multiplier = 2;
        public float multiplierTimer = 25f;
        private float multiplierElapsedTime = 0f;

        public bool isMultiplied = false;
        public int muliplierCollisionCount = 0;

        public float timeMult;
        public bool isRestored = false;




        // Start is called before the first frame update
        void Start()
        {
            // PlayerPrefs.DeleteAll();
            // animator.SetBool(MotionParameter.Run.ToString(), true);



            animator = GetComponent<Animator>();
            rigidbody = GetComponent<Rigidbody>();

            scoreSystem = GameObject.FindObjectOfType<ScoreSystem>();

            screenShake = GameObject.FindObjectOfType<ScreenShake>();

            //Sheild Ability Functionality
            ShieldUI = GameObject.Find("ShieldUI");

            //Multiplier Ability Functionality
            MultiplierUI = GameObject.Find("MultiplierUI");

            storedCoinScore = ScoreParam.TOP_COINSCORE.ToString();



        }

        void Update()
        {
           
            if (isDead == true)
            {
                animator.SetBool(MotionParameter.Death.ToString(), true);
                animator.SetBool(MotionParameter.Run.ToString(), false);

                GameObject[] canvasChild = GameObject.FindGameObjectsWithTag("CanvasChild");

                foreach (var item in canvasChild)
                {
                    if (item != null)
                    {
                        item.SetActive(false);
                    }
                    else
                    {
                        return;
                    }
                }

                StartCoroutine(DeadTime(2f));
                GameObject[] coinParent = GameObject.FindGameObjectsWithTag("CoinParent");
                foreach (var cp in coinParent)
                {
                    cp.SetActive(false);
                }

            }
            else
            {
                #region Player Movement

                if (isRestored == true)
                {
                    StopAllCoroutines();
                    isRestored = false;

                }

                // animator.SetBool(MotionParameter.Run.ToString(), true);
                if (Input.GetKeyUp(KeyCode.W))
                {
                    // if (this.name == "ZOE")
                    // {
                    //     headstartAbility.ActivateAbility();
                    // }
                    animator.SetBool(MotionParameter.Run.ToString(), true);


                }
                else if (Input.GetKeyUp(KeyCode.S))
                {
                    animator.SetBool(MotionParameter.Slide.ToString(), true);
                    AudioManager.Instance.PlaySFX("Slide");
                }
                else if (Input.GetKeyUp(KeyCode.Space))
                {
                    animator.SetBool(MotionParameter.Jump.ToString(), true);
                    AudioManager.Instance.PlaySFX("Jump");
                }

                else if (Input.GetKeyUp(KeyCode.D))
                {
                    if (!animator.GetBool(MotionParameter.Jump.ToString()) && !animator.GetBool(MotionParameter.Slide.ToString()))
                        animator.SetBool(MotionParameter.Right.ToString(), true);
                    else
                        Right = true;

                    if (rigidbody.position.x >= -3 && rigidbody.position.x < -1)
                    {
                        New_X_Pos = 0;
                    }
                    else if (rigidbody.position.x >= -1 && rigidbody.position.x < 1)
                    {
                        New_X_Pos = 2;
                    }
                }
                else if (Input.GetKeyUp(KeyCode.A))
                {
                    if (!animator.GetBool(MotionParameter.Jump.ToString()) && !animator.GetBool(MotionParameter.Slide.ToString()))
                        animator.SetBool(MotionParameter.Left.ToString(), true);
                    else
                        Left = true;

                    if (rigidbody.position.x >= 1 && rigidbody.position.x < 3)
                    {
                        New_X_Pos = 0;
                    }
                    else if (rigidbody.position.x >= -1 && rigidbody.position.x < 1)
                    {
                        New_X_Pos = -2;
                    }
                }

                //if palyer is dead




                #endregion
            }


            //Shield Ability Functionality
            if (isShieldActive == true)
            {
                // StartCoroutine(ActivateShield(shieldTimer));
                SetObstacle("Obstacle", "Disposable");
            }
            else
            {
                SetObstacle("Disposable", "Obstacle");
            }

            // if (isRestored == true)
            // {
            //     StopAllCoroutines();
            //     // isRestored = false;

            // }



        }



        //Animation Events
        void ToggleOff(string Name)
        {
            animator.SetBool(Name, false);
            isJumpDown = false;
        }

        //Animation Events
        void JumpDown()
        {

            isJumpDown = true;
        }

        void OnAnimatorMove()
        {
            rigidbody.MovePosition(rigidbody.position + new Vector3(0, 0, playerSpeed) * animator.deltaPosition.magnitude);

            if (animator.GetBool(MotionParameter.Jump.ToString()))
            {
                if (isJumpDown)
                {
                    rigidbody.MovePosition(rigidbody.position + new Vector3(0, 0, 2) * animator.deltaPosition.magnitude);
                }
                else
                    rigidbody.MovePosition(rigidbody.position + new Vector3(0, VerticalLeap, 2) * animator.deltaPosition.magnitude);

            }
            if (animator.GetBool(MotionParameter.Right.ToString()))
            {
                if (rigidbody.position.x < New_X_Pos)
                    rigidbody.MovePosition(rigidbody.position + new Vector3(1, 0, 1.5f) * animator.deltaPosition.magnitude * sideSpeed);
                else
                    animator.SetBool(MotionParameter.Right.ToString(), false);
            }
            if (animator.GetBool(MotionParameter.Left.ToString()))
            {
                if (rigidbody.position.x > New_X_Pos)
                    rigidbody.MovePosition(rigidbody.position + new Vector3(-1, 0, 1.5f) * animator.deltaPosition.magnitude * sideSpeed);
                else
                    animator.SetBool(MotionParameter.Left.ToString(), false);
            }

            else
                rigidbody.MovePosition(rigidbody.position + Vector3.forward * animator.deltaPosition.magnitude);


            if (Left)
            {

                if (rigidbody.position.x > New_X_Pos)
                    rigidbody.MovePosition(rigidbody.position + new Vector3(-1, 0, 0) * animator.deltaPosition.magnitude);
                else

                    Left = false;
            }

            else if (Right)
            {

                if (rigidbody.position.x < New_X_Pos)
                    rigidbody.MovePosition(rigidbody.position + new Vector3(1, 0, 0) * animator.deltaPosition.magnitude);
                else

                    Right = false;
            }


        }

        public void DisableMovements()
        {
            animator.SetBool(MotionParameter.Run.ToString(), false);
            animator.SetBool(MotionParameter.Slide.ToString(), false);
            animator.SetBool(MotionParameter.Jump.ToString(), false);
            animator.SetBool(MotionParameter.Left.ToString(), false);
            animator.SetBool(MotionParameter.Right.ToString(), false);
            // animator.SetBool(MotionParameter.Quiz.ToString(), true);
        }

        public void EnableMovements()
        {
            animator.SetBool(MotionParameter.Run.ToString(), true);
            // animator.SetBool(MotionParameter.Quiz.ToString(), false);
        }


        public IEnumerator DeadTime(float t)
        {
            yield return new WaitForSeconds(t);

            cameraSwitch.Transition();

            GameOverUI.SetActive(true);

            skinnedMeshRenderer.enabled = false;

        }

        //============= COLLISION PHYSICS DETECTIONS =========


        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("Obstacle"))
            {
                StartCoroutine(GetComponent<TraumaInducer>().Shake(.8f));
                AudioManager.Instance.PlaySFX("PowerUp");
                //Player is dead.
                string TopDistancePref = Hanzo.ScoreParam.TOP_DISTANCE.ToString();
                isDead = true;


                //Display Game Over UI
                 if (scoreSystem.Distance > scoreSystem.topDistance)
            PlayerPrefs.SetInt(TopDistancePref, (int)scoreSystem.Distance);
            }
        }

        void OnTriggerEnter(Collider other)
        {

            if (other.CompareTag("Coin"))
            {
                //Increase Score here
                Instantiate(particleFX, other.transform.position, Quaternion.identity);
                AudioManager.Instance.PlaySFX("CoinCollect");
                scoreSystem.coinScore += coinScore;
                int tempScore = PlayerPrefs.GetInt(storedCoinScore);
                PlayerPrefs.SetInt(storedCoinScore, tempScore + coinScore);
                other.gameObject.SetActive(false);
            }

            if (other.CompareTag("Shield"))
            {
                Instantiate(particleFX, other.transform.position, Quaternion.identity);

                if (isShieldActive)
                {
                    other.gameObject.SetActive(false);
                    isShieldActive = true;
                    // If shield is already active, reset the timer and check the shield count
                    shieldElapsedTime = 0f;

                    shieldCollisionCount++;

                    if (shieldCollisionCount >= 2)
                    {
                        shieldCollisionCount = 0; // Reset the counter
                        StopCoroutine("ActivateShield");
                        StartCoroutine(ActivateShield(shieldTimer)); // Restart the coroutine
                    }
                }
                else
                {
                    other.gameObject.SetActive(false);
                    isShieldActive = true;

                    StartCoroutine(ActivateShield(shieldTimer));
                }

            }

            if (other.CompareTag("MultiplierUI"))
            {
                Instantiate(particleFX, other.transform.position, Quaternion.identity);

                if (isMultiplied)
                {
                    other.gameObject.SetActive(false);
                    isMultiplied = true;
                    multiplierElapsedTime = 0f;
                    muliplierCollisionCount++;

                    if (muliplierCollisionCount >= 2)
                    {
                        muliplierCollisionCount = 0;
                        StopCoroutine("ActivateCoinMultiplier");
                        StartCoroutine(ActivateCoinMultiplier(multiplierTimer));
                    }
                }
                else
                {
                    other.gameObject.SetActive(false);
                    isMultiplied = true;
                    StartCoroutine(ActivateCoinMultiplier(multiplierTimer));

                }
            }

            // IAbilities ability = other.GetComponent<IAbilities>();
            // if (ability != null)
            //     ability.ActivateAbility();

        }

        ////////////=============== ABILITIES ==============================/////////
        #region Shield Ability
        //=========Shield Ability =========//
        IEnumerator ActivateShield(float t)
        {

            shieldFX.SetActive(true);
            Air.SetActive(true);
            AudioManager.Instance.PlaySFX("PowerUp");

            SetObstacle("Obstacle", "Disposable");
            ShieldUIChildren(true);
            ShieldSliderByChild();

            while (shieldElapsedTime < t)
            {
                shieldElapsedTime += Time.deltaTime;
                ShieldSlider.value = t - shieldElapsedTime;
                yield return null;

            }

            isShieldActive = false;
            shieldFX.SetActive(false);
            Air.SetActive(false);
            SetObstacle("Disposable", "Obstacle");
            ShieldUIChildren(false);  // Deactivate Shield UI

        }


        void SetObstacle(string initialTag, string finalTag)
        {

            GameObject[] obstacles = GameObject.FindGameObjectsWithTag(initialTag);
            foreach (var obstacle in obstacles)
            {
                obstacle.tag = finalTag;
            }

        }

        void ShieldUIChildren(bool active)
        {
            // Clear the list before adding new elements
            shieldTransforms = new List<Transform>();

            foreach (Transform child in ShieldUI.transform)
            {
                shieldTransforms.Add(child);
                child.gameObject.SetActive(active);
            }
        }

        void ShieldSliderByChild()
        {
            // Find the Slider child by name
            Transform sliderChild = ShieldUI.transform.Find("Slider");

            // Check if the child is found and if it has a Slider component
            if (sliderChild != null)
            {
                ShieldSlider = sliderChild.GetComponent<Slider>();
                if (ShieldSlider != null)
                {
                    ShieldSlider.maxValue = shieldTimer;
                }
                else
                {
                    Debug.LogError("The child named 'Slider' does not have a Slider component.");
                }
            }
            else
            {
                Debug.LogError("The child named 'Slider' not found under ShieldUI.");
            }
        }

        #endregion

        #region Multiplier

        IEnumerator ActivateCoinMultiplier(float t)
        {
            isMultiplied = true;
            int originalCoinScore = coinScore;
            coinScore *= multiplier;

            SetActiveMultiplierUIChildren(true);
            MultiplierGetSliderByChild();

            // float elapsedTime = 0f;

            while (multiplierElapsedTime < t)
            {
                multiplierElapsedTime += Time.deltaTime;

                // Update your UI slider or any other visual representation of the timer
                multiplierSlider.value = t - multiplierElapsedTime;

                yield return null; // Wait for the next frame
            }

            SetActiveMultiplierUIChildren(false);

            // Ensure the timer display is accurate at the end
            multiplierSlider.value = 0f;
            coinScore = originalCoinScore;
            isMultiplied = false;

        }

        void SetActiveMultiplierUIChildren(bool active)
        {
            foreach (Transform child in MultiplierUI.transform)
            {
                child.gameObject.SetActive(active);
            }
        }

        void MultiplierGetSliderByChild()
        {
            // Find the Slider child by name
            Transform sliderChild = MultiplierUI.transform.Find("Slider");

            // Check if the child is found and if it has a Slider component
            if (sliderChild != null)
            {
                multiplierSlider = sliderChild.GetComponent<Slider>();
                multiplierSlider.maxValue = multiplierTimer;

                if (multiplierSlider == null)
                {
                    Debug.LogError("The child named 'SliderChildName' does not have a Slider component.");
                }
            }
            else
            {
                Debug.LogError("The child named 'SliderChildName' not found under MultiplierUI.");
            }
        }

        #endregion


    }


}
