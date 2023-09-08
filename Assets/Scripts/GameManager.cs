using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{

    public static GameManager Instance { get; private set; }

    [SerializeField]
    private PokerManager _pokerManager;
    [SerializeField]
    private RPS_Manager _Rps;
    [SerializeField]
    private CurveLine _Curve;
    [SerializeField]
    private GameObject _scoreMenu;

    Animator Anim;
    [SerializeField] GameObject RouletteObj;

    MiniGame _currentGame;
    public float StartMoney;
    private float _currentMoney;
    [SerializeField]
    private float _Bid1Value;
    [SerializeField]
    private float _Bid2Value;
    [SerializeField]
    private float _Bid3Value;
    [SerializeField]
    private float _Bid4Value;
    private float _currentBid;
    [SerializeField]
    private AudioSource WinningMachine;
    [SerializeField]
    private AudioSource FallingCoin;
    [SerializeField]
    private ParticleSystem MoneyRain1;
    [SerializeField]
    private ParticleSystem MoneyRain2;
    [SerializeField]
    private AudioSource LosingGame;
    [SerializeField]
    private AudioSource Mise;
    [SerializeField]
    private AudioSource StartGame;
    [SerializeField]
    private TMP_Text _textCurrentBid;
    [SerializeField]
    private float playerScore = 0;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    void Start()
    {
        _scoreMenu.SetActive(false);
        _currentMoney = StartMoney;
        _pokerManager.StartGame();
        _currentGame = _pokerManager;
        _pokerManager.OnValidate += ValidateCurrentGame;
        _currentBid = _Bid1Value;
        _textCurrentBid.text = "" + _currentBid;
        Anim = RouletteObj.GetComponent<Animator>();
        StartGame.Play();
    }

    void Update()
    {
        if (Input.GetButtonDown("Validate"))
        {
            Debug.Log("hit button validate");
            _currentGame.Validate();
        }
        if (Input.GetButtonDown("Bet1"))
        {
            Debug.Log("Change Bet1");
            _currentBid = _Bid1Value;
            _textCurrentBid.text = "" + _currentBid;
            Mise.Play();
        }
        if (Input.GetButtonDown("Bet2"))
        {
            Debug.Log("Change Bet2");
            _currentBid = _Bid2Value;
            _textCurrentBid.text = "" + _currentBid;
            Mise.Play();
        }
        if (Input.GetButtonDown("Bet3"))
        {
            Debug.Log("Change Bet3");
            _currentBid = _Bid3Value;
            _textCurrentBid.text = "" + _currentBid;
            Mise.Play();
        }
        if (Input.GetButtonDown("Bet4"))
        {
            Debug.Log("Change Bet4");
            _currentBid = _Bid4Value;
            _textCurrentBid.text = "" + _currentBid;
            Mise.Play();
        }
    }

    void ValidateCurrentGame(bool PlayerWon)
    {
        //Start coroutine or shits for FX Sounds
        //Then call ChangeGame
        if (PlayerWon)
        {
            Debug.Log("Player won");
            _Curve.MovementCurve((int)_currentBid);
            ScoreUpdate();
            WinningMachine.Play();
            FallingCoin.Play();
            StartCoroutine(playRoulette());
            MoneyRain1.Play();
            MoneyRain2.Play();

        }
        else
        {
            Debug.Log("Player lost");
            _Curve.MovementCurve((int)-_currentBid);
            LosingGame.Play();
        }
        ChangeGame();
    }

    IEnumerator playRoulette()
    {
        Debug.Log("play anim");
        RouletteObj.SetActive(true);
        Anim.SetTrigger("isRoulette");
        yield return new WaitForSeconds(2.2f);
        RouletteObj.SetActive(false);
    }

    void ChangeGame()
    {
        if (_pokerManager.gameObject.activeSelf)
        {
            _pokerManager.CloseGame();
            _pokerManager.OnValidate -= ValidateCurrentGame;
            _pokerManager.gameObject.SetActive(false);

            _Rps.gameObject.SetActive(true);
            _Rps.OnValidate += ValidateCurrentGame;
            _currentGame = _Rps;
        }
        else if (_Rps.gameObject.activeSelf)
        {
            _Rps.CloseGame();
            _Rps.OnValidate -= ValidateCurrentGame;
            _Rps.gameObject.SetActive(false);

            _pokerManager.gameObject.SetActive(true);
            _pokerManager.OnValidate += ValidateCurrentGame;
            _currentGame = _pokerManager;
        }
        _currentGame.StartGame();
    }

    public void ScoreUpdate()
    {
        playerScore += _currentBid;
    }

    public void Death()
    {
        _scoreMenu.SetActive(true);
        _scoreMenu.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "Ton score: " + playerScore;
        switch (playerScore)
        {
            
            case < 100:
                _scoreMenu.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Bravo! Tu as obtenu le titre de: Nul!";
                break;
            case >= 100 and < 200:
                _scoreMenu.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Bravo! Tu as obtenu le titre de: Caude4 l'Ane!";
                break;
            case >= 200 and < 300:
                _scoreMenu.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Bravo! Tu as obtenu le titre de: SeuquouiZy_Officiel!";
                break;
            case >= 300 and < 400:
                _scoreMenu.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Bravo! Tu as obtenu le titre de: Atsem Turlupin";
                break;
            case >= 400 and < 500:
                _scoreMenu.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Bravo! Tu as obtenu le titre de: Bernard m'adore";
                break;
            case > 500:
                _scoreMenu.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Bravo! Tu as obtenu le titre de: Le canide de Mur Rue";
                break;

        }

        if (Input.anyKeyDown)
        {
            SceneManager.LoadScene("MenuPrincipale");
        }

    }
}
