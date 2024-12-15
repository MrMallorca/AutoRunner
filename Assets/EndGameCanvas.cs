using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndGameCanvas : MonoBehaviour
{
    [SerializeField] Button SalirBtn;
    [SerializeField] Button ReiniciarBtn;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    private void OnEnable()
    {
        SalirBtn.onClick.AddListener(Salir);
        ReiniciarBtn.onClick.AddListener(Reiniciar);

    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void Salir()
    {
        Application.Quit();
        #if UNITY_EDITOR
            EditorApplication.isPlaying = false;

        #endif
    }

    public void Reiniciar()
    {
        Time.timeScale = 1.0f;
        gameObject.SetActive(false);
        SceneManager.LoadScene(0);
    }

    private void OnDisable()
    {
        SalirBtn.onClick.RemoveListener(Salir);
        ReiniciarBtn.onClick.AddListener(Reiniciar);

    }
}
