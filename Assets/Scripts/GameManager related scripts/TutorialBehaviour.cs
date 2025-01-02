using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class TutorialBehaviour : MonoBehaviour
{
    
    public int tutorialStep;
    
    public static bool isInTutorial = true;
    public static float tutorialTimeScale = 1f;

    public GameObject textBox;
    public GameObject highLighter;
    public GameObject bottomPosition;
    
    public string[] tutorialText;
    void Start() //étape 0 du tutoriel
    {
        MoveTextBox(bottomPosition.transform.position);
        ModifyTextBox(tutorialText[tutorialStep]);
        ModifySpeakingCharacter(1);
        HideCharacter(2);
        ShowTextBox();
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

    public void DesactivateGameObject(GameObject gameObjectToDesactivate) //désactive n'importe quel gameobject, peut etre utilisé pour désactiver des boutons que le joueur pourrait activer avant d'y etre introduit
    {
        gameObjectToDesactivate.SetActive(false);
    }

    public void ActivateGameobject(GameObject gameObjectToActivate)
    {
        gameObjectToActivate.SetActive(true);
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

    public void Highlight(Vector2 highLightPosition, Vector2 highLightScale)
    {
        highLighter.SetActive(true);
        RectTransform rectTransform = highLighter.GetComponent<RectTransform>();
        if (rectTransform != null)
        {
            rectTransform.anchoredPosition = highLightPosition;
            rectTransform.localScale = highLightScale;
        }
    }


    public void StopHighlighting()
    {
        highLighter.SetActive(false);
    }
    
    public void ModifyTimeScale(float newTimeScale = 1f) // change la vitesse de l'écoulement du temps (bypass l'option de fast mode du gameManager). par défaut met la vitesse de jeu en normale
    {
        tutorialTimeScale = newTimeScale;
        Time.timeScale = newTimeScale;
    }

    public void StopTutorial()
    {
        isInTutorial = false;
        HideTextBox();
        tutorialTimeScale = 1f;
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
                ShowCharacter(2);
                ModifySpeakingCharacter(2);
                ModifyToCurentText();
                Highlight(new Vector2(0,0), new Vector2(1, 1));
                break;
            
            case 2:
                ModifySpeakingCharacter(1);
                ModifyToCurentText();
                break;
            
            case 3:
                ModifyToCurentText(); 
                break;
            
            case 4:
                ModifySpeakingCharacter(2);
                ModifyToCurentText();
                break;
            
            case 5:
                ModifySpeakingCharacter(1);
                ModifyToCurentText();
                break;
            
            case 6:
                StopTutorial();
                break;
        }
    }

}
