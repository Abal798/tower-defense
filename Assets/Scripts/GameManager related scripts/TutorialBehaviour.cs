using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class TutorialBehaviour : MonoBehaviour
{
    
    [Header("GD ne pas toucher")]
    public static int tutorialStep = 0;
    
    public static bool isInTutorial;
    public static bool cameraLocked = false;
    public static float tutorialTimeScale = 1f;

    public GameObject textBox;
    public GameObject highLighter;
    public Sprite[] tomSprites;
    public Sprite[] arthurSprites;
    
    [Header("espace GD")]
    public GameObject bottomPosition;
    public string[] tutorialText;
    public GameObject[] objectsToHideAndShow;
    

    

    
    void Start() //étape 0 du tutoriel
    {
        tutorialStep = 0;
        ResumeTutorial();
        MoveTextBox(bottomPosition.transform.position);
        ModifyTextBox(tutorialText[tutorialStep]);
        ModifySpeakingCharacter(1);
        HideCharacter(2);
        StopHighlighting();
        ShowTextBox();
        ChangeExpression(1,23);
        DesactivateAllGameObjects();
        LockCamera();
    }
    
    public void ShowTextBox() //affiche toute la textboxe , personnages compris
    {
        textBox.SetActive(true);
    }

    public void HideTextBox() //cache toute la textboxe , personnages compris
    {
        textBox.SetActive(false);
    }

    public void ModifyToCurentText() //change le text de la text boxe pour celui contenu dans le tutorialtext de l'etape actuel
    {
        if (tutorialStep < tutorialText.Length) ModifyTextBox(tutorialText[tutorialStep]);
    }
    
    public void ModifyTextBox(string text = "") //moifie le text de la textboxe pour nimporte quel string (ne sert a rien en théorie a part pour bypass la fonction ci dessus. par défaut renvoie un texte vide
    {
        textBox.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = text;
    }

    public void MoveTextBox(Vector3 newPosition) //déplace la textboxe en entier (personnages compris) au coordonnées qu'on lui donne (pour utiliser avec un gameObject comme repere, utiliser comme ceci : MoveTextBox(GameObject.transform.position));
    {
        textBox.transform.position = newPosition;
    }

    public void ModifyTextBoxScale(float newWidth = 650f, float newHeight = 155f) // modifie la taille de la zone de texte selon une largeure puis une hauteur. ne change pas l'emplacement des personnages. prend par défaut la taille d'origine
    {
        textBox.transform.GetChild(0).gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(newWidth, newHeight);
        textBox.transform.GetChild(1).gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(newWidth, newHeight);
    }

    public void DesactivateGameObject(int gameObjectToDesactivate) //désactive n'importe quel gameobject renseigné dans le tableau "objectsToHideAndShow" dans l'inpecteur. prend pour parametre le numero de l'element en question dans l'inpecteur a partir de 0
    {
        objectsToHideAndShow[gameObjectToDesactivate].SetActive(false);
    }

    public void ActivateGameobject(int gameObjectToActivate)// active n'importe quel gameobject renseigné dans le tableau "objectsToHideAndShow" dans l'inpecteur. prend pour parametre le numero de l'element en question dans l'inpecteur a partir de 0
    {
        objectsToHideAndShow[gameObjectToActivate].SetActive(true);
    }

    public void DesactivateAllGameObjects() // désactive tout les gameObjects renseignés dans le tableau "objectsToHideAndShow"
    {
        foreach (GameObject objectToDesactivate in objectsToHideAndShow)
        {
            objectToDesactivate.SetActive(false);
        }
    }
    
    public void ActivateAllGameObjects() // active tout les gameObjects renseignés dans le tableau "objectsToHideAndShow"
    {
        foreach (GameObject objectToActivate in objectsToHideAndShow)
        {
            objectToActivate.SetActive(true);
        }
    }

    public void ModifySpeakingCharacter(int newSpeakingCharacter) //change l'intensité du personnage selectionné pour 1 et met en semi-transparent le deuxieme. le personnage de gauche est désigné par 1 et celui de droite par 2
    {
        if (newSpeakingCharacter == 1)
        {
            var image2 = textBox.transform.GetChild(2).GetComponent<Image>();
            var color2 = image2.color;
            color2.a = 1f;
            image2.color = color2;

            var image3 = textBox.transform.GetChild(3).GetComponent<Image>();
            var color3 = image3.color;
            color3.a = 0.5f;
            image3.color = color3;
        }
        else
        {
            var image3 = textBox.transform.GetChild(3).GetComponent<Image>();
            var color3 = image3.color;
            color3.a = 1f;
            image3.color = color3;

            var image2 = textBox.transform.GetChild(2).GetComponent<Image>();
            var color2 = image2.color;
            color2.a = 0.5f;
            image2.color = color2;
        }
    }

    public void HideCharacter(int characterToHide) // cache le personnage sélectionné. le personnage de gauche est désigné par 1 et celui de droite par 2
    {
        if (characterToHide == 1) textBox.transform.GetChild(2).gameObject.SetActive(false);
        else if (characterToHide == 2) textBox.transform.GetChild(3).gameObject.SetActive(false);
    }
    
    public void ShowCharacter(int characterToShow) // affiche le personnage sélectionné. le personnage de gauche est désigné par 1 et celui de droite par 2 (ne concerne pas l'intensité de celui ci)
    {
        if (characterToShow == 1) textBox.transform.GetChild(2).gameObject.SetActive(true);
        else if (characterToShow == 2) textBox.transform.GetChild(3).gameObject.SetActive(true);
    }

    public void PlaceCharacterInCenter(int characterToPlace) // place le personnage désigné seul au centre de la textboxe. le personnage de gauche est désigné par 1 et celui de droite par 2
    {
        if (characterToPlace == 1)
        {
            HideCharacter(2);
            textBox.transform.GetChild(2).position = new Vector3(textBox.transform.position.x, textBox.transform.position.y + 130, textBox.transform.position.z);
        }
        else
        {
            HideCharacter(1);
            textBox.transform.GetChild(3).position = new Vector3(textBox.transform.position.x, textBox.transform.position.y + 130, textBox.transform.position.z);
        }
    }

    public void BackToDialogue() //utile uniquement si on a emplyé la fonction "PlaceCharacterInCenter(characterToPlace)". replace les deux personnages a leurs positions d'origine
    {
        if (textBox.transform.GetChild(2).gameObject.activeSelf)
        {
            ShowCharacter(2);
            textBox.transform.GetChild(2).position = new Vector3(textBox.transform.position.x - 255, textBox.transform.position.y + 130, textBox.transform.position.z);
        }
        
        if (textBox.transform.GetChild(3).gameObject.activeSelf)
        {
            ShowCharacter(1);
            textBox.transform.GetChild(3).position = new Vector3(textBox.transform.position.x + 255, textBox.transform.position.y + 130, textBox.transform.position.z);
        }
    }

    public void ChangeExpression(int characterToChange, int newExpression) // change l'expression de visage du personnage sélectionné selon un indice. les expressions sont dans l'ordre dans les dossiers dans lesquelles elles sont référencées. le personnage de gauche est désigné par 1 et celui de droite par 2
    {
        if (characterToChange == 1 && newExpression <= tomSprites.Length) textBox.transform.GetChild(2).gameObject.GetComponent<Image>().sprite = tomSprites[newExpression];
        if (characterToChange == 2 && newExpression <= arthurSprites.Length) textBox.transform.GetChild(3).gameObject.GetComponent<Image>().sprite = arthurSprites[newExpression];
    }

    public void Highlight(Vector2 highLightPosition, Vector2 highLightScale) // affiche un cadre roue clignotant la la position indiquée en parametre 1 (coordonées x/y) et de la taille indiquée en parametre 2 (largeur/hauteur)
    {
        highLighter.SetActive(true);
        RectTransform rectTransform = highLighter.GetComponent<RectTransform>();
        if (rectTransform != null)
        {
            Debug.Log("Highlight");
            rectTransform.anchoredPosition = highLightPosition;
            rectTransform.localScale = highLightScale;
        }
    }


    public void StopHighlighting() // retire le cadre clignotant de l'écran.
    {
        highLighter.SetActive(false);
    }
    
    public void ModifyTimeScale(float newTimeScale = 1f) // change la vitesse de l'écoulement du temps (bypass l'option de fast mode du gameManager). par défaut met la vitesse de jeu en normale
    {
        tutorialTimeScale = newTimeScale;
        Time.timeScale = newTimeScale;
    }

    public void LockCamera()
    {
        cameraLocked = true;
    }

    public void UnlockCamera()
    {
        cameraLocked = false;
    }

    public void StopTutorial() // arrete le tutoriel, retire la texteboxe, retire le carde clignotant, permet au joueur d'utiliser les raccourcis et le fast mode. réactive tout les gameObject du tableau "objectsToHideAndShow"
    {
        isInTutorial = false;
        HideTextBox();
        StopHighlighting();
        ActivateAllGameObjects();
        tutorialTimeScale = 1f;
        Time.timeScale = 1f;
    }

    public void ResumeTutorial()
    {
        isInTutorial = true;
        ShowTextBox();
    }

    public void NextStep()
    {
        tutorialStep++;
        switch (tutorialStep)
        {
            case 0:
                break;
            
            case 1:
                ChangeExpression(2, 2);
                ShowCharacter(2);
                ModifySpeakingCharacter(2);
                ModifyToCurentText();
                Debug.Log("Arthur parle");
                break;
            
            case 2:
                ChangeExpression(2,19);
                ChangeExpression(1,18);
                ModifyToCurentText();
                break;
            
            case 3:
                ModifySpeakingCharacter(1);
                ChangeExpression(1,3);
                ModifyToCurentText(); 
                break;
            
            case 4:
                ChangeExpression(1,17);
                ModifyToCurentText();
                ActivateGameobject(11); // Affiche le SoulsDisplay pour montrer les ressources
                ActivateGameobject(0); // Affiche les différentes tours et leurs prix
                ActivateGameobject(1);
                ActivateGameobject(2);
                ActivateGameobject(3);
                ActivateGameobject(4);
                ActivateGameobject(5);
               // Highlight(new Vector2(-85,450),new Vector2(3,1)); // Highlight des Âmes à faire au préalable (il manque une intro des différentes ressources)
                Highlight(new Vector2(-700, -370), new Vector2(6,3.25f)); // Highlight des différentes tours
                break;
            
            case 5:
                StopHighlighting();
                ChangeExpression(2,9);
                ModifyToCurentText();
                break; //Ajouter un case qui parle des âmes et comment on en obtient (parler bien plus tard du converter)
            case 6:
                ModifyToCurentText();
                Highlight(new Vector2(-105, -270), new Vector2(2.5f, 1.25f));
                break;
            case 7:
                ModifyToCurentText();
                Highlight(new Vector2(487, -55), new Vector2(1.25f, 1.25f));
                // Visuel du feu comme mentionné précédemment
                break;
            case 8:
                ModifyToCurentText();
                Highlight(new Vector2(-220, 380), new Vector2(2.5f, 1.25f));
                // Le joueur doit placer une tour avant de pouvoir poursuivre.
                break;
            
            case 9:
                ChangeExpression(2, 2);
                StopHighlighting();
                ModifySpeakingCharacter(2);
                ModifyToCurentText();
                // Le joueur doit placer le reste de ses défenses (c'est pas assez explicit dans le texte)
                break;
            case 10:
                ModifySpeakingCharacter(1);
                ChangeExpression(1, 3);
                ModifyToCurentText();
                break;
            case 11: 
                ModifyToCurentText();
                ActivateGameobject(6);
                ActivateGameobject(11);
                Highlight(new Vector2(805, 452.5f), new Vector2(3.2f, 0.35f));
                // Il doit lancer la prochaine vague le prochain dialogue apparaissant après la vague
                UnlockCamera();
                break;
            case 12:
                ModifySpeakingCharacter(2);
                ChangeExpression(2, 23);
                ChangeExpression(1, 18);
                ModifyToCurentText();
                break;
            case 13:
                ChangeExpression(1, 8);
                ModifyToCurentText();
                break;
            case 14:
                ChangeExpression(2, 2);
                ModifyToCurentText();
                break;
            case 15:
                ModifySpeakingCharacter(1);
                ChangeExpression(1, 17);
                ModifyToCurentText();
                ActivateGameobject(7);
                    Highlight(new Vector2(-858, 310), new Vector2(1.90f, 0.8f));
                // Le joueur doit cliquer sur le bouton d'Alchemy pour continuer
                    break;
            case 16:
                ChangeExpression(1, 8);
                ModifyToCurentText();
                StopHighlighting();
                break;
            case 17:
                ChangeExpression(1, 20);
                ModifyToCurentText();
                UnlockCamera();
                Highlight(new Vector2(750, -245), new Vector2(5f, 0.85f));
                // Le joueur place ensuite son sort nouvellement créer qu'il doit placer pour pouvoir continuer
                break;
            case 18:
                ChangeExpression(1, 1);
                ModifyToCurentText();
                ActivateGameobject(13); // Activation Triangle Barry
                Highlight(new Vector2(325, 475), new Vector2(1.75f, 1.5f));
                    break;
            case 19:
                ChangeExpression(1, 14);
                ChangeExpression(2, 18);
                ModifyToCurentText();
                Highlight(new Vector2(805, 452.5f), new Vector2(3.2f, 0.35f));
                // Le joueur doit lancer la prochaine vague pour continuer.
                break;
            case 20:
                // Une fois la vague terminée
                ModifySpeakingCharacter(2);
                ChangeExpression(2, 9);
                ModifyToCurentText();
                ActivateAllGameObjects();
                UnlockCamera();
                break;
            case 21:
                StopTutorial();
                // Restera le Converter à introduire juste avant
                break;





        }
    }

}
