using TMPro;
using UnityEngine;

public class WishPanelScript : MonoBehaviour
{
    MainController mainController;

    public TextMeshProUGUI pink_wish_counter_text;
    public TextMeshProUGUI blue_wish_counter_text;

    public GameObject interactPanel;

    public GameObject pinkBackgroundPanel;
    public GameObject blueBackgroundPanel;

    public GameObject starsGO;
    public GameObject blueStarsGO;

    public GameObject pinkWishMadePanel;
    public GameObject blueWishMadePanel;

    public GameObject pinkWishInteractPanel;
    public GameObject blueWishInteractPanel;

    private Animator pink_stars_animator;
    private Animator blue_stars_animator;

    private Animator current_animator;

    bool is_pink = true;

    public class WishParameters
    {
        public float chance_to_get_5_star = 0.05f;
        public float chance_to_get_4_star = 0.2f;
        public int get_5_star_wish_amount = 60;
        public int get_4_star_wish_amount = 10;
        public int current_wish_made_amount = 0;

        public WishParameters(float chance_to_get_5_star_, float chance_to_get_4_star_, int get_5_star_wish_amount_, int get_4_star_wish_amount_)
        {
            chance_to_get_5_star = chance_to_get_5_star_;
            chance_to_get_4_star = chance_to_get_4_star_;
            get_5_star_wish_amount = get_5_star_wish_amount_;
            get_4_star_wish_amount = get_4_star_wish_amount_;
            current_wish_made_amount = 0;
        }
    }

    WishParameters pink_wish_parameters;
    WishParameters blue_wish_parameters;

    void Awake()
    {
        mainController = GameObject.Find("MainController").GetComponent<MainController>();
        pink_stars_animator = starsGO.GetComponent<Animator>();
        blue_stars_animator = blueStarsGO.GetComponent<Animator>();

        pink_wish_parameters = new WishParameters(0.5f, 0.1f, 60, 10);
        blue_wish_parameters = new WishParameters(0.5f, 0.1f, 60, 10);
    }

    public void OpenWishPanel()
    {
        gameObject.SetActive(true);
        mainController.UpdateWishPanelInfo();
        SwitchToPinkWish();
    }

    public void StartWish10()
    {
        bool success = UseWish(10);

        if (success)
        {
            CloseWishInteractPanel();

            current_animator.SetBool("is_wishing", true);

            ComputeRewards(10);

            Invoke("StopWish", 0.3f);
        }
        else
        {
            Debug.Log("NOT ENOUGTH WISHES");
            return;
        }
        mainController.UpdateWishPanelInfo();
    }

    public void StartWish()
    {
        bool success = UseWish(1);

        if (success)
        {
            CloseWishInteractPanel();

            current_animator.SetBool("is_wishing", true);

            ComputeRewards(1);

            Invoke("StopWish", 0.3f);
        }
        else
        {
            Debug.Log("NOT ENOUGTH WISHES");
            return;
        }
        mainController.UpdateWishPanelInfo();
    }

    bool UseWish(int number)
    {
        return mainController.UseWish(is_pink, number);
    }

    void ComputeRewards(int number)
    {
        if (is_pink)
        {
            int temp_wish_amount = pink_wish_parameters.current_wish_made_amount + number;

            if (temp_wish_amount / pink_wish_parameters.get_4_star_wish_amount > pink_wish_parameters.current_wish_made_amount / pink_wish_parameters.get_4_star_wish_amount)
            {
                Obtain4StaarCharacter();
            }

            if (temp_wish_amount / pink_wish_parameters.get_5_star_wish_amount > pink_wish_parameters.current_wish_made_amount / pink_wish_parameters.get_5_star_wish_amount)
            {
                Obtain5StaarCharacter();
            }

            pink_wish_parameters.current_wish_made_amount += number;

            // Random chance
        }
    }

    void Obtain5StaarCharacter()
    {

    }

    void Obtain4StaarCharacter()
    {
        Debug.Log("GET 4* CHARACTER");
    }

    void StopWish()
    {
        current_animator.SetBool("is_wishing", false);
    }

    public void CompleteWish()
    {
        Debug.Log("WISH MADE");

        if (is_pink)
        {
            OpenPinkWishInteractPanel();
            OpenPinkWishMade();
        }
        else
        {
            OpenBlueWishInteractPanel();
            OpenBlueWishMade();
        }
    }

    public void CompleteBlueWish()
    {
        Debug.Log("BLUE WISH MADE");

        OpenBlueWishInteractPanel();
        OpenBlueWishMade();
    }

    void OpenPinkWishInteractPanel()
    {
        interactPanel.SetActive(true);
        pinkWishInteractPanel.SetActive(true);
    }

    void OpenBlueWishInteractPanel()
    {
        interactPanel.SetActive(true);
        blueWishInteractPanel.SetActive(true);
    }

    void CloseWishInteractPanel()
    {
        interactPanel.SetActive(false);
        pinkWishInteractPanel.SetActive(false);
        blueWishInteractPanel.SetActive(false);
    }

    void OpenPinkWishMade()
    {
        pinkWishMadePanel.SetActive(true);
    }

    void OpenBlueWishMade()
    {
        blueWishMadePanel.SetActive(true);
    }

    public void CloseWishMade()
    {
        pinkWishMadePanel.SetActive(false);
        blueWishMadePanel.SetActive(false);
    }

    public void SwitchToPinkWish()
    {
        PinkPanelSetActive(true);
    }

    public void SwitchToBlueWish()
    {
        PinkPanelSetActive(false);
    }

    void PinkPanelSetActive(bool is_active)
    {
        pinkBackgroundPanel.SetActive(is_active);
        blueBackgroundPanel.SetActive(!is_active);

        pinkWishInteractPanel.SetActive(is_active);
        blueWishInteractPanel.SetActive(!is_active);

        if (is_active)
        {
            is_pink = true;
            current_animator = pink_stars_animator;
        }
        else
        {
            is_pink = false;
            current_animator = blue_stars_animator;
        }
    }

    public void UpdatePinkWishInfo(int new_counter)
    {
        pink_wish_counter_text.text = new_counter.ToString();
    }

    public void UpdateBlueWishInfo(int new_counter)
    {
        blue_wish_counter_text.text = new_counter.ToString();
    }
}
