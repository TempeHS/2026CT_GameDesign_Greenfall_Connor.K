using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class CutSceneManager : MonoBehaviour
{
    public GameObject BlackPanel;
    public GameObject cutscene;
    private Image panelSpriteRenderer;
    private Image cutsceneSpriteRenderer;
    private float fadeDuration = 2.0f;
    public GameObject player;
    public GameObject industrialParallax;
    public GameObject greenParallax;
    [SerializeField] private PlayerMovement playerScript;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        panelSpriteRenderer = BlackPanel.GetComponent<Image>();
        cutsceneSpriteRenderer = cutscene.GetComponent<Image>();
        BlackPanel.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator FadeInCoroutine(int alpha)
    {
        BlackPanel.SetActive(true);
        PlayerMovement.canInput = false;
        Color startColor = panelSpriteRenderer.color;

        Color targetColor = new Color(startColor.r, startColor.g, startColor.b, alpha);

        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;

            panelSpriteRenderer.color = Color.Lerp(startColor, targetColor, elapsedTime / fadeDuration);


            yield return null;
        }

        industrialParallax.SetActive(false);
        greenParallax.SetActive(true);
        panelSpriteRenderer.color = targetColor;
        if(alpha == 0)
        {
            PlayerMovement.canInput = true;
            BlackPanel.SetActive(false);
        }


        
    }
    private IEnumerator FadeInCutCoroutine(int alpha)
    {
        cutscene.SetActive(true);
        PlayerMovement.canInput = false;
        Color startColor = cutsceneSpriteRenderer.color;

        Color targetColor = new Color(startColor.r, startColor.g, startColor.b, alpha);

        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;

            cutsceneSpriteRenderer.color = Color.Lerp(startColor, targetColor, elapsedTime / fadeDuration);


            yield return null;
        }


        cutsceneSpriteRenderer.color = targetColor;
        if (alpha == 0)
        {
            PlayerMovement.canInput = true;
            BlackPanel.SetActive(false);
        }



    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(BothCutscenes());
        }
    }
    private IEnumerator Cutscene1()
    {
        yield return StartCoroutine(FadeInCoroutine(1));

        industrialParallax.SetActive(false);
        greenParallax.SetActive(true);
        player.transform.position = new Vector3(565f, -4f, 0f);
        yield return new WaitForSeconds(2.4f);

        yield return StartCoroutine(FadeInCoroutine(0));



    }
    private IEnumerator Cutscene2()
    {
        yield return new WaitForSeconds(5.2f);
        yield return StartCoroutine(FadeInCoroutine(1));

        yield return new WaitForSeconds(2f);

        yield return StartCoroutine(FadeInCutCoroutine(1));



    }
    private IEnumerator BothCutscenes()
    {
        yield return StartCoroutine(Cutscene1());

        yield return StartCoroutine(Cutscene2());



    }
}
